using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
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
    public class ClientViewModel : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно профиля View
        /// </summary>
        private BunkClientWindow _BunkClientWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly BankDbContext _DataBase;

        /// <summary>
        /// Данные пользователя
        /// </summary>
        private readonly Bank_client _Bank_Client;

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

        #region Лист с Компаниями

        private List<Bank_client_company> _BankCompany;

        /// <summary>
        /// Список всех компаний в банке
        /// </summary>
        public List<Bank_client_company> BankCompany
        {
            get => _BankCompany;
            set
            {
                if (Equals(_BankCompany, value)) return;
                _BankCompany = value;
                OnPropertyChanged();
            }
        }

        private Bank_client_company _SelectedBankCompany;

        /// <summary>
        /// Выделенная компания
        /// </summary>
        public Bank_client_company SelectedBankCompany
        {
            get => _SelectedBankCompany;
            set
            {
                if (Equals(_SelectedBankCompany, value)) return;
                _SelectedBankCompany = value;
                OnPropertyChanged();
            }
        }

        #endregion Лист с Компаниями

        #region Свойства пользователя

        public string _Name;
        /// <summary>
        /// Имя клиента
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

        private string _Surname;
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string Surname
        {
            get => _Surname;
            set
            {
                if (Equals(_Surname, value)) return;

                if (value.Length < 2 || value.Any(ch => char.IsWhiteSpace(ch)))
                {
                    MessageBox.Show("Фамилия не может быть:\n" +
                                    "-> Меньше 2 символов\n" +
                                    "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Surname = value;
                OnPropertyChanged();
            }
        }

        private string _Patronymic;
        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string Patronymic
        {
            get => _Patronymic;
            set
            {
                if (Equals(_Patronymic, value)) return;

                if (value.Length < 2 || value.Any(ch => char.IsWhiteSpace(ch)))
                {
                    MessageBox.Show("Отчество не может быть:\n" +
                                    "-> Меньше 2 символов\n" +
                                    "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Patronymic = value;
                OnPropertyChanged();
            }
        }

        private string _Login;
        /// <summary>
        /// Логин клиента
        /// </summary>
        public string Login
        {
            get => _Login;
            set
            {
                if (Equals(_Login, value)) return;

                if (value.Length < 6 || value.Any(ch => char.IsWhiteSpace(ch)))
                {
                    MessageBox.Show("Отчество не может быть:\n" +
                                    "-> Меньше 6 символов\n" +
                                    "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Login = value;
                OnPropertyChanged();
            }
        }

        private string _Password;
        /// <summary>
        /// Пароль клиента
        /// </summary>
        public string Password
        {
            get => _Password;
            set
            {
                if (Equals(_Password, value)) return;

                if (value.Length < 5)
                {
                    if (value.Length < 2 || value.Any(ch => char.IsWhiteSpace(ch)))
                    {
                        MessageBox.Show("Пароль не может быть:\n" +
                                        "-> Меньше 5 символов\n" +
                                        "-> Содержать пробелы", "Ошибка ввода", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                    }
                }

                _Password = value;
                OnPropertyChanged();
            }
        }

        private bool _Sex;
        /// <summary>
        /// Пол клиента
        /// </summary>
        public bool Sex
        {
            get => _Sex;
            set
            {
                if (Equals(_Sex, value)) return;
                _Sex = value;
                OnPropertyChanged();
            }
        }

        private DateTime _Register_data;
        /// <summary>
        /// Дата регистрации клиента
        /// </summary>
        public DateTime Register_data
        {
            get => _Register_data;
            set
            {
                if (Equals(_Register_data, value)) return;
                _Register_data = value;
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

            _Bank_Client.Client_name = _Name;
            _Bank_Client.Client_surname = _Surname;
            _Bank_Client.Client_patronymic = _Patronymic;
            _Bank_Client.Client_login = _Login;
            _Bank_Client.Client_password = _Password;
            _Bank_Client.Client_sex = _Sex;
            _Bank_Client.Client_company = _SelectedBankCompany.Clcomp_id;
            _Bank_Client.Bank_client_company = _SelectedBankCompany;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_client.Update(_Bank_Client);
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
            /// Данные нового пользователя
            Bank_client NewClient = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                _Surname == null ||
                _Patronymic == null ||
                _Login == null ||
                _Password == null ||
                _SelectedBankCompany == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewClient.Client_name = _Name;
            NewClient.Client_surname = _Surname;
            NewClient.Client_patronymic = _Patronymic;
            NewClient.Client_login = _Login;
            NewClient.Client_password = _Password;
            NewClient.Client_company = _SelectedBankCompany.Clcomp_id;
            NewClient.Client_sex = _Sex;
            NewClient.Client_register_data = DateTime.Now;


            #endregion 

            /// Добавление в базу данных нового пользователя
            _DataBase.Bank_client.Add(NewClient);
            _DataBase.SaveChanges();

            /// Обновление таблицы в Профилях
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлен новый Клиент \n" +
                            $"Ф.И.О.: {NewClient.Client_name} {NewClient.Client_patronymic} {NewClient.Client_surname} ");
            _workSpaceWindowViewModel.SetUpdateTabel();
            _BunkClientWindow.Close();
        }

        #endregion Добавление данных

        #endregion Обновление

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна Профиля
        /// </summary>
        public ICommand CloseWindowCommand { get; }

        private bool CanCloseWindowCommandExecuted(object p) => true;

        private void OnCloseWindowCommandExecute(object p)
        {
            _BunkClientWindow.Close();
        }

        #endregion 

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования профиля пользователя
        /// </summary>
        /// <param name="workVM">ViewModel Окна профилей</param>
        /// <param name="bank_Client">Объект Bank_Client заполненный данными из выбранного(Selected) в таблице</param>
        public ClientViewModel(WorkSpaceWindowViewModel workVM, Bank_client bank_Client)
        {

            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _TitleName = $"Изменить: {bank_Client.Client_name} {bank_Client.Client_patronymic}";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            /// Данные Профиля (пользователя)
            _Bank_Client = bank_Client;

            #region Значение свойство пользователя

            _Name = bank_Client.Client_name;
            _Surname = bank_Client.Client_surname;
            _Patronymic = bank_Client.Client_patronymic;
            _Login = bank_Client.Client_login;
            _Password = bank_Client.Client_password;
            _Sex = bank_Client.Client_sex;
            _Register_data = bank_Client.Client_register_data;

            #endregion Значение свойство пользователя

            /// Выборка компаний
            _BankCompany = _DataBase.Bank_client_company.ToList();

            /// Выделить текущий статус
            _SelectedBankCompany = _BankCompany
                .SingleOrDefault(bc => bc.Clcomp_id == bank_Client.Client_company);

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        /// <summary>
        /// Конструктор для добавления Нового профиля
        /// </summary>
        /// <param name="profilesWM">ViewModel Окна профилей</param>
        /// <param name="actionName">Название операции</param>
        public ClientViewModel(WorkSpaceWindowViewModel workVM, string actionName)
        {
            _NameAction = actionName;

            /// Окно с профилями
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _TitleName = $"Добавить нового Клиента";

            /// Контекст базы данных
            _DataBase = _workSpaceWindowViewModel.User.DataBase;

            _Register_data = DateTime.Now;

            /// Выборка компаний
            _BankCompany = _DataBase.Bank_client_company.ToList();

            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public ClientViewModel(Bank_client bank_Client, BankDbContext dbContext)
        {

            /// Заголовок окна с именем
            _TitleName = $"Просмотр: {bank_Client.Client_name} {bank_Client.Client_patronymic}";

            /// Данные Профиля (пользователя)
            _Bank_Client = bank_Client;

            #region Значение свойство пользователя

            _Name = bank_Client.Client_name;
            _Surname = bank_Client.Client_surname;
            _Patronymic = bank_Client.Client_patronymic;
            _Login = bank_Client.Client_login;
            _Password = bank_Client.Client_password;
            _Sex = bank_Client.Client_sex;
            _Register_data = bank_Client.Client_register_data;

            #endregion Значение свойство пользователя

            /// Выборка компаний
            _BankCompany = dbContext.Bank_client_company.ToList();

            /// Выделить текущий статус
            _SelectedBankCompany = _BankCompany
                .SingleOrDefault(bc => bc.Clcomp_id == bank_Client.Client_company);

            IsEnabled = false;

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseWindowCommand = new ActionCommand(OnCloseWindowCommandExecute, CanCloseWindowCommandExecuted);
        }

        #endregion Конструкторы

        public void ShowBankClientWindow()
        {
            _BunkClientWindow = new BunkClientWindow()
            {
                DataContext = this
            };
            _BunkClientWindow.ShowDialog();
        }

    }
}
