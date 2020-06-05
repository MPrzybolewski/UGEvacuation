using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace UGEvacuationBLL.Helpers
{
    public static class Encryption
    {
        public static string CalculateSHA1Hash(string input)
        {
            var passwordHashAsBytes = SHA1.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", passwordHashAsBytes.Select(b => b.ToString("x2")).ToArray());
        }
    }
}