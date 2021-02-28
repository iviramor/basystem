using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bas.website.Service;
using System.ComponentModel;

namespace bas.website.Controllers
{
    public class HomeController : Controller
    {



        /// <summary>
        /// Предметная область
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [Route("/subject-area")]
        public IActionResult SubjectArea()
        {
            return View();
        }


        /// <summary>
        /// Калькулятор
        /// </summary>
        /// <returns>Рендер страницы</returns>
        [Route("/credit/calculator")]
        public IActionResult CreditCalc()
        {

            return View();
        }


        /// <summary>
        /// Вывод параметров калькулятора
        /// </summary>
        /// <param name="sum"> Сумма кредита/займа </param>
        /// <param name="cur">Валюта кредита </param>
        /// <param name="pur">Информация о целях кредита(вычетаем)</param>
        /// <param name="rate">Процентная ставка</param>
        /// <param name="ddl">Срок займа</param>
        /// <param name="tdl">Срок займа в годах или месяцах</param>
        /// <param name="sdate">Дата выдачи</param>
        /// <param name="tpay">Порядок погашения</param>
        /// <param name="perpay">Периодичность погашения</param>
        /// <param name="fdate">Сроки досрочного погашения</param>
        /// <returns></returns>

        [Route("/credit/calculator/out/{sum, cur, pur, rate, ddl, tdl, sdate, tpay, perpay, fdate?}")]
        public IActionResult CreditCalcOut(int sum, string cur, float pur, float rate, int ddl, string tdl, string sdate, char tpay, string perpay, string fdate)
        {
            ViewBag.Sum = sum;
            ViewBag.Cur = cur;
            ViewBag.Tdl = tdl;
    
            return View();
        }



    }
}
