using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.Active;
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

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveAssetViewModels : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        private B5Window _BankWindow;

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
        private readonly Bank_active_asset _Bank_data;

        #endregion Классы

        #region Видимость элементов

        private string _TName = "Наименование";
        /// <summary>
        /// Заголовок блока 
        /// </summary>
        public string TName
        {
            get
            {
                return _TName;
            }
            set
            {
                if (Equals(_TName, value)) return;
                _TName = value;
                OnPropertyChanged();
            }
        }

        private string _TSumma = "Средство";
        /// <summary>
        /// Заголовок блока 
        /// </summary>
        public string TSumma
        {
            get
            {
                return _TSumma;
            }
            set
            {
                if (Equals(_TSumma, value)) return;
                _TSumma = value;
                OnPropertyChanged();
            }
        }

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

                if (value.Length < 2)
                {
                    MessageBox.Show("Имя не может быть:\n" +
                                    "-> Меньше 2 символов\n", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Name = value;
                OnPropertyChanged();
            }
        }

        private decimal _Summa;
        /// <summary>
        /// Описание
        /// </summary>
        public decimal Summa
        {
            get => _Summa;
            set
            {
                if (Equals(_Summa, value)) return;
                _Summa = value;
                OnPropertyChanged();
            }
        }

        #region Средства

        private List<Bank_currency> _Currency;

        /// <summary>
        /// Список 
        /// </summary>
        public List<Bank_currency> Currency
        {
            get => _Currency;
            set
            {
                if (Equals(_Currency, value)) return;
                _Currency = value;
                OnPropertyChanged();
            }
        }

        private Bank_currency _SelectCurrency;

        /// <summary>
        /// Выделенный в списке
        /// </summary>
        public Bank_currency SelectCurrency
        {
            get => _SelectCurrency;
            set
            {
                if (Equals(_SelectCurrency, value)) return;

                _SelectCurrency = value;
                OnPropertyChanged();
            }
        }

        #endregion

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

            var data = _DataBase.Bank_active_asset.SingleOrDefault(d => d.Ass_name == _Bank_data.Ass_name);

            #region Смена изменений в сессии пользователя

            data.Ass_name = _Name;
            data.Ass_cash = _Summa;
            data.Ass_type = _SelectCurrency.Currency_id;
            data.Bank_currency = _SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_asset.Update(data);
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
            Bank_active_asset NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                _SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Ass_name = _Name;
            NewData.Ass_cash = _Summa;
            NewData.Ass_type = _SelectCurrency.Currency_id;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_active_asset.Add(NewData);
            _DataBase.SaveChanges();

            /// Обновление таблицы 
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public BankActiveAssetViewModels(WorkSpaceWindowViewModel workVM, Bank_active_asset bank_data)
        {
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _Title = $"Изменить: {bank_data.Ass_name}";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Ass_name;
            _Summa = bank_data.Ass_cash;

            _Currency = workVM.User.DataBase.Bank_currency.ToList();

            _SelectCurrency = _Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ass_type);

            #endregion Значение свойство

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveAssetViewModels(WorkSpaceWindowViewModel workVM, string actionName)
        {
            _NameAction = actionName;

            /// Окно с профилями
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _Title = $"Добавить";

            _Currency = workVM.User.DataBase.Bank_currency.ToList();

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveAssetViewModels(Bank_active_asset bank_data, BankDbContext dbContext)
        {

            /// Заголовок окна с именем
            _Title = $"Просмотр: {bank_data.Ass_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            _Name = bank_data.Ass_name;
            _Summa = bank_data.Ass_cash;

            _Currency = dbContext.Bank_currency.ToList();

            _SelectCurrency = _Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ass_type);

            #endregion Значение свойство

            IsEnabled = true;
            IsVisibility = false;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        #endregion Конструкторы

        public void ShowWindow()
        {
            _BankWindow = new B5Window()
            {
                DataContext = this
            };
            _BankWindow.ShowDialog();
        }

    }
}
