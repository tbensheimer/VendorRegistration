using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.FunctionsService.TypeCatCompanyChangesService
{
    public interface ITypeCatCompanyChangesService
    {
        void TypeChange(ChangeEventArgs args, int TypeId, List<Business_Types> Types, Business_Types Type, List<Business_Types> CheckedTypes);
        void CategoryLoad(List<Business_Categories> FilteredCategories, List<Business_Types> CheckedTypes, List<Business_Categories> Categories);
        void CategoryChange(ChangeEventArgs args, int CatId, List<Business_Categories> Categories, Business_Categories Category, List<Business_Categories> CheckedCategories);
        void NotificationTypeChange(ChangeEventArgs args, int TypeId, List<Business_Types> Types, Business_Types Type, List<Business_Types> CheckedTypes, bool TypeAll);
        void NotificationCategoryChange(ChangeEventArgs args, int CatId, List<Business_Categories> Categories, Business_Categories Category, List<Business_Categories> CheckedCategories, List<Company_Types_Categories> CompanyTypesCats,
           List<Company> Companies, List<Company_Types_Categories> RelatedCompanies, bool CategoryAll);
        void NotificationCompanyChange(ChangeEventArgs args, int CompanyId, List<Company> Companies, Company Company, List<Company> SelectedCompanies, bool CompanyAll);
        void TypeAllHandle(bool TypeAll, Business_Types[] QueryableTypes, List<Business_Types> CheckedTypes);
        void CategoryAllHandle(bool CategoryAll, List<Business_Categories> QueryableCategories, List<Business_Categories> CheckedCategories, List<Company_Types_Categories> CompanyTypesCats, List<Company> Companies,
            List<Company_Types_Categories> RelatedCompanies);
        void CompanyAllHandle(bool CompanyAll, List<Company_Types_Categories> QueryableCompanies, List<Company> SelectedCompanies, List<Company> Companies);
    }
}
