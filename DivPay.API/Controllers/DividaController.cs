using DivPay.API.Models;
using DivPay.DAL.Data;
using DivPay.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Security.Cryptography;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DivPay.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DividaController : ControllerBase
    {
        private readonly DivPayContext context;
        private readonly ILogger<DividaController> logger;
        private readonly IConfiguration configuration;

        private const string chave = "D1vP4Y2023*";

        public DividaController(DivPayContext context, ILogger<DividaController> logger, IConfiguration configuration)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        // GET: api/<DividaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.Dividas.Include("Cliente").ToListAsync());
        }

        // GET api/<DividaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var divida = await context.Dividas.FindAsync(id);

            if (divida == null)
                return NotFound();

            return Ok(divida);
        }

        // POST api/<DividaController>
        [HttpPost]
        public async Task<IActionResult> Post(Divida divida)
        {
            if (ModelState.IsValid)
            {

                //Verifica se existe a dívida
                var dividaBanco = context.Dividas
                    .Include("Cliente")
                    .FirstOrDefault(d => d.NumeroContrato.Trim() == divida.NumeroContrato.Trim() && d.CpfCnpjCredor.Trim() == divida.CpfCnpjCredor.Trim() && d.Cliente.CpfCnpj.Trim() == divida.Cliente.CpfCnpj.Trim());

                if (dividaBanco == null)
                {
                    var cliente = context.Clientes
                        .Include("Enderecos")
                        .Include("Telefones")
                        .Include("Emails")
                        .FirstOrDefault(c => c.CpfCnpj.Trim() == divida.Cliente.CpfCnpj.Trim());

                    //Cadastrar Cliente
                    if (cliente == null)
                    {
                        context.Clientes.Add(divida.Cliente);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        cliente.Nome = divida.Cliente.Nome;
                        cliente.RG = divida.Cliente.RG;

                        //Cadastrar endereços, telefones, emails
                        divida.Cliente.Enderecos?.ToList().ForEach(e =>
                        {
                            var endereco = cliente.Enderecos?.FirstOrDefault(end => end.Logradouro.Trim() == e.Logradouro.Trim() && end.Numero == e.Numero);

                            if (endereco == null)
                            {
                                (cliente.Enderecos ?? new List<Endereco>()).Add(e);
                            }
                            else
                            {
                                endereco.Bairro = e.Bairro;
                                endereco.Complemento = e.Complemento;
                                endereco.CEP = e.CEP;
                                endereco.Cidade = e.Cidade;
                                endereco.UF = e.UF;
                            }
                        });

                        divida.Cliente.Telefones?.ToList().ForEach(t =>
                        {
                            var telefone = cliente.Telefones?.FirstOrDefault(tel => tel.DDD.Trim() == t.DDD.Trim() && tel.Numero.Trim() == t.Numero.Trim());

                            if (telefone == null)
                            {
                                (cliente.Telefones ?? new List<Telefone>()).Add(t);
                            }
                        });

                        divida.Cliente.Emails?.ToList().ForEach(e =>
                        {
                            var email = cliente.Emails?.FirstOrDefault(em => em.Endereco.Trim() == e.Endereco.Trim());

                            if (email == null)
                            {
                                (cliente.Emails ?? new List<Email>()).Add(e);
                            }
                        });

                        divida.Cliente = cliente;
                    }

                    divida.Token = GerarMD5(chave, divida.NumeroContrato.Trim() + "|" + divida.Cliente.CpfCnpj.Trim());
                    divida.UrlToken = $"{this.configuration.GetValue<string>("AppSettings:SiteUrl")}/Token/{divida.Token}";

                    context.Dividas.Add(divida);
                    await context.SaveChangesAsync();

                    EnviarEmail(divida);
                    EnviarSMS(divida);

                    var uri = Url.Action("Get", new { id = divida.DividaId });
                    return Created(uri, divida);
                }
                else
                {
                    throw new Exception("Dívida já cadastrada para esse devedor e credor");
                }
            }

            return BadRequest(ErrorResponse.FromModelState(ModelState)); //400
        }

        private void EnviarEmail(Divida divida)
        {
            try
            {
                var client = new SendGridClient("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");

                var emails = divida.Cliente.Emails.ToList();

                Parallel.For(0, emails.Count, async index =>
                {
                    var email = emails[index];

                    var msg = new SendGridMessage()
                    {
                        From = new EmailAddress("1412519@sga.pucminas.br", "Projeto Integrado - PUC Minas"),
                        Subject = divida.Cliente.Nome.Trim(),
                        PlainTextContent = $"Olá {divida.Cliente.Nome}. Realize o pagamento da dívida referente ao contrato {divida.NumeroContrato.Trim()} e regularize sua situação! Para isso, acesse agora mesmo o link {divida.UrlToken}",
                        HtmlContent = $"Olá <strong> {divida.Cliente.Nome}.</strong></br></br> Realize o pagamento da dívida referente ao contrato {divida.NumeroContrato.Trim()} e regularize sua situação! Para isso, acesse agora mesmo o link {divida.UrlToken}"
                    };
                    msg.AddTo(new EmailAddress(email.Endereco.Trim(), divida.Cliente.Nome.Trim()));
                    client.SendEmailAsync(msg).Wait();
                    var response = await client.SendEmailAsync(msg);

                    if (!response.IsSuccessStatusCode)
                    {
                        var messageResponse = await response.DeserializeResponseBodyAsync();
                        logger.LogError($"Erro SendGrid: {(int)response.StatusCode}");
                    }
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao enviar e-mail (SendGrid): {ex.Message}");
            }
        }

        private void EnviarSMS(Divida divida)
        {
            const string accountSid = "AC59b1d0aabbd64725113ca34f4cf3474f";
            const string authToken = "b70d0e68904e467835a1fcaaacbb0237";

            try
            {
                var telefones = divida.Cliente.Telefones.Where(t => t.Numero.StartsWith("7") || t.Numero.StartsWith("8") || t.Numero.StartsWith("9")).ToList();

                Parallel.For(0, telefones.Count, index =>
                {
                    var telefone = telefones[index];

                    TwilioClient.Init(accountSid, authToken);

                    var message = MessageResource.Create(
                        body: $"Olá {divida.Cliente.Nome}. Realize o pagamento da dívida referente ao contrato {divida.NumeroContrato.Trim()} e regularize sua situação! Para isso, acesse agora mesmo o link {divida.UrlToken}",
                        from: new Twilio.Types.PhoneNumber("+12566641619"),
                        to: new Twilio.Types.PhoneNumber("+55" + telefone.DDD.Trim() + telefone.Numero.Trim())
                    );

                });
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, $"Erro ao enviar SMS (Twilio): {ex.Message}");
            }
        }

        internal string GerarMD5(string chave, string conteudo)
        {
            var hash = new StringBuilder();

            conteudo += chave;

            MD5.Create().ComputeHash(Encoding.Default.GetBytes(conteudo)).ToList().ForEach(item =>
            {
                hash.Append(item.ToString("x2"));
            });

            return (hash.ToString());
        }

        // PUT api/<DividaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Divida divida)
        {
            if (id != divida.DividaId)
                return BadRequest();

            context.Entry(divida).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Dividas.Any(e => e.DividaId == id))
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

        // DELETE api/<DividaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var divida = await context.Dividas.FindAsync(id);
            if (divida == null)
            {
                return NotFound();
            }

            context.Dividas.Remove(divida);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}