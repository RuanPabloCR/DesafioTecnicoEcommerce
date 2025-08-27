namespace MSEstoque.Domain.Models.Base
{
    public class Nome
    {
        private string _name { get; }
        public Nome(string name)
        {
            bool isValid = NameValidation(name);
            if (isValid)
            {
                _name = name;
            }
            else
            {
                throw new ArgumentException("Nome invalido.", nameof(name));
            }
        }
        private bool NameValidation(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nome nao pode ser nulo ou espaco em branco.", nameof(name));
            }
            if (name.Length < 3 || name.Length > 50)
            {
                throw new ArgumentException("Nome deve ter entre 3 e 50 caracteres.", nameof(name));
            }
            return true;
        }
    }
}
