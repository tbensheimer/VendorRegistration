using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.VendorLinksService
{
    public interface IVendorLinksService
    {
        List<Vendor_Links> GetVendorLinks();
        void DbAddLink(Vendor_Links link);
        void DbRemoveLink(Vendor_Links link);
        void RemoveExpiredLinks();
        void DbRemoveLinkAndUpdateCompany(Vendor_Links link, Company company);
    }
}
