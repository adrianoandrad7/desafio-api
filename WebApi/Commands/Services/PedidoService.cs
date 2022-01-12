using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Data;
using Domain;
using Domain.Enums;
using Commands.Requests;

namespace Commands.Services
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
        public async Task<Pedido> AdicionarItem(AdicionarItem request)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == request.PedidoId);

            if (pedido?.Id != request.PedidoId)
                throw new InvalidOperationException("Pedido não encontrado");

            var produto = await _context.Produtos.FindAsync(request.ProdutoId);

            if (produto?.Id != request.ProdutoId)
                throw new InvalidOperationException("Produto não encontrado");

            pedido.AdicionarItem(produto, request.Quantidade);
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task<Pedido> AtualizarPedidoItem(AtualizarItem requestItem)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == requestItem.IdPedido);

            if (pedido?.Id != requestItem.IdPedido)
                throw new InvalidOperationException("Pedido não encontrado");

            pedido.AtualizaQuantidade(requestItem.IdItem,requestItem.Quantidade);
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task<Pedido> DeletarPedido(Guid id)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == id);

            if (pedido == null)
                throw new InvalidOperationException("Pedido Inválido");

            if (ValidaStatusConcluido(pedido.Status))
                throw new InvalidOperationException("Status Inválido");

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }
        public async Task<Pedido> DeletarPedidoItem(Guid idPedido, Guid idItem)
        {
            var pedido = await _context.Pedidos.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == idPedido);

            if (pedido == null)
                throw new InvalidOperationException("Pedido Inválido");

            pedido.Removertem(idItem);
            await _context.SaveChangesAsync();

            return pedido;
        }
        private bool ValidaStatusConcluido(PedidoStatus status)
        {
            if (status == PedidoStatus.Concluido)
                return true;
            else
                return false;
        }
    }
}
