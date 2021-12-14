using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().ToTable("pedido");
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<Produto>().ToTable("produto");
            modelBuilder.Entity<PedidoItem>().ToTable("pedido_item");

            ConfigurarPedido(modelBuilder);

            ConfigurarProduto(modelBuilder);

            ConfigurarUsuario(modelBuilder);

            ConfigurarPedidoItem(modelBuilder);
        }
        private static void ConfigurarPedido(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
           .HasMany(b => b.Itens)
           .WithOne();

            modelBuilder.Entity<Pedido>(
                        eb =>
                        {
                            eb.Property(b => b.Id).HasColumnName("Id");
                            eb.Property(b => b.Status).HasColumnName("Status");
                            eb.Property(b => b.TotalPedido).HasColumnName("TotalPedido");
                            eb.Property(b => b.IdUsuario).HasColumnName("UsuarioId");
                        });
        }

        private static void ConfigurarProduto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(
            eb =>
            {
                eb.Property(b => b.Id).HasColumnName("Id");
                eb.Property(b => b.Descricao).HasColumnName("Descricao");
                eb.Property(b => b.Valor).HasColumnName("Valor");
                eb.Property(b => b.Ativo).HasColumnName("Ativo");
                eb.Property(b => b.QuantidadeEstoque).HasColumnName("QuantidadeEstoque");
            });
        }



        private static ModelBuilder ConfigurarPedidoItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoItem>()
            .HasOne(p => p.Pedido)
            .WithMany(b => b.Itens)
            .HasForeignKey(p => p.PedidoId);
            return modelBuilder.Entity<PedidoItem>(
                        eb =>
                        {
                            eb.Property(b => b.Id).HasColumnName("Id");
                            eb.Property(b => b.Quantidade).HasColumnName("Quantidade");
                            eb.Property(b => b.PedidoId).HasColumnName("PedidoId");
                            eb.Property(b => b.ProdutoId).HasColumnName("ProdutoId");

                        });
        }

        private static void ConfigurarUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(
                        eb =>
                        {
                            eb.Property(b => b.Id).HasColumnName("Id");
                            eb.Property(b => b.Nome).HasColumnName("Nome");
                            eb.Property(b => b.CPF).HasColumnName("Cpf");
                            eb.Property(b => b.Email).HasColumnName("Email");
                            eb.Property(b => b.Ativo).HasColumnName("Ativo");
                            eb.Property(b => b.DataCadastro).HasColumnName("DataCadastro");
                            eb.Property(b => b.DataNascimento).HasColumnName("DataNascimento");

                        });
        }
    }
}
