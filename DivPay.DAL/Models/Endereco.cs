using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
    }
}