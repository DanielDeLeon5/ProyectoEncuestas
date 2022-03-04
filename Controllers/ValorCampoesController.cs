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
    public class ValorCampoesController : ControllerBase
    {
        private readonly PEncuestasDbContext _context;

        public ValorCampoesController(PEncuestasDbContext context)
        {
            _context = context;
        }

        // GET: api/ValorCampoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValorCampo>>> GetValorCampo()
        {
            return await _context.ValorCampo.ToListAsync();
        }

        // GET: api/ValorCampoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ValorCampo>> GetValorCampo(int id)
        {
            var valorCampo = await _context.ValorCampo.FindAsync(id);

            if (valorCampo == null)
            {
                return NotFound();
            }

            return valorCampo;
        }


        // PUT: api/ValorCampoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValorCampo(int id, ValorCampo valorCampo)
        {
            if (id != valorCampo.Id)
            {
                return BadRequest();
            }

            _context.Entry(valorCampo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValorCampoExists(id))
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

        // POST: api/ValorCampoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ValorCampo>> PostValorCampo(ValorCampoEncuesta valores)
        {
            /// Encuentra los campos a ingresar relacionados a esa encuesta
            int idEncuesta = valores.idEncuesta;
            var campos = (from c in _context.CampoEncuestas
                         where c.IdEncuesta == idEncuesta
                         select c).ToList();
            /// Recorre los campos y le asigna el id al valor ingresado
            for(int i = 0; i < campos.Count; i++)
            {
                valores.valorCampo[i].IdCampoEncuesta = campos[i].Id;
                _context.ValorCampo.Add(valores.valorCampo[i]);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValorCampo", new { valores.valorCampo }, valores.valorCampo);
        }

        // DELETE: api/ValorCampoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValorCampo(int id)
        {
            var valorCampo = await _context.ValorCampo.FindAsync(id);
            if (valorCampo == null)
            {
                return NotFound();
            }

            _context.ValorCampo.Remove(valorCampo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValorCampoExists(int id)
        {
            return _context.ValorCampo.Any(e => e.Id == id);
        }
    }
}
