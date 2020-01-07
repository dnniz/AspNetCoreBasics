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
        public IActionResult Index()
        {
            //var rep = new Reporte(_context.ObtenerDiccionarioEscuela());
            var model = new JsonDTO();
            //model.Asignaturas = new List<Asignatura>();
            //model.Asignaturas = rep.ListaAsignaturas();

            model.Alumnos = _context.Alumnos.ToList();

            return View(model);
        }
    }
}