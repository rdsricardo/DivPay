using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivPay.DAL.Data.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECOS");

            builder.HasKey(p => p.EnderecoId);

            builder.Property(p => p.Logradouro)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Numero)
                .HasColumnType("int");

            builder.Property(p => p.Bairro)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.Complemento)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.CEP)
                .HasMaxLength(8)
                .IsRequired(false);

            builder.Property(p => p.Cidade)
                .HasMaxLength(60)
                .IsRequired(false);

            builder.Property(p => p.UF)
                .HasMaxLength(2)
                .IsRequired(false);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}
