using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers.APIs
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsApiController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PatientsApiController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _context.Patients
                .Include(p => p.Diseases)
                .Include(p => p.Doctor)
                .ToListAsync();

            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Diseases)
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient([FromBody] Patient patient)
        {
            Doctor doctor = await _context.Doctors.FindAsync(patient.DoctorId);
            Disease disease = await _context.Diseases.FindAsync(patient.DiseaseId);

            if (doctor == null)
            {
                ModelState.AddModelError("DoctorId", "Invalid Doctor ID");
                return BadRequest(ModelState);
            }
            if (disease == null)
            {
                ModelState.AddModelError("DiseaseId", "Invalid Disease ID");
                return BadRequest(ModelState);
            }

            patient.Doctor = doctor;
            patient.Diseases = disease;

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            var doctor = await _context.Doctors.FindAsync(patient.DoctorId);
            var disease = await _context.Diseases.FindAsync(patient.DiseaseId);

            if (doctor != null && disease != null)
            {
                patient.Doctor = doctor;
                patient.Diseases = disease;

                _context.Entry(patient).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
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
            else
            {
                ModelState.AddModelError("DoctorId", "Invalid Doctor ID");
                ModelState.AddModelError("DiseaseId", "Invalid Disease ID");
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }

}
