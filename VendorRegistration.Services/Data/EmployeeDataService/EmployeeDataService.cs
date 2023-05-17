using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.Data.EmployeeDataService
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private IConfiguration _config;
        public EmployeeDataService(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            string sql = @"SELECT Email, AccountName, CommonName as FullName, Department FROM [HHCEmployee].[dbo].[ADAccounts] WHERE Department = 'CORP INFORMATION SERVICES' OR Department = 'PURCHASING'";
            using (var connection = new SqlConnection(_config["ConnectionStrings:EmpData"]))
            {
                return connection.Query<Employee>(sql);
            }
        }
    }

    public class Employee
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? AccountName { get; set; }
        public string? Department { get; set; }

    }
}
