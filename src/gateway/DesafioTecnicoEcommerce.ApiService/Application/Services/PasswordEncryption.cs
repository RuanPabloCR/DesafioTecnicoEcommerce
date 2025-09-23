using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Services
{
    public class PasswordEncryption
    {
        private readonly int _workfactor;
        public PasswordEncryption(int workfactor = 11)
        {
            _workfactor = workfactor;
        }
        public Password Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be whitespace", nameof(password));
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, _workfactor);
            return new Password(hashedPassword);
        }
        public bool Verify(Password password, Password other)
        {
            if (string.IsNullOrEmpty(password.Value) || string.IsNullOrWhiteSpace(password.Value))
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password.Value, other.Value);
        }
    }
}
