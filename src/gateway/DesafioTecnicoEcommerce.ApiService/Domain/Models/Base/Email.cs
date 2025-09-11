namespace DesafioTecnicoEcommerce.ApiGateway.Models.Base
{
    public class Email
    {
        public string EmailAddress { get; private set; }
        public Email(string emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email nao pode ser nulo ou vazio.", nameof(emailAddress));
            }
            EmailAddress = emailAddress;
        }

    }
}
