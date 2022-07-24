using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDTest.Context;
using CRUDTest.Models;
using CRUDTest.DTO;
using AutoMapper;

namespace CRUDTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public PessoasController(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaListResponseDto>>> Get()
        {
            if (_context.Pessoas == null)
            {
                return NotFound();
            }

            var pessoas = await _context.Pessoas.ToListAsync();
            var pessoasDto = _mapper.Map<List<PessoaListResponseDto>>(pessoas);

            return Ok(pessoasDto);
        }

        // GET: api/Pessoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponseDto>> Get(int id)
        {
            if (_context.Pessoas == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoas.FindAsync(id);

            var pessoaDto = _mapper.Map<PessoaResponseDto>(pessoa);

            var cidade = await _context.Cidades.FindAsync(pessoaDto.Id_Cidade);
            pessoaDto.Cidade = _mapper.Map<CidadeListResponseDto>(cidade);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoaDto;
        }

        // POST: api/Pessoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PessoaRequestDto>> Create(PessoaRequestDto pessoaDto)
        {
            if (_context.Pessoas == null)
            {
                return Problem("Entity set 'TestContext.Pessoas'  is null.");
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaDto);

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = pessoa.Id }, pessoaDto);
        }

        // PUT: api/Pessoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, PessoaRequestDto pessoaDto)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null || id != pessoa.Id)
            {
                return BadRequest();
            }

            pessoa.Nome = pessoaDto.Nome;
            pessoa.CPF = pessoaDto.CPF;
            pessoa.Idade = pessoaDto.Idade;
            pessoa.Id_Cidade = pessoaDto.Id_Cidade;

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Pessoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Pessoas == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return (_context.Pessoas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
