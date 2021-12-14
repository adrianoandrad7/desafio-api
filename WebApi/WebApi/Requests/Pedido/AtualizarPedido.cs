using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Enums;
using WebApi.Requests.Produto;

namespace WebApi.Requests
{
    public class AtualizarPedido
    {
        public Guid IdPedido { get; set; }
        public PedidoStatus Status { get; set; }
    }
}
