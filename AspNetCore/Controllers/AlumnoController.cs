using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Models;
using CSharpNetCore.Entidades;

namespace AspNetCore.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly EscuelaContext _context;

        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Alumno
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Alumnos.Include(a => a.Curso);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoModel = await _context.Alumnos
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumnoModel == null)
            {
                return NotFound();
            }

            return View(alumnoModel);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId");
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlumnoId,Nombre,CursoId,UniqueId")] Alumno alumnoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumnoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", alumnoModel.CursoId);
            return View(alumnoModel);
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoModel = await _context.Alumnos.FindAsync(id);
            if (alumnoModel == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "Nombre", alumnoModel.CursoId);
            return View(alumnoModel);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AlumnoId,Nombre,CursoId,UniqueId")] Alumno alumnoModel)
        {
            if (id != alumnoModel.AlumnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumnoModel.AlumnoId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", alumnoModel.CursoId);
            return View(alumnoModel);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumnoModel = await _context.Alumnos
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AlumnoId == id);
            if (alumnoModel == null)
            {
                return NotFound();
            }

            return View(alumnoModel);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var alumnoModel = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumnoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(string id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
    }
}
