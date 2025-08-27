using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnicoEcommerce.ApiGateway.Data
{
    public class GatewayDbContext : IdentityDbContext
    {
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options)
        {
        }
    }
}
