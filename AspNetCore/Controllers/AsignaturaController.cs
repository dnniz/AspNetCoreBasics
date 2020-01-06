using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Models;
using CSharpNetCore.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class AsignaturaController : Controller
    {
        public IActionResult Index()
        {
            var model = new JsonDTO();
            model.Asignaturas = new List<Asignatura>();


            return View();
        }
    }
}