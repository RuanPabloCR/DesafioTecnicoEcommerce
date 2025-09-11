using FluentValidation;
using MSEstoque.Application.DTOs;
using MSEstoque.Application.Interfaces;
using MSEstoque.Application.Validators;
using MSEstoque.Domain.Models;
using MSEstoque.Domain.Models.Base;

namespace MSEstoque.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ProdutoRequestValidator _validator = new ProdutoRequestValidator();
        private readonly int _pageSize = 10;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<ProdutoResponse> Execute(ProdutoRequest produtoRequest)
        {
            var validationResult = _validator.Validate(produtoRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Produto produto = new Produto(
                Guid.NewGuid(),
                new Nome(produtoRequest.Name),
                produtoRequest.Preco,
                produtoRequest.Quantidade,
                new Descricao(produtoRequest.ProdutoDescricao),
                produtoRequest.Quantidade > 0
            );

            await _produtoRepository.RegisterProduto(produto);
            return new ProdutoResponse(produto.Name.Name, produto.Preco,
                produto.Quantidade, produto.ProdutoDescricao.ProductDescription, produto.Id);
        }
        public async Task<IEnumerable<ProdutoResponse>> GetProdutosAsync(int? page)
        {
            int pageNumber = page ?? 1;
            var produtos = await _produtoRepository.GetProdutos(pageNumber, _pageSize);
            return produtos.Select(p => new ProdutoResponse(p.Name.Name, p.Preco, p.Quantidade, p.ProdutoDescricao.ProductDescription, p.Id));
        }
    }
}
