using DivPay.DAL.Models;

namespace DivPay.DAL.Contracts
{
    public interface IUsuario
    {
        int UsuarioId { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        NivelUsuario NivelUsuario { get; set; }
        void Assign(Usuario usuario);
    }
}