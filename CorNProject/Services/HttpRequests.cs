using CorNProject.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorNProject.Services
{
    public static class HttpRequests
    {

        //public async Task GetItemAsync()
        //{
        //    HttpClient client = new()
        //    {
        //        BaseAddress = new Uri("https://virtserver.swaggerhub.com")
        //    };

        //    HttpResponseMessage response = await client.GetAsync("/PJ2009PJ2009_1/Project/1.0.0/lightingSummary");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var product = await response.Content.ReadAsStringAsync();

        //        Console.WriteLine($"{product}\n");
        //    }

        //}
        public static async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
        {
            var client = new HttpClient();

            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri(url);
            httpMessage.Method = method;

            if (content != null)
            {
                httpMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var result = await client.SendAsync(httpMessage);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
                return response;
            }

            return default!;
 
        }
       
        //public async Task<bool> IsIn()
        //{
        //    var result = await SendAsync<bool, object>("http://localhost:5269/api/v1/value/isinbasket",
        //        HttpMethod.Get,
        //       new { });

        //    return result;

        //}
    }
}
