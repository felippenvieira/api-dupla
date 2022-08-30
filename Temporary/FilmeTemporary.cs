using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locadora.Models;

namespace api_dupla.Temporary
{
    public class FilmeTemporary
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Genero Genero { get; set; }
        public int Ano { get; set; }
    }
}