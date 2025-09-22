using DesafioTecnicoEcommerce.ApiGateway.Models;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Domain.Models
{
    public class Empresa : User
    {
        public CNPJ CNPJ { get; set; }
        public Empresa() : base() { }
        public Empresa(Nome nome, Email email, Password password, CNPJ cnpj) : base(nome, email, password)
        {
            CNPJ = cnpj;
        }
    }
}
