using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("EMPRESAS");

            builder.HasKey(p => p.EmpresaId);

            builder.Property(p => p.EmpresaId)
                .UseIdentityColumn();

            builder.Property(p => p.Nome)
                .HasMaxLength(100).
                IsRequired();

            builder.Property(p => p.CpfCnpj)
                .HasMaxLength(14)
                .IsRequired();

            builder.HasData(new Empresa
            {
                EmpresaId = 1,
                CpfCnpj = "12345678909",
                Nome = "DivPay"
            });
        }
    }
}