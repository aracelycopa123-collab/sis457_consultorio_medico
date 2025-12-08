using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        // Añadidos parámetros para búsqueda: q, idEspecialidad, idDoctor, fecha (yyyy-MM-dd)
        public async Task<IActionResult> Index(string q, int? idEspecialidad, int? idDoctor, string fecha)
        {
            // Guardar valores para la vista (mantener selección)
            ViewBag.Search = q ?? string.Empty;
            ViewBag.SelectedEspecialidad = idEspecialidad;
            ViewBag.SelectedDoctor = idDoctor;
            ViewBag.Fecha = fecha ?? string.Empty;

            ViewBag.Especialidades = _context.Especialidades.Where(e => e.Estado == 1).ToList();
            ViewBag.Doctores = _context.Doctores.Where(d => d.Estado == 1).ToList();

            // Guardar valores para la vista (mantener selección)
            ViewBag.Search = q ?? string.Empty;
            ViewBag.SelectedEspecialidad = idEspecialidad;
            ViewBag.SelectedDoctor = idDoctor;
            ViewBag.Fecha = fecha ?? string.Empty;

            ViewBag.Especialidades = _context.Especialidades.Where(e => e.Estado == 1).ToList();
            ViewBag.Doctores = _context.Doctores.Where(d => d.Estado == 1).ToList();

            // Guardar valores para la vista (mantener selección)
            ViewBag.Search = q ?? string.Empty;
            ViewBag.SelectedEspecialidad = idEspecialidad;
            ViewBag.SelectedDoctor = idDoctor;
            ViewBag.Fecha = fecha ?? string.Empty;

            ViewBag.Especialidades = _context.Especialidades.Where(e => e.Estado == 1).ToList();
            ViewBag.Doctores = _context.Doctores.Where(d => d.Estado == 1).ToList();

            var hoy = DateOnly.FromDateTime(DateTime.Now);

            var citas = await _context.Cita
                .Where(c => c.Fecha >= hoy && c.Estado == 1)
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.Pagos)
                .OrderBy(c => c.Fecha)
                .ToListAsync();

            return View(citas);
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Cita
                .Include(c => c.Pagos)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cita == null) return NotFound();

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            CargarDatosVista();
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita cita)
        {
            cita.UsuarioRegistro = User.Identity.Name;
            cita.FechaRegistro = DateTime.Now;
            cita.Estado = 1;

            // Validación de horario ocupado
            bool citaExistente = _context.Cita.Any(c =>
                c.IdDoctor == cita.IdDoctor &&
                c.Fecha == cita.Fecha &&
                c.Hora == cita.Hora &&
                c.Estado == 1
            );

            if (citaExistente)
            {
                ModelState.AddModelError("", "Ese horario ya está ocupado por el Dr.");
                CargarDatosVista();
                return View(cita);
            }

            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CargarDatosVista();
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Cita
                .Include(c => c.IdPacienteNavigation)
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null) return NotFound();

            CargarDatosVista();
            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDoctor,IdPaciente,IdEspecialidad,Fecha,Hora,UsuarioRegistro,FechaRegistro,Estado")] Cita cita)
        {
            if (id != cita.Id) return NotFound();

            // Validación de horario ocupado
            bool citaExistente = _context.Cita.Any(c =>
                c.IdDoctor == cita.IdDoctor &&
                c.Fecha == cita.Fecha &&
                c.Hora == cita.Hora &&
                c.Estado == 1 &&
                c.Id != cita.Id
            );

            if (citaExistente)
            {
                ModelState.AddModelError("", "Ese horario ya está ocupado para el Dr.");
                CargarDatosVista();
                return View(cita);
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
                    if (!CitaExists(cita.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            CargarDatosVista();
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Cita
                .Include(c => c.IdDoctorNavigation)
                .Include(c => c.IdEspecialidadNavigation)
                .Include(c => c.IdPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cita == null) return NotFound();

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

            if (cita == null) return NotFound();

            if (cita.Pagos != null && cita.Pagos.Any())
            {
                TempData["Error"] = "La cita ya está pagada, no se puede eliminar.";
                return View("Delete", cita);
            }

            cita.Estado = -1;
            cita.UsuarioRegistro = User.Identity.Name;

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

        [HttpGet]
        public IActionResult ObtenerHorarioDisponible(int? idDoctor, int? idEspecialidad, string fecha, int? excludeCitaId)
        {
            if (string.IsNullOrWhiteSpace(fecha)) return Json(new object[0]);

            if (!DateOnly.TryParse(fecha, out var fechaDate))
            {
                if (!DateOnly.TryParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaDate))
                    return Json(new object[0]);
            }

            int diaSemana = (int)fechaDate.DayOfWeek; // 0 = domingo ... 6 = sábado

            // Si se consulta por doctor -> obtener sus horarios para el día
            if (idDoctor.HasValue && idDoctor.Value > 0)
            {
                var horarios = _context.DoctorHorario
                    .Where(h => h.IdDoctor == idDoctor.Value && h.DiaSemana == diaSemana && h.Estado == 1)
                    .Select(h => h.Hora)
                    .Distinct()
                    .OrderBy(h => h)
                    .ToList();

                var result = horarios
                    .Select(h =>
                    {
                        var ocupada = _context.Cita.Any(c =>
                            c.IdDoctor == idDoctor.Value &&
                            c.Fecha == fechaDate &&
                            c.Hora == h &&
                            c.Estado == 1 &&
                            (!excludeCitaId.HasValue || c.Id != excludeCitaId.Value)
                        );

                        return new
                        {
                            time = h.ToString("HH:mm"),
                            available = !ocupada,
                            bookedDoctors = ocupada ? 1 : 0,
                            availableDoctors = ocupada ? 0 : 1
                        };
                    })
                    .ToList();

                return Json(result);
            }

            // Si no hay doctor -> agregamos por especialidad: combinar horarios de todos los doctores de la especialidad
            if (idEspecialidad.HasValue && idEspecialidad.Value > 0)
            {
                var doctorIds = _context.Doctores
                    .Where(d => d.Estado == 1 && d.IdEspecialidad == idEspecialidad.Value)
                    .Select(d => d.Id)
                    .ToList();

                if (!doctorIds.Any()) return Json(new object[0]);

                var horariosPorDoctor = _context.DoctorHorario
                    .Where(h => doctorIds.Contains(h.IdDoctor) && h.DiaSemana == diaSemana && h.Estado == 1)
                    .GroupBy(h => h.Hora)
                    .Select(g => new { Hora = g.Key, DoctorsCount = g.Select(x => x.IdDoctor).Distinct().Count() })
                    .OrderBy(x => x.Hora)
                    .ToList();

                var result = horariosPorDoctor
                    .Select(x =>
                    {
                        var citasReservadas = _context.Cita
                            .Where(c => c.Fecha == fechaDate && c.Hora == x.Hora && c.Estado == 1 && doctorIds.Contains(c.IdDoctor))
                            .Where(c => !excludeCitaId.HasValue || c.Id != excludeCitaId.Value)
                            .Count();

                        var availableCount = Math.Max(0, x.DoctorsCount - citasReservadas);

                        return new
                        {
                            time = x.Hora.ToString("HH:mm"),
                            available = availableCount > 0,
                            availableDoctors = availableCount,
                            bookedDoctors = citasReservadas
                        };
                    })
                    .ToList();

                return Json(result);
            }

            return Json(new object[0]);
        }

        // NUEVO: devuelve array de horas ocupadas (["09:00","09:30"]) usado por la vista Edit/Create para deshabilitar opciones
        [HttpGet]
        public IActionResult ObtenerHorasOcupadas(string fecha, int? idDoctor, int? idEspecialidad, int? excludeCitaId)
        {
            if (string.IsNullOrWhiteSpace(fecha)) return Json(new string[0]);

            if (!DateOnly.TryParse(fecha, out var fechaDate))
            {
                if (!DateOnly.TryParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaDate))
                    return Json(new string[0]);
            }

            int diaSemana = (int)fechaDate.DayOfWeek;
            var ocupadas = new List<string>();

            // lista por defecto de horas (coincide con las de la vista)
            var horasPorDefecto = new[] { "09:00","09:30","10:00","10:30","11:00","11:30","12:00","12:30","13:00","13:30","14:00","14:30","15:00","15:30","16:00","16:30","17:00" }
                .Select(s => TimeOnly.ParseExact(s, "HH:mm"))
                .ToList();

            // Por doctor: marcamos las horas que ese doctor ya tiene reservadas.
            if (idDoctor.HasValue && idDoctor.Value > 0)
            {
                var horarios = _context.DoctorHorario
                    .Where(h => h.IdDoctor == idDoctor.Value && h.DiaSemana == diaSemana && h.Estado == 1)
                    .Select(h => h.Hora)
                    .Distinct()
                    .OrderBy(h => h)
                    .ToList();

                // Si no hay horarios definidos en DoctorHorario, usar la lista por defecto
                if (!horarios.Any())
                {
                    horarios = horasPorDefecto;
                }

                foreach (var h in horarios)
                {
                    var existe = _context.Cita.Any(c =>
                        c.IdDoctor == idDoctor.Value &&
                        c.Fecha == fechaDate &&
                        c.Hora == h &&
                        c.Estado == 1 &&
                        (!excludeCitaId.HasValue || c.Id != excludeCitaId.Value)
                    );

                    if (existe) ocupadas.Add(h.ToString("HH:mm"));
                }

                return Json(ocupadas);
            }

            // Por especialidad: mantengo la lógica existente (marcar sólo si TODOS los doctores con ese horario están ocupados)
            if (idEspecialidad.HasValue && idEspecialidad.Value > 0)
            {
                var doctorIds = _context.Doctores
                    .Where(d => d.Estado == 1 && d.IdEspecialidad == idEspecialidad.Value)
                    .Select(d => d.Id)
                    .ToList();

                if (!doctorIds.Any()) return Json(new string[0]);

                var horariosPorDoctor = _context.DoctorHorario
                    .Where(h => doctorIds.Contains(h.IdDoctor) && h.DiaSemana == diaSemana && h.Estado == 1)
                    .GroupBy(h => h.Hora)
                    .Select(g => new { Hora = g.Key, DoctorsCount = g.Select(x => x.IdDoctor).Distinct().Count() })
                    .OrderBy(x => x.Hora)
                    .ToList();

                foreach (var x in horariosPorDoctor)
                {
                    var citasReservadas = _context.Cita
                        .Where(c => c.Fecha == fechaDate && c.Hora == x.Hora && c.Estado == 1 && doctorIds.Contains(c.IdDoctor))
                        .Where(c => !excludeCitaId.HasValue || c.Id != excludeCitaId.Value)
                        .Count();

                    var availableCount = Math.Max(0, x.DoctorsCount - citasReservadas);

                    if (availableCount == 0)
                    {
                        ocupadas.Add(x.Hora.ToString("HH:mm"));
                    }
                }

                return Json(ocupadas);
            }

            return Json(new string[0]);
        }
    }
}
