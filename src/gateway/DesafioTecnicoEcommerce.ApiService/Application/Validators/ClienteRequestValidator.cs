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
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Must(BeAValidCPF).WithMessage("O CPF informado não é válido.");
        }

        private bool BeAValidCPF(string cpf)
        {
            // Remove non-numeric characters
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            // CPF must have 11 digits
            if (cpf.Length != 11)
                return false;

            // Check if all digits are the same
            bool allDigitsEqual = true;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    allDigitsEqual = false;
                    break;
                }
            }
            if (allDigitsEqual)
                return false;

            // Calculate first verification digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);

            int remainder = sum % 11;
            int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != verificationDigit1)
                return false;

            // Calculate second verification digit
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == verificationDigit2;
        }
    }
}
