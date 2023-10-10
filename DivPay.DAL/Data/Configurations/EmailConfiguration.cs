using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivPay.DAL.Data.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("EMAILS");

            builder.HasKey(p => p.EmailId);

            builder.Property(p => p.Endereco)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Emails)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}