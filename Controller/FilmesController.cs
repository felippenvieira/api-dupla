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
    public class FilmesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Filme>>> GetFilme()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campo e tente novamente." });
            var filmes = await _context.Filmes.ToListAsync();
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campo e tente novamente." });

            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
                return BadRequest(new { Message = "Filme não encontrado." });
            return Ok(filme);
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(FilmeTemporary filmeTemp)
        {
            Filme filme = new Filme(filmeTemp.Nome, filmeTemp.Genero, filmeTemp.Ano);

            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Filme salvo com sucesso!" });
        }

        [HttpPut]
        public async Task<ActionResult<Filme>> AtualizarFilme(FilmeTemporary filmeTemp)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campo e tente novamente." });

            var filme = await _context.Filmes.FindAsync(filmeTemp.Id);
            if (filme == null)
                return BadRequest(new { Message = "Filme não encontrado." });

            filme.AtualizarNome(filmeTemp.Nome);
            filme.AtualizarGenero(filmeTemp.Genero);
            filme.AtualizarAno(filmeTemp.Ano);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Filme atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Apagar(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
                return BadRequest(new { Message = "Filme não encontrado." });

            filme.AtualizarStatus(false);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Filme deletado com sucesso." });
        }
    }
}