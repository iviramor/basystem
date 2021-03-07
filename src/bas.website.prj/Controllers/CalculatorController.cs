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
using System.IO;

namespace bas.website.Controllers
{
    public class CalculatorController : Controller
    {
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public BankDbContext db = new BankDbContext(ProjectConfig.Connection);



        /// <summary>
        /// Калькулятор
        /// </summary>
        /// <returns>Рендер страницы</returns>

        [Route("/credit/calculator")]
        public IActionResult CreditCalc()
        {

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Authentication = true;
            }
            else
            {

                ViewBag.Authentication = false;
            }

            return View();
        }



        [HttpPost]
        [Route("/credit/calculator")]
        public async Task<IActionResult> CreditCalcAsync(LoginViewModel model)
        {

            var user = await db.Bank_client
                .SingleOrDefaultAsync(x => x.Client_login == model.UserLogin && x.Client_password == model.Password);

            if (user == null)
            {
                ModelState.TryAddModelError("", "Пользователь не найден!");
                ViewBag.Authentication = false;
                return View();
            }

            HttpContext.Response.Cookies.Append("UserName", user.Client_name);
            HttpContext.Response.Cookies.Append("UserSurname", user.Client_surname);

            if (user.Client_login == "il")
            {
                HttpContext.Response.Cookies.Append("UserCredStatus", "normally");
                HttpContext.Response.Cookies.Append("UserPercent", "0.8");
            }
            if (user.Client_login == "ivi")
            {
                HttpContext.Response.Cookies.Append("UserCredStatus", "good");
                HttpContext.Response.Cookies.Append("UserPercent", "0.3");
            }


            var claims = new List<Claim>
            {
                new Claim("Name", user.Client_login),
            };


            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect("/credit/calculator");
        }



        /// <summary>
        /// Выйти из аккаунта
        /// </summary>
        /// <returns>Перенаправление</returns>
        public IActionResult CreditCalcLogOff()
        {
            HttpContext.Response.Cookies.Delete("UserName");
            HttpContext.Response.Cookies.Delete("UserSurname");
            HttpContext.Response.Cookies.Delete("UserPercent");
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/credit/calculator");
        }


        /// <summary>
        /// Вывод параметров калькулятора
        /// </summary>
        /// <param name="sum"> Сумма кредита/займа </param>
        /// <param name="cur">Валюта кредита </param>
        /// <param name="pur">Информация о целях кредита(вычетаем)</param>
        /// <param name="rate">Процентная ставка</param>
        /// <param name="ddl">Срок займа в месяцах</param>
        /// <param name="sdate">Дата выдачи</param>
        /// <param name="tpay">Порядок погашения</param>
        /// <param name="perpay">Периодичность погашения</param>
        /// <param name="fdate">Сроки досрочного погашения</param>
        /// <returns></returns>

        [Route("/credit/calculator/out/{sum, cur, pur, rate, ddl, sdate, tpay, perpay, fdate, arr?}")]
        public IActionResult CreditCalcOut(int sum, string cur, float pur, float rate, int ddlm, string sdate, char tpay, string perpay, string fdate, string[] arr)
        {
            ViewBag.Sum = sum;
            ViewBag.Cur = cur;
            ViewBag.Rate = rate;
            ViewBag.Ddl = ddlm;
            ViewBag.Arr = arr;

            string[] subs = sdate.Split('-');
            DateTime date = new DateTime(int.Parse(subs[0]), int.Parse(subs[1]), int.Parse(subs[2])).Date;
            ViewBag.Sdate = date.ToString("MM / dd / yyyy");
            ViewBag.Fdate = date.AddMonths(ddlm).ToString("dd / MM / yyyy");



            return View();
        }

    }

    public class LoginViewModel
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }

    }
}
