using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.Theme
{
    public class ThemeResponse
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("jobTitle")]
        public string? JobTitle { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }
        [JsonProperty("employeeId")]
        public string? EmployeeId { get; set; }
        [JsonProperty("costCenter")]
        public string? CostCenter { get; set; }
    }
}
