namespace DivPay.Web.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public Cliente Cliente { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}
