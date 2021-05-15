﻿using bas.program.Models.Tables.Passive;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive
{
    public class BankPassiveCreditDebitViewModel : ABAC3Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_credit_debit _Bank_data;

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_passive_credit_debit.SingleOrDefault(d => d.Cdebit_id == _Bank_data.Cdebit_id);

            #region Смена изменений в сессии пользователя

            data.Cdebit_name = _Name;
            data.Cdebit_describ = Description;
            data.Cdebit_lender = SelectedBankClient.Client_id;
            data.Bank_client = SelectedBankClient;
            data.Cdebit_cash = Credit;
            data.Cdebit_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_credit_debit.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_credit_debit NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectedBankClient == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Cdebit_name = _Name;
            NewData.Cdebit_describ = Description;
            NewData.Cdebit_lender = SelectedBankClient.Client_id;
            NewData.Bank_client = SelectedBankClient;
            NewData.Cdebit_cash = Credit;
            NewData.Cdebit_type = SelectCurrency.Currency_id;
            NewData.Bank_currency = SelectCurrency;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_passive_credit_debit.Add(NewData);
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
        public BankPassiveCreditDebitViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_credit_debit bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Cdebit_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Cdebit_name;
            Description = bank_data.Cdebit_describ;

            BankClient = workVM.User.DataBase.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Cdebit_lender);

            Credit = bank_data.Cdebit_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Cdebit_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveCreditDebitViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            BankClient = workVM.User.DataBase.Bank_client.ToList();
            Currency = workVM.User.DataBase.Bank_currency.ToList();

        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveCreditDebitViewModel(Bank_passive_credit_debit bank_data, BankDbContext dbContext)
            : base(dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Cdebit_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Cdebit_name;
            Description = bank_data.Cdebit_describ;

            BankClient = dbContext.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Cdebit_lender);

            Credit = bank_data.Cdebit_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Cdebit_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы

    }
}
