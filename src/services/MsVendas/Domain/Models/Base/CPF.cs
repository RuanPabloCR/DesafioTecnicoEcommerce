namespace MsVendas.Domain.Models.Base
{
    public class CPF
    {
        private String Cpf { get;}
        public CPF(String cpf)
        {
          if(string.IsNullOrEmpty(cpf) || (cpf.Length != 11))
          {
              throw new ArgumentException("CPF deve ter 11 caracteres.", nameof(cpf));
          }
          Cpf = cpf;
        }
    }
}
