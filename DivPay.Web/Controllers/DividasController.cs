using DivPay.Web.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DivPay.Web.Controllers
{
    [Authorize]
    public class DividasController : Controller
    {
        private readonly DividaApiClient dividasApiClient;
        private readonly ClienteApiClient clienteApiClient;

        public DividasController(DividaApiClient dividasApiClient, ClienteApiClient clienteApiClient)
        {
            this.dividasApiClient = dividasApiClient;
            this.clienteApiClient = clienteApiClient;
        }

        // GET: DividaController
        public async Task<IActionResult> Index()
        {
            var dividas = await dividasApiClient.GetDividasAsync();

            return View(dividas);
        }

        // GET: DividaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var divida = await dividasApiClient.GetDividaAsync(id);

            divida.Cliente = await clienteApiClient.GetClienteAsync(divida.ClienteId);

            return View(divida);
        }

        //[HttpGet("Usuario/{id}")]
        //public async Task<IActionResult> DividasUsuario(int id)
        //{
        //    var dividas = await dividasApiClient.GetDividasUsuarioAsync(id);

        //    return View("DividasUsuario", dividas);
        //}

        // GET: DividaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DividaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DividaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DividaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DividaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
