using System;

namespace Commands.Requests
{
    public class CriaUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}
