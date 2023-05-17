using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CompanyService
{
    public class CompanyService : ICompanyService
    {

        private VendorDataDbContext _db;

        public CompanyService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> companyList = new();

            companyList = _db.Companies.OrderBy(comp => comp.Name).ToList();

            return companyList;
        }

        public List<Company> GetVerifiedCompanies()
        {
            List<Company> verifiedCompanies = new();

            verifiedCompanies = _db.Companies.Where(comp => comp.Completed_Registeration).OrderBy(comp => comp.Name).ToList();

            return verifiedCompanies;
        }

        public List<Company> GetMinorityCompanies()
        {
            List<Company> minorityCompanies = new();

            minorityCompanies = _db.Companies.Where(comp => comp.Completed_Registeration && comp.Minority).OrderBy(comp => comp.Name).ToList();

            return minorityCompanies;
        }

        public List<Company> GetRecentlyEditedActiveCompanies()
        {
            List<Company> companies = new();

            companies = _db.Companies.Where(comp => DateTime.Now < comp.EditedSince.AddDays(30) && !comp.Is_Deleted && comp.Completed_Registeration).ToList();

            return companies;
        }

        public Company[] GetVerifiedCompaniesArray()
        {

            Company[] verifiedCompanies = _db.Companies.Where(comp => comp.Completed_Registeration).OrderBy(comp => comp.Name).ToArray();

            return verifiedCompanies;
        }

        public double GetVerifiedCompaniesCountNotDeleted()
        {
            double count = _db.Companies.Where(comp => comp.Completed_Registeration && !comp.Is_Deleted).Count();

            return count;
        }

        public Company GetCompanyById(int companyId)
        {
            Company company = _db.Companies.FirstOrDefault(comp => comp.Id == companyId);

            return company;

        }

        public Company GetCompanyByIdString(string companyId)
        {
            Company company = _db.Companies.FirstOrDefault(comp => comp.Id.ToString() == companyId);

            return company;
        }

        public void DbAddCompany(Company company)
        {
            _db.Companies.Add(company);
            _db.SaveChanges();
        }

        public void DbUpdateCompany(Company company)
        {
            var companyToUpdate = _db.Companies.FirstOrDefault(comp => comp.Id == company.Id);

            if (companyToUpdate is not null)
            {
                companyToUpdate.Completed_Registeration = company.Completed_Registeration;
                companyToUpdate.Description_Goods_Services = company.Description_Goods_Services;
                companyToUpdate.Disabled_From_Notifications = company.Disabled_From_Notifications;
                companyToUpdate.Diversity_Certification = company.Diversity_Certification;
                companyToUpdate.EditedSince = company.EditedSince;
                companyToUpdate.EmployeeIdNumber = company.EmployeeIdNumber;
                companyToUpdate.Fed_Id_SSN = company.Fed_Id_SSN;
                companyToUpdate.Is_Deleted = company.Is_Deleted;
                companyToUpdate.Minority = company.Minority;
                companyToUpdate.Name = company.Name;
                companyToUpdate.Other_Diversity_Certification = company.Other_Diversity_Certification;
                companyToUpdate.Sign_Up = company.Sign_Up;
                companyToUpdate.Supplier_Category = company.Supplier_Category;
                companyToUpdate.Website = company.Website;


                _db.Companies.Update(companyToUpdate);
                _db.SaveChanges();
            }
        }

        public async Task DbUpdateCompanyAsync(Company company)
        {
            _db.Companies.Update(company);
            await _db.SaveChangesAsync();
        }

        public void UnverifiedCompaniesRemove()
        {
            var companies = _db.Companies.Where(comp => !comp.Completed_Registeration).ToList();

            foreach (var comp in companies)
            {
                if (DateTime.Now > comp.Sign_Up.AddDays(30))
                {
                    _db.Companies.Remove(comp);
                }
            }
            _db.SaveChanges();
        }

        public double GetTotalActiveMinorityVendorsCount()
        {
            double count = _db.Companies.Where(comp => comp.Completed_Registeration && !comp.Is_Deleted && comp.Minority).Count();

            return count;
        }

        public double GetCertifiedMinorityVendorsCount()
        {
            double count = _db.Companies.Where(comp => comp.Completed_Registeration && !comp.Is_Deleted && comp.Minority && comp.Diversity_Certification != "Not Certified").Count();

            return count;
        }

        public double GetUncertifiedMinorityVendorsCount()
        {
            double count = _db.Companies.Where(comp => comp.Completed_Registeration && !comp.Is_Deleted && comp.Minority && comp.Diversity_Certification == "Not Certified").Count();

            return count;
        }

        public double GetDeletedCompaniesCount()
        {
            double count = _db.Companies.Where(comp => comp.Completed_Registeration && comp.Is_Deleted).Count();

            return count;
        }
    }
}
