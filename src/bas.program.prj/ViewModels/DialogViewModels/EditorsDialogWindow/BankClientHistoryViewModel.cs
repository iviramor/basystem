using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using bas.program.Views.DialogViews;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow
{
    public class BankClientHistoryViewModel : ViewModel
    {
        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        private BankClientHistoryWindow _BankClientHistoryWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly BankDbContext _DataBase = new();

        /// <summary>
        /// Данные истории кредита
        /// </summary>
        private readonly Bank_client_history _Bank_client_history;

        #endregion Классы

        #region Видимость элементов

        private string _TitleName;

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string TitleName
        {
            get
            {
                return _TitleName;
            }
            set
            {
                if (Equals(_TitleName, value)) return;
                _TitleName = value;
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

        private bool _IsEnabled = true;

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

        #endregion Видимость элементов

        #region Листы окна

        #region Лист с клиентами

        public string _DisplayBankClient;

        /// <summary>
        /// Отображение элемента
        /// </summary>
        public string DisplayBankClient
        {
            get => _DisplayBankClient;
            set
            {
                if (Equals(_DisplayBankClient, value)) return;
                _DisplayBankClient = value;
                OnPropertyChanged();
            }
        }

        private Bank_client _SelectedBankClient;

        /// <summary>
        /// Выделенный из списка "клиент"
        /// </summary>
        public Bank_client SelectedBankClient
        {
            get => _SelectedBankClient;
            set
            {
                if (Equals(_SelectedBankClient, value)) return;
                _SelectedBankClient = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_client> _BankClient;

        /// <summary>
        /// Список всех клиентов
        /// </summary>
        public List<Bank_client> BankClient
        {
            get => _BankClient;
            set
            {
                if (Equals(_BankClient, value)) return;
                _BankClient = value;
                OnPropertyChanged();
            }
        }

        #endregion Лист с клиентами

        #region Лист с валютой

        private Bank_currency _SelectedBankCurrency;

        /// <summary>
        /// Выделенный из списка "Средств"
        /// </summary>
        public Bank_currency SelectedBankCurrency
        {
            get => _SelectedBankCurrency;
            set
            {
                if (Equals(_SelectedBankCurrency, value)) return;
                _SelectedBankCurrency = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_currency> _BankCurrency;

        /// <summary>
        /// Список всех типов средств
        /// </summary>
        public List<Bank_currency> BankCurrency
        {
            get => _BankCurrency;
            set
            {
                if (Equals(_BankCurrency, value)) return;
                _BankCurrency = value;
                OnPropertyChanged();
            }
        }

        #endregion Лист с валютой

        #region Статус кредита

        private Bank_status_history _SelectedBankStatusHistory;

        /// <summary>
        /// Выделенный из списка "Статусов истории"
        /// </summary>
        public Bank_status_history SelectedBankStatusHistory
        {
            get => _SelectedBankStatusHistory;
            set
            {
                if (Equals(_SelectedBankStatusHistory, value)) return;
                _SelectedBankStatusHistory = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_status_history> _BankStatusHistory;

        /// <summary>
        /// Список всех типов Статусов истории
        /// </summary>
        public List<Bank_status_history> BankStatusHistory
        {
            get => _BankStatusHistory;
            set
            {
                if (Equals(_BankStatusHistory, value)) return;
                _BankStatusHistory = value;
                OnPropertyChanged();
            }
        }

        #endregion Статус кредита

        #endregion Листы окна

        #region Свойства элементов

        public Guid _Numb;

        /// <summary>
        /// Номер кредита
        /// </summary>
        public Guid Numb
        {
            get => _Numb;
            set
            {
                if (Equals(_Numb, value)) return;
                _Numb = value;
                OnPropertyChanged();
            }
        }

        private decimal _Percent;

        /// <summary>
        /// Процентная ставка
        /// </summary>
        public decimal Percent
        {
            get => _Percent;
            set
            {
                if (Equals(_Percent, value))
                {
                    MessageBox.Show("Фамилия не может быть:\n" +
                            "-> Меньше 2 символов\n" +
                            "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Percent = value;
                OnPropertyChanged();
            }
        }

        private decimal _FullSum;

        /// <summary>
        /// Сумма кредита
        /// </summary>
        public decimal FullSum
        {
            get => _FullSum;
            set
            {
                if (Equals(_FullSum, value)) return;
                _FullSum = value;
                OnPropertyChanged();
            }
        }

        private DateTime _StartDate;

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate
        {
            get => _StartDate;
            set
            {
                if (Equals(_StartDate, value)) return;
                _StartDate = value;
                OnPropertyChanged();
            }
        }

        private decimal _PaidOff;

        /// <summary>
        /// Оплачено
        /// </summary>
        public decimal PaidOff
        {
            get => _PaidOff;
            set
            {
                if (Equals(_PaidOff, value)) return;
                _PaidOff = value;
                OnPropertyChanged();
            }
        }

        private decimal _Paid;

        /// <summary>
        /// Долг
        /// </summary>
        public decimal Paid
        {
            get => _Paid;
            set
            {
                if (Equals(_Paid, value)) return;
                _Paid = value;
                OnPropertyChanged();
            }
        }

        private int _CountMonths;

        /// <summary>
        /// Срок
        /// </summary>
        public int CountMonths
        {
            get => _CountMonths;
            set
            {
                if (Equals(_CountMonths, value)) return;
                _CountMonths = value;
                OnPropertyChanged();
            }
        }

        #endregion Свойства элементов

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
            var data = _DataBase.Bank_client_history.SingleOrDefault(d => d.Clihis_id == _Bank_client_history.Clihis_id);

            #region Смена изменений в сессии пользователя

            data.Clihis_client = _SelectedBankClient.Client_id;
            data.Clihis_percent = _Percent;
            data.Clihis_all_sum = _FullSum;
            data.Clihis_start_date = _StartDate;
            data.Clihis_ddl_date = _CountMonths;
            data.Clihis_paid_off = _PaidOff;
            data.Clihis_paid = _Paid;
            data.Clihis_status = _SelectedBankStatusHistory.Status_id;
            data.Clihis_cur = _SelectedBankCurrency.Currency_id;

            data.Bank_client = _SelectedBankClient;
            data.Bank_currency = _SelectedBankCurrency;
            data.Bank_status_history = _SelectedBankStatusHistory;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_client_history.Update(data);
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
            /// Данные новой записи
            Bank_client_history NewHist = new();

            #region Смена изменений в сессии пользователя

            #region Проверка полей

            if (_SelectedBankClient == null ||
                _SelectedBankStatusHistory == null ||
                _SelectedBankCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion Проверка полей

            #region Заполнение

            NewHist.Clihis_numb = Guid.NewGuid();
            NewHist.Clihis_client = _SelectedBankClient.Client_id;
            NewHist.Clihis_percent = _Percent;
            NewHist.Clihis_all_sum = _FullSum;
            NewHist.Clihis_start_date = _StartDate;
            NewHist.Clihis_ddl_date = _CountMonths;
            NewHist.Clihis_paid_off = _PaidOff;
            NewHist.Clihis_paid = _Paid;
            NewHist.Clihis_status = _SelectedBankStatusHistory.Status_id;
            NewHist.Clihis_cur = _SelectedBankCurrency.Currency_id;

            #endregion Заполнение

            #endregion Смена изменений в сессии пользователя

            /// Добавление в базу данных нового пользователя
            _DataBase.Bank_client_history.Add(NewHist);
            _DataBase.SaveChanges();

            MessageBox.Show("Добавлена новая запись");
            _workSpaceWindowViewModel.SetUpdateTabel();
            _BankClientHistoryWindow.Close();
        }

        #endregion Добавление данных

        #endregion Обновление

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand CloseWindowCommand { get; }

        private bool CanCloseWindowCommandExecuted(object p) => true;

        private void OnCloseWindowCommandExecute(object p)
        {
            _BankClientHistoryWindow.Close();
        }

        #endregion Закрыть окно

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования
        /// </summary>
        /// <param name="workVM">ViewModel Окна профилей</param>
        /// <param name="bank_Client_History">Объект Bank_client_history заполненный данными из выбранного(Selected) в таблице</param>
        public BankClientHistoryViewModel(WorkSpaceWindowViewModel workVM, Bank_client_history bank_Client_History)
        {
            TitleName = $"Изменить: {bank_Client_History.Clihis_numb}";
            NameAction = "Изменить";

            _Bank_client_history = bank_Client_History;
            _workSpaceWindowViewModel = workVM;

            #region Заполнение полей

            BankClient = _DataBase.Bank_client
                .Include(bc => bc.Bank_client_company).ToList();
            SelectedBankClient = BankClient.SingleOrDefault(bc => bc.Client_id == bank_Client_History.Clihis_client);

            Numb = bank_Client_History.Clihis_numb;
            Percent = bank_Client_History.Clihis_percent;
            FullSum = bank_Client_History.Clihis_all_sum;

            BankCurrency = _DataBase.Bank_currency.ToList();
            SelectedBankCurrency = BankCurrency.SingleOrDefault(c => c.Currency_id == bank_Client_History.Clihis_cur);

            BankStatusHistory = _DataBase.Bank_status_history.ToList();
            SelectedBankStatusHistory = BankStatusHistory.SingleOrDefault(h => h.Status_id == bank_Client_History.Clihis_status);

            StartDate = bank_Client_History.Clihis_start_date;
            Paid = bank_Client_History.Clihis_paid;
            PaidOff = bank_Client_History.Clihis_paid_off;
            CountMonths = bank_Client_History.Clihis_ddl_date;

            #endregion Заполнение полей

            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
        }

        /// <summary>
        /// Конструктор для Добавление
        /// </summary>
        /// <param name="workVM">ViewModel Окна профилей</param>
        /// <param name="nameAction">Название операции</param>
        public BankClientHistoryViewModel(WorkSpaceWindowViewModel workVM, string nameAction)
        {
            TitleName = nameAction;
            NameAction = nameAction;

            _workSpaceWindowViewModel = workVM;

            #region Заполнение полей

            BankClient = _DataBase.Bank_client
                .Include(bc => bc.Bank_client_company).ToList();

            BankCurrency = _DataBase.Bank_currency.ToList();

            BankStatusHistory = _DataBase.Bank_status_history.ToList();

            StartDate = DateTime.Now;

            #endregion Заполнение полей

            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
        }

        /// <summary>
        /// Просмотр
        /// </summary>
        /// <param name="bank_Client_History"></param>
        public BankClientHistoryViewModel(Bank_client_history bank_Client_History)
        {
            TitleName = $"Просмотр: {bank_Client_History.Clihis_numb}";

            #region Заполнение полей

            BankClient = _DataBase.Bank_client
                .Include(bc => bc.Bank_client_company).ToList();
            SelectedBankClient = BankClient.SingleOrDefault(bc => bc.Client_id == bank_Client_History.Clihis_client);

            Numb = bank_Client_History.Clihis_numb;
            Percent = bank_Client_History.Clihis_percent;
            FullSum = bank_Client_History.Clihis_all_sum;

            BankCurrency = _DataBase.Bank_currency.ToList();
            SelectedBankCurrency = BankCurrency.SingleOrDefault(c => c.Currency_id == bank_Client_History.Clihis_cur);

            BankStatusHistory = _DataBase.Bank_status_history.ToList();
            SelectedBankStatusHistory = BankStatusHistory.SingleOrDefault(h => h.Status_id == bank_Client_History.Clihis_status);

            StartDate = bank_Client_History.Clihis_start_date;
            Paid = bank_Client_History.Clihis_paid;
            PaidOff = bank_Client_History.Clihis_paid_off;
            CountMonths = bank_Client_History.Clihis_ddl_date;

            #endregion Заполнение полей

            IsEnabled = false;

            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
        }

        #endregion Конструкторы

        /// <summary>
        /// Отображения окна
        /// </summary>
        public void ShowBankClientWindow()
        {
            _BankClientHistoryWindow = new BankClientHistoryWindow()
            {
                DataContext = this
            };
            _BankClientHistoryWindow.ShowDialog();
        }
    }
}