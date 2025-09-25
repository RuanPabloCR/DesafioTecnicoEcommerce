using Microsoft.EntityFrameworkCore;
using MSEstoque.Domain.Models;

namespace MSEstoque.Infrastructure.Data
{
    public class EstoqueDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
