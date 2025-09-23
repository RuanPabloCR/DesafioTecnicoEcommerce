using DesafioTecnicoEcommerce.ApiGateway.Application.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Validators
{
    public class ClienteRequestValidator : AbstractValidator<ClienteRequest>
    {
        public ClienteRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email não é válido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O CPF é obrigatório.");


        }

    }
    }
