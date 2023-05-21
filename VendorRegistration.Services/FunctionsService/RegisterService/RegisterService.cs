using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.FunctionsService.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private VendorDataDbContext _db;
        private NavigationManager navigationManager;
        private IConfiguration _cfg;

        public RegisterService(VendorDataDbContext db, NavigationManager nm, IConfiguration cfg)
        {
            _db = db;
            navigationManager = nm;
            _cfg = cfg;
        }

        public void CreateVendorAccount(Company Company, VendorAccount Account, Address Address, List<Company_Types_Categories> CompTypesCats, bool admin, Vendor_Links vendorLinks, List<Authorized_Contacts> ContactsList)
        {

            Company.Sign_Up = DateTime.Now;

            _db.Companies.Add(Company);
            _db.SaveChanges();

            Account.CompanyId = Company.Id;
            Address.CompanyId = Company.Id;

            foreach (var ctc in CompTypesCats)
            {
                ctc.CompanyId = Company.Id;
            }

            foreach (var contact in ContactsList)
            {
                contact.CompanyId = Company.Id;
            }

            _db.Accounts.Add(Account);
            _db.Addresses.Add(Address);
            _db.Company_Types_Categories.AddRange(CompTypesCats);
            _db.Authorized_Contacts.AddRange(ContactsList);

            var baseURI = navigationManager.BaseUri;
            Guid guid = Guid.NewGuid();
            string randomString = guid.ToString().Replace("-", "");

            if (!admin)
            {
                MailMessage message = new MailMessage();

                message.Subject = "Verify your account";
                message.From = new MailAddress("HHC-MCPHD-Purchasing-Do-Not-Reply@hhcorp.org");
                message.IsBodyHtml = true;
                message.To.Add(new MailAddress($"{Account.Email}"));
                message.Body = $"<h2>Account Verification:</h2><p>Hello {Company.Name},</p> <br/><p>Please click on this temporary link below to verify your account and finish the registration process: {baseURI}account-verification/{randomString}/{Company.Id}</p><br/><p>Thank you,<br><p>Purchasing Administrator<br></p><p>HHC/MCPHD <br></p><p>quotes@hhcorp.org</p>";

                var sClient = new SmtpClient("fakeClient.org");
                sClient.Port = 25;
                sClient.UseDefaultCredentials = false;

                //sClient.Send(message);
                Company.Completed_Registeration = true;  //Used HHC's SMTP Client for Email Sending

                if (vendorLinks is not null)
                {
                    vendorLinks.CompanyId = Company.Id;
                    vendorLinks.Link = $"{baseURI}account-verification/{randomString}/{Company.Id}";
                    vendorLinks.DateLinkSent = DateTime.Now;

                    _db.Vendor_Links.Add(vendorLinks);
                }
            }
            else
            {
                MailMessage message = new MailMessage();

                message.Subject = "Accept Terms and Conditions for HHC/MCPHD Purchasing";
                message.From = new MailAddress("HHC-MCPHD-Purchasing-Do-Not-Reply@hhcorp.org");
                message.IsBodyHtml = true;
                message.To.Add(new MailAddress($"{Account.Email}"));
                message.Body = $"<h2>Accepting Terms and Conditions:</h2><p>Hello {Company.Name},</p> <br/><p>Please click on the link below to finish the registration process by accepting the terms and conditions: {baseURI}terms-and-conditions/{randomString}/{Company.Id}</p><br/><p>Thank you,<br><p>Purchasing Administrator<br></p><p>HHC/MCPHD <br></p><p>quotes@hhcorp.org</p>";

                var sClient = new SmtpClient("fakeClient.org");
                sClient.Port = 25;
                sClient.UseDefaultCredentials = false;

                Company.Completed_Registeration = true;
                //sClient.Send(message);
            }

            _db.SaveChanges();
        }
    }
}
