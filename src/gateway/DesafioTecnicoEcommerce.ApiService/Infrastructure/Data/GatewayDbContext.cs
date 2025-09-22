using DesafioTecnicoEcommerce.ApiGateway.Domain.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioTecnicoEcommerce.ApiGateway.Infrastructure.Data
{
    public class GatewayDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var nomeConverter = new ValueConverter<Nome, string>(
                v => v.Name,
                v => new Nome(v));
            var emailConverter = new ValueConverter<Email, string>(
                v => v.EmailAddress,
                v => new Email(v));
            var passwordConverter = new ValueConverter<Password, string>(
                v => v.Value,
                v => new Password(v));
            var cpfConverter = new ValueConverter<CPF, string>(
                v => v.Cpf,
                v => new CPF(v));
            var cnpjConverter = new ValueConverter<CNPJ, string>(
                v => v.Cnpj,
                v => new CNPJ(v));
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Name).HasConversion(nomeConverter);
                entity.Property(e => e.Email).HasConversion(emailConverter);
                entity.Property(e => e.Senha).HasConversion(passwordConverter);
                entity.Property(e => e.CPF).HasConversion(cpfConverter);

                entity.HasIndex(e => e.Email).IsUnique();
            });
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.Property(e => e.Name).HasConversion(nomeConverter);
                entity.Property(e => e.Email).HasConversion(emailConverter);
                entity.Property(e => e.Senha).HasConversion(passwordConverter);
                entity.Property(e => e.CNPJ).HasConversion(cnpjConverter);

                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
