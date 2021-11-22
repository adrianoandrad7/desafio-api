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
        public Usuario()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            Ativo = true;
        }
        public void informarNome(string nome)
        {
            Nome = nome; 
        }
        public void informarCPF(string cpf)
        {
            CPF = cpf;
        }
        public void informarDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }
        public void informarEmail(string email)
        {
            Email = email;
        }

    }
}
