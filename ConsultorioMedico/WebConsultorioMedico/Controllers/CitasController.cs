using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public CitasController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }
        private void CargarDatosVista()
        {
            var doctores = _context.Doctores
                .Where(d => d.Estado == 1)
                .Include(d => d.IdEspecialidadNavigation)
                .ToList();

            ViewBag.Doctores = doctores;

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Where(p => p.Estado == 1)
                    .Select(p => new { p.Id, NombreCompleto = p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido }),
                "Id",
                "NombreCompleto"
            );
            ViewBag.Especialidades = _context.Especialidades.ToList();
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre");

        }


        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Now);

            var citas = await _context.Cita
                .Where(c => c.Fecha >= hoy && c.Estado == 1)
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.Pagos).OrderBy(c => c.Fecha)
                .ToListAsync();

            return View(citas);
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.Pagos)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            var doctores = _context.Doctores
     .Where(d => d.Estado == 1)
     .Include(d => d.IdEspecialidadNavigation)
     .ToList();

            Console.WriteLine("Doctores encontrados: " + doctores.Count);

            ViewBag.Doctores = doctores;
            /*ViewData["IdDoctor"] = new SelectList(
             _context.Doctores.Where(d => d.Estado == 1)
                 .Select(d => new { d.Id, NombreCompleto = d.Nombres + " " + d.PrimerApellido + " " + d.SegundoApellido }),
             "Id",
             "NombreCompleto"
            );*/

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Where(p => p.Estado == 1)
                    .Select(p => new { p.Id, NombreCompleto = p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido }),
                "Id",
                "NombreCompleto"
            );
            ViewBag.Especialidades = _context.Especialidades.ToList();
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita cita)
        {
            cita.UsuarioRegistro = User.Identity.Name;;
            cita.FechaRegistro = DateTime.Now;
            cita.Estado = 1;
            bool citaExistente = _context.Cita.Any(c =>
            c.IdPaciente == cita.IdPaciente &&
            c.Fecha == cita.Fecha &&
            c.Estado == 1);

            if (citaExistente)
            {
                ModelState.AddModelError("", "El paciente ya tiene una cita para esa fecha.");
                var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == cita.IdPaciente);
                ViewBag.NombrePaciente = paciente != null
                    ? $"{paciente.Nombres} {paciente.PrimerApellido} {paciente.SegundoApellido}"
                    : "";
                var doctor = _context.Doctores.Include(d => d.IdEspecialidadNavigation).FirstOrDefault(d => d.Id == cita.IdDoctor);
                ViewBag.NombreEspecialidad = doctor?.IdEspecialidadNavigation?.Nombre ?? "";
                CargarDatosVista();
                return View(cita);
            }
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var doctores = _context.Doctores
                .Where(d => d.Estado == 1)
                .Include(d => d.IdEspecialidadNavigation)
                .ToList();

            Console.WriteLine("Doctores encontrados: " + doctores.Count);

            ViewBag.Doctores = doctores;

            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Where(p => p.Estado == 1)
                    .Select(p => new { p.Id, NombreCompleto = p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido }),
                "Id",
                "NombreCompleto"
            );
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            ViewBag.Especialidades = _context.Especialidades.ToList();
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cita = await _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);

            //var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            var doctores = _context.Doctores
               .Where(d => d.Estado == 1)
               .Include(d => d.IdEspecialidadNavigation)
               .ToList();

            Console.WriteLine("Doctores encontrados: " + doctores.Count);

            ViewBag.Doctores = doctores;
            ViewData["IdPaciente"] = new SelectList(
                _context.Pacientes.Where(p => p.Estado == 1)
                    .Select(p => new { p.Id, NombreCompleto = p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido }),
                "Id",
                "NombreCompleto"
            );
            ViewBag.Especialidades = _context.Especialidades.ToList();
            ViewData["IdEspecialidad"] = new SelectList(_context.Especialidades, "Id", "Nombre");
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDoctor,IdPaciente,IdEspecialidad,Fecha,Hora,UsuarioRegistro,FechaRegistro,Estado")] Cita cita)
        {
            if (id != cita.Id)
            {
                return NotFound();
            }
            bool citaExistente = _context.Cita.Any(c =>
                c.IdPaciente == cita.IdPaciente &&
                c.Fecha == cita.Fecha &&
                c.Estado == 1 && c.Id != cita.Id);

            if (citaExistente)
            {
                ModelState.AddModelError("", "El paciente ya tiene una cita para esa fecha.");
                var citaCompleta = _context.Cita
                    .Include(c => c.IdPacienteNavigation)
                    .Include(c => c.IdEspecialidadNavigation)
                    .FirstOrDefault(c => c.Id == cita.Id);

                CargarDatosVista();
                return View(citaCompleta);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    _context.Entry(cita).Property(x => x.FechaRegistro).IsModified = false;
                    _context.Entry(cita).Property(x => x.UsuarioRegistro).IsModified = false;
                    _context.Entry(cita).Property(x => x.Estado).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.Id))
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
            CargarDatosVista();
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Cita
     .Include(c => c.Pagos)
     .Include(c => c.IdPacienteNavigation)
     .Include(c => c.IdDoctorNavigation)
         .ThenInclude(d => d.IdEspecialidadNavigation)
     .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                return NotFound();
            }

            if (cita.Pagos != null && cita.Pagos.Any())
            {
                TempData["Error"] = "La cita ya está pagada, no se puede eliminar.";
                return View("Delete", cita);
            }

            cita.Estado = -1;
            cita.UsuarioRegistro = User.Identity.Name;;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Cita.Any(e => e.Id == id);
        }
        [HttpGet]
        public IActionResult ObtenerDoctoresPorEspecialidad(int idEspecialidad)
        {
            var doctores = _context.Doctores
                .Include(d => d.IdEspecialidadNavigation)
                .Where(d => d.Estado == 1 && d.IdEspecialidad == idEspecialidad)
                .Select(d => new
                {
                    d.Id,
                    NombreCompleto = d.Nombres + " " + d.PrimerApellido + " " + d.SegundoApellido
                })
                .ToList();


            return Json(doctores);
        }
    }
}
