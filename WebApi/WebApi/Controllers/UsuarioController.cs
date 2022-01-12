﻿using Comands.Services;
using Commands.Requests;
using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


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
            var usuario =  await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] CriaUsuario request)
        {
            try
            {
                var usuarioService = new UsuarioService(_context);
                var usuario = await usuarioService.Adicionar(request);

                return CreatedAtAction("GetUsuarios", new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Problem("Erro Inesperado");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, [FromBody] AtualizaUsuario request)
        {
            try
            {
                var usuarioService = new UsuarioService(_context);
                var usuario = await usuarioService.Atualizar(id,request);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Problem("Erro Inesperado");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuarioService = new UsuarioService(_context);
            var usuario = await usuarioService.Deletar(id);
            
            return NoContent();
        }
    }
}
