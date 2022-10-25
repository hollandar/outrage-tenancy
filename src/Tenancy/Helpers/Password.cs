using System;
using System.Collections.Generic;
using System.Linq;
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
            var random = new Random();

            while (builder.Length < length)
            {
                builder.Append(Allowed[random.Next(Allowed.Length)]);
            }

            return builder.ToString();
        }
    }
}
