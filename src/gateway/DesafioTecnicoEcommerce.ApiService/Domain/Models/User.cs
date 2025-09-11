using DesafioTecnicoEcommerce.ApiGateway.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace DesafioTecnicoEcommerce.ApiGateway.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String[] Roles { get; set; }
        public Password Senha { get; set; }
        public Nome Name { get; set; }
        public Email Email { get; set; }
        public DateTime DataCriacao { get; set; }

        public User(Nome nome, Email email, Password password)
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }
    }
}
