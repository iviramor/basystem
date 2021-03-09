using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bas.website.Controllers
{
    public class CalculatorOutController : Controller
    {
        /// <summary>
        /// Вывод параметров калькулятора
        /// </summary>
        /// <param name="sum"> Сумма кредита/займа </param>
        /// <param name="cur">Валюта кредита </param>
        /// <param name="pur">Информация о целях кредита(вычетаем)</param>
        /// <param name="rate">Процентная ставка</param>
        /// <param name="ddlm">Срок займа в месяцах</param>
        /// <param name="sdate">Дата выдачи</param>
        /// <param name="tpay">Порядок погашения</param>
        /// <param name="perpay">Периодичность погашения</param>
        /// <param name="fdate">Сроки досрочного погашения</param>
        /// <returns></returns>

        [Route("/credit/calculator/out/{sum, cur, pur, rate, ddlm, sdate, tpay, perpay, fdate, arr?}")]
        public IActionResult CreditCalcOut(decimal sum, string cur, decimal pur, decimal rate, int ddlm, string sdate, char tpay, string perpay, string fdate, string[] arr)
        {
            ViewBag.Sum = sum;
            ViewBag.Cur = cur;
            ViewBag.Rate = rate;
            ViewBag.Ddl = ddlm;
            ViewBag.Arr = arr;

            string[] subs = sdate.Split('-');

            DateTime date = new DateTime(int.Parse(subs[0]), int.Parse(subs[1]), int.Parse(subs[2])).Date;

            ViewBag.Sdate = date.ToString("dd / MM / yyyy");
            ViewBag.Fdate = date.AddMonths(ddlm).ToString("dd / MM / yyyy");
            ViewBag.Type = date.GetType();


            var tb = AnnuityTable(sum, rate, ddlm, date, perpay);
            ViewBag.Table = AnnuityTable(sum, rate, ddlm, date, perpay);
            
            return View();
        }



        private DataTable AnnuityTable(decimal sum, decimal rate, int ddlm, DateTime date, string perpay)
        {
            /// Создания таблицы
            DataTable At = new DataTable("AnnuityTable");
            DataColumn column;
            DataRow row;

            /// Создание 0 столбца с Номером платежа
            column = new DataColumn("Numb", Type.GetType("System.Int32"));
            At.Columns.Add(column);

            /// Создание 1 столбца с датой
            column = new DataColumn("Date", Type.GetType("System.DateTime"));
            At.Columns.Add(column);

            /// Создание 2 столбца с платежем
            column = new DataColumn("Paid", Type.GetType("System.Decimal"));
            At.Columns.Add(column);

            /// Создание 3 столбца с Телом займа
            column = new DataColumn("BodyPaid", Type.GetType("System.Decimal"));
            At.Columns.Add(column);

            /// Создание 4 столбца с Процентами
            column = new DataColumn("PercentPaid", Type.GetType("System.Decimal"));
            At.Columns.Add(column);

            /// Создание 5 столбца с Остатоком платежа
            column = new DataColumn("OffPaid", Type.GetType("System.Decimal"));
            At.Columns.Add(column);


            row = At.NewRow();

            row["Date"] = date;
            row["Paid"] = 0;
            row["BodyPaid"] = 0;
            row["PercentPaid"] = 0;
            row["OffPaid"] = sum;

            At.Rows.Add(row);

            var _I = rate / 100 / 12;

            var Paid = sum * (_I + _I / (( (decimal)Math.Pow((1 + (double)_I), ddlm)) - 1));

            for (int m = 1; m <= ddlm; ++m)
            {
                row = At.NewRow();
                row["Numb"] = m;
                row["Date"] = date.AddMonths(m);
                row["Paid"] = Math.Round(Paid, 2);

                var offP = (dynamic) At.Rows[m - 1][5];

                decimal percO = offP * _I;

                row["BodyPaid"] = Math.Round(Paid - percO, 2);
                row["PercentPaid"] = Math.Round(percO, 2);
                row["OffPaid"] = (Math.Round(offP - (Paid - percO), 2) < 0) ? 0 : Math.Round(offP - (Paid - percO));

                At.Rows.Add(row);
            }

            return At;
        }

    }
}
