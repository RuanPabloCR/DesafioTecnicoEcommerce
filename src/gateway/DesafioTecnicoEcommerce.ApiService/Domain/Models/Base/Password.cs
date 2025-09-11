namespace DesafioTecnicoEcommerce.ApiGateway.Models.Base
{
    public class Password
    {
        public string Value { get; private set; }
        public Password(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Password cannot be null or whiteSpace", nameof(value));
            }
            Value = value;
        }
    }
}