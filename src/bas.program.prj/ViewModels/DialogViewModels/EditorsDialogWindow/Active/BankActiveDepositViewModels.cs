using bas.program.Models.Tables.Active;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System.Linq;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveDepositViewModels : AB5Window
    {
        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_active_deposits _Bank_data;

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_active_deposits.Any(i => i.Act_deposit_name == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {
            var data = _DataBase.Bank_active_deposits.SingleOrDefault(d => d.Act_deposit_name == _Bank_data.Act_deposit_name);

            #region Смена изменений в сессии пользователя

            data.Act_deposit_name = Name;
            data.Act_deposit_cash = Summa;
            data.Act_deposit_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_deposits.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_active_deposits NewData = new();

            #region Смена изменений в сессии пользователя

            if (Name == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Act_deposit_name = Name;
            NewData.Act_deposit_cash = Summa;
            NewData.Act_deposit_type = SelectCurrency.Currency_id;

            #endregion Смена изменений в сессии пользователя



            /// Добавление в базу данных
            _DataBase.Bank_active_deposits.Add(NewData);
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
        public BankActiveDepositViewModels(WorkSpaceWindowViewModel workVM, Bank_active_deposits bank_data)
            : base(workVM)
        {
            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Act_deposit_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Act_deposit_name;
            Summa = bank_data.Act_deposit_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Act_deposit_type);

            #endregion Значение свойство
        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveDepositViewModels(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {
            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveDepositViewModels(Bank_active_deposits bank_data, BankDbContext dbContext)
            : base(dbContext)
        {
            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Act_deposit_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Act_deposit_name;
            Summa = bank_data.Act_deposit_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Act_deposit_type);

            #endregion Значение свойство
        }

        #endregion Конструкторы
    }
}