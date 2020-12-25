using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }
    }
}
