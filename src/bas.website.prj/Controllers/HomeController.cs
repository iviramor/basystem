using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bas.website.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("twoPage")]
        public IActionResult AboutUs()
        {
            
            return View();
        }

        [Route("threePage")]
        public IActionResult Shop()
        {
            return View();
        }

    }
}
