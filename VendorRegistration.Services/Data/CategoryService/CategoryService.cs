using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private VendorDataDbContext _db;

        public CategoryService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Business_Categories> GetCategoriesList()
        {
            List<Business_Categories> categorylist = new();

            categorylist = _db.Business_Categories.OrderBy(cat => cat.Category_Name).ToList();

            return categorylist;
        }

        public double GetCategoriesCountNotDeleted()
        {
            double count = _db.Business_Categories.Where(cat => !cat.Is_Deleted).Count();

            return count;
        }

        public List<Business_Categories> GetCategoriesNotDeleted()
        {
            List<Business_Categories> categorylist = new();

            categorylist = _db.Business_Categories.Where(cat => !cat.Is_Deleted).OrderBy(cat => cat.Category_Name).ToList();

            return categorylist;
        }

        public List<Business_Categories> GetCategoriesFromTypeId(int typeId)
        {
            List<Business_Categories> categorylist = new();

            categorylist = _db.Business_Categories.Where(cat => cat.Type_Id == typeId).OrderBy(cat => cat.Category_Name).ToList();

            return categorylist;
        }

        public Business_Categories GetCategoryFromId(int catId)
        {
            var category = _db.Business_Categories.FirstOrDefault(cat => cat.Id == catId);

            return category;
        }

        public Business_Categories GetCategoryFromIdAndGivenList(int catId, List<Business_Categories> Categories)
        {
            var category = Categories.FirstOrDefault(cat => cat.Id == catId);

            return category;
        }

        public List<Business_Categories> GetCategoriesForRegistration()
        {
            List<Business_Categories> categorylist = new();

            categorylist = _db.Business_Categories.Where(cat => !cat.Is_Deleted && !cat.Is_Disabled).OrderBy(cat => cat.Category_Name).ToList();

            return categorylist;
        }

        public void DbUpdateCategory(Business_Categories category)
        {
            _db.Business_Categories.Update(category);
            _db.SaveChanges();
        }
        public async Task DbUpdateCategoryAsync(Business_Categories category)
        {
            _db.Business_Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public void DbAddCategory(Business_Categories category)
        {
            _db.Business_Categories.Add(category);
            _db.SaveChanges();
        }
        public async Task DbAddCategoryAsync(Business_Categories category)
        {
            _db.Business_Categories.Add(category);
            await _db.SaveChangesAsync();
        }
        public bool FindDuplicateCategory(string name, int typeId)
        {
            var result = _db.Business_Categories.Any(cat => cat.Category_Name == name && cat.Type_Id == typeId);

            return result;
        }

        public bool FindDuplicateCategoryWithId(int id, string name)
        {
            var result = _db.Business_Categories.Where(type => type.Id != id).Any(cat => cat.Category_Name == name);

            return result;
        }
    }
}
