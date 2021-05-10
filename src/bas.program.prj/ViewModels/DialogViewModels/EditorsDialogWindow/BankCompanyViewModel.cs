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
    public class BankCompanyViewModel : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        private BankCompanyWindow _BankWindow;

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
        private readonly Bank_client_company _Bank_data;

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

        private string _Description;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get => _Description;
            set
            {
                if (Equals(_Description, value)) return;
                _Description = value;
                OnPropertyChanged();
            }
        }

        private string _Type;
        /// <summary>
        /// Тип компании
        /// </summary>
        public string Type
        {
            get => _Type;
            set
            {
                if (Equals(_Type, value)) return;

                if (value.Length < 2 || value.Any(ch => char.IsWhiteSpace(ch)) 
                    || value.Length > 150)
                {
                    MessageBox.Show("Тип не может быть:\n" +
                                    "-> Меньше 2 символов\n" +
                                    "-> Содержать пробелы\n" +
                                    "Больше 150 символов", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Type = value;
                OnPropertyChanged();
            }
        }

        private string _Address;
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address
        {
            get => _Address;
            set
            {
                if (Equals(_Address, value)) return;

                if (value.Length < 6 || value.Any(ch => char.IsWhiteSpace(ch)) 
                    || value.Length > 150)
                {
                    MessageBox.Show("Отчество не может быть:\n" +
                                    "-> Меньше 6 символов\n" +
                                    "-> Содержать пробелы\n" +
                                    "Больше 150 символов", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Address = value;
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

            #region Смена изменений в сессии пользователя

            _Bank_data.Clcomp_name = _Name;
            _Bank_data.Clcomp_descr = _Description;
            _Bank_data.Clcomp_type = _Type;
            _Bank_data.Clcomp_adr = _Address;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_client_company.Update(_Bank_data);
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
            Bank_client_company NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                _Description == null ||
                _Type == null ||
                _Address == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Clcomp_name = _Name;
            NewData.Clcomp_descr = _Description;
            NewData.Clcomp_type = _Type;
            NewData.Clcomp_adr = _Address;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_client_company.Add(NewData);
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
        public BankCompanyViewModel(WorkSpaceWindowViewModel workVM, Bank_client_company bank_Client_Company)
        {
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _Title = $"Изменить: {bank_Client_Company.Clcomp_name}";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            /// Данные
            _Bank_data = bank_Client_Company;

            #region Значение свойство

            _Name = bank_Client_Company.Clcomp_name;
            _Description = bank_Client_Company.Clcomp_descr;
            _Type = bank_Client_Company.Clcomp_type;
            _Address = bank_Client_Company.Clcomp_adr;

            #endregion Значение свойство

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankCompanyViewModel(WorkSpaceWindowViewModel workVM, string actionName)
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
        public BankCompanyViewModel(Bank_client_company bank_Client_Company, BankDbContext dbContext)
        {

            /// Заголовок окна с именем
            _Title = $"Просмотр: {bank_Client_Company.Clcomp_name} ";

            /// Данные
            _Bank_data = bank_Client_Company;

            #region Значение свойство

            _Name = bank_Client_Company.Clcomp_name;
            _Description = bank_Client_Company.Clcomp_descr;
            _Type = bank_Client_Company.Clcomp_type;
            _Address = bank_Client_Company.Clcomp_adr;

            #endregion Значение свойство

            IsEnabled = true;
            IsVisibility = false;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        #endregion Конструкторы

        public void ShowWindow()
        {
            _BankWindow = new BankCompanyWindow()
            {
                DataContext = this
            };
            _BankWindow.ShowDialog();
        }

    }
}
