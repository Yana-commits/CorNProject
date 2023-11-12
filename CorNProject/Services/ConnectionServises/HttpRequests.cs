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

namespace CorNProject.Services.ConnectionServises
{
    public static class HttpRequests
    {

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

    }
}
