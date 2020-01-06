using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class EscuelaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}