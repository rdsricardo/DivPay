namespace DivPay.DAL.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
    }
}