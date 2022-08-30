using System;
using Locadora.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var clienteUm = new
            {
                Id = 1,
                Nome = "João Paulo",
                Cpf = "123456",
                DataNascimento = new DateTime(),
                Email = "jp123@gmail.com",
                Status = true
            };

            var clienteDois = new
            {
                Id = 2,
                Nome = "Maria Joaquina",
                Cpf = "123456",
                DataNascimento = new DateTime(),
                Email = "mj123@gmail.com",
                Status = true
            };


            var locacaoUm = new
            {
                Id = 1,
                DataLocacao = new DateTime(2022, 07, 15, 00, 00, 00),
                DataEntrega = new DateTime(2022, 07, 18, 00, 00, 00),
                DataEntregue = new DateTime(2022, 07, 18, 00, 00, 00),
                ClienteId = 1,
                Valor = 20.00,
                Status = true
            };

            var filmeUm = new
            {
                Id = 1,
                Nome = "João e o Pé de Feijão",
                Genero = Genero.Acao,
                Ano = 2022,
                LocacaoId = locacaoUm.Id,
                Status = true
            };

            var filmeDois = new
            {
                Id = 2,
                Nome = "Toy Story 2",
                Genero = Genero.Fantasia,
                Ano = 2022,
                LocacaoId = locacaoUm.Id,
                Status = true
            };

            var filmeTres = new
            {
                Id = 3,
                Nome = "Fogo em Alto Mar",
                Genero = Genero.Comedia,
                Ano = 2022,
                Status = true
            };

            modelBuilder.Entity<Filme>()
                .HasData(
                    filmeUm,
                    filmeDois,
                    filmeTres
                );

            modelBuilder.Entity<Cliente>()
                .HasData(
                    clienteUm,
                    clienteDois
                );

            modelBuilder.Entity<Locacao>()
                .HasData(
                    locacaoUm
                );
        }
    }
}