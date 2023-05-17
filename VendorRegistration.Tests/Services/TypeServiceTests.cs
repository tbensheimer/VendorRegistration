using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.TypeService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class TypeServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static TypeService typeService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            typeService = new TypeService(_db);

            _db.Business_Types.AddRange(new[]
            {
                new Business_Types {Id = 1, Type_Name = "App", Is_Disabled = false, Is_Deleted = false },
                new Business_Types {Id = 2, Type_Name = "Bot", Is_Disabled = false, Is_Deleted = false },
                new Business_Types {Id = 3, Type_Name = "Cat", Is_Disabled = true, Is_Deleted = false },
                new Business_Types {Id = 4, Type_Name = "Dog", Is_Disabled = false, Is_Deleted = true },
                new Business_Types {Id = 5, Type_Name = "Hat", Is_Disabled = true, Is_Deleted = true }
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllTypes()
        {
            var typeList = typeService.GetTypesList();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(5));
            Assert.That(typeList[0].Type_Name, Is.EqualTo("App"));
        }

        [Test]
        public void ShouldGetActiveTypesCount()
        {
            var count = typeService.GetActiveTypesCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldGetAllTypesNotDeleted()
        {
            var typeList = typeService.GetTypesNotDeleted();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(3));
            Assert.That(typeList[2].Type_Name, Is.EqualTo("Cat"));
        }

        [Test]
        public void ShouldGetAllTypesNotDeletedQueryable()
        {
            var typeList = typeService.GetTypesNotDeletedQueryable();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(3));

            foreach (var type in typeList)
            {
                Assert.That(type.Is_Deleted, Is.EqualTo(false));
            }
        }

        [Test]
        public void ShouldGetTypesNotDeletedAndNotDisabledQueryable()
        {
            var typeList = typeService.GetTypesNotDisabledAndNotDeletedQueryable();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(2));

            foreach (var type in typeList)
            {
                Assert.That(type.Is_Disabled, Is.EqualTo(false));
            }
        }

        [Test]
        public void ShouldGetAllTypesNotDisabled()
        {
            var typeList = typeService.GetTypesNotDisabled();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(3));
            Assert.That(typeList[2].Type_Name, Is.EqualTo("Dog"));
        }

        [Test]
        public void ShouldGetTypesForRegistration()
        {
            var typeList = typeService.GetTypesForRegistration();

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(2));
            Assert.That(typeList[1].Type_Name, Is.EqualTo("Bot"));
            Assert.That(typeList[1].Is_Deleted, Is.EqualTo(false));
        }

        [Test]
        public void ShouldGetTypeFromId()
        {
            var type = typeService.GetTypeFromId(3);

            Assert.NotNull(type);
            Assert.That(type.Type_Name, Is.EqualTo("Cat"));
            Assert.That(type.Is_Disabled, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetTypeFromIdAndGivenList()
        {
            var typeList = typeService.GetTypesList();

            var type = typeService.GetTypeFromIdAndGivenList(4, typeList);

            Assert.NotNull(type);
            Assert.That(type.Id, Is.EqualTo(4));
            Assert.That(type.Type_Name, Is.EqualTo("Dog"));
        }

        [Test]
        public void ShouldAddType()
        {
            Business_Types type = new Business_Types { Id = 6, Type_Name = "Boats", Is_Deleted = false, Is_Disabled = false };

            typeService.DbAddType(type);

            var typeList = typeService.GetTypesList();

            var addedType = typeService.GetTypeFromId(6);

            Assert.NotNull(typeList);
            Assert.That(typeList.Count, Is.EqualTo(6));

            Assert.NotNull(addedType);
            Assert.That(addedType.Type_Name, Is.EqualTo("Boats"));
        }

        [Test]
        public void ShouldUpdateType()
        {
            var type = typeService.GetTypeFromId(1);

            type.Type_Name = "AppService";

            typeService.DbUpdateType(type);

            var updatedType = typeService.GetTypeFromId(1);

            Assert.NotNull(updatedType);
            Assert.That(updatedType.Type_Name, Is.EqualTo("AppService"));
            Assert.That(updatedType.Id, Is.EqualTo(1));
        }

    }
}
