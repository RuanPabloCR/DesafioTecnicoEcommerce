namespace DesafioTecnicoEcommerce.ApiGateway.Models.Base
{
    public class CNPJ
    {
        private string Cnpj { get; }
        public CNPJ(string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj) || (cnpj.Length != 14))
            {
                throw new ArgumentException("CNPJ deve ter 14 caracteres.", nameof(cnpj));
            }
            Cnpj = cnpj;
        }
    }
}
