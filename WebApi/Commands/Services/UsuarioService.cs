using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data;
using Domain;
using Commands.Requests;

namespace Commands.Services
{
    public class UsuarioService
    {
        private readonly Repository<Usuario> _repository;
        private readonly ApiContext _context;
        public UsuarioService(ApiContext context, Repository<Usuario> repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<Usuario> Adicionar(CriaUsuario request)
        {
            if (!ValidaEmail(request.Email))
                throw new InvalidOperationException("Email Inválido");
            else if (!ValidaNome(request.Nome))
                throw new InvalidOperationException("Nome Inválido");
            else if (!ValidaCPF(request.CPF))
                throw new InvalidOperationException("CPF Inválido");
            else if (request.DataNascimento == null)
                throw new InvalidOperationException("Data Inválido");

            Usuario usuario = new Usuario(request.Email, request.Nome, request.DataNascimento, request.CPF);

            _repository.Add(usuario);
            await _repository.SaveChangesAsync();

            return usuario;
        }
        public async Task<Usuario> Atualizar(Guid id, AtualizaUsuario request)
        {
            if (!ValidaNome(request.Nome))
                throw new InvalidOperationException("Nome Inválido");
            else if (!ValidaCPF(request.CPF))
                throw new InvalidOperationException("CPF Inválido");
            else if (request.DataNascimento == null)
                throw new InvalidOperationException("Data Inválido"); 

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario?.Id != id)
                throw new InvalidOperationException("Usuário não encontrado");

            usuario.InformarNome(request.Nome);
            usuario.InformarCPF(request.CPF);
            usuario.InformarDataNascimento(request.DataNascimento);

            await _repository.SaveChangesAsync();

            return usuario;
        }
        public async Task<Usuario> Deletar(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário Inválido");

            await _repository.DeleteAsync(usuario);

            await _repository.SaveChangesAsync();

            return usuario;
        }
        private bool ValidaNome(string nome)
        {
            if (nome != null && nome.Length <= 60)
                return true;
            else
                return false;
        }
        private bool ValidaEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (email != null && rg.IsMatch(email))
                return true;
            else
                return false;
        }
        public static bool ValidaCPF(string cpf)
        {
            return (IsCpf(cpf));
        }
        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
