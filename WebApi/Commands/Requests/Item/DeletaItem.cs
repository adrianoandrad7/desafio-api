using System;

namespace Commands.Requests
{
    public class DeletaItem
    {
        public Guid IdPedido { get; set; }
        public Guid IdItem { get; set; }
    }
}
