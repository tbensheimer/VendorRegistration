using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class VendorDataDbContext : DbContext
    {
        public VendorDataDbContext(
           DbContextOptions<VendorDataDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<VendorAccount> Accounts { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Authorized_Contacts> Authorized_Contacts { get; set; } = default!;
        public DbSet<Business_Categories> Business_Categories { get; set; } = default!;
        public DbSet<Business_Types> Business_Types { get; set; } = default!;
        public DbSet<Company_Types_Categories> Company_Types_Categories { get; set; } = default!;
        public DbSet<Notifications> NotificationsList { get; set; } = default!;
        public DbSet<Notification_Recipient> Notification_Recipients { get; set; } = default!;
        public DbSet<Email_Attachments> Email_Attachments { get; set; } = default!;
        public DbSet<Vendor_Links> Vendor_Links { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
