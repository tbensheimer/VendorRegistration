using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CompanyTypesAndCatsService
{
    public class CompanyTypesAndCatsService : ICompanyTypesAndCatsService
    {
        private VendorDataDbContext _db;

        public CompanyTypesAndCatsService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Company_Types_Categories> GetAllCompanyTypesAndCats()
        {
            List<Company_Types_Categories> list = new();

            list = _db.Company_Types_Categories.ToList();

            return list;
        }

        public List<Company_Types_Categories> GetCompanyTypesAndCatsFromCompanyId(int companyId)
        {
            List<Company_Types_Categories> listFromCompany = new();

            listFromCompany = _db.Company_Types_Categories.Where(ctc => ctc.CompanyId == companyId).ToList();

            return listFromCompany;
        }

        public List<Company_Types_Categories> GetCompanyTypesAndCatsFromCompanyIdAndFromGivenList(int companyId, List<Company_Types_Categories> CtcList)
        {
            List<Company_Types_Categories> listFromCompany = new();

            listFromCompany = CtcList.Where(ctc => ctc.CompanyId == companyId).ToList();

            return listFromCompany;
        }

        public Company_Types_Categories GetCompanyCategoryFromId(int ctcId)
        {
            var companyTypeAndCat = _db.Company_Types_Categories.FirstOrDefault(ctc => ctc.Id == ctcId);

            return companyTypeAndCat;
        }

        public void DbAddRangeCompanyTypeAndCat(List<Company_Types_Categories> list)
        {
            _db.Company_Types_Categories.AddRange(list);
            _db.SaveChanges();
        }

        public void DbAddRangeCompanyTypeAndCatEdit(List<Company_Types_Categories> list, Company Company)
        {
            var categoriesToRemove = _db.Company_Types_Categories.Where(ctc => ctc.CompanyId == Company.Id);

            foreach (var ctc in categoriesToRemove)
            {
                _db.Company_Types_Categories.Remove(ctc);
            }

            _db.Company_Types_Categories.AddRange(list);
            _db.Companies.Update(Company);
            _db.SaveChanges();
        }
        public void DbRemoveCompanyTypeAndCat(Company_Types_Categories ctc)
        {
            _db.Company_Types_Categories.Remove(ctc);
            _db.SaveChanges();
        }
    }
}
