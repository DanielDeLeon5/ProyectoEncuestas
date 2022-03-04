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
using System.Collections;

namespace Encuestas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasController : ControllerBase
    {
        private readonly PEncuestasDbContext _context;

        public EncuestasController(PEncuestasDbContext context)
        {
            _context = context;
        }

        // GET: api/Encuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Encuesta>>> GetEncuesta()
        {
            return await _context.Encuesta.ToListAsync();
        }

        /// Retorna los campos a llenar segun el Link de la encuesta
        // GET: api/Encuestas/link
        [HttpGet("{link}")]
        public IList GetEncuesta(string link)
        {

            var encuesta = _context.Encuesta.Where(e => e.Link == link).ToList();

            if (encuesta.Count == 0)
            {
                return encuesta;
            }
            var data = (from e in encuesta
                        join c in _context.CampoEncuestas on e.Id equals c.IdEncuesta
                        select new
                        {
                            nombreEncuesta = e.Nombre,
                            Campo = c.Titulo,
                            Tipo = c.Tipo
                        }).ToList();


            return data;
        }

        // GET: api/Encuestas/GetResultados/link
        [HttpGet("GetResultados")]
        public IList GetEncuesta2(string link)
        {

            var encuesta = _context.Encuesta.Where(e => e.Link == link).ToList();

            if (encuesta.Count == 0)
            {
                return encuesta;
            }
            var data = (from e in encuesta
                        join c in _context.CampoEncuestas on e.Id equals c.IdEncuesta
                        join v in _context.ValorCampo on c.Id equals v.IdCampoEncuesta
                        select new
                        {
                            nombreEncuesta = e.Nombre,
                            Campo = c.Titulo,
                            Valor = v.Valor
                            
                        }).ToList();


            return data;
        }

        // PUT: api/Encuestas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuesta(int id, Encuesta encuesta)
        {
            if (id != encuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(encuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaExists(id))
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

        // POST: api/Encuestas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Encuesta>> PostEncuesta(EncuestaPost encuesta)
        {
            /// Se crea el link de la encuesta
            encuesta.Encuesta.Link = setURL();

            _context.Encuesta.Add(encuesta.Encuesta);
            await _context.SaveChangesAsync();
            /// Recorre los campos ingresados para guardarlos y asociarlos a la encuesta
            foreach(var campo in encuesta.CampoEncuestas)
            {
                campo.IdEncuesta = encuesta.Encuesta.Id;
                _context.CampoEncuestas.Add(campo);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetEncuesta", new { id = encuesta.Encuesta.Id }, encuesta);
        }

        // DELETE: api/Encuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncuesta(int id)
        {
            var encuesta = await _context.Encuesta.FindAsync(id);
            if (encuesta == null)
            {
                return NotFound();
            }

            _context.Encuesta.Remove(encuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncuestaExists(int id)
        {
            return _context.Encuesta.Any(e => e.Id == id);
        }

        /// Crea el link de la encuesta
        private string setURL()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var link = new char[10];
            
            var random = new Random();

            for (int i = 0; i < link.Length; i++)
            {
                link[i] = characters[random.Next(characters.Length)];
            }
            /// Verifica si existe el link
            var Encuestas = new Encuesta();
            if(_context.Encuesta.Any(e => e.Link == Convert.ToString(link)))
            {
                setURL();
            }

            return new String(link);
        }
    }
}
