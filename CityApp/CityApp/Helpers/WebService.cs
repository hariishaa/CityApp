using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Helpers
{
    public static class WebService
    {
        public static async Task<T> MakeApiRequest<T>(string method, Dictionary<string, string> parameters = null)
        {
            var baseUrl = "http://46.101.183.135/api/";
            // todo: попробовать упростить способ построения url с параметрами
            var url = MakeUrl(baseUrl + method, parameters);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
        }

        public static async Task<JObject> MakeUrlRequest(string url, Dictionary<string, string> parameters = null)
        {
            url = MakeUrl(url, parameters);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JObject>(json);
            }
        }

        public static async Task<byte[]> MakeUrlBytesRequest(string url, Dictionary<string, string> parameters = null)
        {
            url = MakeUrl(url, parameters);
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return bytes;
            }
        }

        static string MakeUrl(string url, Dictionary<string, string> parameters = null)
        {
            url += "?";
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    url += $"{param.Key}={param.Value}&";
                }
            }
            return url;
        }
    }
}
