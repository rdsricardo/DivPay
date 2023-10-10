using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("TELEFONES");

            builder.HasKey(p => p.TelefoneId);

            builder.Property(p => p.DDD)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.Numero)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Telefones)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}