using System;


namespace WebApi.Requests
{
    public class AtualizarItem
    {
        public Guid IdPedido { get; set; }
        public Guid IdItem { get; set; }
        public int Quantidade { get; set; }

    }
}
