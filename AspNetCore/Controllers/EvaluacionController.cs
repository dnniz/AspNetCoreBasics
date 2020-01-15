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
    public class EvaluacionController : Controller
    {
        private readonly EscuelaContext _context;

        public EvaluacionController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Evaluacion
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Evaluaciones.Include(e => e.Alumno).Include(e => e.Asignatura);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: Evaluacion/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionModel = await _context.Evaluaciones
                .Include(e => e.Alumno)
                .Include(e => e.Asignatura)
                .FirstOrDefaultAsync(m => m.EvaluacionId == id);
            if (evaluacionModel == null)
            {
                return NotFound();
            }

            return View(evaluacionModel);
        }

        // GET: Evaluacion/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "AlumnoId");
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "AsignaturaId");
            return View();
        }

        // POST: Evaluacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvaluacionId,AlumnoId,AsignaturaId,Nota,Promedio,UniqueId,Nombre")] Evaluacion evaluacionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluacionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "AlumnoId", evaluacionModel.AlumnoId);
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "AsignaturaId", evaluacionModel.AsignaturaId);
            return View(evaluacionModel);
        }

        // GET: Evaluacion/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionModel = await _context.Evaluaciones.FindAsync(id);
            if (evaluacionModel == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "AlumnoId", evaluacionModel.AlumnoId);
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "AsignaturaId", evaluacionModel.AsignaturaId);
            return View(evaluacionModel);
        }

        // POST: Evaluacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EvaluacionId,AlumnoId,AsignaturaId,Nota,Promedio,UniqueId,Nombre")] Evaluacion evaluacionModel)
        {
            if (id != evaluacionModel.EvaluacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluacionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluacionExists(evaluacionModel.EvaluacionId))
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
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "AlumnoId", "AlumnoId", evaluacionModel.AlumnoId);
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "AsignaturaId", evaluacionModel.AsignaturaId);
            return View(evaluacionModel);
        }

        // GET: Evaluacion/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionModel = await _context.Evaluaciones
                .Include(e => e.Alumno)
                .Include(e => e.Asignatura)
                .FirstOrDefaultAsync(m => m.EvaluacionId == id);
            if (evaluacionModel == null)
            {
                return NotFound();
            }

            return View(evaluacionModel);
        }

        // POST: Evaluacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var evaluacionModel = await _context.Evaluaciones.FindAsync(id);
            _context.Evaluaciones.Remove(evaluacionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluacionExists(string id)
        {
            return _context.Evaluaciones.Any(e => e.EvaluacionId == id);
        }
    }
}
