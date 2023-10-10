using DivPay.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DivPay.Web.HttpClients
{
    public class ClienteApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor accessor;

        public ClienteApiClient(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            this.httpClient = httpClient;
            this.accessor = accessor;
        }

        private void AddBearerToken()
        {
            var token = accessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            AddBearerToken();
            var response = await httpClient.GetAsync($"cliente/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());
        }
    }
}