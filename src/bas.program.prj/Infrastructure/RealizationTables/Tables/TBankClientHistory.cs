using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankClientHistory : ATable
    {

        #region Свойства

        private Bank_client_history Bank_Client_History;

        #endregion Свойства

        #region Элементы главного окна

        public override bool Filter { get; } = true;
        public override bool Maths { get; } = true;

        #endregion Элементы главного окна

        #region Методы

        #region Работа с таблицей


        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {
            var data = BankDbContext.Bank_client_history
                .Include(ch => ch.Bank_client)
                .ThenInclude(ch => ch.Bank_client_company)
                .Include(ch => ch.Bank_currency)
                .Include(ch => ch.Bank_status_history)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
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

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;

            MessageBox.Show($"{Bank_Client_History.Clihis_numb} - Удалено");
            UpdateDataInTable();
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            if (HasNullObject()) return;
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;
        }

        #endregion Изменить

        #endregion

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_Client_History = null;
                return;
            }

            Bank_Client_History = BankDbContext.Bank_client_history
                .SingleOrDefault(item =>
                            item.Clihis_numb.ToString() == (string)selectedItem[0]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public override bool HasNullObject()
        {
            if (Bank_Client_History == null)
            {
                ShowNullObjectError();
                return true;
            }
            return false;
        }

        public TBankClientHistory(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {

        }

    }
}
