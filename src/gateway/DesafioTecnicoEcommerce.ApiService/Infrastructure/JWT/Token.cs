namespace DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT
{
    public class Token
    {
        public string AccessToken { get; set; }
        public Token(string acessToken)
        {
            AccessToken = acessToken;
        }
    }
}
