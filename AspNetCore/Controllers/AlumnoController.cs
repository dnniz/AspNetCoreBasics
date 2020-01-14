using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Models;
using CSharpNetCore.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly EscuelaContext _context;

        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new JsonDTO();

            model.Alumnos = _context.Alumnos.ToList();


            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Alumnos.Add(alumno);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Delete(string alumnoId)
        {
            var model = new JsonDTO();
            var alumDB = _context.Alumnos.Find(alumnoId);
            model.Alumno = alumDB;
            (model.Exito) = (alumDB != null);
            _context.Alumnos.Remove(alumDB);
            _context.SaveChanges();

            return Json(Url.Action("Index", "Alumno"));
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete([FromBody]JsonDTO alumno)
        {
            var model = new JsonDTO();
            var alumDB = _context.Alumnos.Find(alumno.Id);
            model.Alumno = alumDB;
            (model.Exito) = (alumDB != null);
            _context.Alumnos.Remove(alumDB);
            _context.SaveChanges();

            return Json(model);
        }
    }
}