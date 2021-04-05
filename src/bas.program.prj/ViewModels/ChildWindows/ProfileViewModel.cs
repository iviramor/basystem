using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views.ChildViews;
using bas.website.Models.Data;

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfileViewModel : ViewModel
    {

        #region Поля и свойства 

        /// <summary>
        /// Окно профиля View
        /// </summary>
        private ProfileWindow _ProfileWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// ViewModel окна Профилей
        /// </summary>
        private ProfilesViewModel _ProfilesWiewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private BankDbContext _DataBase;

        /// <summary>
        /// Данные пользователя
        /// </summary>
        private Bank_user _BankUser;

        #region Видимость элементов

        private string _NameAction = "Изменить";
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

        private bool _AccessLabel = false;
        public bool AccessLabel
        {
            get => _AccessLabel;
            set
            {
                if (Equals(_AccessLabel, value)) return;
                _AccessLabel = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisibility = true;
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

        private bool _StatusEnable = false;
        public bool StatusEnable
        {
            get => _StatusEnable;
            set
            {
                if (Equals(_StatusEnable, value)) return;
                _StatusEnable = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Лист со статусами

        private List<Bank_user_status> _BankStatuses;

        public List<Bank_user_status> BankStatuses
        {
            get => _BankStatuses;
            set
            {
                if (Equals(_BankStatuses, value)) return;
                _BankStatuses = value;
                OnPropertyChanged();
            }
        }

        private Bank_user_status _SelectedStatus;

        public Bank_user_status SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                if (Equals(_SelectedStatus, value)) return;
                _SelectedStatus = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Свойства пользователя

        public string _Name;
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

        private DateTime _Age;
        public DateTime Age
        {
            get => _Age;
            set
            {
                if (Equals(_Age, value)) return;
                _Age = value;
                OnPropertyChanged();
            }
        }

        private string _Login;
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


        #endregion

        #endregion

        #region Команды

        #region Обновление

        public ICommand UpdateDataCommand { get; }

        #region Изменение данных

        private bool CanUpdateDataCommandExecuted(object p) => true;

        private void OnUpdateDataCommandExecute(object p)
        {
            /// Смена имени пользователя в главом окне
            _workSpaceWindowViewModel.UserName = _Name;

            #region Смена изменений в сессии пользователя

            _workSpaceWindowViewModel.User.User.User_name = _Name;
            _workSpaceWindowViewModel.User.User.User_surname = _Surname;
            _workSpaceWindowViewModel.User.User.User_patronymic = _Patronymic;
            _workSpaceWindowViewModel.User.User.User_login = _Login;
            _workSpaceWindowViewModel.User.User.User_password = _Password;
            _workSpaceWindowViewModel.User.User.User_sex = _Sex;
            _workSpaceWindowViewModel.User.User.User_status_to_system = _SelectedStatus.Status_id;
            _workSpaceWindowViewModel.User.User.User_register_data = _Register_data;
            _workSpaceWindowViewModel.User.User.User_age = _Age;

            #endregion

            /// Изменение данных в базе данных
            _DataBase.Update(_workSpaceWindowViewModel.User.User);
            _DataBase.SaveChanges();

            /// Уведомление об успешой операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanUpdateProfileCommandExecuted(object p) => true;

        private void OnUpdateProfileExecute(object p)
        {

            #region Смена изменений в сессии пользователя

            _BankUser.User_name = _Name;
            _BankUser.User_surname = _Surname;
            _BankUser.User_patronymic = _Patronymic;
            _BankUser.User_login = _Login;
            _BankUser.User_password = _Password;
            _BankUser.User_sex = _Sex;
            _BankUser.User_status_to_system = _SelectedStatus.Status_id;
            _BankUser.User_register_data = _Register_data;
            _BankUser.User_age = _Age;

            #endregion

            /// Сравнение сессии и изменяемого Профиля, для смены имени окна
            if (_ProfilesWiewModel._workSpaceWindowViewModel.User.User.User_id == _BankUser.User_id)
            {
                _ProfilesWiewModel._workSpaceWindowViewModel.UserName = _BankUser.User_name + " " + _BankUser.User_surname;
            }

            /// Изменение данных в базе данных
            _DataBase.Update(_BankUser);
            _DataBase.SaveChanges();

            /// Обновление таблицы в Профилях
            _ProfilesWiewModel.UpdateTable();

            /// Уведомление об успешой операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region Добавление данных
        private bool CanAddDataCommandExecuted(object p) => true;

        private void OnAddDataCommandExecute(object p)
        {

        }

        #endregion

        #endregion

        #region Закрыть окно

        public ICommand CloseProfileCommand { get; }
        

        private bool CanCloseProfileCommandExecuted(object p) => true;

        private void OnCloseProfileCommandExecute(object p)
        {
            _ProfileWindow.Close();
        }

        #endregion

        #endregion

        public ProfileViewModel(WorkSpaceWindowViewModel workVM)
        {

            /// Главное окно
            _workSpaceWindowViewModel = workVM;

            /// Контекст базы данных
            _DataBase = workVM.User.DataBase;

            #region Значение совойств пользователя

            _Name = workVM.User.User.User_name;
            _Surname = workVM.User.User.User_surname;
            _Patronymic = workVM.User.User.User_patronymic;
            _Login = workVM.User.User.User_login;
            _Password = workVM.User.User.User_password;
            _Sex = workVM.User.User.User_sex;
            _Register_data = workVM.User.User.User_register_data;
            _Age = workVM.User.User.User_age;

            #endregion

            #region Ограничение доступа к полям

            if (workVM.User.User.User_status_to_system == 1) _StatusEnable = false;
            else if (workVM.User.User.User_status_to_system == 2)
            {
                _IsEnabled = false;
                _IsVisibility = false;
                _AccessLabel = true;
            }
            #endregion

            #region Список статусов

            _BankStatuses = _DataBase.Bank_user_status
                .ToList();

            _SelectedStatus = _DataBase.Bank_user_status
                .SingleOrDefault(status => status.Status_id == workVM.User.User.User_status_to_system);

            #endregion

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);

        }

        public ProfileViewModel(ProfilesViewModel profilesWM, Bank_user bankUser)
        {

            /// Окно с профилями
            _ProfilesWiewModel = profilesWM;

            /// Контекст базы данных
            _DataBase = profilesWM._workSpaceWindowViewModel.User.DataBase;


            /// Данные Профиля (пользователя)
            _BankUser = bankUser;

            #region Значение совойств пользователя

            _Name = bankUser.User_name;
            _Surname = bankUser.User_surname;
            _Patronymic = bankUser.User_patronymic;
            _Login = bankUser.User_login;
            _Password = bankUser.User_password;
            _Sex = bankUser.User_sex;
            _Register_data = bankUser.User_register_data;
            _Age = bankUser.User_age;

            #endregion

            /// Разблокировать список статусов
            _StatusEnable = true;

            if (bankUser.User_status_to_system == 1)
            {
                /// Выборка статусов (кроме админа)
                _BankStatuses = _DataBase.Bank_user_status.ToList();
                _StatusEnable = false;
            }
            else
            {
                /// Выборка статусов (кроме админа)
                _BankStatuses = _DataBase.Bank_user_status
                    .Where(x => x.Status_id != 1)
                    .ToList();
            }

            /// Выделить текущий статус
            _SelectedStatus = _DataBase.Bank_user_status
                .SingleOrDefault(status => status.Status_id == bankUser.User_status_to_system);

            UpdateDataCommand = new ActionCommand(OnUpdateProfileExecute, CanUpdateProfileCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);

        }

        public ProfileViewModel(ProfilesViewModel profilesWM, string actionName)
        {

            _NameAction = actionName;

            /// Окно с профилями
            _ProfilesWiewModel = profilesWM;

            /// Контекст базы данных
            _DataBase = profilesWM._workSpaceWindowViewModel.User.DataBase;


            /// Разблокировать список статусов
            _StatusEnable = true;

            /// Выборка статусов (кроме админа)
            _BankStatuses = _DataBase.Bank_user_status
                .Where(x => x.Status_id != 1)
                .ToList();

            UpdateDataCommand = new ActionCommand(OnUpdateProfileExecute, CanUpdateProfileCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);

        }


        public void ShowProfileWindow()
        {
            
            _ProfileWindow = new ProfileWindow();
            _ProfileWindow.DataContext = this;
            _ProfileWindow.ShowDialog();

        }

    }
}
