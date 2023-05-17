using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.CompanyService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static CompanyService companyService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            companyService = new CompanyService(_db);

            _db.Companies.AddRange(new[]
            {
                new Company { Id = 1, Name = "TrentCo", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111111", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = true, EditedSince = DateTime.Now, Minority = true },
                new Company { Id = 2, Name = "Airplane", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111112", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "Not Certified", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = true, EditedSince = new DateTime(2022, 12, 5, 8, 30, 0), Minority = false },
                new Company { Id = 3, Name = "Army", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111113", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = false, EditedSince = DateTime.Now, Minority = true },
                new Company { Id = 4, Name = "Bills Lumber", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111114", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = true, Completed_Registeration = false, EditedSince = DateTime.Now, Minority = true },
                new Company { Id = 5, Name = "Trent Foods", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111115", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = true, Completed_Registeration = true, EditedSince = DateTime.Now, Minority = false }
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllCompanies()
        {
            var companyList = companyService.GetAllCompanies();

            Assert.NotNull(companyList);
            Assert.That(companyList.Count, Is.EqualTo(5));
            Assert.That(companyList[0].Fed_Id_SSN, Is.EqualTo("111111112"));
        }

        [Test]
        public void ShouldGetAllVerifiedCompanies()
        {
            var companyList = companyService.GetVerifiedCompanies();

            Assert.NotNull(companyList);
            Assert.That(companyList.Count, Is.EqualTo(3));
            Assert.That(companyList[0].Fed_Id_SSN, Is.EqualTo("111111112"));
        }

        [Test]
        public void ShouldGetAllMinorityCompanies()
        {
            var minorityList = companyService.GetMinorityCompanies();

            Assert.NotNull(minorityList);
            Assert.That(minorityList.Count, Is.EqualTo(1));
            Assert.That(minorityList[0].Diversity_Certification, Is.EqualTo("City of Indianapolis"));
        }

        [Test]
        public void ShouldGetAllVerifiedCompaniesArray()
        {
            var companyList = companyService.GetVerifiedCompaniesArray();

            Assert.NotNull(companyList);
            Assert.That(companyList.Count, Is.EqualTo(3));
            Assert.That(companyList[0].Fed_Id_SSN, Is.EqualTo("111111112"));
        }

        [Test]
        public void ShouldGetVerifiedCompaniesNotDeletedCount()
        {
            var count = companyService.GetVerifiedCompaniesCountNotDeleted();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetCompanyById()
        {
            var company = companyService.GetCompanyById(4);

            Assert.NotNull(company);
            Assert.That(company.Id, Is.EqualTo(4));
            Assert.That(company.Name, Is.EqualTo("Bills Lumber"));
        }

        [Test]

        public void ShouldAddCompany()
        {
            Company comp = new Company { Id = 6, Name = "Trent Foods", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111116", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = true };

            companyService.DbAddCompany(comp);

            var companyList = companyService.GetAllCompanies();

            var company = companyService.GetCompanyById(6);

            Assert.NotNull(companyList);
            Assert.That(companyList.Count, Is.EqualTo(6));

            Assert.NotNull(company);
            Assert.That(company.Fed_Id_SSN, Is.EqualTo("111111116"));
        }
        [Test]
        public void ShouldUpdateCompany()
        {
            var company = companyService.GetCompanyById(5);

            company.Name = "Food";
            company.Fed_Id_SSN = "111111118";

            companyService.DbUpdateCompany(company);

            var updatedCompany = companyService.GetCompanyById(5);

            Assert.NotNull(updatedCompany);
            Assert.That(updatedCompany.Name, Is.EqualTo("Food"));
            Assert.That(updatedCompany.Fed_Id_SSN, Is.EqualTo("111111118"));
        }

        [Test]
        public void ShouldRemoveUnverifiedCompaniesAfter30Days()
        {
            Company company1 = new Company { Id = 6, Name = "Bills Lumber", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111114", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = true, Completed_Registeration = false };
            Company company2 = new Company { Id = 7, Name = "Trent Foods", Sign_Up = new DateTime(2022, 12, 5, 8, 30, 0), Fed_Id_SSN = "111111115", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = true, Completed_Registeration = false };
            Company company3 = new Company { Id = 8, Name = "Trent Foods", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111115", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = true, Completed_Registeration = false };


            _db.Companies.Add(company1);
            _db.Companies.Add(company2);
            _db.Companies.Add(company3);
            _db.SaveChanges();

            companyService.UnverifiedCompaniesRemove();

            var list = companyService.GetAllCompanies();

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(7));
            Assert.That(list[0].Name, Is.EqualTo("Airplane"));
        }

        [Test]
        public void ShouldGetRecentlyEditedCompanies()
        {
            var list = companyService.GetRecentlyEditedActiveCompanies();

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetTotalMinorityVendorsCount()
        {
            var count = companyService.GetTotalActiveMinorityVendorsCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetCertifiedMinorityVendorsCount()
        {
            var count = companyService.GetCertifiedMinorityVendorsCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldGetUncertifiedMinorityVendorsCount()
        {
            var count = companyService.GetUncertifiedMinorityVendorsCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldGetDeletedCompaniesCount()
        {
            var count = companyService.GetDeletedCompaniesCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(1));
        }

    }
}
