using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{
    public class AtualizaProduto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
