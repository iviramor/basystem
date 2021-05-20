using bas.program.Models.Tables.Active;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System.Linq;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveAuthorizedCapitalViewModel : ABC3Window
    {
        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_active_authorized_capital _Bank_data;

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_active_authorized_capital.Any(i => i.Aac_name_transactions == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {
            var data = _DataBase.Bank_active_authorized_capital.SingleOrDefault(d => d.Aac_id == _Bank_data.Aac_id);

            #region Смена изменений в сессии пользователя

            data.Aac_name_transactions = _Name;
            data.Aac_describtion_transactions = Description;
            data.Aac_debit = Debit;
            data.Aac_credit = Credit;
            data.Aac_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_authorized_capital.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_active_authorized_capital NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Aac_name_transactions = _Name;
            NewData.Aac_describtion_transactions = Description;
            NewData.Aac_debit = Debit;
            NewData.Aac_credit = Credit;
            NewData.Aac_type = SelectCurrency.Currency_id;

            #endregion Смена изменений в сессии пользователя



            /// Добавление в базу данных
            _DataBase.Bank_active_authorized_capital.Add(NewData);
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
        public BankActiveAuthorizedCapitalViewModel(WorkSpaceWindowViewModel workVM, Bank_active_authorized_capital bank_data)
            : base(workVM)
        {
            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Aac_name_transactions}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Aac_name_transactions;
            Description = bank_data.Aac_describtion_transactions;
            Debit = bank_data.Aac_debit;
            Credit = bank_data.Aac_credit;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Aac_type);

            #endregion Значение свойство
        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveAuthorizedCapitalViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {
            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveAuthorizedCapitalViewModel(Bank_active_authorized_capital bank_data, BankDbContext dbContext)
            : base(dbContext)
        {
            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Aac_name_transactions} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Aac_name_transactions;
            Description = bank_data.Aac_describtion_transactions;
            Debit = bank_data.Aac_debit;
            Credit = bank_data.Aac_credit;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Aac_type);

            #endregion Значение свойство
        }

        #endregion Конструкторы
    }
}