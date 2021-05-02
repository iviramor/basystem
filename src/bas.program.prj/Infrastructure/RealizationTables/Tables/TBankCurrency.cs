using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.UserTables;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankCurrency : ATable
    {

        #region Свойства

        private Bank_currency Bank_Currency;

        #endregion Свойства

        #region Методы

        #region Работа с таблицей

        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {
            var data = BankDbContext.Bank_currency
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Currency_name,
                    item.Currency_dollar,
                    item.Currency_euro,
                    item.Currency_rub
                    );
        }

        #endregion

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveProfCommandExecute(object p)
        {
            if (Bank_Currency == null)
            {
                ShowRemoveError();
                return;
            }
            MessageBox.Show($"{Bank_Currency.Currency_name} - Удалено");
        }

        #endregion Удалить

        #endregion

        public override void SetSelected(DataRowView selectedItem)
        {
            Bank_Currency = BankDbContext.Bank_currency
                .SingleOrDefault(item =>
                            item.Currency_name == (string)selectedItem[0]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankCurrency(Bank_user_access bank_User_Access) : base(bank_User_Access)
        {

        }
    }
}
