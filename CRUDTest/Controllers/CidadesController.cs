using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDTest.Context;
using CRUDTest.Models;
using AutoMapper;
using CRUDTest.DTO;

namespace CRUDTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public CidadesController(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeListResponseDto>>> Get()
        {
            if (_context.Cidades == null)
            {
                return NotFound();
            }
            
            var cidades = await _context.Cidades.ToListAsync();
            var cidadesDto = _mapper.Map<List<CidadeListResponseDto>>(cidades);

            return Ok(cidadesDto);
        }

        // GET: api/Cidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CidadeResponseDto>> Get(int id)
        {
            if (_context.Cidades == null)
            {
                return NotFound();
            }
            var cidade = await _context.Cidades.FindAsync(id);

            var cidadeDto = _mapper.Map<CidadeResponseDto>(cidade);

            if (cidade == null)
            {
                return NotFound();
            }

            return cidadeDto;
        }

        // POST: api/Cidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CidadeRequestDto>> Create(CidadeRequestDto cidadeDto)
        {
            if (_context.Cidades == null)
            {
                return Problem("Entity set 'TestContext.Cidades'  is null.");
            }

            var cidade = _mapper.Map<Cidade>(cidadeDto);

            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = cidade.Id }, cidadeDto);
        }

        // PUT: api/Cidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, CidadeRequestDto cidadeDto)
        {
            var cidade = await _context.Cidades.FindAsync(id);

            if (cidade == null || id != cidade.Id)
            {
                return BadRequest();
            }

            cidade.Nome = cidadeDto.Nome;
            cidade.UF = cidadeDto.UF;

            _context.Entry(cidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeExists(id))
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


        // DELETE: api/Cidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Cidades == null)
            {
                return NotFound();
            }
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CidadeExists(int id)
        {
            return (_context.Cidades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
