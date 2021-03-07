using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bas.website.Service;
using System.ComponentModel;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using bas.website.Models.Data;

namespace bas.website.Controllers
{
    
    public class HomeController : Controller
    {


        public BankDbContext db = new BankDbContext(ProjectConfig.Connection);

        /// <summary>
        /// Старинца: Предметная область
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [Route("/subject-area")]
        public IActionResult SubjectArea()
        {
            return View();
        }

    }

}
