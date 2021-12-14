using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Requests;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApiContext _context;
        public PedidoController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
           return  await _context.Pedidos.Include(x => x.Itens).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido([FromBody] CriaPedido request)
        {
            try
            {
                var pedidoService = new PedidoService(_context);
                var pedido = await pedidoService.AdicionarPedido(request);

                return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/itens")]
        public async Task<ActionResult<PedidoItem>>PostPedidoItem([FromBody] AdicionarItem request)
        {
            try
            {
                var pedidoService = new PedidoService(_context);
                var itemPedido = await pedidoService.AdicionarItem(request);

                return CreatedAtAction("GetPedido", new { id = itemPedido.Id }, itemPedido);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido([FromBody] AtualizarItem requestItem)
        {
            try
            {
                var pedidoService = new PedidoService(_context);
                var pedido = await pedidoService.AtualizarPedidoItem(requestItem);

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
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var pedidoService = new PedidoService(_context);
            var pedido = await pedidoService.DeletarPedido(id);

            return NoContent();
        }

        [HttpDelete("{id}/itens")]
        public async Task<IActionResult> DeletePedidoItem(Guid id)
        {
            var pedidoService = new PedidoService(_context);
            var pedido = await pedidoService.DeletarPedidoItem(id);

            return NoContent();
        }
    }
}
