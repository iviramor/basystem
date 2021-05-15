using bas.program.Models.Tables.Passive;
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
    public class BankPassiveCampViewModel : AB5Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_camp _Bank_data;

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_passive_camp.SingleOrDefault(d => d.Pcamp_name == _Bank_data.Pcamp_name);

            #region Смена изменений в сессии пользователя

            data.Pcamp_name = Name;
            data.Pcamp_quantity = Summa;
            data.Pcamp_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_camp.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_camp NewData = new();

            #region Смена изменений в сессии пользователя

            if (Name == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Pcamp_name = Name;
            NewData.Pcamp_quantity = Summa;
            NewData.Pcamp_type = SelectCurrency.Currency_id;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_passive_camp.Add(NewData);
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
        public BankPassiveCampViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_camp bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Pcamp_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Pcamp_name;
            Summa = bank_data.Pcamp_quantity;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Pcamp_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveCampViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            /// Окно с профилями
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            Title = $"Добавить";

            Currency = workVM.User.DataBase.Bank_currency.ToList();

        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveCampViewModel(Bank_passive_camp bank_data, BankDbContext dbContext)
            : base(dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Pcamp_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Pcamp_name;
            Summa = bank_data.Pcamp_quantity;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Pcamp_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы

    }
}
