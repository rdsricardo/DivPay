using System.ComponentModel;

namespace DivPay.Web.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [DisplayName("Cliente")]
        public string Nome { get; set; }

        [DisplayName("CPF/CNPJ")]
        public string CpfCnpj { get; set; }

        [DisplayName("RG")]
        public string RG { get; set; }

        public Usuario Usuario { get; set; }

        [DisplayName("Endereços")]
        public ICollection<Endereco> Enderecos { get; set; }

        [DisplayName("Telefones")]
        public ICollection<Telefone> Telefones { get; set; }

        [DisplayName("E-Mails")]
        public ICollection<Email> Emails { get; set; }

        public ICollection<Divida> Dividas { get; set; }
    }
}