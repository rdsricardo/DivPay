using System.ComponentModel;

namespace DivPay.Web.Models
{
    public class Divida
    {
        public int DividaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [DisplayName("Contrato")]
        public string NumeroContrato { get; set; }

        [DisplayName("Credor")]
        public string NomeCredor { get; set; }

        [DisplayName("CPF/CNPJ Credor")]
        public string CpfCnpjCredor { get; set; }

        [DisplayName("Telefone Contato")]
        public string TelefoneContato { get; set; }

        [DisplayName("Email Contato")]
        public string EmailContato { get; set; }

        [DisplayName("Whatsapp Contato")]
        public string LinkWhatsappContato { get; set; }

        [DisplayName("Site")]
        public string LinkSiteContato { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Total Original")]
        public decimal TotalOriginal { get; set; }

        [DisplayName("URL Notificação")]
        public string UrlNotificacao { get; set; }

        [DisplayName("Identificador")]
        public string Identificador { get; set; }

        [DisplayName("Status")]
        public StatusPagamento StatusPagamento { get; set; }

        [DisplayName("Pago")]
        public bool Pago { get; set; }

        [DisplayName("Token")]
        public string Token { get; set; }

        [DisplayName("URL Token")]
        public string UrlToken { get; set; }

        [DisplayName("Parcelas")]
        public ICollection<Parcela> Parcelas { get; set; }
    }
}