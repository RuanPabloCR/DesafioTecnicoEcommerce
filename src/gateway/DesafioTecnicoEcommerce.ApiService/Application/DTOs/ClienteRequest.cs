using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.DTOs
{
    public class ClienteRequest
    {
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
    }
}
