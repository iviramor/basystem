using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views.ChildViews;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfileViewModel : ViewModel
    {

        #region Поля и свойства 

        private ProfileWindow _ProfileWindow;
        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;
        private BankDbContext _DataBase;

        #region Видимость элементов

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

            #endregion

            /// Изменение данных в базе данных
            _DataBase.Update(_workSpaceWindowViewModel.User.User);
            _DataBase.SaveChanges();

            /// Уведомление об успешой операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

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
            _workSpaceWindowViewModel = workVM;

            _DataBase = workVM.User.DataBase;

            #region Значение совойств пользователя

            _Name = workVM.User.User.User_name;
            _Surname = workVM.User.User.User_surname;
            _Patronymic = workVM.User.User.User_patronymic;
            _Login = workVM.User.User.User_login;
            _Password = workVM.User.User.User_password;
            _Sex = workVM.User.User.User_sex;
            _Register_data = workVM.User.User.User_register_data;

            #endregion

            #region Ограничение доступа к полям

            if (workVM.User.User.User_status_to_system == 2) _StatusEnable = false;
            else if (workVM.User.User.User_status_to_system == 3)
            {
                _IsEnabled = false;
                _IsVisibility = false;
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

        public void ShowProfileWindow()
        {
            
            _ProfileWindow = new ProfileWindow();
            _ProfileWindow.DataContext = this;
            _ProfileWindow.ShowDialog();

        }

    }
}
