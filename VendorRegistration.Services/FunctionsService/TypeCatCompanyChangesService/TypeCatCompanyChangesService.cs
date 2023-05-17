using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.FunctionsService.TypeCatCompanyChangesService
{
    public class TypeCatCompanyChangesService : ITypeCatCompanyChangesService
    {
        public void TypeChange(ChangeEventArgs args, int TypeId, List<Business_Types> Types, Business_Types Type, List<Business_Types> CheckedTypes)
        {
            if (args.Value is not null)
            {
                bool IsSelected = (bool)args.Value;


                if (Types is not null)
                {
                    Type = Types.FirstOrDefault(type => type.Id == TypeId);
                }

                if (IsSelected)
                {
                    if (Type is not null && !CheckedTypes.Any(check => check.Id == TypeId))
                    {
                        CheckedTypes.Add(Type);
                    }
                }

                if (!IsSelected)
                {
                    CheckedTypes.RemoveAll(type => type.Id == TypeId);
                }
            }

        }


        public void CategoryLoad(List<Business_Categories> FilteredCategories, List<Business_Types> CheckedTypes, List<Business_Categories> Categories)
        {
            FilteredCategories.Clear();

            foreach (var type in CheckedTypes)
            {
                if (Categories is not null)
                {
                    var CatList = Categories.Where(cat => cat.Type_Id == type.Id);


                    foreach (var cat in CatList)
                    {
                        if (!FilteredCategories.Any(filter => filter.Id == cat.Id))
                        {
                            FilteredCategories.Add(cat);
                        }
                    }
                }
            }
        }

        public void CategoryChange(ChangeEventArgs args, int CatId, List<Business_Categories> Categories, Business_Categories Category, List<Business_Categories> CheckedCategories)
        {
            if (args.Value is not null)
            {
                bool IsSelected = (bool)args.Value;
                if (Categories is not null)
                {
                    Category = Categories.FirstOrDefault(cat => cat.Id == CatId);
                }

                if (IsSelected)
                {
                    if (Category is not null && !CheckedCategories.Any(cat => cat.Id == CatId))
                    {
                        CheckedCategories.Add(Category);
                    }
                }

                if (!IsSelected)
                {
                    CheckedCategories.RemoveAll(cat => cat.Id == CatId);
                }
            }
        }

        public void NotificationTypeChange(ChangeEventArgs args, int TypeId, List<Business_Types> Types, Business_Types Type, List<Business_Types> CheckedTypes, bool TypeAll)
        {
            if (args.Value is not null)
            {
                bool IsSelected = (bool)args.Value;
                if (Types is not null)
                {
                    Type = Types.FirstOrDefault(type => type.Id == TypeId);
                }

                if (IsSelected)
                {
                    if (Type is not null)
                    {
                        CheckedTypes.Add(Type);
                    }
                }

                if (!IsSelected)
                {
                    CheckedTypes.RemoveAll(type => type.Id == TypeId);
                    TypeAll = false;
                }
            }

        }

        public void NotificationCategoryChange(ChangeEventArgs args, int CatId, List<Business_Categories> Categories, Business_Categories Category, List<Business_Categories> CheckedCategories, List<Company_Types_Categories> CompanyTypesCats,
           List<Company> Companies, List<Company_Types_Categories> RelatedCompanies, bool CategoryAll)
        {
            if (args.Value is not null)
            {
                bool IsSelected = (bool)args.Value;

                if (Categories is not null)
                {
                    Category = Categories.FirstOrDefault(cat => cat.Id == CatId);

                    if (IsSelected)
                    {
                        if (Category is not null && !CheckedCategories.Any(check => check.Id == CatId))
                        {
                            CheckedCategories.Add(Category);

                            if (CompanyTypesCats is not null)
                            {
                                foreach (var company in CompanyTypesCats.Where(comp => comp.Category_Id == CatId))
                                {
                                    if (Companies is not null)
                                    {
                                        var Company = Companies.FirstOrDefault(comp => comp.Id == company.CompanyId);

                                        if (Company is not null && Company.Completed_Registeration && !Company.Disabled_From_Notifications && !Company.Is_Deleted)
                                        {
                                            if (!RelatedCompanies.Any(comp => comp.CompanyId == company.CompanyId) && !Company.Disabled_From_Notifications && !Company.Is_Deleted)
                                            {
                                                RelatedCompanies.Add(company);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (!IsSelected)
                {
                    if (Category is not null)
                    {
                        CheckedCategories.RemoveAll(cat => cat.Id == CatId);

                        RelatedCompanies.Clear();

                        CategoryAll = false;


                        foreach (var cat in CheckedCategories)
                        {
                            if (CompanyTypesCats is not null)
                            {
                                foreach (var company in CompanyTypesCats.Where(company => company.Category_Id == cat.Id))
                                {
                                    if (Companies is not null)
                                    {
                                        var Company = Companies.FirstOrDefault(comp => comp.Id == company.CompanyId && !comp.Is_Deleted && !comp.Disabled_From_Notifications && comp.Completed_Registeration);
                                        if (Company is not null)
                                        {
                                            if (!RelatedCompanies.Any(comp => comp.CompanyId == company.CompanyId) && !Company.Disabled_From_Notifications && !Company.Is_Deleted)
                                            {
                                                RelatedCompanies.Add(company); //This allows to add company back in if they have other categories that were checked
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void NotificationCompanyChange(ChangeEventArgs args, int CompanyId, List<Company> Companies, Company Company, List<Company> SelectedCompanies, bool CompanyAll)
        {
            if (args.Value is not null)
            {
                bool IsSelected = (bool)args.Value;
                if (Companies is not null)
                {
                    Company = Companies.Where(comp => !comp.Is_Deleted && !comp.Disabled_From_Notifications).FirstOrDefault(comp => comp.Id == CompanyId);

                    if (Company is not null)
                    {
                        if (IsSelected)
                        {
                            SelectedCompanies.Add(Company);
                        }

                        if (!IsSelected)
                        {
                            SelectedCompanies.RemoveAll(comp => comp.Id == CompanyId);
                            CompanyAll = false;
                        }
                    }
                }
            }
        }

        public void TypeAllHandle(bool TypeAll, Business_Types[] QueryableTypes, List<Business_Types> CheckedTypes)
        {

            if (TypeAll && QueryableTypes is not null)
            {
                foreach (var type in QueryableTypes)
                {
                    if (!CheckedTypes.Any(check => check.Id == type.Id))
                    {
                        CheckedTypes.Add(type);
                    }
                }
            }

            if (!TypeAll && QueryableTypes is not null)
            {
                foreach (var type in QueryableTypes)
                {
                    CheckedTypes.RemoveAll(Type => Type.Id == type.Id);
                }
            }
        }

        public void CategoryAllHandle(bool CategoryAll, List<Business_Categories> QueryableCategories, List<Business_Categories> CheckedCategories, List<Company_Types_Categories> CompanyTypesCats, List<Company> Companies,
            List<Company_Types_Categories> RelatedCompanies)
        {

            if (CategoryAll && QueryableCategories is not null)
            {
                foreach (var cat in QueryableCategories)
                {
                    if (!CheckedCategories.Any(check => check.Id == cat.Id))
                    {
                        CheckedCategories.Add(cat);
                    }

                    if (CompanyTypesCats is not null)
                    {
                        foreach (var ctc in CompanyTypesCats.Where(ctc => ctc.Category_Id == cat.Id))
                        {
                            if (Companies is not null)
                            {
                                var Company = Companies.Where(comp => !comp.Is_Deleted && !comp.Disabled_From_Notifications).FirstOrDefault(comp => comp.Id == ctc.CompanyId);
                                if (Company is not null)
                                {

                                    if (!RelatedCompanies.Any(comp => comp.CompanyId == ctc.CompanyId) && !Company.Disabled_From_Notifications && !Company.Is_Deleted)
                                    {
                                        RelatedCompanies.Add(ctc);
                                    }
                                }
                            }
                        }
                    }

                }
            }

            if (!CategoryAll && QueryableCategories is not null)
            {
                foreach (var cat in QueryableCategories)
                {
                    CheckedCategories.RemoveAll(check => check.Id == cat.Id);

                }

                RelatedCompanies.Clear();
                foreach (var cat in CheckedCategories)
                {
                    if (CompanyTypesCats is not null)
                    {
                        foreach (var company in CompanyTypesCats.Where(company => company.Category_Id == cat.Id))
                        {
                            if (Companies is not null)
                            {
                                var Company = Companies.Where(comp => !comp.Is_Deleted && !comp.Disabled_From_Notifications).FirstOrDefault(comp => comp.Id == company.CompanyId);

                                if (Company is not null)
                                {
                                    if (!RelatedCompanies.Any(comp => comp.CompanyId == company.CompanyId) && !Company.Disabled_From_Notifications && !Company.Is_Deleted)
                                    {
                                        RelatedCompanies.Add(company); //This allows to add company back in if they have other categories that were checked
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CompanyAllHandle(bool CompanyAll, List<Company_Types_Categories> QueryableCompanies, List<Company> SelectedCompanies, List<Company> Companies)
        {
            if (CompanyAll && QueryableCompanies is not null)
            {
                foreach (var company in QueryableCompanies)
                {
                    if (Companies is not null)
                    {
                        var Company = Companies.Where(comp => !comp.Is_Deleted && !comp.Disabled_From_Notifications).FirstOrDefault(comp => comp.Id == company.CompanyId);

                        if (Company is not null)
                        {
                            if (!SelectedCompanies.Any(company => company.Id == Company.Id) && !Company.Disabled_From_Notifications && !Company.Is_Deleted && Company.Completed_Registeration)
                            {
                                SelectedCompanies.Add(Company);
                            }
                        }
                    }
                }

            }

            if (!CompanyAll && QueryableCompanies is not null)
            {
                foreach (var company in QueryableCompanies)
                {
                    if (Companies is not null)
                    {
                        var Company = Companies.Where(comp => !comp.Is_Deleted && !comp.Disabled_From_Notifications).FirstOrDefault(comp => comp.Id == company.CompanyId);

                        if (Company is not null)
                        {
                            SelectedCompanies.RemoveAll(sel => sel.Id == Company.Id);
                        }
                    }
                }
            }
        }

    }
}
