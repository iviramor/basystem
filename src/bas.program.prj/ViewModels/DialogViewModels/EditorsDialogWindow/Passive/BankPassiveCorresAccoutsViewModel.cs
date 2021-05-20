using bas.program.Models.Tables.Passive;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.website.Models.Data;
using System.Linq;
using System.Windows;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Passive
{
    public class BankPassiveCorresAccoutsViewModel : ABAC3Window
    {
        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_passive_corres_accouts _Bank_data;

        public override bool FindMatch(string name)
        {
            return _DataBase.Bank_passive_corres_accouts.Any(i => i.Ca_bank_name == name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {
            var data = _DataBase.Bank_passive_corres_accouts.SingleOrDefault(d => d.Ca_bank_id == _Bank_data.Ca_bank_id);

            #region Смена изменений в сессии пользователя

            data.Ca_bank_name = _Name;
            data.Ca_bank_describ = Description;
            data.Ca_bank_company = SelectedBankClient.Client_id;
            data.Bank_client = SelectedBankClient;
            data.Ca_bank_cash = Credit;
            data.Ca_bank_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_passive_corres_accouts.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        public override void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_passive_corres_accouts NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                Description == null ||
                SelectedBankClient == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Ca_bank_name = _Name;
            NewData.Ca_bank_describ = Description;
            NewData.Ca_bank_company = SelectedBankClient.Client_id;
            NewData.Bank_client = SelectedBankClient;
            NewData.Ca_bank_cash = Credit;
            NewData.Ca_bank_type = SelectCurrency.Currency_id;
            NewData.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя



            /// Добавление в базу данных
            _DataBase.Bank_passive_corres_accouts.Add(NewData);
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
        public BankPassiveCorresAccoutsViewModel(WorkSpaceWindowViewModel workVM, Bank_passive_corres_accouts bank_data)
            : base(workVM)
        {
            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Ca_bank_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Ca_bank_name;
            Description = bank_data.Ca_bank_describ;

            BankClient = workVM.User.DataBase.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Ca_bank_company);

            Credit = bank_data.Ca_bank_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ca_bank_type);

            #endregion Значение свойство
        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankPassiveCorresAccoutsViewModel(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {
            BankClient = workVM.User.DataBase.Bank_client.ToList();
            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankPassiveCorresAccoutsViewModel(Bank_passive_corres_accouts bank_data, BankDbContext dbContext)
            : base(dbContext)
        {
            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Ca_bank_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Ca_bank_name;
            Description = bank_data.Ca_bank_describ;

            BankClient = dbContext.Bank_client.ToList();

            SelectedBankClient = BankClient.SingleOrDefault(c => c.Client_id == bank_data.Ca_bank_company);

            Credit = bank_data.Ca_bank_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ca_bank_type);

            #endregion Значение свойство
        }

        #endregion Конструкторы
    }
}