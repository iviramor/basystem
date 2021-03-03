using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bas.website.Service;
using System.ComponentModel;
using System.Data;

namespace bas.website.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Sum(int a, int b)
        {
            
            return a + b; 
        }

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
        /// <param name="ddl">Срок займа в месяцах</param>
        /// <param name="sdate">Дата выдачи</param>
        /// <param name="tpay">Порядок погашения</param>
        /// <param name="perpay">Периодичность погашения</param>
        /// <param name="fdate">Сроки досрочного погашения</param>
        /// <returns></returns>

        [Route("/credit/calculator/out/{sum, cur, pur, rate, ddl, sdate, tpay, perpay, fdate?}")]
        public IActionResult CreditCalcOut(int sum, string cur, float pur, float rate, int ddlm, string sdate, char tpay, string perpay, string fdate)
        {
            ViewBag.Sum = sum;
            ViewBag.Cur = cur;
            ViewBag.Rate = rate;
            ViewBag.Ddl = ddlm;


            string[] subs = sdate.Split('-');
            DateTime date = new DateTime(int.Parse(subs[0]), int.Parse(subs[1]), int.Parse(subs[2])).Date;
            ViewBag.Sdate = date.ToString("MM / dd / yyyy");
            ViewBag.Fdate = date.AddMonths(ddlm).ToString("dd / MM / yyyy");

            

            return View();
        }

    }
}
