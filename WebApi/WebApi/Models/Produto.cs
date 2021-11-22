using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public bool Ativo { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public Produto(string descricao, double valor, int quantidadeNoEstoque)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
            QuantidadeEstoque = quantidadeNoEstoque;
        }
        public Produto()
        {
            Id = Guid.NewGuid();
            Ativo = true;
        }
        public void informarDescricao(string descricao)
        {
            Descricao = descricao;
        }
        public void informarValor(double valor)
        {
            Valor = valor;
        }
        public void informarEstoque(int estoque)
        {
            QuantidadeEstoque = estoque;
        }
    }
}
