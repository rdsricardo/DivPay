namespace DivPay.Web.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; }
        public Cliente Cliente { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
    }
}
