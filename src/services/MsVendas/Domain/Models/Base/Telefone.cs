namespace MsVendas.Domain.Models.Base
{
    public class Telefone
    {
        private String _numero;
        public Telefone(String numero)
        {
            if(string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Número de telefone não pode ser vazio.", nameof(numero));
            _numero = numero;
        }
    }
}
