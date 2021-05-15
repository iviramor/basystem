using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Active;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables.Active
{
    public class TBankActiveDeposits : ATable
    {

        #region Свойства

        private Bank_active_deposits Bank_data;

        #endregion Свойства

        #region Элементы главного окна

        public override bool Filter { get; } = true;
        public override bool Maths { get; } = false;

        #endregion Элементы главного окна

        #region Методы

        #region Работа с таблицей

        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {

            UpdateDBContext();

            var data = BankDbContext.Bank_active_deposits
                .Include(aac => aac.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Act_deposit_name,
                    item.Act_deposit_cash,
                    item.Bank_currency.Currency_name
                    );
        }

        #endregion

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;
            if (CheckUserPassword())
            {
                BankDbContext.Bank_active_deposits.Remove(Bank_data);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_data.Act_deposit_name} - Удалено");
                UpdateDataInTable();
                return;
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankActiveDepositViewModels clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveDepositViewModels clientViewModel = new(workSpaceWindowViewModel, Bank_data);
            clientViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveDepositViewModels clientViewModel = new(Bank_data, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowWindow();
        }

        #endregion

        #endregion

        public override bool HasNullObject()
        {
            if (Bank_data == null)
            {
                ShowNullObjectError();
                return true;
            }
            return false;
        }

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_data = null;
                return;
            }

            Bank_data = BankDbContext.Bank_active_deposits
                .SingleOrDefault(item =>
                            item.Act_deposit_name == (string)selectedItem[0] &&
                            item.Act_deposit_cash == decimal.Parse((string)selectedItem[1]));
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankActiveDeposits(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {

        }

    }
}
