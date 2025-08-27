namespace MSEstoque.Domain.Models.Base
{
    public abstract class User
    {
        public Nome Name { get; set; }
        public Guid Id { get; set; }
        protected User(Nome name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Nome nao pode ser nulo.");
            }
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
