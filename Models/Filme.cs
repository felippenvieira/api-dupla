using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locadora.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; init; }
        public string Nome { get; private set; }
        public Genero Genero { get; private set; }
        public int Ano { get; private set; }
        public Locacao Locacao { get; private set; }
        public bool Status { get; private set; }

        public void AdicionarLocacao(Locacao locacao)
        {
            Locacao = locacao;
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarGenero(Genero genero)
        {
            Genero = genero;
        }

        public void AtualizarAno(int ano)
        {
            Ano = ano;
        }

        public void AtualizarStatus(bool status)
        {
            Status = status;
        }

        public Filme(string nome, Genero genero, int ano)
        {
            Nome = nome;
            Genero = genero;
            Ano = ano;
            Status = true;
        }

        public Filme(int id, string nome, Genero genero, int ano)
        {
            Id = id;
            Nome = nome;
            Genero = genero;
            Ano = ano;
            Status = true;
        }

        protected Filme() { }
    }
}