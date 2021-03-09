using bas.website.Models.Data;
using bas.website.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bas.website.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public BankDbContext db = new BankDbContext(ProjectConfig.Connection);

        [Route("/credit/user/history")]
        public IActionResult History()
        {

            var user = int.Parse(HttpContext.Request.Cookies["UserID"]);

            var histiry = db.Bank_client_history
                .Include(h => h.Bank_client)
                .Include(h => h.Bank_currency)
                .Include(h => h.Bank_status_history)
                .Where(h => h.Clihis_client == user);

            return View(histiry.ToList());
        }
    }
}
