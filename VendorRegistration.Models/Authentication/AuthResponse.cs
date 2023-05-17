using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.Authentication
{
    public class AuthResponse
    {
        [JsonProperty("IsAuthed")]
        public bool IsAuthenticated { get; set; }
        [JsonProperty("Username")]
        public string? Username { get; set; }
        [JsonProperty("Roles")]
        public string? Roles { get; set; }
        [JsonProperty("Error")]
        public ErrorResponse? Error { get; set; }
    }
}
