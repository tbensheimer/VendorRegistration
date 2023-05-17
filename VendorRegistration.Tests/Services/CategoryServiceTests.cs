using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.CategoryService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class CategoryServiceTest
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static CategoryService categoryService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            categoryService = new CategoryService(_db);

            _db.Business_Categories.AddRange(new[]
            {
                new Business_Categories { Id = 1, Type_Id = 1, Category_Name = "Microwaves", Is_Disabled = false, Is_Deleted = false },
                new Business_Categories { Id = 2, Type_Id = 1, Category_Name = "Apple", Is_Disabled = true, Is_Deleted = false },
                new Business_Categories { Id = 3, Type_Id = 2, Category_Name = "Banana", Is_Disabled = false, Is_Deleted = true },
                new Business_Categories { Id = 4, Type_Id = 2, Category_Name = "Fridge", Is_Disabled = false, Is_Deleted = true },
                new Business_Categories { Id = 5, Type_Id = 2, Category_Name = "Fork", Is_Disabled = false, Is_Deleted = false },

            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllCategories()
        {
            var categoryList = categoryService.GetCategoriesList();

            Assert.NotNull(categoryList);
            Assert.That(categoryList.Count, Is.EqualTo(5));
            Assert.That(categoryList[0].Category_Name, Is.EqualTo("Apple"));
        }

        [Test]
        public void ShouldGetActiveCategoriesCount()
        {
            var count = categoryService.GetCategoriesCountNotDeleted();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(3));
        }


        [Test]
        public void ShouldGetAllCategoriesNotDeleted()
        {
            var categoryList = categoryService.GetCategoriesNotDeleted();

            Assert.NotNull(categoryList);
            Assert.That(categoryList.Count, Is.EqualTo(3));
            Assert.That(categoryList[2].Category_Name, Is.EqualTo("Microwaves"));
        }

        [Test]
        public void ShouldGetAllCategoriesFromTypeId()
        {
            var categoryList = categoryService.GetCategoriesFromTypeId(2);

            Assert.NotNull(categoryList);
            Assert.That(categoryList.Count, Is.EqualTo(3));
            Assert.That(categoryList[0].Is_Deleted, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetCategoryFromId()
        {
            var category = categoryService.GetCategoryFromId(2);

            Assert.NotNull(category);
            Assert.That(category.Id, Is.EqualTo(2));
            Assert.That(category.Category_Name, Is.EqualTo("Apple"));
        }

        [Test]
        public void ShouldGetCategoryFromIdAndList()
        {
            var categoryList = categoryService.GetCategoriesList();

            var category = categoryService.GetCategoryFromIdAndGivenList(4, categoryList);

            Assert.NotNull(category);
            Assert.That(category.Id, Is.EqualTo(4));
            Assert.That(category.Category_Name, Is.EqualTo("Fridge"));
        }

        [Test]
        public void ShouldGetCategoriesForRegistration()
        {
            var categoryList = categoryService.GetCategoriesForRegistration();

            Assert.NotNull(categoryList);
            Assert.That(categoryList.Count, Is.EqualTo(2));
            Assert.That(categoryList[0].Category_Name, Is.EqualTo("Fork"));
        }

        [Test]
        public void voidShouldAddCategory()
        {
            Business_Categories category = new Business_Categories { Id = 6, Type_Id = 3, Category_Name = "Spoon", Is_Disabled = false, Is_Deleted = false };

            categoryService.DbAddCategory(category);

            var categoryList = categoryService.GetCategoriesList();
            var categoryAdded = categoryService.GetCategoryFromId(6);

            Assert.NotNull(categoryList);
            Assert.That(categoryList.Count, Is.EqualTo(6));

            Assert.NotNull(categoryAdded);
            Assert.That(categoryAdded.Category_Name, Is.EqualTo("Spoon"));
        }

        [Test]
        public void ShouldUpdateCategory()
        {
            var category = categoryService.GetCategoryFromId(2);

            category.Category_Name = "AppleTree";

            categoryService.DbUpdateCategory(category);

            var updatedCategory = categoryService.GetCategoryFromId(2);

            Assert.NotNull(updatedCategory);
            Assert.That(updatedCategory.Category_Name, Is.EqualTo("AppleTree"));
            Assert.That(updatedCategory.Id, Is.EqualTo(2));
        }

    }
}
