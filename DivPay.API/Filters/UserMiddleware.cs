using DivPay.DAL.Contracts;
using DivPay.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace DivPay.API.Filters
{
    public class UserMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<UserMiddleware> logger;

        public UserMiddleware(RequestDelegate next, ILogger<UserMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IUsuario usuario)
        {
            context!.Request.Headers.TryGetValue("Authorization", out var authorization);

            if (!authorization.IsNullOrEmpty())
            {
                var token = authorization.FirstOrDefault()![7..];
                usuario.Assign(this.DecodeUser(token));
            }

            //Serilog.Extensions.Hosting
            //if (usuario != null)
            //{
            //    diagnosticContext.Set("UsuarioId", usuario.UsuarioId);
            //    diagnosticContext.Set("NivelId", usuario.NivelUsuario.ToInt());
            //}

            await next(context);
        }

        private Usuario DecodeUser(string token)
        {
            var tokenDescriptor = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Usuario usuario = new Usuario();
            usuario.Login = tokenDescriptor.Claims.First(c => c.Type == "unique_name").Value;
            usuario.UsuarioId = Convert.ToInt32(tokenDescriptor.Claims.First(c => c.Type == "ID").Value);
            usuario.NivelUsuario = (NivelUsuario)Convert.ToInt32(tokenDescriptor.Claims.First(c => c.Type == "Nivel").Value);

            return usuario;
        }
    }
}