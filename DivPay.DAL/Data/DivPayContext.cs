using DivPay.DAL.Data.Configurations;
using DivPay.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivPay.DAL.Data
{
    public class DivPayContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Divida> Dividas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        public DivPayContext(DbContextOptions<DivPayContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration<Cliente>(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration<Endereco>(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration<Telefone>(new TelefoneConfiguration());
            modelBuilder.ApplyConfiguration<Email>(new EmailConfiguration());
            modelBuilder.ApplyConfiguration<Divida>(new DividaConfiguration());
            modelBuilder.ApplyConfiguration<Parcela>(new ParcelaConfiguration());
            modelBuilder.ApplyConfiguration<Empresa>(new EmpresaConfiguration());

            //modelBuilder.Entity<Usuario>(model =>
            //{
            //    model.ToTable("USUARIOS");

            //});
        }
    }
}
