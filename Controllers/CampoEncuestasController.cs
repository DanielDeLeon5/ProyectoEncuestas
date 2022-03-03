#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Encuestas.Entities;
using Encuestas.Services;

namespace Encuestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampoEncuestasController : ControllerBase
    {
        private readonly PEncuestasDbContext _context;

        public CampoEncuestasController(PEncuestasDbContext context)
        {
            _context = context;
        }

        // GET: api/CampoEncuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampoEncuesta>>> GetCampoEncuestas()
        {
            return await _context.CampoEncuestas.ToListAsync();
        }

        // GET: api/CampoEncuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampoEncuesta>> GetCampoEncuesta(int id)
        {
            var campoEncuesta = await _context.CampoEncuestas.FindAsync(id);

            if (campoEncuesta == null)
            {
                return NotFound();
            }

            return campoEncuesta;
        }

        // PUT: api/CampoEncuestas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampoEncuesta(int id, CampoEncuesta campoEncuesta)
        {
            if (id != campoEncuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(campoEncuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampoEncuestaExists(id))
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

        // POST: api/CampoEncuestas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CampoEncuesta>> PostCampoEncuesta(CampoEncuesta campoEncuesta)
        {
            _context.CampoEncuestas.Add(campoEncuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampoEncuesta", new { id = campoEncuesta.Id }, campoEncuesta);
        }

        // DELETE: api/CampoEncuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampoEncuesta(int id)
        {
            var campoEncuesta = await _context.CampoEncuestas.FindAsync(id);
            if (campoEncuesta == null)
            {
                return NotFound();
            }

            _context.CampoEncuestas.Remove(campoEncuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampoEncuestaExists(int id)
        {
            return _context.CampoEncuestas.Any(e => e.Id == id);
        }
    }
}
