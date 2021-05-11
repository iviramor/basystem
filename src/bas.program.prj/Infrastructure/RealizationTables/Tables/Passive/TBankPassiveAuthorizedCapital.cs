using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Passive;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables.Passive
{
    public class TBankPassiveAuthorizedCapital : ATable
    {

        #region Свойства

        private Bank_passive_authorized_capital Bank_data;

        #endregion Свойства

        #region Элементы главного окна

        public override bool Filter { get; } = false;
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

            var data = BankDbContext.Bank_passive_authorized_capital
                .Include(a => a.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Apc_name_transactions,
                    item.Apc_describtion_transactions,
                    item.Apc_debit,
                    item.Apc_credit,
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
                BankDbContext.Bank_passive_authorized_capital.Remove(Bank_data);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_data.Apc_name_transactions} - Удалено");
                UpdateDataInTable();
                return;
            }

            return;

        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankPassiveAuthorizedCapitalViewModel clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankPassiveAuthorizedCapitalViewModel clientViewModel = new(workSpaceWindowViewModel, Bank_data);
            clientViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankPassiveAuthorizedCapitalViewModel clientViewModel = new(Bank_data, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowWindow();
        }

        #endregion

        #endregion

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_data = null;
                return;
            }

            Bank_data = BankDbContext.Bank_passive_authorized_capital
                .SingleOrDefault(item =>
                            item.Apc_name_transactions == (string)selectedItem[0] &&
                            item.Apc_describtion_transactions == (string)selectedItem[1]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public override bool HasNullObject()
        {
            if (Bank_data == null)
            {
                ShowNullObjectError();
                return true;
            }
            return false;
        }

        public TBankPassiveAuthorizedCapital(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {

        }
    
    }
}
