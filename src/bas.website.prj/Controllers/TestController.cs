using bas.website.Models.Data;
using bas.website.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bas.website.Controllers
{
    public class TestController : Controller
    {
        public BankDbContext db = new (ProjectConfig.Connection);



        [Route("/test")]
        public IActionResult Test()
        {
            var company = db.Bank_client.Include(x => x.Bank_client_company);
            return View(company.ToList());
        }

        [Route("/test/2")]
        public IActionResult Test2()
        {
            var company = db.Bank_client_history.Include(c => c.Bank_client).ToList();
            return View(company);
        }
    }
}
