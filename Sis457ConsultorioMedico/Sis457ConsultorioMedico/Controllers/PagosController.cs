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
    public class PagosController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public PagosController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Pagos
        public async Task<IActionResult> Index()
        {
            var finalConsultorioMedicoContext = _context.Pagos.Include(p => p.IdCitaNavigation)
                .ThenInclude(cita => cita.IdPacienteNavigation) 
                .Include(p => p.IdConceptoNavigation).Include(p => p.IdConceptoNavigation).OrderByDescending(p => p.Fecha);
            return View(await finalConsultorioMedicoContext.ToListAsync());
        }

        // GET: Pagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdCitaNavigation).ThenInclude(c => c.IdPacienteNavigation)
                .Include(p => p.IdConceptoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // GET: Pagos/Create
        public IActionResult Create(int idCita, int idEspecialidad)
        {
            var cita = _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefault(c => c.Id == idCita);

            if (cita == null)
            {
                return NotFound();
            }

            var pago = new Pago
            {
                IdCita = idCita
            };

            var conceptos = _context.Conceptos
                .Where(c => c.IdEspecialidad == idEspecialidad)
                .ToList();
            var nombreEspecialidad = _context.Especialidades
                        .Where(e => e.Id == idEspecialidad)
                        .Select(e => e.Nombre)
                        .FirstOrDefault();

            ViewBag.NombreEspecialidad = nombreEspecialidad;
            ViewBag.CostosConceptos = conceptos.ToDictionary(c => c.Id, c => c.Costo);
            ViewBag.IdConcepto = new SelectList(conceptos, "Id", "Descripcion");
            ViewBag.NombrePaciente = cita.IdPacienteNavigation.Nombres + " " + cita.IdPacienteNavigation.PrimerApellido + " " + cita.IdPacienteNavigation.SegundoApellido;

            return View(pago);
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pago pago)
        {
            pago.UsuarioRegistro = User.Identity.Name;
            pago.Estado = 1;
            var concepto = await _context.Conceptos.FindAsync(pago.IdConcepto);
            if (concepto == null || pago.IdConcepto == 0)
            {
                ModelState.AddModelError(nameof(pago.IdConcepto), "Debe seleccionar un Concepto de Pago.");
            }
            else
            {
                if (pago.Efectivo < concepto.Costo)
                {
                    ModelState.AddModelError(nameof(pago.Efectivo), $"El monto pagado no puede ser menor al costo del concepto ({concepto.Costo:C}).");
                }
                pago.IdConceptoNavigation = concepto;
            }
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Citas");

            }
            var cita = _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefault(c => c.Id == pago.IdCita);

            var idEspecialidad = _context.Cita
                .Where(c => c.Id == pago.IdCita)
                .Select(c => c.IdEspecialidad)
                .FirstOrDefault();

            var conceptos = _context.Conceptos
                .Where(c => c.IdEspecialidad == idEspecialidad)
                .ToList();

            var nombreEspecialidad = _context.Especialidades
                .Where(e => e.Id == idEspecialidad)
                .Select(e => e.Nombre)
                .FirstOrDefault();

            ViewBag.NombreEspecialidad = nombreEspecialidad;
            ViewBag.CostosConceptos = conceptos.ToDictionary(c => c.Id, c => c.Costo);
            ViewBag.IdConcepto = new SelectList(conceptos, "Id", "Descripcion");
            ViewBag.NombrePaciente = cita.IdPacienteNavigation.Nombres + " " +
                                      cita.IdPacienteNavigation.PrimerApellido + " " +
                                      cita.IdPacienteNavigation.SegundoApellido;

            return View(pago);
        }


        // GET: Pagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["IdCita"] = new SelectList(_context.Cita, "Id", "Id", pago.IdCita);
            ViewData["IdConcepto"] = new SelectList(_context.Conceptos, "Id", "Id", pago.IdConcepto);
            return View(pago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCita,IdConcepto,Fecha,UsuarioRegistro,FechaRegistro,Estado")] Pago pago)
        {
            if (id != pago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.Id))
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
            ViewData["IdCita"] = new SelectList(_context.Cita, "Id", "Id", pago.IdCita);
            ViewData["IdConcepto"] = new SelectList(_context.Conceptos, "Id", "Id", pago.IdConcepto);
            return View(pago);
        }

        // GET: Pagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.IdCitaNavigation)
                .Include(p => p.IdConceptoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Id == id);
        }
    }
}
