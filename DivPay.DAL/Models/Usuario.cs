using DivPay.DAL.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public class Usuario : IUsuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string? CpfCnpj { get; set; }
        public string? RG { get; set; }
        public bool Ativo { get; set; }
        public NivelUsuario NivelUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public void Assign(Usuario usuario)
        {
            this.Login = usuario.Login;
            this.UsuarioId = usuario.UsuarioId;
            this.NivelUsuario = usuario.NivelUsuario;
        }
    }
}