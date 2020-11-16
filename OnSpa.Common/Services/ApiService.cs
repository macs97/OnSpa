using Newtonsoft.Json;
using OnSpa.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnSpa.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Stream> GetPictureAsync(string urlBase, string servicePrefix)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                string url = $"{servicePrefix}";
                HttpResponseMessage response = await client.GetAsync(url);
                Stream stream = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return stream;
            }
            catch
            {
                return null;
            }
        }

        public async Task<RandomUsers> GetRandomUser(string urlBase, string servicePrefix)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                string url = $"{servicePrefix}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<RandomUsers>(result);
            }
            catch
            {
                return null;
            }
        }

    }
}
