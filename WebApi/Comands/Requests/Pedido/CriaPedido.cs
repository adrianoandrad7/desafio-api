using Domain.Enums;
using System;


namespace Comands.Requests
{
    public class CriaPedido
    {
        public Guid IdUsuario { get; set; }
        public PedidoStatus Status { get; set; }

    }
}
