using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanatorio.Models;

namespace Sanatorio.Controllers
{
    public class CitaController : Controller
    {
        private readonly SanatorioContext _context;

        public CitaController(SanatorioContext context)
        {
            _context = context;
        }

        // GET: Cita
        public async Task<IActionResult> Index()
        {
            var sanatorioContext = _context.Citas.Include(c => c.Medicos).Include(c => c.Pacientes);
            return View(await sanatorioContext.ToListAsync());
        }

        // GET: Cita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Medicos)
                .Include(c => c.Pacientes)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Cita/Create
        public IActionResult Create()
        {
            ViewData["MedicosId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre");
            ViewData["PacientesId"] = new SelectList(_context.Pacientes, "IdPaciente", "Nombre");
            return View();
        }

        // POST: Cita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,Fecha,Hora,Tratamiento,PacientesId,MedicosId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicosId"] = new SelectList(_context.Medicos, "IdMedico", "Nombre", cita.MedicosId);
            ViewData["PacientesId"] = new SelectList(_context.Pacientes, "IdPaciente", "Nombre", cita.PacientesId);
            return View(cita);
        }

        // GET: Cita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["MedicosId"] = new SelectList(_context.Medicos, "IdMedico", "IdMedico", cita.MedicosId);
            ViewData["PacientesId"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", cita.PacientesId);
            return View(cita);
        }

        // POST: Cita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,Fecha,Hora,Tratamiento,PacientesId,MedicosId")] Cita cita)
        {
            if (id != cita.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))
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
            ViewData["MedicosId"] = new SelectList(_context.Medicos, "IdMedico", "IdMedico", cita.MedicosId);
            ViewData["PacientesId"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", cita.PacientesId);
            return View(cita);
        }

        // GET: Cita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Medicos)
                .Include(c => c.Pacientes)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
