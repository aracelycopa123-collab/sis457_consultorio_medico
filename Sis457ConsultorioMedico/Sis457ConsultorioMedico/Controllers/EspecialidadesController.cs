using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    public class EspecialidadesController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public EspecialidadesController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Especialidades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialidades.ToListAsync());
        }

        // GET: Especialidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var especialidad = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.Id == id);

            if (especialidad == null) return NotFound();

            return View(especialidad);
        }

        // GET: Especialidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Especialidad especialidad)
        {
            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");
            }

            // Validar duplicado
            if (await _context.Especialidades.AnyAsync(e => e.Nombre.ToUpper() == especialidad.Nombre.Trim().ToUpper()))
            {
                ModelState.AddModelError("Nombre", "Ya existe una especialidad con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                especialidad.Nombre = especialidad.Nombre.Trim();
                especialidad.UsuarioRegistro = User?.Identity?.Name ?? "system";
                especialidad.FechaRegistro = DateTime.Now;
                especialidad.Estado = 1;

                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Especialidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null) return NotFound();

            return View(especialidad);
        }

        // POST: Especialidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Especialidad especialidad)
        {
            if (id != especialidad.Id) return NotFound();

            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");
            }

            // Validar duplicado al editar
            if (await _context.Especialidades.AnyAsync(e => e.Nombre.ToUpper() == especialidad.Nombre.Trim().ToUpper() && e.Id != id))
            {
                ModelState.AddModelError("Nombre", "Ya existe una especialidad con ese nombre.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existente = await _context.Especialidades.FindAsync(id);
                    if (existente == null) return NotFound();

                    existente.Nombre = especialidad.Nombre.Trim();
                    _context.Update(existente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadExists(especialidad.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(especialidad);
        }

        // GET: Especialidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var especialidad = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.Id == id);

            if (especialidad == null) return NotFound();

            return View(especialidad);
        }

        // POST: Especialidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialidad = await _context.Especialidades
                .Include(e => e.Conceptos)
                .Include(e => e.Doctors)
                .Include(e => e.Cita)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (especialidad == null) return RedirectToAction(nameof(Index));

            if ((especialidad.Conceptos != null && especialidad.Conceptos.Any()) ||
                (especialidad.Doctors != null && especialidad.Doctors.Any()) ||
                (especialidad.Cita != null && especialidad.Cita.Any()))
            {
                TempData["ErrorEspecialidad"] = "No se puede eliminar la especialidad porque tiene registros relacionados.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            _context.Especialidades.Remove(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadExists(int id)
        {
            return _context.Especialidades.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<JsonResult> ExisteNombre(string nombre, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return Json(false);

            var nombreNorm = nombre.Trim().ToUpperInvariant();
            bool existe = await _context.Especialidades
                .AnyAsync(e => e.Nombre.ToUpper() == nombreNorm && (id == null || e.Id != id.Value));
            return Json(existe);
        }
    }
}
