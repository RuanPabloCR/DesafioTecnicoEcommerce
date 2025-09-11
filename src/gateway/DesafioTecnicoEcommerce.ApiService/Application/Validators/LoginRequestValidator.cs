using DesafioTecnicoEcommerce.ApiGateway.Application.DTOs;
using FluentValidation;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<ClienteLoginRequest>
    {
        public LoginRequestValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email não é válido.");
                
            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}
