namespace MSEstoque.Domain.Models.Base
{
    public class Email
    {
        private String EmailAddress { get; }
        public Email(String emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email nao pode ser nulo ou vazio.", nameof(emailAddress));
            }
            EmailAddress = emailAddress;
        }

    }
}
