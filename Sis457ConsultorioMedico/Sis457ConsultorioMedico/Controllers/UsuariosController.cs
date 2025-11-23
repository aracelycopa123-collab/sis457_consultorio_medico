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
    public class UsuariosController : Controller
    {
        private readonly FinalConsultorioMedicoContext _context;

        public UsuariosController(FinalConsultorioMedicoContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var finalConsultorioMedicoContext = _context.Usuarios.Include(u => u.IdDoctorNavigation);
            return View(await finalConsultorioMedicoContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create(int idDoctor)
        {
            ViewData["IdDoctor"] = new SelectList(_context.Doctores, "Id", "Nombre");
            var doctor = _context.Doctores
                .Where(d => d.Id == idDoctor)
                .Select(d => new { d.Id, NombreCompleto = d.Nombres + " " + d.PrimerApellido + " " + d.SegundoApellido })
                .FirstOrDefault();

            if (doctor == null)
            {
                return NotFound();
            }

            ViewBag.NombreDoctor = doctor.NombreCompleto;
            ViewBag.IdDoctor = doctor.Id;

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            usuario.UsuarioRegistro = User.Identity.Name;
            usuario.FechaRegistro = DateTime.Now;
            usuario.Estado = 1;
            usuario.Clave = AccountController.Encrypt(usuario.Clave);
            bool TieneUsuario = await _context.Usuarios
                    .AnyAsync(u => u.IdDoctor == usuario.IdDoctor);

            if (TieneUsuario)
            {
                ModelState.AddModelError("Usuario1", "Este doctor ya tiene un usuario asignado.");
            }
            bool usuarioExiste = await _context.Usuarios
                    .AnyAsync(u => u.Usuario1 == usuario.Usuario1);

            if (usuarioExiste)
            {
                ModelState.AddModelError("Usuario1", "El Usuario ya está en uso.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Doctores");
            }

            var doctor = _context.Doctores
                .Where(d => d.Id == usuario.IdDoctor)
                .Select(d => d.Nombres + " " + d.PrimerApellido + " " + d.SegundoApellido)
                .FirstOrDefault();
            ViewBag.NombreDoctor = doctor;
            ViewBag.NombreDoctor = await _context.Doctores
                .Where(d => d.Id == usuario.IdDoctor)
                .Select(d => d.Nombres + " " + d.PrimerApellido + " " + d.SegundoApellido)
                 .FirstOrDefaultAsync();

            ViewBag.IdDoctor = usuario.IdDoctor; 
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdDoctor"] = new SelectList(_context.Doctores, "Id", "CedulaIdentidad", usuario.IdDoctor);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDoctor,Usuario1,Clave,UsuarioRegistro,FechaRegistro,Estado")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["IdDoctor"] = new SelectList(_context.Doctores, "Id", "CedulaIdentidad", usuario.IdDoctor);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdDoctorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
