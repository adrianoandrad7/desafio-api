using Domain.Enums;
using System;


namespace Commands.Requests
{
    public class CriaPedido
    {
        public Guid IdUsuario { get; set; }
        public PedidoStatus Status { get; set; }

    }
}
