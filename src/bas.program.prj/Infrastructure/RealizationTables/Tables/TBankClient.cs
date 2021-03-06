using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankClient : ATable
    {
        #region Свойства

        private Bank_client Bank_Client;

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

            var data = BankDbContext.Bank_client
                .Include(ac => ac.Bank_client_company)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Client_name,
                    item.Client_surname,
                    item.Client_patronymic,
                    (item.Client_sex) ? "Мужской" : "Женский",
                    (item.Bank_client_company == null) ? "Нет компании" : item.Bank_client_company.Clcomp_name,
                    item.Client_login
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
                var res = MessageBox.Show("Вместе с Клиентом удалится: \n" +
                                           "Его история Кредитов \n" +
                                           "Корреспондентские счета коммерческих банков, открытые в банке \n" +
                                           "Информация об компании клиента \n", "Предупреждение",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    BankDbContext.Bank_client.Remove(Bank_Client);
                    BankDbContext.SaveChanges();
                    MessageBox.Show($"{Bank_Client.Client_name} - Удалено");
                    UpdateDataInTable();
                    return;
                }

                return;
            }
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

            ClientViewModel clientViewModel = new(workSpaceWindowViewModel, Bank_Client);
            clientViewModel.ShowBankClientWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            ClientViewModel clientViewModel = new(Bank_Client, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowBankClientWindow();
        }

        #endregion Просмотр

        #endregion Команды

        public override bool HasNullObject()
        {
            if (Bank_Client == null)
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
                Bank_Client = null;
                return;
            }

            Bank_Client = BankDbContext.Bank_client
                .SingleOrDefault(item =>
                            item.Client_surname == (string)selectedItem[1] &&
                            item.Client_login == (string)selectedItem[5]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankClient(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {
        }
    }
}