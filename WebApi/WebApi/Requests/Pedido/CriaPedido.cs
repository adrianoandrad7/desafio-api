using System;
using WebApi.Models;
using WebApi.Models.Enums;

namespace WebApi.Requests
{
    public class CriaPedido
    {
        public Guid IdUsuario { get; set; }
        public PedidoStatus Status { get; set; }

    }
}
