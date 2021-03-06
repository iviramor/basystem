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
    public class TBankClientCompany : ATable
    {
        #region Свойства

        private Bank_client_company Bank_Client_Company;

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

            var data = BankDbContext.Bank_client_company
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Clcomp_name,
                    item.Clcomp_descr,
                    item.Clcomp_type,
                    item.Clcomp_adr
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
                if (workSpaceWindowViewModel.User.DataBase.Bank_client.Any(u =>
                                             u.Client_company == Bank_Client_Company.Clcomp_id))
                {
                    MessageBox.Show("Данные существуют в другой таблице", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                BankDbContext.Bank_client_company.Remove(Bank_Client_Company);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_Client_Company.Clcomp_name} - Удалено");
                UpdateDataInTable();
                return;
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankCompanyViewModel bankCompanyViewModel = new(workSpaceWindowViewModel, "Добавить");
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;
            BankCompanyViewModel bankCompanyViewModel = new(workSpaceWindowViewModel, Bank_Client_Company);
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            BankCompanyViewModel bankCompanyViewModel = new(Bank_Client_Company);
            bankCompanyViewModel.ShowWindow();
        }

        #endregion Просмотр

        #endregion Команды

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_Client_Company = null;
                return;
            }

            Bank_Client_Company = BankDbContext.Bank_client_company
                .SingleOrDefault(item =>
                            item.Clcomp_name == (string)selectedItem[0] &&
                            item.Clcomp_descr == (string)selectedItem[1]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public override bool HasNullObject()
        {
            if (Bank_Client_Company == null)
            {
                ShowNullObjectError();
                return true;
            }
            return false;
        }

        public TBankClientCompany(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {
        }
    }
}