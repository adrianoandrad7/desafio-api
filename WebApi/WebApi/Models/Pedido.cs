using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Enums;
using WebApi.Models;

namespace WebApi.Models
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public Guid IdUsuario { get; private set; }
        public PedidoStatus Status { get; private set; }
        public double TotalPedido { get; private set; }

        private readonly List<PedidoItem> _itens = new List<PedidoItem>();
        public IReadOnlyCollection<PedidoItem> Itens => _itens;
        public Pedido(Guid usuarioId)
        {
            Id = Guid.NewGuid();
            IdUsuario = usuarioId;
            Status = PedidoStatus.Criado;
            TotalPedido = 0;
        }
        protected Pedido()
        {

        }
        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (_itens.Any(e => e.ProdutoId == produto.Id))
                throw new InvalidOperationException("Produto já está no pedido");

            _itens.Add(new PedidoItem(this, produto, quantidade));

            this.TotalPedidoAtualizado();

        }
        public void AtualizaQuantidade(Guid id, int quantidade)
        {
            var item = _itens.FirstOrDefault(e => e.Id == id);

            item.InformarQuantidade(quantidade,item.ValorProduto);

            this.TotalPedidoAtualizado();
        }

        public void Removertem(Guid id)
        {
            var item = _itens.FirstOrDefault(e => e.Id == id);

            if(item == null)
                throw new InvalidOperationException("Item inválido");

            var pedido = _itens.Remove(_itens.FirstOrDefault(e => e.Id == id));

            this.TotalPedidoAtualizado();
        }

        public void TotalPedidoAtualizado()
        {
            TotalPedido = Itens.Sum(_itens => _itens.ValorTotalItem);
        }
    }
}
