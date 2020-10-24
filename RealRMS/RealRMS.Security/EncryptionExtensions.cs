using System;
using System.Security.Cryptography;

namespace RealRMS.Security {
    public static class EncryptionExtensions {
        public static string GenerateSalt(this RNGCryptoServiceProvider provider, int size) {
            var buffer = new byte[size];
            provider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);            
        }

        public static string GenerateHash(this SHA256Managed hasher, byte[] bytes) {
            byte[] hash = hasher.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
            //return System.Text.Encoding.ASCII.GetString(hash);                 
        }
    }
}
