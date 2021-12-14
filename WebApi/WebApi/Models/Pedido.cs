using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Enums;

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
            TotalPedido = Itens.Sum(_itens => _itens.Valor);
            
        }
        protected Pedido()
        {

        }
    }
}