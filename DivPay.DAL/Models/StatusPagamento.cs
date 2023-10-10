using System.ComponentModel.DataAnnotations;

namespace DivPay.DAL.Models
{
    public enum StatusPagamento
    {
        Aberto = 1,
        Pendente = 2,
        Pago = 3,
        Cancelado = 4,
    }
}