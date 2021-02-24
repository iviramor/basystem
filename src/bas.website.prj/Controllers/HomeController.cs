﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bas.website.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/subject-area")]
        public IActionResult SubjectArea()
        {
            return View();
        }

        [Route("/credit/calculator")]
        public IActionResult CreditCalc()
        {

            return View();
        }

    }
}
