using DivPay.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace DivPay.Web.HttpClients
{
    public class UsuarioApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor accessor;

        public UsuarioApiClient(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            this.httpClient = httpClient;
            this.accessor = accessor;
        }

        private void AddBearerToken()
        {
            var token = accessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<UsuarioLogado> GetUsuarioLogadoAsync(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("usuario/login");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<UsuarioLogado>(await response.Content.ReadAsStringAsync());
        }
    }
}
