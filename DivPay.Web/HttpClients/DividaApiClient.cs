using DivPay.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DivPay.Web.HttpClients
{
    public class DividaApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor accessor;

        public DividaApiClient(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            this.httpClient = httpClient;
            this.accessor = accessor;
        }

        private void AddBearerToken()
        {
            var token = accessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Divida>> GetDividasAsync()
        {
            AddBearerToken();
            var response = await httpClient.GetAsync("divida");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<Divida>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Divida> GetDividaAsync(int id)
        {
            AddBearerToken();
            var response = await httpClient.GetAsync($"divida/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Divida>(await response.Content.ReadAsStringAsync());
        }

        //public async Task<List<Divida>> GetDividasUsuarioAsync(int id)
        //{
        //    AddBearerToken();
        //    var response = await httpClient.GetAsync($"divida/usuario/{id}");
        //    response.EnsureSuccessStatusCode();
        //    return JsonConvert.DeserializeObject<List<Divida>>(await response.Content.ReadAsStringAsync());
        //}
    }
}
