using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_dupla.Temporary;
using Locadora.Data;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_dupla.Controller
{
    [Route("[controller]")]
    public class LocacoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Locacao>>> Get()
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var locacoes = await _context.Locacoes
                .ToListAsync();

            return Ok(locacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var locacao = await _context.Locacoes
                .FindAsync(id);
            if (locacao == null)
                return BadRequest(new { Message = "Locação não encontrada." });
            return Ok(locacao);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarLocacao([Bind("Id,ClienteId,FilmesId,Valor,Status")] LocacaoTemporary locacaoTemp)
        {
            if (!ModelState.IsValid && !locacaoTemp.FilmesId.Any())
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.FindAsync(locacaoTemp.ClienteId);

            Locacao locacao = new Locacao(cliente, locacaoTemp.Valor);

            await _context.Locacoes.AddAsync(locacao);

            foreach (var item in locacaoTemp.FilmesId)
            {
                var filmeTemp = await _context.Filmes.Include(x => x.Locacao).FirstOrDefaultAsync(x => x.Id == item);

                if (filmeTemp is null)
                    return BadRequest(new { Message = "Filme não existe." });

                if (filmeTemp?.Locacao is not null)
                    return BadRequest(new { Message = "Este filme já está alugado." });

                filmeTemp.AdicionarLocacao(locacao);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Locação criada com sucesso." });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Locacao>> AtualizarLocacao(int id, LocacaoTemporary locacaoTemp)
        {
            if (!ModelState.IsValid && !locacaoTemp.FilmesId.Any())
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var cliente = await _context.Clientes.FindAsync(locacaoTemp.ClienteId);

            var locacao = await _context.Locacoes.Include(f => f.Filmes).FirstOrDefaultAsync(x => x.Id == id);

            locacao.AtualizarFilmes();

            foreach (var item in locacaoTemp.FilmesId)
            {
                var filmeTemp = await _context.Filmes.Include(x => x.Locacao).FirstOrDefaultAsync(x => x.Id == item);

                if (filmeTemp is null)
                    return BadRequest(new { Message = "Filme não existe." });

                if (filmeTemp?.Locacao is not null)
                    return BadRequest(new { Message = "Este filme já está alugado." });

                filmeTemp.AdicionarLocacao(locacao);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Locação atualizada com sucesso." });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Locacao>> DevolverFilme(int id, DateTime dataDeDevolucao)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var locacao = await _context.Locacoes.Include(x => x.Filmes).FirstOrDefaultAsync(x => x.Id == id);

            if (locacao == null)
                return BadRequest(new { Message = "Locação não encontrada." });

            locacao.DevolverFilme(dataDeDevolucao);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Filme devolvido com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Locacao>> Apagar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var locacao = await _context.Locacoes.Include(x => x.Filmes).FirstOrDefaultAsync(x => x.Id == id);

            if (locacao == null)
                return BadRequest(new { Message = "Locação não encontrada." });

            locacao.DeletarLocacao(false);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Locação deletada com sucesso." });
        }
    }
}