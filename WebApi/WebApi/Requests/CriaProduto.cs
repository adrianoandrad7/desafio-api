using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests.Produto
{
    public class CriaProduto
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
