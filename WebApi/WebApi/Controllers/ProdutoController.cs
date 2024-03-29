﻿using Commands.Requests;
using Commands.Services;
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
    public class ProdutoController : ControllerBase
    {
        private readonly ApiContext _context;

        private readonly ProdutoService _produtoService;
        public ProdutoController(ApiContext context,ProdutoService produtoService )
        {
            _context = context;
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostPoduto([FromBody] CriaProduto request)
        {
            try
            {
                var produto = await _produtoService.Adicionar(request);

                return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
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
        public async Task<IActionResult> PutProduto(Guid id, [FromBody] AtualizaProduto request)
        {
            try
            {
                var pedido = await _produtoService.Atualizar(id,request);
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
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _produtoService.Deletar(id);

            return NoContent();
        }
    }
}
