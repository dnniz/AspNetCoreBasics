using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Models;
using CSharpNetCore.App;
using CSharpNetCore.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class AsignaturaController : Controller
    {
        private readonly EscuelaContext _context;
        public AsignaturaController(EscuelaContext context )
        {
            _context = context;
        }
        [Route("Asignatura/Index/{asignaturaId}")]
        [Route("Asignatura/Index")]
        public IActionResult Index(string asignaturaId)
        {
            var model = new JsonDTO();

            //model.Alumnos = _context.Alumnos.ToList();
            model.Asignaturas = _context.Asignaturas
                                .Where(x => string.IsNullOrEmpty(asignaturaId) || x.AsignaturaId == asignaturaId )
                                .ToList();

            return View(model);
        }
    }
}