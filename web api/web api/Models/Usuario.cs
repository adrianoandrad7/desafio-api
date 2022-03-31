using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class Usuario
    {

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        public Usuario(Guid id, string nome, string email, DateTime dataNascimento, string cpf, DateTime dataCadastro, bool ativo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            CPF = cpf;
            DataCadastro = dataCadastro;
            Ativo = ativo;
        }

    }
}
