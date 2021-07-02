using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.API.Controllers
{
    public class WeeklyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
