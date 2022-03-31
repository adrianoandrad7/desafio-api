using System;
using Domain.Enums;

namespace Commands.Requests
{
    public class AtualizarPedido
    {
        public Guid IdPedido { get; set; }
        public PedidoStatus Status { get; set; }
    }
}
