using DivPay.AuthProvider.Models;
using DivPay.DAL.Contracts;
using DivPay.DAL.Data;
using DivPay.DAL.Extensions;
using DivPay.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DivPay.AuthProvider.Infrastructure
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key;
        private readonly DivPayContext context;
        private IUsuario usuario;

        public JwtAuthenticationManager(DivPayContext context, IUsuario usuario)
        {
            this.key = "D1vP4yP4g4m3nt051578!93@)";
            this.context = context;
            this.usuario = usuario;
        }

        public string Authenticate(string username, string password)
        {
            usuario = context.Usuarios.FirstOrDefault(u => u.Login == username);
            if (usuario != null)
            {
                if (usuario.Password != password)
                    return null;
            }
            else
                return null;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("ID", usuario.UsuarioId.ToString()),
                    new Claim("Nivel", usuario.NivelUsuario.ToInt().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
