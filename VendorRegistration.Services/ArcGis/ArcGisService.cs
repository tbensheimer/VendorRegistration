using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Models.ArcGis;

namespace VendorRegistration.Services.ArcGis
{
    public class ArcGisService
    {
        private IConfiguration _config;

        public ArcGisService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ArcGisSuggestion> GetSuggestions(string userInput, double longitutde = -86.148003, double latitutde = 39.791000)
        {
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
                    var httpResponse = await client.GetAsync($"{_config["ArcGIS:suggest"]}?token={_config["ArcGIS:token"]}&f=json&text={userInput}&sourceCountry=USA");
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArcGisSuggestion>(responseBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return null;
        }

        public async Task<ArcGisAddress> GetAddressWithKey(string magicKey)
        {
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
                    var httpResponse = await client.GetAsync($"{_config["ArcGIS:address"]}?token={_config["ArcGIS:token"]}&f=json&magicKey={magicKey}");
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArcGisAddress>(responseBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return null;
        }

        public async Task<ArcGisAddress> GetAddress(string address)
        {
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
                    var httpResponse = await client.GetAsync($"{_config["ArcGIS:address"]}?token={_config["ArcGIS:token"]}&f=json&SingleLine={address}");
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArcGisAddress>(responseBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return null;
        }

        public async Task<float?> GetDistance(XYLocation start, XYLocation end)
        {
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
                    var httpResponse = await client.GetAsync($"{_config["ArcGIS:route"]}?token={_config["ArcGIS:token"]}&f=json&stops={start.x},{start.y};{end.x},{end.y}");
                    httpResponse.EnsureSuccessStatusCode();
                    string responseBody = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArcGisRoute>(responseBody)?.routes.features.First().attributes.Total_Miles;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return 0;
        }
    }
}
