using Microsoft.EntityFrameworkCore;
using MsVendas.Domain.Models;

namespace MsVendas.Infrastructure.Data
{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ClienteId).IsRequired();
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();

                // Configuração para ignorar a navegação de Produtos
                // já que não vamos armazenar os produtos no MSVendas
                entity.Ignore(p => p.Produtos);
            });
        }
    }
}
