using bas.program.Models.Tables.Active;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveCreditsOutViewModel : ABAC3Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_active_credits_out _Bank_data;

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_active_credits_out.Any(i => i.Co_name == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_active_credits_out.SingleOrDefault(d => d.Co_id == _Bank_data.Co_id);

            #region Смена изменений в сессии пользователя

            data.Co_name = _Name;
            data.Co_describ = Description;
            data.Co_debtor = SelectedBankClient.Client_id;
            data.Bank_client = SelectedBankClient;
            data.Co_cash= Credit;
            data.Co_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_credits_out.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_active_credits_out NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectedBankClient == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Co_name = _Name;
            NewData.Co_describ = Description;
            NewData.Co_debtor = SelectedBankClient.Client_id;
            NewData.Bank_client = SelectedBankClient;
            NewData.Co_cash = Credit;
            NewData.Co_type = SelectCurrency.Currency_id;
            NewData.Bank_currency = SelectCurrency;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_active_credits_out.Add(NewData);
            _DataBase.SaveChanges();

            /// Обновление таблицы 
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            _BankWindow.Close();
        }

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования
        /// </summary>
        public BankActiveCreditsOutViewModel(WorkSpaceWindowViewModel workVM, Bank_active_credits_out bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Co_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Co_name;
            Description = bank_data.Co_describ;

            BankClient = workVM.User.DataBase.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Co_debtor);

            Credit = bank_data.Co_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Co_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveCreditsOutViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            BankClient = workVM.User.DataBase.Bank_client.ToList();
            Currency = workVM.User.DataBase.Bank_currency.ToList();

        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveCreditsOutViewModel(Bank_active_credits_out bank_data, BankDbContext dbContext)
            : base(dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Co_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Co_name;
            Description = bank_data.Co_describ;

            BankClient = dbContext.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Co_debtor);

            Credit = bank_data.Co_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Co_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы

    }
}
