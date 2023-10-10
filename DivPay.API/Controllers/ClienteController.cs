using DivPay.DAL.Data;
using DivPay.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DivPay.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly DivPayContext context;
        private readonly ILogger<ClienteController> logger;

        public ClienteController(DivPayContext context, ILogger<ClienteController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.Clientes.ToListAsync());
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await context.Clientes
                .Include("Enderecos")
                .Include("Telefones")
                .Include("Emails")
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            var uri = Url.Action("Get", new { id = cliente.ClienteId });
            return Created(uri, cliente);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return BadRequest();

            context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Clientes.Any(e => e.ClienteId == id))
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

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}