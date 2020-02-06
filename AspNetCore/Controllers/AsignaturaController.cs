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
    public class AsignaturaController : Controller
    {
        private readonly EscuelaContext _context;

        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Asignatura
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Asignaturas.Include(a => a.Curso);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: Asignatura/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturaModel = await _context.Asignaturas
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AsignaturaId == id);
            if (asignaturaModel == null)
            {
                return NotFound();
            }

            return View(asignaturaModel);
        }

        // GET: Asignatura/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId");
            return View();
        }

        // POST: Asignatura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignaturaId,CursoId,UniqueId,Nombre")] Asignatura asignaturaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignaturaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", asignaturaModel.CursoId);
            return View(asignaturaModel);
        }

        // GET: Asignatura/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturaModel = await _context.Asignaturas.FindAsync(id);
            if (asignaturaModel == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "Nombre", asignaturaModel.CursoId);
            return View(asignaturaModel);
        }

        // POST: Asignatura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AsignaturaId,CursoId,UniqueId,Nombre")] Asignatura asignaturaModel)
        {
            if (id != asignaturaModel.AsignaturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaExists(asignaturaModel.AsignaturaId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "CursoId", "CursoId", asignaturaModel.CursoId);
            return View(asignaturaModel);
        }

        // GET: Asignatura/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignaturaModel = await _context.Asignaturas
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(m => m.AsignaturaId == id);
            if (asignaturaModel == null)
            {
                return NotFound();
            }

            return View(asignaturaModel);
        }

        // POST: Asignatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asignaturaModel = await _context.Asignaturas.FindAsync(id);
            _context.Asignaturas.Remove(asignaturaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturaExists(string id)
        {
            return _context.Asignaturas.Any(e => e.AsignaturaId == id);
        }
    }
}
