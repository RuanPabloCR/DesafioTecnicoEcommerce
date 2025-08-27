using MSEstoque.Domain.Models.Base;

namespace MSEstoque.Domain.Models
{
    public class Cliente : User
    {
        public CPF Cpf { get; set; }
        public Telefone Phone { get; set; }
        public Cliente(Nome name, CPF cpf, Telefone phone) : base(name)
        {
            Cpf = cpf;
            Phone = phone;
        }
    }
}
