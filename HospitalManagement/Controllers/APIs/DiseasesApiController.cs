using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers.APIs
{
    [Route("api/diseases")]
    [ApiController]
    public class DiseasesApiController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DiseasesApiController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/diseases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disease>>> GetDiseases()
        {
            return _context.Diseases != null ?
                        Ok(await _context.Diseases.ToListAsync()) :
                        Problem("Entity set 'AppDBContext.Diseases' is null.");
        }

        // GET: api/diseases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disease>> GetDisease(int id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return NotFound();
            }

            return Ok(disease);
        }

        // POST: api/diseases
        [HttpPost]
        public async Task<ActionResult<Disease>> PostDisease([FromBody] Disease disease)
        {
            if (ModelState.IsValid)
            {
                _context.Diseases.Add(disease);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDisease", new { id = disease.Id }, disease);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/diseases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisease(int id, [FromBody] Disease disease)
        {
            if (id != disease.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(disease).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseExists(id))
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

            return BadRequest(ModelState);
        }

        // DELETE: api/diseases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(disease);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseaseExists(int id)
        {
            return _context.Diseases?.Any(e => e.Id == id) ?? false;
        }
    }
}
