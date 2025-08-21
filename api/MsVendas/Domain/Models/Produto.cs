﻿using MsVendas.Domain.Models.Base;

namespace MsVendas.Domain.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Nome Name { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public Produto(Guid id, Nome nome, decimal preco, int quantidade)
        {
            Id = id;
            Name = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
