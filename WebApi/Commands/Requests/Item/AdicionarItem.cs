﻿using System;

namespace Commands.Requests
{ 
    public class AdicionarItem
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }

    }
}
