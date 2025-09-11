using Microsoft.EntityFrameworkCore;
using MSEstoque.Application.DTOs;
using MSEstoque.Application.Interfaces;
using MSEstoque.Domain.Models;
using MSEstoque.Infrastructure.Data;

namespace MSEstoque.Infrastructure.Repositories
{
    public class ProdutosRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutosRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task RegisterProduto(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }
        public async Task<IList<Produto>> GetProdutos(int pageNumber, int pageSize)
        {
            return await _context.Produtos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
