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
    public class PedidoController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly PedidoService _pedidoService;
        public PedidoController(ApiContext context,PedidoService pedidoService)
        {
            _context = context;
            _pedidoService = pedidoService;
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
                var pedidoService = _pedidoService;
                var pedido = await pedidoService.AdicionarPedido(request);

                return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/itens")]
        public async Task<ActionResult<Pedido>>PostPedidoItem([FromBody] AdicionarItem request)
        {
            try
            {
                var pedidoService = _pedidoService;
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
                var pedidoService = _pedidoService;
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
            var pedidoService = _pedidoService;
            var pedido = await pedidoService.DeletarPedido(id);

            return NoContent();
        }

        [HttpDelete("{id}/itens")]
        public async Task<IActionResult> DeletePedidoItem([FromBody] DeletaItem request)
        {
            var pedidoService = _pedidoService;
            var pedido = await pedidoService.DeletarPedidoItem(request.IdPedido,request.IdItem);

            return NoContent();
        }
    }
}
