using System.Security.Cryptography;

namespace RealRMS.Security {
    public class Encryption {
        public string GenerateSalt(int size) {
            var crypto = new RNGCryptoServiceProvider();
            return crypto.GenerateSalt(size);      
        }

        public string GenerateHash(string salt, string input){
            string inputString = string.Format("{0}{1}", input, salt);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(inputString);
            SHA256Managed hasher = new SHA256Managed();
            return hasher.GenerateHash(bytes);
        }
    }
}