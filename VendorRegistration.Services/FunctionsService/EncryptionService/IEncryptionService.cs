using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.FunctionsService.EncryptionService
{
    public interface IEncryptionService
    {
        Task<string> Encrypt(string InputString);
        string Decrypt(string encryptedData);
    }
}
