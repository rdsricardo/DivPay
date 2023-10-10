using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivPay.DAL.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string? RG { get; set; }
        public Usuario? Usuario { get; set; }
        public ICollection<Endereco>? Enderecos { get; set; }
        public ICollection<Telefone>? Telefones { get; set; }
        public ICollection<Email>? Emails { get; set; }
        public ICollection<Divida>? Dividas { get; set; }
    }
}
