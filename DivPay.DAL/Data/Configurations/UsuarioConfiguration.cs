using DivPay.DAL.Extensions;
using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIOS");

            builder.HasKey(p => p.UsuarioId);

            builder.Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.CpfCnpj)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(p => p.RG)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.Login)
                .HasMaxLength(20);

            builder.Property(p => p.Password)
                .HasMaxLength(20);
                //.HasColumnType("nvarchar(20)");

            builder.Property(p => p.NivelUsuario)
                .HasColumnType("int")
                .IsRequired()
                .HasConversion<int>(
                    tipo => tipo.ToInt(),
                    id => id.ToNivelUsuario()
                );

            builder.HasData(new Usuario
            {
                UsuarioId = 1,
                Nome = "Administrador",
                Email = "admin@divpay.com.br",
                CpfCnpj = "",
                RG = "",
                Ativo = true,
                Login = "admin",
                Password = "123456",
                NivelUsuario = NivelUsuario.Administrador
            });
        }
    }
}