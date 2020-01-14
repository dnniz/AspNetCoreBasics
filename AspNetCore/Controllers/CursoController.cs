using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Models;
using CSharpNetCore.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class CursoController : Controller
    {
        private readonly EscuelaContext _context;

        public CursoController(EscuelaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new JsonDTO();

            model.Cursos = _context.Cursos.ToList();


            return View(model);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Curso curso)
        {

            return View();
        }
    }
}