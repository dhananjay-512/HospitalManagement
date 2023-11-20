using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Models;

namespace HospitalManagement.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AppDBContext _context;

        public PatientsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Patients.Include(p => p.Diseases).Include(p => p.Doctor);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Diseases)
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "Id", "Id");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Gender,DiseaseId,DoctorId,AdmittedDate,DischargeDate")] Patient patient)
        {
            // Retrieve the Doctor and Disease objects based on their IDs
            Doctor doctor = await _context.Doctors.FindAsync(patient.DoctorId);
            Disease disease = await _context.Diseases.FindAsync(patient.DiseaseId);

            if (doctor == null)
            {
                ModelState.AddModelError("DoctorId", "Invalid Doctor ID");
            }
            if (disease == null)
            {
                ModelState.AddModelError("DiseaseId", "Invalid Disease ID");
            }

                // Assign the Doctor and Disease objects to the patient
                patient.Doctor = doctor;
                patient.Diseases = disease;
            if (ModelState.IsValid)
            {

                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "Id", "Id", patient.DiseaseId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", patient.DoctorId);

            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Gender,DiseaseId,DoctorId,AdmittedDate,DischargeDate")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var doctor = await _context.Doctors.FindAsync(patient.DoctorId);
                var disease = await _context.Diseases.FindAsync(patient.DiseaseId);

                if (doctor != null && disease != null)
                {
                    patient.Doctor = doctor;
                    patient.Diseases = disease;

                    try
                    {
                        _context.Update(patient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PatientExists(patient.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("DoctorId", "Invalid Doctor ID");
                    ModelState.AddModelError("DiseaseId", "Invalid Disease ID");
                }
            }

            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "Id", "Id", patient.DiseaseId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", patient.DoctorId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.Diseases)
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'AppDBContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
