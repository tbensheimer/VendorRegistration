using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.TypeService
{
    public interface ITypeService
    {
        public List<Business_Types> GetTypesList();
        double GetActiveTypesCount();
        List<Business_Types> GetTypesNotDeleted();
        IQueryable<Business_Types> GetTypesNotDeletedQueryable();
        IQueryable<Business_Types> GetTypesNotDisabledAndNotDeletedQueryable();
        List<Business_Types> GetTypesNotDisabled();
        List<Business_Types> GetTypesForRegistration();
        Business_Types GetTypeFromId(int typeId);
        Business_Types GetTypeFromIdAndGivenList(int typeId, List<Business_Types> Types);
        void DbAddType(Business_Types type);
        Task DbAddTypeAsync(Business_Types type);
        void DbUpdateType(Business_Types type);
        Task DbUpdateTypeAsync(Business_Types type);
        bool FindDuplicateType(string name);
        bool FindDuplicateTypeWithId(int id, string name);
    }
}
