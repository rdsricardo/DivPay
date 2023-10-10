using DivPay.DAL.Models;

namespace DivPay.DAL.Extensions
{
    public static class StatusPagamentoExtension
    {
        public static int ToInt(this StatusPagamento statusPagamento)
        {
            return (int)statusPagamento;
        }

        public static StatusPagamento ToStatusPagamnento(this int id)
        {
            return (StatusPagamento)id;
        }
    }
}