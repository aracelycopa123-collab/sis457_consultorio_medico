using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sis457ConsultorioMedico.Models;

namespace Sis457ConsultorioMedico.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public PacientesController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index(string buscar)
        {
            var query = _context.Pacientes
                        .Where(x => x.Estado != -1)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.Trim().ToLower();

                query = query.Where(p =>
                    p.CedulaIdentidad.Contains(buscar) ||
                    p.Nombres.ToLower().Contains(buscar) ||
                    p.PrimerApellido.ToLower().Contains(buscar) ||
                    p.SegundoApellido.ToLower().Contains(buscar)
                );
            }

            return View(await query.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            paciente.UsuarioRegistro = User.Identity.Name;
            paciente.FechaRegistro = DateTime.Now;
            paciente.Estado = 1;

            if (!string.IsNullOrWhiteSpace(paciente.CedulaIdentidad))
            {
                bool ciExiste = await _context.Pacientes
                                    .AnyAsync(x => x.CedulaIdentidad == paciente.CedulaIdentidad);

                if (ciExiste)
                {
                    ModelState.AddModelError("CedulaIdentidad", "Ya existe un paciente registrado con esta Cédula de Identidad.");
                }
            }
            if (string.IsNullOrWhiteSpace(paciente.PrimerApellido) && string.IsNullOrWhiteSpace(paciente.SegundoApellido))
            {
                ModelState.AddModelError("PrimerApellido", "Debe ingresar al menos un apellido.");
                ModelState.AddModelError("SegundoApellido", "Debe ingresar al menos un apellido.");
            }
            if (paciente.Celular.ToString().Length != 8)
            {
                ModelState.AddModelError("Celular", "El número de celular debe ser de 8 dígitos.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CedulaIdentidad,Nombres,PrimerApellido,SegundoApellido,FechaNacimiento,Direccion,Celular,UsuarioRegistro,FechaRegistro,Estado")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }
            if (!string.IsNullOrWhiteSpace(paciente.CedulaIdentidad))
            {
                bool ciExisteParaOtroPaciente = await _context.Pacientes
                                                    .AnyAsync(x => x.CedulaIdentidad == paciente.CedulaIdentidad && x.Id != paciente.Id);

                if (ciExisteParaOtroPaciente)
                {
                    ModelState.AddModelError("CedulaIdentidad", "Ya existe otro paciente registrado con esta Cédula de Identidad.");
                }
            }

            if (string.IsNullOrWhiteSpace(paciente.PrimerApellido) && string.IsNullOrWhiteSpace(paciente.SegundoApellido))
            {
                ModelState.AddModelError("PrimerApellido", "Debe ingresar al menos un apellido.");
                ModelState.AddModelError("SegundoApellido", "Debe ingresar al menos un apellido.");
            }

            if (paciente.Celular.ToString().Length != 8)
            {
                ModelState.AddModelError("Celular", "El número de celular debe ser de 8 dígitos.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    _context.Entry(paciente).Property(x => x.FechaRegistro).IsModified = false;
                    _context.Entry(paciente).Property(x => x.UsuarioRegistro).IsModified = false;
                    _context.Entry(paciente).Property(x => x.Estado).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                paciente.Estado = -1;
                paciente.UsuarioRegistro = User.Identity.Name;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult BuscarPorCI(string ci)
        {
            var paciente = _context.Pacientes
                .Where(p => p.CedulaIdentidad == ci)
                .Select(p => new
                {
                    Id = p.Id,
                    NombreCompleto = p.Nombres + " " + p.PrimerApellido + " " + p.SegundoApellido
                })
                .FirstOrDefault();

            return Json(paciente);
        }
    }
}
