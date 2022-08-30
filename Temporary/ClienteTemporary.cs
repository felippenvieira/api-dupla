using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dupla.Temporary
{
    public class ClienteTemporary
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
    }
}