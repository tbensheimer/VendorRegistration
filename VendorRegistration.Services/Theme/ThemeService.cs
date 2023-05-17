using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Models.Theme;

namespace VendorRegistration.Services.Theme
{
    public class ThemeService
    {
        private IConfiguration _config;
        public ThemeResponse Theme = new ThemeResponse();
        public bool ThemeRetrieved { get; set; } = false;

        public ThemeService(IConfiguration config)
        {
            _config = config;
        }

        public void GetThemeData(string username)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Username = username
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
                    var httpResponse = client.PostAsync(_config["Theme:Url"], content).Result;
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                    Theme = JsonConvert.DeserializeObject<ThemeResponse>(responseBody);
                    ThemeRetrieved = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}

