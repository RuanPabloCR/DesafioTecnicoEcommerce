using MSEstoque.Domain.Models.Base;

namespace MSEstoque.Domain.Models
{
    public class Vendedor : User
    {
        public CNPJ Cnpj { get; set; }
        public Telefone Phone { get; set; }
        public Vendedor(Nome name, CNPJ cnpj, Telefone phone) : base(name)
        {
            Cnpj = cnpj;
            Phone = phone;
        }
    }
}
