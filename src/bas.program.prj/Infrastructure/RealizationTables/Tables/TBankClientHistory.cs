using bas.program.Infrastructure.RealizationTables.Base;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankClientHistory : ATable
    {

        #region Свойства


        #endregion Свойства

        #region Методы

        #region Работа с таблицей


        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {
            var data = _BankDbContext.Bank_client_history
                .Include(ch => ch.Bank_client)
                .ThenInclude(ch => ch.Bank_client_company)
                .Include(ch => ch.Bank_currency)
                .Include(ch => ch.Bank_status_history)
                .ToList();

            _DataTable.Clear();

            foreach (var item in data)
                _DataTable.Rows.Add(
                    item.Clihis_numb,
                    item.Clihis_percent,
                    item.Clihis_all_sum,
                    item.Clihis_start_date,
                    item.Clihis_ddl_date,
                    item.Clihis_paid_off,
                    item.Clihis_paid,
                    $"{item.Bank_client.Client_name} {item.Bank_client.Client_surname}",
                    item.Bank_currency.Currency_name,
                    item.Bank_status_history.Status_name
                    );
        }

        #endregion

        #endregion Методы

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return _DataTable;
        }

        public TBankClientHistory(string name) : base(name)
        {

        }

    }
}
