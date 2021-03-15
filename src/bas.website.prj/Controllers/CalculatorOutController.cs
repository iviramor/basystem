using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace bas.website.Controllers
{
    public class CalculatorOutController : Controller
    {

        DataTable At;
        DataColumn column;
        DataRow row;



        /// <summary>
        /// Вывод параметров калькулятора
        /// </summary>
        /// <param name="sum"> Сумма кредита/займа </param>
        /// <param name="cur">Валюта кредита </param>
        /// <param name="rate">Процентная ставка</param>
        /// <param name="ddlm">Срок займа в месяцах</param>
        /// <param name="sdate">Дата выдачи</param>
        /// <param name="tpay">Порядок погашения</param>
        /// <param name="fdate">Сроки досрочного погашения</param>
        /// <returns></returns>

        [Route("/credit/calculator/out/{sum, cur, pur, rate, ddlm, sdate, tpay, perpay, fdate, arr?}")]
        public IActionResult CreditCalcOut(decimal sum, string cur, decimal rate, int ddlm, string sdate, char tpay, string[] arr)
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

            if (tpay == 'a') ViewBag.Table = AnnuityTable(sum, rate, ddlm, date);
            else if (tpay == 'b')  ViewBag.Table = AnnuityTable(sum, rate, ddlm, date);


            return View();
        }


        private DataTable GetDataTable()
        {
            /// Создания таблицы
            At = new DataTable("AnnuityTable");

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

            return At;
        }


        private DataTable AnnuityTable(decimal sum, decimal rate, int ddlm, DateTime date)
        {

            var At = GetDataTable();

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
                row["OffPaid"] = (Math.Round(offP - (Paid - percO), 2) < 0) ? 0 : Math.Round(offP - (Paid - percO), 2);

                At.Rows.Add(row);
            }

            return At;
        }

    }
}
