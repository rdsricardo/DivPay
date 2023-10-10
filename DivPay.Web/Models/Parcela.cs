namespace DivPay.Web.Models
{
    public class Parcela
    {
        public int ParcelaId { get; set; }
        public Divida Divida { get; set; }
        public int Numero { get; set; }
        public Decimal ValorOriginal { get; set; }
        public DateTime Vencimento { get; set; }
        public Decimal Juros { get; set; }
        public Decimal Multa { get; set; }
        public Decimal Taxa { get; set; }
        public Decimal Acrescimo { get; set; }
        public Decimal OutrosEncargos { get; set; }
    }
}
