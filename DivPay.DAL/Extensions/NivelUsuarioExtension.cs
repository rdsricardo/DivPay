using DivPay.DAL.Models;

namespace DivPay.DAL.Extensions
{
    public static class NivelUsuarioExtension
    {
        public static int ToInt(this NivelUsuario nivel)
        {
            return (int)nivel;
        }

        public static NivelUsuario ToNivelUsuario(this int id)
        {
            return (NivelUsuario)id;
        }
    }
}