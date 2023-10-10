using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
    }
}