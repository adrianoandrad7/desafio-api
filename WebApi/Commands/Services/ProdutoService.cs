using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Data;
using Domain;
using Commands.Requests;
namespace Commands.Services
{
    public class ProdutoService
    {
        private readonly ApiContext _context;
        private readonly Repository<Produto> _repository;

        public ProdutoService(ApiContext context, Repository<Produto> repository)
        {
            _context = context;
            _repository = repository;

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

            _repository.Add(produto);
            await _repository.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto> Atualizar(Guid id, AtualizaProduto request)
        {
            if (!ValidaDescricao(request.Descricao))
                throw new InvalidOperationException("Descrição Inválida");
            else if (!ValidaValor(request.Valor))
                throw new InvalidOperationException("Valor Inválido");
            else if (!ValidaEstoque(request.QuantidadeEstoque))
                throw new InvalidOperationException("Quantidade Estoque Inválido");

            var produto = await _context.Produtos.FindAsync(id);

            if (produto?.Id != id)
                throw new InvalidOperationException("Produto não encontrado");

            produto.InformarDescricao(request.Descricao);
            produto.InformarValor(request.Valor);
            produto.InformarEstoque(request.QuantidadeEstoque);

            await _repository.SaveChangesAsync();

            return produto;
        }
        public async Task<Produto> Deletar(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if (produto == null)
                throw new InvalidOperationException("Usuário Inválido");

            await _repository.DeleteAsync(produto);
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
