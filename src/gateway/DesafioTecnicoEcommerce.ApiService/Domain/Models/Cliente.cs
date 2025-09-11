using DesafioTecnicoEcommerce.ApiGateway.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Domain.Models
{
    public class Cliente : User
    {
        public CPF CPF { get; set; }
        public Cliente(Nome nome, Email email, Password password, CPF cpf) : base(nome, email, password)
        {
            CPF = cpf;
        }

    }
}
