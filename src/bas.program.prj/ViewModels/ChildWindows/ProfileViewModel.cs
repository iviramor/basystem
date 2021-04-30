using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views.ChildViews;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// ViewModel окна Профилей
        /// </summary>
        private readonly ProfilesViewModel _ProfilesWiewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly BankDbContext _DataBase;

        /// <summary>
        /// Данные пользователя
        /// </summary>
        private readonly Bank_user _BankUser;

        #region Видимость элементов

        private string _TitleName;
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

        #endregion Видимость элементов

        #region Лист со статусами, для окна Добавить профиль

        private List<Bank_user_status> _BankStatuses;

        /// <summary>
        /// Список всех статусов в банке
        /// </summary>
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

        /// <summary>
        /// Храниться выделенный в списке статус, для добавления
        /// </summary>
        public Bank_user_status SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                if (Equals(_SelectedStatus, value)) return;

                /// Проверка на выделенный статус
                if (_SelectedStatus != null)
                {
                    /// Повышение на Администратор
                    if (!_SelectedStatus.Status_higher &&
                        value.Status_higher)
                    {
                        MessageBox.Show("Вы пытаетесь повысить этого сотрудника до Администратор\n" +
                                        $"Смена \"{_SelectedStatus.Status_name}\" на \"{value.Status_name}\"", "Предупреждение", MessageBoxButton.OK,
                            MessageBoxImage.Information);

                    }
                    ///Повышение уровня до "Без полного доступа"
                    if (!_SelectedStatus.Status_full_access && value.Status_full_access)
                    {
                        MessageBox.Show("Этот статус имеет \"Полный доступ\" ! \n" +
                                        $"Смена \"{_SelectedStatus.Status_name}\" на \"{value.Status_name}\"", "Предупреждение", MessageBoxButton.OK,
                                MessageBoxImage.Information);

                    }
                    ///Понижение уровня "Без полного доступа"
                    if (_SelectedStatus.Status_full_access && !value.Status_full_access)
                    {
                        MessageBox.Show("Вы понижаете статус! \n" +
                                        "С полного доступа на НЕ полный доступ\n" +
                                        $"Смена \"{_SelectedStatus.Status_name}\" на \"{value.Status_name}\"", "Предупреждение", MessageBoxButton.OK,
                                MessageBoxImage.Information);

                    }

                    MessageBox.Show("Информация о статусах: \n" +
                                    "------------------------------------------------------------------------\n" +
                                    $"С \"{_SelectedStatus.Status_name}\" имеет: \n" +
                                    $"* - \n" +
                                    $"На \"{value.Status_name}\" имеет: \n" +
                                    $"* - \n");
                }
                
                _SelectedStatus = value;
                OnPropertyChanged();
            }
        }

        #endregion Лист со статусами, для окна Добавить профиль

        #region Свойства пользователя

        public string _Name;

        /// <summary>
        /// Имя пользователя(Профиля)
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
        /// Фамилия пользователя(Профиля)
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
        /// Отчество пользователя(Профиля)
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

        private DateTime _Age = DateTime.Now;

        /// <summary>
        /// Возраст пользователя(Профиля)
        /// </summary>
        public DateTime Age
        {
            get => _Age;
            set
            {
                _Age = value;
                OnPropertyChanged();
            }
        }

        private string _Login;

        /// <summary>
        /// Логин пользователя(Профиля)
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
        /// Пароль пользователя(Профиля)
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
        /// Пол пользователя(Профиля)
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
        /// Дата регистрации пользователя(Профиля)
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
            /// Смена имени пользователя в главном окне
            _workSpaceWindowViewModel.UserName =  $"{_Name} {_Surname}";

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

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Update(_workSpaceWindowViewModel.User.User);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
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
            _BankUser.User_register_data = _Register_data;
            _BankUser.User_age = _Age;

            #endregion Смена изменений в сессии пользователя

            /// Сравнение сессии и изменяемого Профиля, для смены имени окна
            if (_ProfilesWiewModel._workSpaceWindowViewModel.User.User.User_id == _BankUser.User_id)
            {
                _ProfilesWiewModel._workSpaceWindowViewModel.UserName = $"{_Name} {_Surname}";
                if (_ProfilesWiewModel._workSpaceWindowViewModel.User.
                    User.User_status_to_system != _SelectedStatus.Status_id)
                {
                    var que = MessageBox.Show("Вы изменили свою должность\n" +
                                    "Для применения изменений, необходимо перезагрузить " +
                                    "и заново авторизоваться в Системе", 
                                    "Уведомление",
                                    MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (que == MessageBoxResult.OK)
                    {
                        _BankUser.User_status_to_system = _SelectedStatus.Status_id;

                        /// Изменение данных в базе данных
                        _DataBase.Update(_BankUser);
                        _DataBase.SaveChanges();

                        Application.Current.Shutdown();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            /// Изменение данных в базе данных
            _DataBase.Update(_BankUser);
            _DataBase.SaveChanges();

            /// Обновление таблицы в Профилях
            _ProfilesWiewModel.UpdateTable();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        
        }

        #endregion Изменение данных

        #region Добавление данных

        private bool CanAddDataCommandExecuted(object p) => true;

        private void OnAddDataCommandExecute(object p)
        {
            /// Данные нового пользователя
            Bank_user NewUser = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                _Surname == null ||
                _Patronymic == null ||
                _Login == null ||
                _Password == null ||
                _SelectedStatus == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewUser.User_name = _Name;
            NewUser.User_surname = _Surname;
            NewUser.User_patronymic = _Patronymic;
            NewUser.User_login = _Login;
            NewUser.User_password = _Password;
            NewUser.User_sex = _Sex;
            NewUser.User_status_to_system = _SelectedStatus.Status_id;
            NewUser.User_register_data = DateTime.Now;
            NewUser.User_age = _Age;


            #endregion 

            /// Добавление в базу данных нового пользователя
            _DataBase.Bank_user.Add(NewUser);
            _DataBase.SaveChanges();

            /// Обновление таблицы в Профилях
            _ProfilesWiewModel.UpdateTable();

            MessageBox.Show("Добавлен новый пользователь \n" +
                            $"Ф.И.О.: {NewUser.User_name} {NewUser.User_patronymic} {NewUser.User_surname} \n" +
                            $"Должность: {NewUser.Bank_user_status.Status_name}");

            _ProfileWindow.Close();
        }

        #endregion Добавление данных

        #endregion Обновление

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна Профиля
        /// </summary>
        public ICommand CloseProfileCommand { get; }

        private bool CanCloseProfileCommandExecuted(object p) => true;

        private void OnCloseProfileCommandExecute(object p)
        {
            _ProfileWindow.Close();
        }

        #endregion 

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор для изменения профиля в Главном меню
        /// </summary>
        /// <param name="workVM">ViewModel Главного окна</param>
        public ProfileViewModel(WorkSpaceWindowViewModel workVM)
        {

            /// Главное окно
            _workSpaceWindowViewModel = workVM;

            /// Заголовок окна с именем
            _TitleName = $"Изменить: {workVM.UserName}";

            /// Контекст базы данных
            _DataBase = workVM.User.DataBase;

            #region Значение свойство пользователя

            _Name = workVM.User.User.User_name;
            _Surname = workVM.User.User.User_surname;
            _Patronymic = workVM.User.User.User_patronymic;
            _Login = workVM.User.User.User_login;
            _Password = workVM.User.User.User_password;
            _Sex = workVM.User.User.User_sex;
            _Register_data = workVM.User.User.User_register_data;
            _Age = workVM.User.User.User_age;

            #endregion Значение свойство пользователя

            #region Список статусов

            _BankStatuses = _DataBase.Bank_user_status
                .ToList();

            _SelectedStatus = _DataBase.Bank_user_status
                .SingleOrDefault(status => status.Status_id == workVM.User.User.User_status_to_system);

            #endregion Список статусов

            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);

            #region Ограничение доступа к полям

            var TableAccess = workVM.User.User
                .Bank_user_status.Bank_user_access
                .SingleOrDefault(ua => ua.Bank_tables_info.Tables_key == "Bank_user");

            /// Проверка На статус Администратор
            if (workVM.User.User.Bank_user_status.Status_full_access) _StatusEnable = false;

            #endregion Ограничение доступа к полям

            ShowProfileWindow();
        }

        /// <summary>
        /// Конструктор для редактирования профиля пользователя
        /// </summary>
        /// <param name="profilesWM">ViewModel Окна профилей</param>
        /// <param name="bankUser">Объект Bank_user заполненный данными из выбранного(Selected) в таблице</param>
        public ProfileViewModel(ProfilesViewModel profilesWM, Bank_user bankUser)
        {
            /// Окно с профилями
            _ProfilesWiewModel = profilesWM;

            /// Заголовок окна с именем
            _TitleName = $"Изменить: {bankUser.User_name} {bankUser.User_patronymic}";

            /// Контекст базы данных
            _DataBase = profilesWM._workSpaceWindowViewModel.User.DataBase;

            /// Данные Профиля (пользователя)
            _BankUser = bankUser;

            #region Значение свойство пользователя

            _Name = bankUser.User_name;
            _Surname = bankUser.User_surname;
            _Patronymic = bankUser.User_patronymic;
            _Login = bankUser.User_login;
            _Password = bankUser.User_password;
            _Sex = bankUser.User_sex;
            _Register_data = bankUser.User_register_data;
            _Age = bankUser.User_age;

            #endregion Значение свойство пользователя

            /// Разблокировать список статусов
            _StatusEnable = true;

            /// Если Текущий пользователь является Высшим 
            /// и его id равен id того профиля который изменяется
            if (profilesWM._workSpaceWindowViewModel.User.User.Bank_user_status.Status_higher &
                profilesWM._workSpaceWindowViewModel.User.User.User_id == bankUser.User_id)
            {
                /// Выборка статусов
                _BankStatuses = _DataBase.Bank_user_status.ToList();
                _StatusEnable = false;

            }
            /// Если текущий пользователь - Админ
            else if (profilesWM._workSpaceWindowViewModel.User.User.Bank_user_status.Status_higher)
            {
                _BankStatuses = _DataBase.Bank_user_status
                    .Include(st => st.Bank_user_access)
                    .ToList();
            }
            /// Если изменяемый профиль выше текущего
            else if (profilesWM._workSpaceWindowViewModel.User.User.Bank_user_status.Status_higher 
                != bankUser.Bank_user_status.Status_higher)
            {
                _BankStatuses = _DataBase.Bank_user_status.ToList();
                _IsEnabled = false;
                _StatusEnable = false;
            }
            else
            {
                /// Выборка статусов (кроме админа)
                _BankStatuses = _DataBase.Bank_user_status
                    .Include(st => st.Bank_user_access)
                    .Where(st => !st.Status_higher)
                    .ToList();
            }

            /// Выделить текущий статус
            _SelectedStatus = _DataBase.Bank_user_status
                .SingleOrDefault(status => status.Status_id == bankUser.User_status_to_system);

            UpdateDataCommand = new ActionCommand(OnUpdateProfileExecute, CanUpdateProfileCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);
        }

        /// <summary>
        /// Конструктор для добавления Нового профиля
        /// </summary>
        /// <param name="profilesWM">ViewModel Окна профилей</param>
        /// <param name="actionName">Название операции</param>
        public ProfileViewModel(ProfilesViewModel profilesWM, string actionName)
        {
            _NameAction = actionName;

            /// Окно с профилями
            _ProfilesWiewModel = profilesWM;

            /// Заголовок окна с именем
            _TitleName = $"Добавить новый профиль";

            /// Контекст базы данных
            _DataBase = profilesWM._workSpaceWindowViewModel.User.DataBase;

            _Register_data = DateTime.Now;

            /// Разблокировать список статусов
            _StatusEnable = true;

            /// Выборка статусов (кроме админа)
            _BankStatuses = _DataBase.Bank_user_status
                .Where(x => x.Status_id != 1)
                .ToList();

            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
            CloseProfileCommand = new ActionCommand(OnCloseProfileCommandExecute, CanCloseProfileCommandExecuted);
        }

        #endregion Конструкторы

        public void ShowProfileWindow()
        {
            _ProfileWindow = new ProfileWindow
            {
                DataContext = this
            };
            _ProfileWindow.ShowDialog();
        }
    }
}