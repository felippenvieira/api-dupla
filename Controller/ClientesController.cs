using System.Collections.Generic;
using System.Threading.Tasks;
using api_dupla.Temporary;
using Locadora.Data;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_dupla.Controller
{
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var clientes = await _context.Clientes.AsNoTracking().ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
                return BadRequest(new { Message = "Cliente não encontrado." });
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarCliente(ClienteTemporary clienteTemp)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            Cliente cliente = new Cliente(clienteTemp.Nome, clienteTemp.Cpf, clienteTemp.DataNascimento, clienteTemp.Email);

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cliente criado com sucesso." });
        }

        [HttpPut]
        public async Task<ActionResult<Cliente>> AtualizarCliente(ClienteTemporary clienteTemp)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.FindAsync(clienteTemp.Id);
            if (cliente == null)
                return BadRequest(new { Message = "Cliente não encontrado." });

            cliente.AtualizarNome(clienteTemp.Nome);
            cliente.AtualizarCpf(clienteTemp.Cpf);
            cliente.AtualizarNascimento(clienteTemp.DataNascimento);
            cliente.AtualizarEmail(clienteTemp.Email);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cliente atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> Apagar(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
                return BadRequest(new { Message = "Cliente não encontrado." });

            cliente.AtualizarStatus(false);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cliente deletado com sucesso." });
        }
    }
}