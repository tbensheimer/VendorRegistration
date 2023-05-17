using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.VendorLinksService
{
    public class VendorLinksService : IVendorLinksService
    {
        private VendorDataDbContext _db;

        public VendorLinksService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Vendor_Links> GetVendorLinks()
        {
            List<Vendor_Links> vendorLinks = new();

            vendorLinks = _db.Vendor_Links.ToList();

            return vendorLinks;
        }

        public void DbAddLink(Vendor_Links link)
        {
            _db.Vendor_Links.Add(link);
            _db.SaveChanges();
        }

        public void DbRemoveLink(Vendor_Links link)
        {
            _db.Vendor_Links.Remove(link);
            _db.SaveChanges();
        }

        public void DbRemoveLinkAndUpdateCompany(Vendor_Links link, Company company)
        {
            _db.Vendor_Links.Remove(link);
            _db.Companies.Update(company);
            _db.SaveChanges();
        }

        public void RemoveExpiredLinks()
        {
            var links = _db.Vendor_Links.ToArray();

            foreach (var link in links)
            {
                if (DateTime.Now > link.DateLinkSent.AddDays(5))
                {
                    _db.Vendor_Links.Remove(link);
                }
            }
            _db.SaveChanges();
        }
    }
}
