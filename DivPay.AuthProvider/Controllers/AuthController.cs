using DivPay.AuthProvider.Infrastructure;
using DivPay.AuthProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace DivPay.AuthProvider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(ILogger<AuthController> logger, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.logger = logger;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        public async Task<IActionResult> Token(UsuarioLogin model)
        {
            var token = jwtAuthenticationManager.Authenticate(model.Login, model.Senha);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}