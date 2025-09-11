using Microsoft.EntityFrameworkCore;
using MSEstoque.Infrastructure.Data;

namespace MSEstoque.Infrastructure.Repositories
{
    public class EstoqueRepository
    {
        private readonly AppDbContext AppDbContext;
        public EstoqueRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public async Task<int> ObterQuantidadePorProdutoId(Guid produtoId)
        {
            var estoque = await AppDbContext.Produtos
                .FirstOrDefaultAsync(e => e.Id == produtoId);
            return estoque?.Quantidade ?? 0;
        }
        public async Task<bool> AtualizarEstoque(Guid produtoId, int quantidade)
        {
            using (var transaction = await AppDbContext.Database.BeginTransactionAsync())
            {
                var estoque = await AppDbContext.Produtos
                    .FirstOrDefaultAsync(e => e.Id == produtoId);
                if (estoque == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
                estoque.Quantidade += quantidade;
                if (estoque.Quantidade < 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                estoque.Available = estoque.Quantidade > 0;

                AppDbContext.Produtos.Update(estoque);
                await AppDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
        }
    }
}
