using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.Data.EmployeeDataService
{
    public interface IEmployeeDataService
    {
        IEnumerable<Employee> GetEmployees();
    }
}
