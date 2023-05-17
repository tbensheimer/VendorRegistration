using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.FunctionsService.EncryptionService
{
    public class EncryptionService : IEncryptionService
    {
        private static byte[] key = new byte[32] { 249, 82, 76, 171, 164, 66, 2, 10, 233, 62, 203, 111, 102, 155, 159, 53, 66, 117, 110, 242, 4, 225, 14, 165, 121, 15, 76, 171, 215, 242, 129, 68 };
        private static byte[] IV = new byte[16] { 179, 235, 26, 200, 185, 119, 112, 237, 38, 5, 45, 55, 192, 233, 116, 22 };

        public async Task<string> Encrypt(string InputString)
        {
            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = IV;

            MemoryStream encryptedString = new MemoryStream();
            CryptoStream crypto = new CryptoStream(encryptedString, aes.CreateEncryptor(), CryptoStreamMode.Write);

            await crypto.WriteAsync(Encoding.Unicode.GetBytes(InputString));
            crypto.FlushFinalBlock();

            return Convert.ToBase64String(encryptedString.ToArray());
        }

        public string Decrypt(string encryptedData)
        {
            if (!string.IsNullOrWhiteSpace(encryptedData))
            {
                bool base64Valid = IsBase64(encryptedData);

                if (!base64Valid)
                {
                    return encryptedData;
                }

                var encryptedBytes = Convert.FromBase64String(encryptedData);
                Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = IV;

                MemoryStream encryptedString = new MemoryStream(encryptedBytes);
                CryptoStream crypto = new CryptoStream(encryptedString, aes.CreateDecryptor(), CryptoStreamMode.Read);

                MemoryStream decryptedString = new MemoryStream();
                decryptedString.Position = 0;
                crypto.CopyTo(decryptedString);

                return Encoding.Unicode.GetString(decryptedString.ToArray());
            }
            else
            {
                return encryptedData;
            }
        }

        private bool IsBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
            return false;
        }
    }
}
