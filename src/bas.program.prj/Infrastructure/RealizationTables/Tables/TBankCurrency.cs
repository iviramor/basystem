using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow;
using bas.website.Models.Data;
using System.Data;
using System.Linq;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankCurrency : ATable
    {
        #region Свойства

        private Bank_currency Bank_data;

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

        #endregion Работа с таблицей

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;

            if (CheckUserPassword())
            {
                try
                {
                    BankDbContext.Bank_currency.Remove(Bank_data);
                    BankDbContext.SaveChanges();
                    MessageBox.Show($"{Bank_data.Currency_name} - Удалено");
                }
                catch
                {
                    MessageBox.Show("Данные существуют в другой таблице", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                finally
                {
                    UpdateDataInTable();
                }
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankCurrencyViewModel bankCompanyViewModel = new(workSpaceWindowViewModel, "Добавить");
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;
            BankCurrencyViewModel bankCompanyViewModel = new(workSpaceWindowViewModel, Bank_data);
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            BankCurrencyViewModel bankCompanyViewModel = new(Bank_data);
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Просмотр

        #endregion Команды

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_data = null;
                return;
            }

            Bank_data = BankDbContext.Bank_currency
                .SingleOrDefault(item =>
                            item.Currency_name == (string)selectedItem[0]);
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

        public TBankCurrency(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {
        }
    }
}