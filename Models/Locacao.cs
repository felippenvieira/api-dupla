using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locadora.Models
{
    public class Locacao
    {
        [Key]
        public int Id { get; init; }
        public DateTime DataLocacao { get; init; }
        public DateTime DataEntrega { get; init; }
        public DateTime DataEntregue { get; private set; }
        public Cliente Cliente { get; private set; }

        [NotMapped]
        public ICollection<int> FilmeId { get; set; }

        public ICollection<Filme> Filmes { get; private set; }

        [Required(ErrorMessage = "É obrigatório informar o valor de locação do filme")]
        [Display(Name = "Valor de locação do filme")]
        public double Valor { get; private set; }

        public bool Status { get; private set; }

        public void AtualizarDataEntrega(DateTime entrega)
        {
            DataEntregue = entrega;
        }

        public void AtualizarFilmes()
        {
            Filmes.Clear();
        }

        public void DeletarLocacao(bool status)
        {
            Status = status;
            Filmes.Clear();
        }

        public void DevolverFilme(DateTime dataEntregue)
        {
            DataEntregue = dataEntregue;

            if (DataEntregue > DataEntrega)
                Valor += 3 * Filmes.Count;

            Filmes.Clear();
            Status = false;
        }

        public Locacao(Cliente cliente, double valor)
        {
            DataLocacao = DateTime.Now;
            DataEntrega = DataLocacao.AddDays(3);
            Cliente = cliente;
            Valor = valor;
            Status = true;
        }

        public Locacao(int id, Cliente cliente, double valor)
        {
            Id = id;
            DataLocacao = DateTime.Now;
            DataEntrega = DataLocacao.AddDays(3);
            Cliente = cliente;
            Valor = valor;
            Status = true;
        }

        protected Locacao() { }
    }
}