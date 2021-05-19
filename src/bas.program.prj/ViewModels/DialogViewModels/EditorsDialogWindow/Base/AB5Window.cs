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

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base
{
    public abstract class AB5Window : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        public B5Window _BankWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        public WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public BankDbContext _DataBase;

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

                if (FindMatch(value))
                {
                    MessageBox.Show("Найдено совпадение", "Ошибка ввода", MessageBoxButton.OK,
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

        public abstract void OnUpdateDataCommandExecute(object p);

        #endregion Изменение данных

        #region Добавление данных

        private bool CanAddDataCommandExecuted(object p) => true;

        public abstract void OnAddDataCommandExecute(object p);

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
        public AB5Window(WorkSpaceWindowViewModel workVM)
        {
            _workSpaceWindowViewModel = workVM;

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public AB5Window(WorkSpaceWindowViewModel workVM, string actionName)
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
        public AB5Window(BankDbContext dbContext)
        {

            _Currency = dbContext.Bank_currency.ToList();

            IsEnabled = true;
            IsVisibility = false;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);

        }

        #endregion Конструкторы

        public abstract bool FindMatch(string name);

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
