using DivPay.API.Models;
using DivPay.DAL.Contracts;
using DivPay.DAL.Data;
using DivPay.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DivPay.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DivPayContext context;
        private readonly IUsuario usuarioLogado;

        public UsuarioController(DivPayContext context, IUsuario usuarioLogado)
        {
            this.context = context;
            this.usuarioLogado = usuarioLogado;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.Usuarios.ToListAsync());
        }

        // GET: api/<UsuarioController>?login={login}
        [HttpGet("login")]
        public async Task<IActionResult> GetLogin()
        {
            var usuario = await context.Usuarios.Where(u => u.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound();

            var usuarioRetorno = new UsuarioLogado
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Nivel = usuario.NivelUsuario,
            };

            return Ok(usuarioRetorno);
        }
            

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);

        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var uri = Url.Action("Get", new { id = usuario.UsuarioId });
            return Created(uri, usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest();

            context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Usuarios.Any(e => e.UsuarioId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
