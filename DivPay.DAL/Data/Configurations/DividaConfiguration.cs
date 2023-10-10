using DivPay.DAL.Extensions;
using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class DividaConfiguration : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.ToTable("DIVIDAS");

            builder.HasKey(p => p.DividaId);

            builder.Property(p => p.NumeroContrato)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(p => p.NomeCredor)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CpfCnpjCredor)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(p => p.TelefoneContato)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.EmailContato)
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(p => p.LinkWhatsappContato)
                .HasMaxLength(500)
                .IsRequired(false); 

            builder.Property(p => p.LinkSiteContato)
                .HasMaxLength(500)
                .IsRequired(false); 

            builder.Property(p => p.Descricao)
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(p => p.TotalOriginal)
                .IsRequired();

            builder.Property(p => p.UrlNotificacao)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.Identificador)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.StatusPagamento)
                .HasConversion<int>(
                    tipo => tipo.ToInt(),
                    id => id.ToStatusPagamnento()
                )
                .HasDefaultValue(StatusPagamento.Aberto);

            builder.Property(p => p.Pago)
                .HasDefaultValue(false);

            builder.Property(p => p.Token)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.UrlToken)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Dividas)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}