namespace MSEstoque.Domain.Models
{
    public class Carrinho
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        private readonly List<Produto> _produtos = new();
        public DateTime Atualizacao { get; private set; }
        
        public Carrinho(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            Atualizacao = DateTime.UtcNow;
        }

        public void AddProduto(Produto produto)
        {
            var existingProduct = _produtos.FirstOrDefault(p => p.Id == produto.Id);
            if(existingProduct != null)
            {
                existingProduct.Quantidade += produto.Quantidade;
            }
            else
            {
                _produtos.Add(produto);
            }
            Atualizacao = DateTime.UtcNow;
        }
        public void RemoveProduto(Guid produtoId)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto != null)
            {
                _produtos.Remove(produto);
                Atualizacao = DateTime.UtcNow;
            }
        }
        public void ClearCarrinho()
        {
            _produtos.Clear();
            Atualizacao = DateTime.UtcNow;
        }
    }
}
