using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bas.website.Service;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using bas.website.Models.Data;

namespace bas.website.Controllers
{
    public class CalculatorController : Controller
    {
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public BankDbContext db = new (ProjectConfig.Connection);



        /// <summary>
        /// Критерии оценки истории кредитов
        /// </summary>
        readonly Dictionary<decimal, string[]> PercentCrit = new () 
        {
            { 0, new string[2] { "0", "excellently" } },
            { (decimal)-0.3, new string[2] { "0.3", "good" } },
            { (decimal)-0.5, new string[2] { "0.5", "good" } },
            { (decimal)-0.7, new string[2] { "0.7", "good" } },
            { (decimal)-0.9, new string[2] { "0.9", "good" } },
            { (decimal)-1.1, new string[2] { "1.0", "normally" } },
            { (decimal)-1.3, new string[2] { "1.3", "normally" } },
            { (decimal)-1.5, new string[2] { "1.5", "normally" } },
            { (decimal)-1.7, new string[2] { "1.7", "normally" } },
            { (decimal)-1.9, new string[2] { "1.9", "normally" } },

        };


        /// <summary>
        /// Калькулятор
        /// </summary>
        /// <returns>Рендер страницы</returns>

        [Route("/credit/calculator")]
        public IActionResult CreditCalc()
        {
            /// Проверка авторизации пользователя
            if (User.Identity.IsAuthenticated) ViewBag.Authentication = true;
            else ViewBag.Authentication = false;

            return View();
        }


        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model">Модель Формы авторизации</param>
        /// <returns>Редирект на страницу калькулятора</returns>
        [HttpPost]
        [Route("/credit/calculator")]
        public async Task<IActionResult> CreditCalcAsync(LoginViewModel model)
        {
            /// Поиск пользователя
            var user = await db.Bank_client
                .SingleOrDefaultAsync(x => x.Client_login == model.UserLogin && x.Client_password == model.Password);


            /// Если пользователя нет в базе данных
            if (user == null)
            {
                ModelState.TryAddModelError("", "Пользователь не найден!");
                ViewBag.Authentication = false;
                return View();
            }


            /// Выборка всей истории клиента
            var history = db.Bank_client_history
                .Include(ch => ch.Bank_status_history)
                .Include(ch => ch.Bank_client)
                .Where(ch => ch.Clihis_client == user.Client_id);

            /// Запись результата истории кредита при чистой истории кредитов
            var CreditResult = new string[2] { "0", "excellently" };


            /// Если список истории кредитов не пуст
            if (history.Any()) CreditResult = GetIndiPercent(history);


            /// Запись данных в Cookie
            HttpContext.Response.Cookies.Append("UserName", user.Client_name);
            HttpContext.Response.Cookies.Append("UserSurname", user.Client_surname);
            HttpContext.Response.Cookies.Append("UserSex", user.Client_sex.ToString());
            HttpContext.Response.Cookies.Append("UserID", user.Client_id.ToString());
            HttpContext.Response.Cookies.Append("UserPercent", CreditResult[0]);
            HttpContext.Response.Cookies.Append("UserCredStatus", CreditResult[1]);


            /// Авторизация с помощью Клеймов
            var claims = new List<Claim> { new Claim("Name", user.Client_login) };
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
            /// Удаление Cookies из сайта
            HttpContext.Response.Cookies.Delete("UserName");
            HttpContext.Response.Cookies.Delete("UserSurname");
            HttpContext.Response.Cookies.Delete("UserPercent");
            HttpContext.Response.Cookies.Delete("UserCredStatus");
            HttpContext.Response.Cookies.Delete("UserID");
            HttpContext.SignOutAsync("Cookie");


            return Redirect("/credit/calculator");
        }


        /// <summary>
        /// Функция дающая расчет оценки кредитной истории
        /// </summary>
        /// <param name="client_id"> Индификатор пользователя </param>
        /// <param name="hist"> История клиента </param>
        /// <returns>Массив с оценкой</returns>
        private string[] GetIndiPercent(IQueryable<Bank_client_history> history)
        {
            /// Выходной массив
            string[] percent = new string[2];

            /// Сумма
            decimal sum = 0;

            /// Сумма статуса
            foreach (var r in history) sum += r.Bank_status_history.Status_value;


            /// Среднее значение
            decimal rang = sum / history.Count();


            /// Сравноение со словарем Критериев для выставления рейтинка истории
            foreach (var row in PercentCrit)
            {
                /// Сравнение Ключем(Баллом) с подсчитанным рейтингом
                if (row.Key <= rang)
                {
                    percent = row.Value;
                    break;
                }
                else
                {
                    percent[0] = "2.3";
                    percent[1] = "satisfactory";
                }
            }


            return percent;
        }

    }


    /// <summary>
    /// Модель Формы авторизации
    /// </summary>
    public class LoginViewModel
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }

    }

}
