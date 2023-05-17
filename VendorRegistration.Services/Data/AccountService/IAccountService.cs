using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.AccountService
{
    public interface IAccountService
    {
        List<VendorAccount> GetVendorAccounts();
        VendorAccount GetAccountById(int accountId);
        void DbUpdateAccount(VendorAccount account);
        void DbAddAccount(VendorAccount account);
        List<VendorAccount> GetAccountsByUsername(string username);
        VendorAccount GetAccountByCompanyId(int companyId);
    }
}
