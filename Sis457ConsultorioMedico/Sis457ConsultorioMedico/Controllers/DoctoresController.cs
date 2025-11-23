using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    [Authorize]
    public class DoctoresController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public DoctoresController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Doctores
        public async Task<IActionResult> Index()
        {
            var finalConsultorioMedicoContext = await _context.Doctores.Where(x => x.Estado != -1).Include(x => x.IdEspecialidadNavigation).Include(x => x.Usuarios).OrderBy(x => x.Nombres).ToListAsync();
            return View(finalConsultorioMedicoContext);
        }

        // GET: Doctores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctores
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctores/Create
        public IActionResult Create()
        {
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            return View();
        }

        // POST: Doctores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            doctor.UsuarioRegistro = User.Identity.Name;
            doctor.FechaRegistro = DateTime.Now;
            doctor.Estado = 1;
            if (!string.IsNullOrWhiteSpace(doctor.CedulaIdentidad))
            {
                bool ciExiste = await _context.Doctores
                                    .AnyAsync(d => d.CedulaIdentidad == doctor.CedulaIdentidad);

                if (ciExiste)
                {
                    ModelState.AddModelError("CedulaIdentidad", "Ya existe un doctor registrado con esta Cédula de Identidad.");
                }
            }
            if (string.IsNullOrWhiteSpace(doctor.PrimerApellido) && string.IsNullOrWhiteSpace(doctor.SegundoApellido))
            {
                ModelState.AddModelError("PrimerApellido", "Debe ingresar al menos un apellido.");
                ModelState.AddModelError("SegundoApellido", "Debe ingresar al menos un apellido.");
            }
            if (doctor.IdEspecialidad == 0)
            {
                ModelState.AddModelError("IdEspecialidad", "Debe seleccionar una especialidad. ");
            }
            if (doctor.Celular.ToString().Length != 8)
            {
                ModelState.AddModelError("Celular", "El número de celular debe ser de 8 dígitos.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", doctor.IdEspecialidad);
            return View(doctor);
        }

        // GET: Doctores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctores.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", doctor.IdEspecialidad);
            return View(doctor);
        }

        // POST: Doctores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEspecialidad,CedulaIdentidad,Nombres,PrimerApellido,SegundoApellido,Direccion,Celular,UsuarioRegistro,FechaRegistro,Estado")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(doctor.CedulaIdentidad))
            {
                bool ciExisteParaOtroDoctor = await _context.Doctores
                                                    .AnyAsync(x => x.CedulaIdentidad == doctor.CedulaIdentidad && x.Id != doctor.Id);

                if (ciExisteParaOtroDoctor)
                {
                    ModelState.AddModelError("CedulaIdentidad", "Ya existe otro doctor registrado con esta Cédula de Identidad.");
                }
            }

            if (string.IsNullOrWhiteSpace(doctor.PrimerApellido) && string.IsNullOrWhiteSpace(doctor.SegundoApellido))
            {
                ModelState.AddModelError("PrimerApellido", "Debe ingresar al menos un apellido.");
                ModelState.AddModelError("SegundoApellido", "Debe ingresar al menos un apellido.");
            }

            if (doctor.IdEspecialidad == 0)
            {
                ModelState.AddModelError("IdEspecialidad", "Debe seleccionar una especialidad.");
            }
            if (doctor.Celular.ToString().Length != 8)
            {
                ModelState.AddModelError("Celular", "El número de celular debe ser de 8 dígitos.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    _context.Entry(doctor).Property(x => x.FechaRegistro).IsModified = false;
                    _context.Entry(doctor).Property(x => x.UsuarioRegistro).IsModified = false;
                    _context.Entry(doctor).Property(x => x.Estado).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre", doctor.IdEspecialidad);
            return View(doctor);
        }

        // GET: Doctores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctores
                .Include(d => d.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctores.FindAsync(id);
            if (doctor != null)
            {
                doctor.Estado = -1;
                doctor.UsuarioRegistro = User.Identity.Name;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctores.Any(e => e.Id == id);
        }
    }
}
