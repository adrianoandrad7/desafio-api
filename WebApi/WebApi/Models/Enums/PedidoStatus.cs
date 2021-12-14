using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Enums
{
    public enum PedidoStatus : int
    {
        Criado = 1,
        Cancelado = 2,
        Concluido = 3,
    }
}
