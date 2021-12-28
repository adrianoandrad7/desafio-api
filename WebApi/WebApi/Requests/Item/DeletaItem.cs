using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests.Item
{
    public class DeletaItem
    {
        public Guid IdPedido { get; set; }
        public Guid IdItem { get; set; }
    }
}
