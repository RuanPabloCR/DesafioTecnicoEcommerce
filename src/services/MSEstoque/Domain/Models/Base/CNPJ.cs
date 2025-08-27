namespace MSEstoque.Domain.Models.Base
{
    public class CNPJ
    {
        private String Cnpj { get; }
        public CNPJ(String cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || (cnpj.Length != 14))
            {
                throw new ArgumentException("CNPJ deve ter 11 caracteres.", nameof(cnpj));
            }
            Cnpj = cnpj;
        }
    }
}
