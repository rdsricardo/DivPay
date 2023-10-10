using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DivPay.Web.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "Usuário requerido")]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
