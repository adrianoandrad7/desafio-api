using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Requests;

namespace WebApi.Services
{
    public class PedidoService
    {
        private readonly ApiContext _context;
        public PedidoService(ApiContext context)
        {
            _context = context;
        }
        public async Task<Pedido> AdicionarPedido(CriaPedido request)
        {
            Pedido pedido = new Pedido(request.IdUsuario);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task<PedidoItem> AdicionarItem(AdicionarItem request)
        {

            if (ProdutoExists(request.ProdutoId))
            {
                throw new InvalidOperationException("Produto já esta no pedido");
            }
       
            var pedido = await _context.Pedidos.FindAsync(request.PedidoId);
            if (pedido?.Id != request.PedidoId)
                throw new InvalidOperationException("Pedido não encontrado");

            var produto = await _context.Produtos.FindAsync(request.ProdutoId);
            if (produto?.Id != request.ProdutoId)
                throw new InvalidOperationException("Produto não encontrado");

            PedidoItem pedidoItem = new PedidoItem(pedido, produto, request.Quantidade);
            _context.PedidoItens.Add(pedidoItem);

            await _context.SaveChangesAsync();

            return pedidoItem;
        }
        public async Task<PedidoItem> AtualizarPedidoItem(AtualizarItem requestItem)
        {
            var pedido = await _context.Pedidos.FindAsync(requestItem.IdPedido);
            if (pedido?.Id != requestItem.IdPedido)
                throw new InvalidOperationException("Pedido não encontrado");

            var item = await _context.PedidoItens.FindAsync(requestItem.IdItem);
            if (item?.Id != requestItem.IdItem)
                throw new InvalidOperationException("Item não encontrado");

            item.InformarQuantidade(requestItem.Quantidade);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Pedido> DeletarPedido(Guid id)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);

            if (!ValidaStatusCriado((int)pedido.Status))
                throw new InvalidOperationException("Status Inválido");

            if (pedido == null)
                throw new InvalidOperationException("Pedido Inválido");

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task<PedidoItem> DeletarPedidoItem(Guid id)
        {
            var item = await _context.PedidoItens.FindAsync(id);

            if (item == null)
                throw new InvalidOperationException("Item Inválido");

            _context.PedidoItens.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }
        private bool ValidaStatusCriado(int status)
        {
            if (status != 3)
                return true;
            else
                return false;
        }
        private bool ProdutoExists(Guid id)
        {
            return _context.PedidoItens.Any(e => e.ProdutoId == id);
        }
    }
}
