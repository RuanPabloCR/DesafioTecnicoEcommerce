using DesafioTecnicoEcommerce.ApiGateway.Domain.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddClienteAsync(Cliente user);
        Task AddEmpresaAsync(Empresa empresa);
        Task<Cliente?> GetClienteByEmailAsync(Email email);
    }
}
