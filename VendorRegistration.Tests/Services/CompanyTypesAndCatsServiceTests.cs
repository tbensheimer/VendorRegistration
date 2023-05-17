using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.CompanyTypesAndCatsService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class CompanyTypesAndCatsServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static CompanyTypesAndCatsService ctcService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            ctcService = new CompanyTypesAndCatsService(_db);

            _db.Company_Types_Categories.AddRange(new[]
            {
                new Company_Types_Categories { Id = 1, CompanyId = 1, Type_Id = 1, Category_Id = 1 },
                new Company_Types_Categories { Id = 2, CompanyId = 1, Type_Id = 1, Category_Id = 2 },
                new Company_Types_Categories { Id = 3, CompanyId = 1, Type_Id = 2, Category_Id = 1 },
                new Company_Types_Categories { Id = 4, CompanyId = 2, Type_Id = 1, Category_Id = 1 },
                new Company_Types_Categories { Id = 5, CompanyId = 2, Type_Id = 2, Category_Id = 3 },
                new Company_Types_Categories { Id = 6, CompanyId = 3, Type_Id = 2, Category_Id = 1 },
            });

            _db.SaveChanges();
        }


        [Test]
        public void ShouldGetAllCompTypesCats()
        {
            var list = ctcService.GetAllCompanyTypesAndCats();

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(6));
            Assert.That(list[5].Id, Is.EqualTo(6));
        }

        [Test]
        public void GetAllCompTypesCatsFromCompanyId()
        {
            var list = ctcService.GetCompanyTypesAndCatsFromCompanyId(1);

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[1].Category_Id, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetCompTypesCatsFromId()
        {
            var ctc = ctcService.GetCompanyCategoryFromId(6);

            Assert.NotNull(ctc);
            Assert.That(ctc.CompanyId, Is.EqualTo(3));
            Assert.That(ctc.Id, Is.EqualTo(6));
        }

        [Test]
        public void ShouldGetCompanyTypesAndCatsFromCompanyIdAndFromGivenList()
        {
            var ctcList = ctcService.GetAllCompanyTypesAndCats();

            var ctc = ctcService.GetCompanyTypesAndCatsFromCompanyIdAndFromGivenList(1, ctcList);

            Assert.NotNull(ctc);
            Assert.That(ctc.Count, Is.EqualTo(3));
            Assert.That(ctc[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddRangeCompTypeCat()
        {
            List<Company_Types_Categories> list = new();

            Company_Types_Categories ctc1 = new Company_Types_Categories { Id = 7, CompanyId = 3, Type_Id = 1, Category_Id = 1 };
            Company_Types_Categories ctc2 = new Company_Types_Categories { Id = 8, CompanyId = 3, Type_Id = 1, Category_Id = 2 };

            list.Add(ctc1);
            list.Add(ctc2);

            ctcService.DbAddRangeCompanyTypeAndCat(list);

            var updatedList = ctcService.GetAllCompanyTypesAndCats();

            Assert.NotNull(updatedList);
            Assert.That(updatedList.Count, Is.EqualTo(8));
            Assert.That(updatedList[7].Id, Is.EqualTo(7));
        }

        [Test]
        public void ShouldRemoveCompTypeCat()
        {
            var ctc = ctcService.GetCompanyCategoryFromId(1);

            ctcService.DbRemoveCompanyTypeAndCat(ctc);

            var list = ctcService.GetAllCompanyTypesAndCats();

            var removedCtc = ctcService.GetCompanyCategoryFromId(1);

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(5));

            Assert.Null(removedCtc);
        }
    }
}
