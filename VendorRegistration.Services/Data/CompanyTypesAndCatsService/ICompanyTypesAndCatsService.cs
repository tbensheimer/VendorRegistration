using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CompanyTypesAndCatsService
{
    public interface ICompanyTypesAndCatsService
    {
        List<Company_Types_Categories> GetAllCompanyTypesAndCats();
        List<Company_Types_Categories> GetCompanyTypesAndCatsFromCompanyId(int companyId);
        List<Company_Types_Categories> GetCompanyTypesAndCatsFromCompanyIdAndFromGivenList(int companyId, List<Company_Types_Categories> CtcList);
        void DbAddRangeCompanyTypeAndCat(List<Company_Types_Categories> list);
        void DbAddRangeCompanyTypeAndCatEdit(List<Company_Types_Categories> list, Company Company);
        void DbRemoveCompanyTypeAndCat(Company_Types_Categories ctc);
    }
}
