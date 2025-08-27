namespace MSEstoque.Domain.Models.Base
{
    public class Descricao
    {
        public String ProductDescription { get; private set; }
        public Descricao(string productDescription)
        {
            if (productDescription.Length < 10 || productDescription.Length > 300)
                throw new ArgumentException("Descricao deve estar entre 10 a 300 caracteres.");
            ProductDescription = productDescription;
        }
    }
}
