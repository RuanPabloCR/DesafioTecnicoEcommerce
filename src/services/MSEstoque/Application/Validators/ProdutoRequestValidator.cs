using FluentValidation;
using MSEstoque.Application.DTOs;

namespace MSEstoque.Application.Validators
{
    public class ProdutoRequestValidator : AbstractValidator<ProdutoRequest>
    {
        public ProdutoRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(70).WithMessage("O nome do produto deve ter no máximo 70 caracteres.");
            RuleFor(x => x.Preco).GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
            RuleFor(x => x.Quantidade).GreaterThanOrEqualTo(0).WithMessage("A quantidade do produto não pode ser negativa.");
            RuleFor(x => x.ProdutoDescricao).NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .MaximumLength(200).WithMessage("A descrição do produto deve ter no máximo 200 caracteres.");
        }
    }
}
