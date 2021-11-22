using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Requests.Produto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApiContext _context;
        public ProdutoController(ApiContext context)
        {
            _context = context;
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
            Produto produto = new Produto();
            produto.informarDescricao(request.Descricao);
            produto.informarValor(request.Valor);
            produto.informarEstoque(request.QuantidadeEstoque);

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            if (!ValidaDescricao(produto.Descricao))
                return BadRequest("Descrição Inválida");
            if (!ValidaValor(produto.Valor))
                return BadRequest("Valor Inválido");
            else if (!ValidaEstoque(produto.QuantidadeEstoque))
                return BadRequest("Quantidade estoque inválida");

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, [FromBody] CriaProduto request)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);

                if (produto?.Id != id)
                    return BadRequest();

                produto.informarDescricao(request.Descricao);
                produto.informarValor(request.Valor);
                produto.informarEstoque(request.QuantidadeEstoque);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if(produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ValidaDescricao(string descricao)
        {
            if (descricao != null && descricao.Length <= 200)
                return true;
            else
                return false;
        }
        private bool ValidaValor(double valor)
        {
            if(valor > 0)
                return true;
            else
                return false;
        }
        private bool ValidaEstoque(int estoque)
        {
            if(estoque < 0)
                return false;
            else
                return true;
        }
        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
