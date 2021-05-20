using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Passive;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables.Passive
{
    public class TBankPassiveCreditDebit : ATable
    {
        #region Свойства

        private Bank_passive_credit_debit Bank_data;

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

            var data = BankDbContext.Bank_passive_credit_debit
                .Include(aac => aac.Bank_client)
                .Include(aac => aac.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Cdebit_name,
                    item.Cdebit_describ,
                    item.Cdebit_cash,
                    $"{item.Bank_client.Client_surname} {item.Bank_client.Client_name} {item.Bank_client.Client_patronymic}",
                    item.Bank_currency.Currency_name
                    );
        }

        #endregion Работа с таблицей

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;
            if (CheckUserPassword())
            {
                BankDbContext.Bank_passive_credit_debit.Remove(Bank_data);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_data.Cdebit_name} - Удалено");
                UpdateDataInTable();
                return;
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankPassiveCreditDebitViewModel clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankPassiveCreditDebitViewModel clientViewModel = new(workSpaceWindowViewModel, Bank_data);
            clientViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankPassiveCreditDebitViewModel clientViewModel = new(Bank_data, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowWindow();
        }

        #endregion Просмотр

        #endregion Команды

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

            Bank_data = BankDbContext.Bank_passive_credit_debit
                .SingleOrDefault(item =>
                            item.Cdebit_name == (string)selectedItem[0] &&
                            item.Cdebit_describ == (string)selectedItem[1]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankPassiveCreditDebit(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {
        }
    }
}