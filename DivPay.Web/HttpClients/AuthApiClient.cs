using DivPay.Web.Models;

namespace DivPay.Web.HttpClients
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
    }

    public class AuthApiClient
    {
        private readonly HttpClient httpClient;

        public AuthApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LoginResult> PostLoginAsync(UsuarioLogin model)
        {
            var response = await httpClient.PostAsJsonAsync("auth", model);

            var loginResult = new LoginResult
            {
                Succeeded = response.IsSuccessStatusCode,
                Token = await response.Content.ReadAsStringAsync()
            };

            return loginResult;
        }
    }
}
