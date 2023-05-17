using Blazored.TextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Models.UploadedFile;

namespace VendorRegistration.Services.FunctionsService.EmailSenderService
{
    public interface IEmailSenderService
    {
        void SendPasswordResetEmail(Company[] Companies, VendorAccount account, string email, string baseURI, Vendor_Links vendorLinks);
        Task SendNotification(string SubjectLine, string SendNotifSuccess, string SendNotifError, MailMessage message, string QuillContent, BlazoredTextEditor Quill, List<UploadedFile> FileList,
           List<Company> SelectedCompanies, string EmailToPerson, string TemplateEmail, Notifications Notif, Email_Attachments Db_EmailAttach, Notification_Recipient RecipientObject);
    }
}
