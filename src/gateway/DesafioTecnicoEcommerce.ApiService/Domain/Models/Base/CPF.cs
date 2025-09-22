namespace DesafioTecnicoEcommerce.ApiGateway.Models.Base
{
    public class CPF
    {
        public string Cpf { get; set; }
        public CPF(string cpf)
        {
            if (String.IsNullOrEmpty(cpf) || (cpf.Length != 11))
            {
                throw new ArgumentException("CPF deve ter 11 caracteres.", nameof(cpf));
            }
            Cpf = cpf;
        }
    }
}
