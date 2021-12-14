using System;

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
        protected Produto()
        {

        }
        public void InformarDescricao(string descricao)
        {
            Descricao = descricao;
        }
        public void InformarValor(double valor)
        {
            Valor = valor;
        }
        public void InformarEstoque(int estoque)
        {
            QuantidadeEstoque = estoque;
        }
    }
}
