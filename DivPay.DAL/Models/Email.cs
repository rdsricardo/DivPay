using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public string Endereco { get; set; }
    }
}