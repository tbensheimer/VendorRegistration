using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.AuthorizedContactsService
{
    public interface IAuthorizedContactsService
    {
        List<Authorized_Contacts> GetAuthorizedContactsList();
        List<Authorized_Contacts> GetContactsFromCompanyId(int companyId);
        Authorized_Contacts GetContactFromId(int contactId);
        void DbAddContact(Authorized_Contacts contact, Company company);
        void DbAddRangeContacts(List<Authorized_Contacts> contacts, int id);
        Task DbAddRangeContactsAsync(List<Authorized_Contacts> contacts);
        void DbUpdateContact(Authorized_Contacts contact);
        void DbUpdateEveryContact(List<Authorized_Contacts> ContactList, Company Company);
        void DbUpdateContactAndCompany(Authorized_Contacts contact, Company company);
        void DbRemoveContact(Authorized_Contacts contact);
    }
}
