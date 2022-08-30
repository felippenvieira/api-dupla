using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locadora.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; init; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public ICollection<Locacao> Locacoes { get; private set; }
        public bool Status { get; private set; }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarCpf(string cpf)
        {
            Cpf = cpf;
        }

        public void AtualizarNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public void AtualizarEmail(string email)
        {
            Email = email;
        }

        public void AtualizarStatus(bool status)
        {
            Status = status;
        }

        public Cliente(string nome, string cpf, DateTime dataNascimento, string email)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            Status = true;
        }

        public Cliente(int id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataNascimento = new DateTime();
            Email = email;
            Status = true;
        }

        protected Cliente() { }
    }
}