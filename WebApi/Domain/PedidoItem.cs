using System;

namespace Domain
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
        public Guid ProdutoId {get; private set; }
        public string DescricaoProduto { get; private set; }
        public double ValorProduto { get; private set; }
        public int Quantidade { get; private set; }
        public double ValorTotalItem { get; private set; }

        public PedidoItem(Pedido pedido, Produto produto, int quantidade)
        {
            Id = Guid.NewGuid();
            Pedido = pedido;
            ProdutoId = produto.Id;
            DescricaoProduto = produto.Descricao;
            Quantidade = quantidade;
            ValorProduto = produto.Valor;
            ValorTotalItem = produto.Valor * quantidade;
        }
        protected PedidoItem()
        {

        }
        public void InformarQuantidade(int quantidade,double valor)
        {
            Quantidade = quantidade;
            ValorTotalItem = valor * quantidade;
        }
    }
}
