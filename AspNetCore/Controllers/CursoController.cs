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
    public class CursoController : Controller
    {
        private readonly EscuelaContext _context;

        public CursoController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            var escuelaContext = _context.Cursos.Include(c => c.Escuela);
            return View(await escuelaContext.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos
                .Include(c => c.Escuela)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (cursoModel == null)
            {
                return NotFound();
            }

            return View(cursoModel);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "EscuelaId", "EscuelaId");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursoId,Jornada,EscuelaId,Direccion,UniqueId,Nombre")] Curso cursoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "EscuelaId", "EscuelaId", cursoModel.EscuelaId);
            return View(cursoModel);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos.FindAsync(id);
            if (cursoModel == null)
            {
                return NotFound();
            }
            ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "EscuelaId", "Nombre", cursoModel.EscuelaId);
            return View(cursoModel);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CursoId,Jornada,EscuelaId,Direccion,UniqueId,Nombre")] Curso cursoModel)
        {
            if (id != cursoModel.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(cursoModel.CursoId))
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
            ViewData["EscuelaId"] = new SelectList(_context.Escuelas, "EscuelaId", "EscuelaId", cursoModel.EscuelaId);
            return View(cursoModel);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos
                .Include(c => c.Escuela)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (cursoModel == null)
            {
                return NotFound();
            }

            return View(cursoModel);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(cursoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(string id)
        {
            return _context.Cursos.Any(e => e.CursoId == id);
        }
    }
}
