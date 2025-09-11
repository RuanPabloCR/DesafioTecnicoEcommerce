using DesafioTecnicoEcommerce.ApiGateway.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnicoEcommerce.ApiGateway.Infrastructure.Data
{
    public class GatewayDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {
        }
        protected void onModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
