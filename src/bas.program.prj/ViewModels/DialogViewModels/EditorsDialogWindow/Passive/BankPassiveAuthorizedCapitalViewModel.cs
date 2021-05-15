using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.Active;
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
    public class BankPassiveAuthorizedCapitalViewModel : ABC3Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_authorized_capital _Bank_data;

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_passive_authorized_capital.SingleOrDefault(d => d.Apc_id == _Bank_data.Apc_id);

            #region Смена изменений в сессии пользователя

            data.Apc_name_transactions = Name;
            data.Apc_describtion_transactions = Description;
            data.Apc_debit = Debit;
            data.Apc_credit = Credit;
            data.Apc_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_authorized_capital.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_authorized_capital NewData = new();

            #region Смена изменений в сессии пользователя

            if (Name == null ||
                Description == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Apc_name_transactions = Name;
            NewData.Apc_describtion_transactions = Description;
            NewData.Apc_debit = Debit;
            NewData.Apc_credit = Credit;
            NewData.Apc_type = SelectCurrency.Currency_id;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_passive_authorized_capital.Add(NewData);
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
        public BankPassiveAuthorizedCapitalViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_authorized_capital bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Apc_name_transactions}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Apc_name_transactions;
            Description = bank_data.Apc_describtion_transactions;
            Debit = bank_data.Apc_debit;
            Credit = bank_data.Apc_credit;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Apc_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveAuthorizedCapitalViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            Currency = workVM.User.DataBase.Bank_currency.ToList();

        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveAuthorizedCapitalViewModel(Bank_passive_authorized_capital bank_data, BankDbContext dbContext)
            : base(dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Apc_name_transactions} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Apc_name_transactions;
            Description = bank_data.Apc_describtion_transactions;
            Debit = bank_data.Apc_debit;
            Credit = bank_data.Apc_credit;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Apc_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы

    }
}
