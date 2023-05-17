using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.FunctionsService.EncryptionService;

namespace VendorRegistration.Services.Data.AuthorizedContactsService
{
    public class AuthorizedContactsService : IAuthorizedContactsService
    {
        private VendorDataDbContext _db;
        private EncryptionService encryptionService;

        public AuthorizedContactsService(VendorDataDbContext db, EncryptionService es)
        {
            _db = db;
            encryptionService = es;
        }

        public List<Authorized_Contacts> GetAuthorizedContactsList()
        {
            List<Authorized_Contacts> contactsList = new();

            contactsList = _db.Authorized_Contacts.ToList();

            return contactsList;
        }

        public List<Authorized_Contacts> GetContactsFromCompanyId(int companyId)
        {
            List<Authorized_Contacts> contactsList = new();

            contactsList = _db.Authorized_Contacts.Where(contact => contact.CompanyId == companyId && !contact.Contact_Deleted).ToList();

            return contactsList;
        }

        public Authorized_Contacts GetContactFromId(int contactId)
        {
            Authorized_Contacts contact = _db.Authorized_Contacts.FirstOrDefault(c => c.Id == contactId);

            return contact;
        }

        public void DbAddContact(Authorized_Contacts contact, Company company)
        {
            if (company is not null && company.Fed_Id_SSN is not null)
            {
                company.EditedSince = DateTime.Now;
                _db.Companies.Update(company);
            }
            _db.Authorized_Contacts.Add(contact);
            _db.SaveChanges();
        }

        public void DbAddRangeContacts(List<Authorized_Contacts> contacts, int id)
        {
            var list = _db.Authorized_Contacts.Where(c => c.CompanyId == id).ToList();

            if (list is not null)
            {
                foreach (var contact in contacts)
                {
                    if (list.Any(c => c.Id == contact.Id))
                    {
                        _db.Authorized_Contacts.Update(contact);
                    }
                    else if (!list.Any(c => c.Id == contact.Id))   // Update if contact exists in database else add into database
                    {
                        _db.Authorized_Contacts.Add(contact);
                    }
                }

                foreach (var contact in list)
                {
                    if (!contacts.Any(c => c.Id == contact.Id))
                    {
                        _db.Authorized_Contacts.Remove(contact);   // Delete if contact in database isn't present in contacts list
                    }
                }
            }

            _db.SaveChanges();
        }

        public async Task DbAddRangeContactsAsync(List<Authorized_Contacts> contacts)
        {
            _db.Authorized_Contacts.AddRange(contacts);
            await _db.SaveChangesAsync();
        }

        public void DbUpdateContact(Authorized_Contacts contact)
        {
            _db.Authorized_Contacts.Update(contact);
            _db.SaveChanges();
        }

        public void DbUpdateEveryContact(List<Authorized_Contacts> ContactList, Company Company)
        {
            foreach (var contact in ContactList)
            {
                var dbContact = _db.Authorized_Contacts.FirstOrDefault(c => c.Id == contact.Id);
                if (dbContact != null)
                {
                    dbContact.CompanyId = contact.CompanyId;
                    dbContact.FirstName = contact.FirstName;
                    dbContact.LastName = contact.LastName;
                    dbContact.Email = contact.Email;
                    dbContact.Phone_Number = contact.Phone_Number;
                    _db.Authorized_Contacts.Update(dbContact);
                    if (Company is not null && Company.Fed_Id_SSN is not null)
                    {
                        Company.EditedSince = DateTime.Now;
                        _db.Companies.Update(Company);
                    }
                    _db.SaveChanges();
                }
            }
        }

        public void DbUpdateContactAndCompany(Authorized_Contacts contact, Company company)
        {
            if (company is not null && company.Fed_Id_SSN is not null)
            {
                company.EditedSince = DateTime.Now;
                _db.Companies.Update(company);
            }
            _db.Authorized_Contacts.Update(contact);
            _db.SaveChanges();
        }

        public void DbRemoveContact(Authorized_Contacts contact)
        {
            _db.Authorized_Contacts.Remove(contact);
            _db.SaveChanges();
        }
    }
}
