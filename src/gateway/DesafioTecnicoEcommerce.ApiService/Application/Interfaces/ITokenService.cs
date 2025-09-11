using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT;
using DesafioTecnicoEcommerce.ApiGateway.Models;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
