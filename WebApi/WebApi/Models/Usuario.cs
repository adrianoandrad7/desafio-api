using System;

namespace WebApi.Models
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }
        public Usuario(Guid id,string email, string nome, DateTime dataNascimento, string cpf)
        {
            Id = id;
            Email = email;
            Nome = nome;
            DataNascimento = dataNascimento;
            DataCadastro = DateTime.Now;
            CPF = cpf;
            Ativo = true;
        }

        protected Usuario()
        {

        }


      
    }
}
