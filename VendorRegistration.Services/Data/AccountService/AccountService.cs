using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.AccountService
{
    public class AccountService : IAccountService
    {
        private VendorDataDbContext _db;

        public AccountService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<VendorAccount> GetVendorAccounts()
        {
            List<VendorAccount> accountList = new();

            accountList = _db.Accounts.ToList();

            return accountList;
        }

        public VendorAccount GetAccountById(int accountId)
        {
            VendorAccount account = _db.Accounts.FirstOrDefault(account => account.Id == accountId);

            return account;
        }

        public void DbAddAccount(VendorAccount account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
        }

        public void DbUpdateAccount(VendorAccount account)
        {
            _db.Accounts.Update(account);
            _db.SaveChanges();
        }

        public List<VendorAccount> GetAccountsByUsername(string username)
        {
            List<VendorAccount> accountsList = new();

            accountsList = _db.Accounts.Where(account => account.Email.ToLower() == username.ToLower()).ToList();

            return accountsList;
        }
        public VendorAccount GetAccountByCompanyId(int companyId)
        {
            var account = _db.Accounts.FirstOrDefault(account => account.CompanyId == companyId);
            return account;
        }

    }
}
