using DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces;
using DesafioTecnicoEcommerce.ApiGateway.Domain.Models;
using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.Data;
using DesafioTecnicoEcommerce.ApiGateway.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GatewayDbContext _gatewayDbContext;
        public UserRepository(GatewayDbContext gatewayDbContext)
        {
            _gatewayDbContext = gatewayDbContext;
        }
        public async Task AddClienteAsync(Cliente cliente)
        {
            await _gatewayDbContext.Clientes.AddAsync(cliente);
            await _gatewayDbContext.SaveChangesAsync();
        }
        public async Task AddEmpresaAsync(Empresa empresa)
        {
            await _gatewayDbContext.Empresas.AddAsync(empresa);
            await _gatewayDbContext.SaveChangesAsync();
        }
        public async Task<Cliente?> GetClienteByEmailAsync(Email email)
        {
            return await _gatewayDbContext.Clientes.FindAsync(email);
        }
    }
}
