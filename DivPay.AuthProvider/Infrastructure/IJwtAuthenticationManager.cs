namespace DivPay.AuthProvider.Infrastructure
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}