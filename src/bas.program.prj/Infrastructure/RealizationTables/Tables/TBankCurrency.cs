using bas.program.Infrastructure.RealizationTables.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankCurrency : ATable
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
            var data = _BankDbContext.Bank_currency
                .ToList();

            _DataTable.Clear();

            foreach (var item in data)
                _DataTable.Rows.Add(
                    item.Currency_name,
                    item.Currency_dollar,
                    item.Currency_euro,
                    item.Currency_rub
                    );
        }

        #endregion

        #endregion Методы

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return _DataTable;
        }

        public TBankCurrency(string name) : base(name)
        {

        }
    }
}
