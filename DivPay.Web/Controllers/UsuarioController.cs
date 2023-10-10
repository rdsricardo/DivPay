using DivPay.Web.HttpClients;
using DivPay.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace DivPay.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AuthApiClient auth;
        private readonly UsuarioApiClient usuarioApiClient;

        public UsuarioController(AuthApiClient auth, UsuarioApiClient usuarioApiClient)
        {
            this.auth = auth;
            this.usuarioApiClient = usuarioApiClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.PostLoginAsync(model);
                if (result.Succeeded)
                {
                    var usuario = await usuarioApiClient.GetUsuarioLogadoAsync(result.Token);

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Login),
                        new Claim("Id", usuario.UsuarioId.ToString()),
                        new Claim("Token", result.Token),
                        new Claim("Nome", usuario.Nome),
                        new Claim("Nivel", usuario.Nivel.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(String.Empty, "Erro na autenticação");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
