using Microsoft.AspNetCore.Mvc;
using bas.website.Service;
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
