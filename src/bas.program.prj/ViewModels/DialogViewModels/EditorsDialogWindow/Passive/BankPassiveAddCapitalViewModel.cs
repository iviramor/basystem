using bas.program.Models.Tables.Passive;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System.Linq;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive
{
    internal class BankPassiveAddCapitalViewModel : ABC3Window
    {
        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_add_capital _Bank_data;

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_passive_add_capital.Any(i => i.Addc_name == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {
            var data = _DataBase.Bank_passive_add_capital.SingleOrDefault(d => d.Addc_id == _Bank_data.Addc_id);

            #region Смена изменений в сессии пользователя

            data.Addc_name = _Name;
            data.Addc_descrip = Description;
            data.Addc_debit = Debit;
            data.Addc_credit = Credit;
            data.Addc_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_add_capital.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_add_capital NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Addc_name = _Name;
            NewData.Addc_descrip = Description;
            NewData.Addc_debit = Debit;
            NewData.Addc_credit = Credit;
            NewData.Addc_type = SelectCurrency.Currency_id;

            #endregion Смена изменений в сессии пользователя



            /// Добавление в базу данных
            _DataBase.Bank_passive_add_capital.Add(NewData);
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
        public BankPassiveAddCapitalViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_add_capital bank_data)
            : base(workVM)
        {
            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Addc_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Addc_name;
            Description = bank_data.Addc_descrip;
            Debit = bank_data.Addc_debit;
            Credit = bank_data.Addc_credit;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Addc_type);

            #endregion Значение свойство
        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveAddCapitalViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {
            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveAddCapitalViewModel(Bank_passive_add_capital bank_data, BankDbContext dbContext)
            : base(dbContext)
        {
            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Addc_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Addc_name;
            Description = bank_data.Addc_descrip;
            Debit = bank_data.Addc_debit;
            Credit = bank_data.Addc_credit;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Addc_type);

            #endregion Значение свойство
        }

        #endregion Конструкторы
    }
}