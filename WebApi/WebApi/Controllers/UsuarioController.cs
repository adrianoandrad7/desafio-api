using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Requests;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiContext _context;
        public UsuarioController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] CriaUsuario request)
        {
            Usuario usuario = new Usuario();
            usuario.informarNome(request.Nome);
            usuario.informarEmail(request.Email);
            usuario.informarDataNascimento(request.DataNascimento);
            usuario.informarCPF(request.CPF);
            usuario.informarDataNascimento(request.DataNascimento);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            if (!ValidaEmail(usuario.Email))
                return BadRequest("Email Inválido");
            else if (!ValidaNome(usuario.Nome))
                return BadRequest("Nome Inválido");
            else if (!ValidaCPF(usuario.CPF))
                return BadRequest("CPF Inválido");
            else if (usuario.DataNascimento == null)
                return BadRequest("Data Inválida");

            return CreatedAtAction("GetUsuarios", new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, [FromBody] AtualizaUsuario request)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario?.Id != id)
                    return BadRequest();

                usuario.informarNome(request.Nome);
                usuario.informarCPF(request.CPF);
                usuario.informarDataNascimento(request.DataNascimento);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
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
        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
