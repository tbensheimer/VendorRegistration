using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.EmailAttachmentsService
{
    public class EmailAttachmentsService : IEmailAttachmentsService
    {
        private VendorDataDbContext _db;

        public EmailAttachmentsService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Email_Attachments> GetAllEmailAttachments()
        {
            List<Email_Attachments> attachmentList = new();

            attachmentList = _db.Email_Attachments.ToList();

            return attachmentList;
        }

        public void DbAddEmailAttachment(Email_Attachments attachment)
        {
            _db.Email_Attachments.Add(attachment);
            _db.SaveChanges();
        }
    }
}
