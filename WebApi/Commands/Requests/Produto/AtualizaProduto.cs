using System;


namespace Commands.Requests
{
    public class AtualizaProduto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
