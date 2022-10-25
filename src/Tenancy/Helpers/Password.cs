using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Outrage.Tenancy.Helpers
{
    public static class Password
    {
        static string Allowed = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Generate(int length)
        {
            var builder = new StringBuilder();
            byte[] indexes = RandomNumberGenerator.GetBytes(length);
            foreach (byte index in indexes)
            {
                int ix = (int)(((double)index / byte.MaxValue) * Allowed.Length);
                builder.Append(Allowed[ix]);
            }

            return builder.ToString();
        }
    }
}
