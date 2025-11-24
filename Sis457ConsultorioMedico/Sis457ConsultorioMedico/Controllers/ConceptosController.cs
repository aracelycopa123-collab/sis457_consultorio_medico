using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    public class ConceptosController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public ConceptosController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Conceptos
        public async Task<IActionResult> Index()
        {
            var finalConsultorioMedicoContext = _context.Conceptos.Include(c => c.IdEspecialidadNavigation);
            return View(await finalConsultorioMedicoContext.ToListAsync());
        }

        // GET: Conceptos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos
                .Include(c => c.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concepto == null)
            {
                return NotFound();
            }

            return View(concepto);
        }

        // GET: Conceptos/Create
        public IActionResult Create()
        {
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            return View();
        }

        // POST: Conceptos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEspecialidad,Descripcion,Costo,UsuarioRegistro,FechaRegistro,Estado")] Concepto concepto)
        {
            if (ModelState.IsValid)
            {
                // Asignar campos que normalmente se llenan automáticamente en la BD o por el usuario actual
                concepto.UsuarioRegistro = User?.Identity?.Name ?? "Sistema";
                concepto.FechaRegistro = DateTime.Now;
                concepto.Estado = 1;

                try
                {
                    _context.Add(concepto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Mostrar la excepción en la vista para diagnóstico
                    ModelState.AddModelError(string.Empty, "Error al guardar en la base de datos: " + ex.Message);
                }
            }
            else
            {
                // Recolectar errores de validación para mostrarlos
                var errores = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Where(m => !string.IsNullOrEmpty(m)));
                if (!string.IsNullOrEmpty(errores))
                {
                    ModelState.AddModelError(string.Empty, "Errores de validación: " + errores);
                }
            }

            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", concepto?.IdEspecialidad);
            return View(concepto);
        }

        // GET: Conceptos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto == null)
            {
                return NotFound();
            }
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", concepto.IdEspecialidad);
            return View(concepto);
        }

        // POST: Conceptos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEspecialidad,Descripcion,Costo,UsuarioRegistro,FechaRegistro,Estado")] Concepto concepto)
        {
            if (id != concepto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concepto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConceptoExists(concepto.Id))
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
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", concepto.IdEspecialidad);
            return View(concepto);
        }

        // GET: Conceptos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concepto = await _context.Conceptos
                .Include(c => c.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concepto == null)
            {
                return NotFound();
            }

            return View(concepto);
        }

        // POST: Conceptos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto != null)
            {
                _context.Conceptos.Remove(concepto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            private bool ConceptoExists(int id)
        {
            return _context.Conceptos.Any(e => e.Id == id);
        }
    }
}
