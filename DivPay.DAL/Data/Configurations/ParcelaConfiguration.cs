using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("PARCELAS");

            builder.HasKey(p => p.ParcelaId);

            builder.HasOne(p => p.Divida)
                .WithMany(d => d.Parcelas)
                .HasForeignKey(p => p.DividaId);

            builder.Ignore(p => p.Divida);

            builder.Property(p => p.Numero)
                .IsRequired();

            builder.Property(p => p.ValorOriginal)
                .IsRequired();

            builder.Property(p => p.Vencimento)
                .IsRequired();

            builder.Property(p => p.Juros)
                .HasDefaultValue(0);

            builder.Property(p => p.Multa)
                .HasDefaultValue(0);

            builder.Property(p => p.Taxa)
                .HasDefaultValue(0);

            builder.Property(p => p.Acrescimo)
                .HasDefaultValue(0);

            builder.Property(p => p.OutrosEncargos)
                .HasDefaultValue(0);
        }
    }
}