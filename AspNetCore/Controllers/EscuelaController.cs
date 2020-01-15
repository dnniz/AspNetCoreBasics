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
    public class EscuelaController : Controller
    {
        private readonly EscuelaContext _context;

        public EscuelaController(EscuelaContext context)
        {
            _context = context;
        }

        // GET: Escuela
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escuelas.ToListAsync());
        }

        // GET: Escuela/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaModel = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.EscuelaId == id);
            if (escuelaModel == null)
            {
                return NotFound();
            }

            return View(escuelaModel);
        }

        // GET: Escuela/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escuela/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscuelaId,AñoCreación,TipoEscuela,CantAlumnos,Direccion,UniqueId,Nombre")] Escuela escuelaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escuelaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escuelaModel);
        }

        // GET: Escuela/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaModel = await _context.Escuelas.FindAsync(id);
            if (escuelaModel == null)
            {
                return NotFound();
            }
            return View(escuelaModel);
        }

        // POST: Escuela/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EscuelaId,AñoCreación,TipoEscuela,CantAlumnos,Direccion,UniqueId,Nombre")] Escuela escuelaModel)
        {
            if (id != escuelaModel.EscuelaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escuelaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscuelaExists(escuelaModel.EscuelaId))
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
            return View(escuelaModel);
        }

        // GET: Escuela/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escuelaModel = await _context.Escuelas
                .FirstOrDefaultAsync(m => m.EscuelaId == id);
            if (escuelaModel == null)
            {
                return NotFound();
            }

            return View(escuelaModel);
        }

        // POST: Escuela/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var escuelaModel = await _context.Escuelas.FindAsync(id);
            _context.Escuelas.Remove(escuelaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscuelaExists(string id)
        {
            return _context.Escuelas.Any(e => e.EscuelaId == id);
        }
    }
}
