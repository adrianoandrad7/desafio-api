using System;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Requests.Produto;

namespace WebApi.Services
{
    public class ProdutoService
    {
        private readonly ApiContext _context;
        public ProdutoService(ApiContext context)
        {
            _context = context;
        }
        public async Task<Produto> Adicionar(CriaProduto request)
        {
            if (!ValidaDescricao(request.Descricao))
                throw new InvalidOperationException("Descrição Inválida");
            else if (!ValidaValor(request.Valor))
                throw new InvalidOperationException("Valor Inválido");
            else if (!ValidaEstoque(request.QuantidadeEstoque))
                throw new InvalidOperationException("Quantidade Estoque Inválido");

            Produto produto = new Produto(request.Descricao, request.Valor, request.QuantidadeEstoque);

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Atualizar(Guid id, CriaProduto request)
        {

            if (!ValidaDescricao(request.Descricao))
                throw new InvalidOperationException("Descrição Inválida");
            else if (!ValidaValor(request.Valor))
                throw new InvalidOperationException("Valor Inválido");
            else if (!ValidaEstoque(request.QuantidadeEstoque))
                throw new InvalidOperationException("Quantidade Estoque Inválido");

            var produto = await _context.Produtos.FindAsync(id);

            if (produto?.Id != id)
                throw new InvalidOperationException("Usuário não encontrado");

            produto.InformarDescricao(request.Descricao);
            produto.InformarValor(request.Valor);
            produto.InformarEstoque(request.QuantidadeEstoque);

            await _context.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto> Deletar(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if (produto == null)
                throw new InvalidOperationException("Usuário Inválido");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
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
            if (valor > 0)
                return true;
            else
                return false;
        }
        private bool ValidaEstoque(int estoque)
        {
            if (estoque <= 0)
                return false;
            else
                return true;
        }
    }
}
