using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.EmailAttachmentsService
{

    public interface IEmailAttachmentsService
    {
        List<Email_Attachments> GetAllEmailAttachments();
        void DbAddEmailAttachment(Email_Attachments attachment);
    }
}
