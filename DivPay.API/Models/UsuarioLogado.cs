using DivPay.DAL.Models;

namespace DivPay.API.Models
{
    public class UsuarioLogado
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public NivelUsuario Nivel { get; set; }
    }
}
