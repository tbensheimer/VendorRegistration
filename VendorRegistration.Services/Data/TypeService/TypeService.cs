using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.TypeService
{
    public class TypeService : ITypeService
    {
        private VendorDataDbContext _db;

        public TypeService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Business_Types> GetTypesList()
        {
            List<Business_Types> typesList = new();

            typesList = _db.Business_Types.OrderBy(type => type.Type_Name).ToList();

            return typesList;
        }

        public double GetActiveTypesCount()
        {
            double count = _db.Business_Types.Where(type => !type.Is_Deleted).Count();

            return count;
        }

        public List<Business_Types> GetTypesNotDeleted()
        {
            List<Business_Types> typesList = new();

            typesList = _db.Business_Types.Where(type => !type.Is_Deleted).OrderBy(type => type.Type_Name).ToList();

            return typesList;
        }

        public IQueryable<Business_Types> GetTypesNotDeletedQueryable()
        {
            IQueryable<Business_Types> typesList;

            typesList = _db.Business_Types.Where(type => !type.Is_Deleted).OrderBy(type => type.Type_Name);

            return typesList;
        }

        public IQueryable<Business_Types> GetTypesNotDisabledAndNotDeletedQueryable()
        {
            IQueryable<Business_Types> typesList;

            typesList = _db.Business_Types.Where(type => !type.Is_Deleted && !type.Is_Disabled).OrderBy(type => type.Type_Name);

            return typesList;
        }

        public List<Business_Types> GetTypesNotDisabled()
        {
            List<Business_Types> typesList = new();

            typesList = _db.Business_Types.Where(type => !type.Is_Disabled).OrderBy(type => type.Type_Name).ToList();

            return typesList;
        }

        public List<Business_Types> GetTypesForRegistration()
        {
            List<Business_Types> typesList = new();

            typesList = _db.Business_Types.Where(type => !type.Is_Disabled && !type.Is_Deleted).OrderBy(type => type.Type_Name).ToList();

            return typesList;
        }

        public Business_Types GetTypeFromId(int typeId)
        {
            var type = _db.Business_Types.FirstOrDefault(type => type.Id == typeId);

            return type;
        }

        public Business_Types GetTypeFromIdAndGivenList(int typeId, List<Business_Types> Types)
        {
            var type = Types.FirstOrDefault(type => type.Id == typeId);

            return type;
        }

        public void DbAddType(Business_Types type)
        {
            _db.Business_Types.Add(type);
            _db.SaveChanges();
        }
        public async Task DbAddTypeAsync(Business_Types type)
        {
            _db.Business_Types.Add(type);
            await _db.SaveChangesAsync();
        }

        public void DbUpdateType(Business_Types type)
        {
            _db.Business_Types.Update(type);
            _db.SaveChanges();
        }
        public async Task DbUpdateTypeAsync(Business_Types type)
        {
            _db.Business_Types.Update(type);
            await _db.SaveChangesAsync();
        }

        public bool FindDuplicateType(string name)
        {
            var result = _db.Business_Types.Any(type => type.Type_Name == name);

            return result;
        }

        public bool FindDuplicateTypeWithId(int id, string name)
        {
            var result = _db.Business_Types.Where(type => type.Id != id).Any(type => type.Type_Name == name);

            return result;
        }
    }
}
