using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CompanyService
{
    public interface ICompanyService
    {
        List<Company> GetAllCompanies();
        List<Company> GetVerifiedCompanies();
        List<Company> GetMinorityCompanies();
        List<Company> GetRecentlyEditedActiveCompanies();
        Company[] GetVerifiedCompaniesArray();
        double GetVerifiedCompaniesCountNotDeleted();
        Company GetCompanyById(int companyId);
        Company GetCompanyByIdString(string companyId);
        void DbUpdateCompany(Company company);
        Task DbUpdateCompanyAsync(Company company);
        void DbAddCompany(Company company);
        void UnverifiedCompaniesRemove();
        double GetTotalActiveMinorityVendorsCount();
        double GetCertifiedMinorityVendorsCount();
        double GetUncertifiedMinorityVendorsCount();
        double GetDeletedCompaniesCount();
    }
}
