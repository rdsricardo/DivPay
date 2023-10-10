using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTES");

            builder.HasKey(p => p.ClienteId);

            builder.Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CpfCnpj)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(p => p.RG)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.HasOne(p => p.Usuario)
                .WithOne()
                .HasForeignKey("Cliente", "UsuarioId")
                .IsRequired(false);

            //builder.HasMany(p => p.Enderecos)
            //    .WithOne(p => p.Cliente);

            //builder.HasMany(p => p.Telefones)
            //    .WithOne(p => p.Cliente);

            //builder.HasMany(p => p.Emails)
            //    .WithOne(p => p.Cliente);

            //builder.HasMany(p => p.Dividas)
            //    .WithOne(p => p.Cliente);
        }
    }
}