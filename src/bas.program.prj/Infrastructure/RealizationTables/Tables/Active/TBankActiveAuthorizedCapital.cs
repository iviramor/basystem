using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Active;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow;
using bas.website.Models.Data;
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
    public class TBankActiveAuthorizedCapital : ATable
    {

        #region Свойства

        private Bank_active_authorized_capital Bank_active_authorized_capital;

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
            var data = BankDbContext.Bank_active_authorized_capital
                .Include(aac => aac.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Aac_name_transactions,
                    item.Aac_describtion_transactions,
                    item.Aac_debit,
                    item.Aac_credit,
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

            var res = MessageBox.Show("Вместе с Клиентом удалится Его история Кредитов!", "Предупреждение",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            //if (res == MessageBoxResult.OK)
            //{
            //    BankDbContext.Bank_client.Remove(Bank_Client);
            //    BankDbContext.SaveChanges();
            //    MessageBox.Show($"{Bank_Client.Client_name} - Удалено");
            //    UpdateDataInTable();
            //    return;
            //}

            return;

        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            ClientViewModel clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowBankClientWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            //ClientViewModel clientViewModel = new(workSpaceWindowViewModel, Bank_Client);
            //clientViewModel.ShowBankClientWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            //ClientViewModel clientViewModel = new(Bank_Client, workSpaceWindowViewModel.User.DataBase);
            //clientViewModel.ShowBankClientWindow();
        }

        #endregion

        #endregion

        public override bool HasNullObject()
        {
            if (Bank_active_authorized_capital == null)
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
                Bank_active_authorized_capital = null;
                return;
            }

            Bank_active_authorized_capital = BankDbContext.Bank_active_authorized_capital
                .SingleOrDefault(item =>
                            item.Aac_name_transactions == (string)selectedItem[0] &&
                            item.Aac_credit.ToString() == (string)selectedItem[1]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankActiveAuthorizedCapital(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {

        }

    }
}
