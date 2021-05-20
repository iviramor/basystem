using bas.program.Models.Tables.Active;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System.Linq;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveDocsViewModel : BADocsWindowVM
    {
        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_active_docs _Bank_data;

        /// <summary>
        /// Ищет совпадение по имени в базе данных таблицы
        /// </summary>
        /// <returns></returns>
        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_active_docs.Any(i => i.Docs_name == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {
            var data = _DataBase.Bank_active_docs.SingleOrDefault(d => d.Docs_id == _Bank_data.Docs_id);

            #region Смена изменений в сессии пользователя

            data.Docs_name = _Name;
            data.Docs_type_doc = Description;
            data.Docs_cash = Summa;
            data.Docs_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_docs.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_active_docs NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Docs_name = _Name;
            NewData.Docs_type_doc = Description;
            NewData.Docs_cash = Summa;
            NewData.Docs_type = SelectCurrency.Currency_id;

            #endregion Смена изменений в сессии пользователя

            /// Добавление в базу данных
            _DataBase.Bank_active_docs.Add(NewData);
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
        public BankActiveDocsViewModel(WorkSpaceWindowViewModel workVM, Bank_active_docs bank_data)
            : base(workVM)
        {
            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Docs_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Docs_name;
            Description = bank_data.Docs_type_doc;
            Summa = bank_data.Docs_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Docs_type);

            #endregion Значение свойство
        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveDocsViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {
            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveDocsViewModel(Bank_active_docs bank_data, BankDbContext dbContext)
            : base(dbContext)
        {
            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Docs_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Docs_name;
            Description = bank_data.Docs_type_doc;
            Summa = bank_data.Docs_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Docs_type);

            #endregion Значение свойство
        }

        #endregion Конструкторы
    }
}