using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.Authentication
{
    public class ErrorResponse
    {

        [JsonProperty("IsError")]
        public bool IsError { get; set; }
        [JsonProperty("ErrorText")]
        public string? ErrorText { get; set; }
    }
}
