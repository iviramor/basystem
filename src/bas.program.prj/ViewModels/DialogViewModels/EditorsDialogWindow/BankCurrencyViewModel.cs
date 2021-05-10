using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using bas.program.Views.DialogViews;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow
{
    public class BankCurrencyViewModel : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        private BankCurrencyWindow _BankWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly BankDbContext _DataBase;

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_currency _Bank_data;

        #endregion Классы

        #region Видимость элементов

        private string _Title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (Equals(_Title, value)) return;
                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _NameAction = "Изменить";
        /// <summary>
        /// Название операции
        /// </summary>
        public string NameAction
        {
            get => _NameAction;
            set
            {
                if (Equals(_NameAction, value)) return;
                _NameAction = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEnabled = false;
        /// <summary>
        /// Блокировка элементов
        /// </summary>
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                if (Equals(_IsEnabled, value)) return;
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisibility = true;
        /// <summary>
        /// Блокировка элементов
        /// </summary>
        public bool IsVisibility
        {
            get => _IsVisibility;
            set
            {
                if (Equals(_IsVisibility, value)) return;
                _IsVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion Видимость элементов

        #region Свойства элементов

        public string _Name;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get => _Name;
            set
            {
                if (Equals(_Name, value)) return;

                if (value.Length < 2 || value.Any(ch => char.IsWhiteSpace(ch)))
                {
                    MessageBox.Show("Имя не может быть:\n" +
                                    "-> Меньше 2 символов\n" +
                                    "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Name = value;
                OnPropertyChanged();
            }
        }

        private decimal _Dollar;
        /// <summary>
        /// В долларах
        /// </summary>
        public decimal Dollar
        {
            get => _Dollar;
            set
            {
                if (Equals(_Dollar, value)) return;
                _Dollar = value;
                OnPropertyChanged();
            }
        }

        private decimal _Ruble;
        /// <summary>
        /// Тип компании
        /// </summary>
        public decimal Ruble
        {
            get => _Ruble;
            set
            {
                if (Equals(_Ruble, value)) return;

                _Ruble = value;
                OnPropertyChanged();
            }
        }

        private decimal _Euro;
        /// <summary>
        /// Адрес
        /// </summary>
        public decimal Euro
        {
            get => _Euro;
            set
            {
                if (Equals(_Euro, value)) return;
                _Euro = value;
                OnPropertyChanged();
            }
        }

        #endregion Свойства пользователя

        #endregion Поля и свойства

        #region Команды

        #region Обновление

        /// <summary>
        /// Команда обновления данных
        /// </summary>
        public ICommand UpdateDataCommand { get; }

        #region Изменение данных

        private bool CanUpdateDataCommandExecuted(object p) => true;

        private void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_currency.SingleOrDefault(d => d.Currency_id == _Bank_data.Currency_id);

            #region Смена изменений в сессии пользователя

            data.Currency_name = _Name;
            data.Currency_dollar = _Dollar;
            data.Currency_rub = _Ruble;
            data.Currency_euro = _Euro;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_currency.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
        }

        #endregion Изменение данных

        #region Добавление данных

        private bool CanAddDataCommandExecuted(object p) => true;

        private void OnAddDataCommandExecute(object p)
        {
            /// Новые данные
            Bank_currency NewData = new();

            #region Смена изменений 

            if (_Name == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Currency_name = _Name;
            NewData.Currency_dollar = _Dollar;
            NewData.Currency_rub = _Ruble;
            NewData.Currency_euro = _Euro;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_currency.Add(NewData);
            _DataBase.SaveChanges();

            /// Обновление таблицы 
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();
            _BankWindow.Close();
        }

        #endregion Добавление данных

        #endregion Обновление

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand CloseCommand { get; }

        private bool CanCloseWindowCommandExecuted(object p) => true;

        private void OnCloseWindowCommandExecute(object p)
        {
            _BankWindow.Close();
        }

        #endregion

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования
        /// </summary>
        public BankCurrencyViewModel(WorkSpaceWindowViewModel workVM, Bank_currency bank_data)
        {
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _Title = $"Изменить: {bank_data.Currency_name}";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Currency_name;
            _Dollar = bank_data.Currency_dollar;
            _Ruble = bank_data.Currency_rub;
            _Euro = bank_data.Currency_euro;

            #endregion Значение свойство

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankCurrencyViewModel(WorkSpaceWindowViewModel workVM, string actionName)
        {
            _NameAction = actionName;

            /// Окно с профилями
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _Title = $"Добавить";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankCurrencyViewModel(Bank_currency bank_data)
        {

            /// Заголовок окна с именем
            _Title = $"Просмотр: {bank_data.Currency_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Currency_name;
            _Dollar = bank_data.Currency_dollar;
            _Ruble = bank_data.Currency_rub;
            _Euro = bank_data.Currency_euro;

            #endregion Значение свойство

            IsEnabled = true;
            IsVisibility = false;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        #endregion Конструкторы

        public void ShowWindow()
        {
            _BankWindow = new BankCurrencyWindow()
            {
                DataContext = this
            };
            _BankWindow.ShowDialog();
        }

    }
}
