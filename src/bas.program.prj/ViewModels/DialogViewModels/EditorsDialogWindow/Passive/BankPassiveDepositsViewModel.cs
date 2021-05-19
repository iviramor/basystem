using bas.program.Models.Tables.Passive;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.program.Views.DialogViews;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive
{
    public class BankPassiveDepositsViewModel : AB5Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_deposits _Bank_data;

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_passive_deposits.SingleOrDefault(d => d.Pas_deposit_name == _Bank_data.Pas_deposit_name);

            #region Смена изменений в сессии пользователя

            data.Pas_deposit_name = Name;
            data.Pas_deposit_cash = Summa;
            data.Pas_deposit_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_deposits.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_deposits NewData = new();

            #region Смена изменений в сессии пользователя

            if (Name == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Pas_deposit_name = Name;
            NewData.Pas_deposit_cash = Summa;
            NewData.Pas_deposit_type = SelectCurrency.Currency_id;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_passive_deposits.Add(NewData);
            _DataBase.SaveChanges();

            /// Обновление таблицы 
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            _BankWindow.Close();
        }

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_passive_deposits.Any(i => i.Pas_deposit_name == name);
        }

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования
        /// </summary>
        public BankPassiveDepositsViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_deposits bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Pas_deposit_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Pas_deposit_name;
            Summa = bank_data.Pas_deposit_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Pas_deposit_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveDepositsViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            Currency = workVM.User.DataBase.Bank_currency.ToList();

        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveDepositsViewModel(Bank_passive_deposits bank_data, BankDbContext dbContext)
            : base (dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Pas_deposit_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Pas_deposit_name;
            Summa = bank_data.Pas_deposit_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Pas_deposit_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы



    }
}
