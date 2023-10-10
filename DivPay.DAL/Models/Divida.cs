using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public class Divida
    {
        [Key]
        public int DividaId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string NumeroContrato { get; set; }
        public string NomeCredor { get; set; }
        public string CpfCnpjCredor { get; set; }
        public string? TelefoneContato { get; set; }
        public string? EmailContato { get; set; }
        public string? LinkWhatsappContato { get; set; }
        public string? LinkSiteContato { get; set; }
        public string? Descricao { get; set; }
        public decimal TotalOriginal { get; set; }
        public string? UrlNotificacao { get; set; }
        public string? Identificador { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public bool Pago { get; set; }
        public string? Token { get; set; }
        public string? UrlToken { get; set; }
        public ICollection<Parcela> Parcelas { get; set; }
    }
}