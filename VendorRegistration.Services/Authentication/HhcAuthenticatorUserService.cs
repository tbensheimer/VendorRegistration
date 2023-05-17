using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Models.Authentication;
using VendorRegistration.Services.Data.EmployeeDataService;

namespace VendorRegistration.Services.Authentication
{
    public class HhcAuthenticatorUserService : IUserService
    {
        private EmployeeDataService employeeDataService;     
        public HhcAuthenticatorUserService(EmployeeDataService _employeeDataService)  
        {                                                                              
            employeeDataService = _employeeDataService;                                 
        }                                                                                
        public User ValidateUser(string userName, string password, string url, string passcode)
        {
            var responseApi = AuthApi(userName, password, url, passcode);
            if (!string.IsNullOrWhiteSpace(responseApi))
            {
                var response = JsonConvert.DeserializeObject<AuthResponse>(responseApi);
                if (response != null)
                {
                    if (response.IsAuthenticated)
                    {
                        if (response.Roles.Contains("admin"))
                        {
                            return new User()
                            {
                                Domain = "HAHPDC1",
                                UserName = response.Username,
                                Role = "admin"
                            };
                        }
                        else
                        {
                            return new User()
                            {
                                Domain = "HAHPDC1",
                                UserName = response.Username,
                                Role = "hhcUser"
                            };
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to Authenticate");
                    }
                }
            }
            else
            {
                throw new Exception("Unable to Authenticate");
            }
            return new User()
            {
                Domain = "",
                UserName = "",
                Role = ""
            };
        }

        private string AuthApi(string username, string password, string url, string passcode)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Username = username,
                Password = password,
                AppId = "HHC.VendorRegistration",
                Passcode = passcode
            }), Encoding.UTF8, "application/json");
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback +=
                (sender, certificate, chain, errors) =>
                {
                    return true;
                };

            using (var client = new HttpClient(handler))
            {
                try
                {
                    var httpResponse = client.PostAsync(url, content).Result;
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return "";
                }
            }
        }
    }
}
