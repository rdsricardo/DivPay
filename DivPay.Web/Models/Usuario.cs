namespace DivPay.Web.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string RG { get; set; }
        public bool Ativo { get; set; }
        public NivelUsuario NivelUsuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
