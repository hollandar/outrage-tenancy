using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tenancy.Data;
using Tenancy.Models;

namespace Tenancy
{
    public sealed class TenancyStore
    {
        private readonly TenancyDbContext dbContext;
        private readonly Models.TenantModel tenantModel;
        private readonly byte[] key;

        public TenancyStore(TenancyDbContext dbContext, byte[] key, Models.TenantModel tenantModel)
        {
            this.dbContext = dbContext;
            this.key = key;
            this.tenantModel = tenantModel;
        }

        public async Task EstablishTenancy(TenantModel tenantModel)
        {
        }

        public void SetValue<TValue>(string key, TValue value, ValueEncodingEnum encoding = ValueEncodingEnum.UTF8)
        {
            var stringValue = (string?)Convert.ChangeType(value, typeof(string));
            if (stringValue == null)
                throw new ArgumentException("Could not convert value to string.", nameof(value));

            var valueObject = this.dbContext.Values.Where(r => r.TenantId == tenantModel.Id && r.Key == key).FirstOrDefault();
            if (valueObject == null)
            {
                valueObject = new TenantValue { TenantId = tenantModel.Id, Key = key };
                this.dbContext.Values.Add(valueObject);
            }

            switch (encoding)
            {
                case ValueEncodingEnum.UTF8:
                    valueObject.Value = Encoding.UTF8.GetBytes(stringValue);
                    valueObject.Encoding = ValueEncodingEnum.UTF8;
                    break;

                case ValueEncodingEnum.UTF8Crypt:
                    valueObject.Value = Encrypt(stringValue, this.key, tenantModel.Iv);
                    valueObject.Encoding = ValueEncodingEnum.UTF8Crypt;
                    break;
            }
        }

        public TValue? GetValue<TValue>(string key)
        {
            var valueObject = this.dbContext.Values.Where(r => r.TenantId == tenantModel.Id && r.Key == key).FirstOrDefault();
            if (valueObject == null)
            {
                throw new KeyNotFoundException(key);
            }

            string value = String.Empty;
            switch (valueObject.Encoding)
            {
                case ValueEncodingEnum.UTF8:
                    value = Encoding.UTF8.GetString(valueObject.Value);
                    break;

                case ValueEncodingEnum.UTF8Crypt:
                    value = Decrypt(valueObject.Value, this.key, tenantModel.Iv);
                    break;

                default:
                    throw new Exception($"Unknown encoding {valueObject.Encoding}.");
            }

            TValue? result = (TValue?)Convert.ChangeType(value, typeof(TValue));

            return result;
        }

        public async Task SaveChangesAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (Aes aes = Aes.Create())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (Aes aes = Aes.Create())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
