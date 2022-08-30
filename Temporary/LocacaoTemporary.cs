using System;
using System.Collections.Generic;

namespace api_dupla.Temporary
{
    public class LocacaoTemporary
    {
        public int ClienteId { get; set; }
        public ICollection<int> FilmesId { get; set; }
        public double Valor { get; set; }
    }
}