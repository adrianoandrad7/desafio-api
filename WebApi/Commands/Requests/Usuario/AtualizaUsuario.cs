using System;

namespace Commands.Requests
{
    public class AtualizaUsuario
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
