using System;

namespace WebApi.Models
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
        public Usuario(string email, string nome, DateTime dataNascimento, string cpf)
        {
            Id = Guid.NewGuid();
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
        public void InformarNome(string nome)
        {
            Nome = nome; 
        }
        public void InformarCPF(string cpf)
        {
            CPF = cpf;
        }
        public void InformarDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }
        public void InformarEmail(string email)
        {
            Email = email;
        }

    }
}
