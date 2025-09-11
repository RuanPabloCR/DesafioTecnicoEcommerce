using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.DTOs
{
    public class ClienteResponseWithToken
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }

        public ClienteResponseWithToken(string nome, string email, string token)
        {
            Nome = nome;
            Email = email;
            Token = token;
        }
    }
}
