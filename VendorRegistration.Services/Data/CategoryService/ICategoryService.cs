using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CategoryService
{
    public interface ICategoryService
    {
        List<Business_Categories> GetCategoriesList();
        double GetCategoriesCountNotDeleted();
        List<Business_Categories> GetCategoriesNotDeleted();
        List<Business_Categories> GetCategoriesFromTypeId(int typeId);
        List<Business_Categories> GetCategoriesForRegistration();
        Business_Categories GetCategoryFromId(int catId);
        Business_Categories GetCategoryFromIdAndGivenList(int catId, List<Business_Categories> Categories);
        void DbUpdateCategory(Business_Categories category);
        void DbAddCategory(Business_Categories category);
        Task DbUpdateCategoryAsync(Business_Categories category);
        Task DbAddCategoryAsync(Business_Categories category);
        bool FindDuplicateCategory(string name, int typeId);
        bool FindDuplicateCategoryWithId(int id, string name);
    }
}
