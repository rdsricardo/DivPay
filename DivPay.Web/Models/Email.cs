namespace DivPay.Web.Models
{
    public class Email
    {
        public int EmailId { get; set; }

        public Cliente Cliente { get; set; }
        public string Endereco { get; set; }
    }
}
