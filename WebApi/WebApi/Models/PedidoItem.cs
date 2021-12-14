using System;

namespace WebApi.Models
{
    public class PedidoItem
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Pedido Pedido { get; private set; }
        public Guid ProdutoId {get; private set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public double Valor { get; private set; }

        public PedidoItem(Pedido pedido, Produto produto, int quantidade)
        {
            Id = Guid.NewGuid();
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            Valor = produto.Valor * quantidade;
        }
        protected PedidoItem()
        {

        }
        public void InformarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }
}
