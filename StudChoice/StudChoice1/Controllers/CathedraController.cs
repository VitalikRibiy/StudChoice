using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudChoice.Controllers
{
    public class CathedraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}