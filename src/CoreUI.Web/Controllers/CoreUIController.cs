using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreUI.Web.Controllers
{
    [Route("CoreUI")]
    public class CoreUIController : Controller
    {
        [Route("{view=Index}")]
        public IActionResult Index(string view)
        {
            ViewData["Title"] = "Sistema de hoteles";

            return View(view);
        }
    }
}
