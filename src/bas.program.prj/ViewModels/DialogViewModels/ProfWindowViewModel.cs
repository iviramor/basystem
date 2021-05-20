using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels
{
    public class ProfWindowViewModel : ViewModel
    {
        #region Свойства

        #region Класс

        private readonly Bank_user_status UserStatus;

        /// <summary>
        /// Окно Свойств Профессии
        /// </summary>
        private ProfWindow _ProfWindow;

        /// <summary>
        /// ViewModel Главного окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _WorkSpaceWindowViewModel;

        #endregion Класс

        #region Свойства окна

        private string _NameAction;

        /// <summary>
        /// Название действия окна
        /// </summary>
        public string NameAction
        {
            get
            {
                return _NameAction;
            }
            set
            {
                if (Equals(_NameAction, value)) return;
                _NameAction = value;
                OnPropertyChanged();
            }
        }

        private string _Title;

        /// <summary>
        /// Название действия окна
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

        private bool _IsVisibility = true;

        /// <summary>
        /// Видимость элементов (Полный доступ)
        /// </summary>
        public bool IsVisibility
        {
            get
            {
                return _IsVisibility;
            }
            set
            {
                if (Equals(_IsVisibility, value)) return;
                _IsVisibility = value;
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

        #endregion Свойства окна

        #region Свойства элементов

        /// <summary>
        /// Название статуса(Должности)
        /// </summary>
        private string _ProfName;

        public string ProfName
        {
            get
            {
                return _ProfName;
            }
            set
            {
                if (Equals(_ProfName, value)) return;

                /// Проверка на длину
                if (value.Length < 2)
                {
                    MessageBox.Show("Название должности не может быть:\n" +
                                    "-> Меньше 2 символов\n", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                /// Проверка на схожесть данных
                if (_WorkSpaceWindowViewModel.User.DataBase.Bank_user_status
                    .Any(user => user.Status_name == value))
                {
                    MessageBox.Show("Данное Название занято!", "Ошибка ввода", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    return;
                }

                _ProfName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Описание статуса(Должности)
        /// </summary>
        private string _ProfDescription = "";

        public string ProfDescription
        {
            get
            {
                return _ProfDescription;
            }
            set
            {
                if (Equals(_ProfDescription, value)) return;
                _ProfDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Полный доступ статуса(Должности)
        /// </summary>
        private bool _ProfFullAccess;

        public bool ProfFullAccess
        {
            get
            {
                return _ProfFullAccess;
            }
            set
            {
                if (Equals(_ProfFullAccess, value)) return;
                _ProfFullAccess = value;
                OnPropertyChanged();
            }
        }

        #endregion Свойства элементов

        #endregion Свойства

        #region Команды

        #region Закрыть окно

        /// <summary>
        /// Команда закрывает окно
        /// </summary>
        public ICommand CloseProfCommand { get; }

        private bool CanCloseProfCommandExecuted(object p) => true;

        private void OnCloseProfCommandExecute(object p)
        {
            _ProfWindow.Close();
        }

        #endregion Закрыть окно

        #region Применить изменения

        public ICommand UpdateDataCommand { get; }

        #region Изменить данные Должности

        private bool CanUpdateDataCommandExecuted(object p) => true;

        private void OnUpdateDataCommandExecute(object p)
        {
            UserStatus.Status_name = _ProfName;
            UserStatus.Status_describ = _ProfDescription;
            UserStatus.Status_full_access = _ProfFullAccess;

            _WorkSpaceWindowViewModel.User.DataBase.Bank_user_status
                .Update(UserStatus);
            _WorkSpaceWindowViewModel.User.DataBase.SaveChanges();

            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _ProfWindow.Close();
        }

        #endregion Изменить данные Должности

        #region Добавить должность

        private bool CanAddDataCommandExecuted(object p) => true;

        private void OnAddDataCommandExecute(object p)
        {
            Bank_user_status bank_User_Status = new();

            bank_User_Status.Status_name = _ProfName;
            bank_User_Status.Status_describ = _ProfDescription;
            bank_User_Status.Status_full_access = _ProfFullAccess;
            bank_User_Status.Status_higher = false;

            _WorkSpaceWindowViewModel.User.DataBase.Bank_user_status
                .Update(bank_User_Status);
            _WorkSpaceWindowViewModel.User.DataBase.SaveChanges();

            MessageBox.Show("Операция выполнена, \n Данные добавлены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _ProfWindow.Close();
        }

        #endregion Добавить должность

        #endregion Применить изменения

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор "Изменить"
        /// </summary>
        /// <param name="adminVM">ViewModel окна администратора</param>
        /// <param name="user_Status">Объект с данными о Статусе(Должности)</param>
        /// <param name="workSpace">ViewModel главного окна</param>
        public ProfWindowViewModel(Bank_user_status user_Status,
                                   ref WorkSpaceWindowViewModel workSpace)
        {
            /// Запись ряда с данными профессии
            UserStatus = user_Status;

            ///Название действия
            NameAction = "Изменить";
            /// Название окна
            Title = $"{NameAction}: {UserStatus.Status_name}";

            #region Добавления значений в свойства

            _ProfName = UserStatus.Status_name;
            _ProfDescription = UserStatus.Status_describ;
            _ProfFullAccess = UserStatus.Status_full_access;

            /// Скрыть CheckBox Полного доступа, если Высшего доступа
            if (!workSpace.User.User.Bank_user_status.Status_higher |
                workSpace.User.User.Bank_user_status.Status_id == UserStatus.Status_id)
                _IsVisibility = false;

            #endregion Добавления значений в свойства

            _WorkSpaceWindowViewModel = workSpace;

            CloseProfCommand = new ActionCommand(OnCloseProfCommandExecute, CanCloseProfCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
        }

        /// <summary>
        /// Конструктор "Добавить"
        /// </summary>
        /// <param name="adminVM">ViewModel Админа</param>
        /// <param name="workSpace">ViewModel Главного окна</param>
        public ProfWindowViewModel(ref WorkSpaceWindowViewModel workSpace)
        {
            ///Название действия
            NameAction = "Добавить";
            /// Название окна
            Title = $"Добавить нового пользователя";

            #region Добавления значений в свойства

            /// Скрыть CheckBox Полного доступа, если Высшего доступа
            if (!workSpace.User.User.Bank_user_status.Status_higher)
                _IsVisibility = false;

            #endregion Добавления значений в свойства

            _WorkSpaceWindowViewModel = workSpace;

            CloseProfCommand = new ActionCommand(OnCloseProfCommandExecute, CanCloseProfCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
        }

        /// <summary>
        /// Конструктор для просмотра Должности
        /// </summary>
        /// <param name="user_Status"></param>
        public ProfWindowViewModel(Bank_user_status user_Status)
        {
            /// Запись ряда с данными профессии
            UserStatus = user_Status;

            ///Название действия
            NameAction = "Просмотр";
            /// Название окна
            Title = $"{NameAction}: {UserStatus.Status_name}";

            #region Добавления значений в свойства

            _ProfName = UserStatus.Status_name;
            _ProfDescription = UserStatus.Status_describ;
            _ProfFullAccess = UserStatus.Status_full_access;

            _IsVisibility = false;
            _IsEnabled = false;

            #endregion Добавления значений в свойства

            CloseProfCommand = new ActionCommand(OnCloseProfCommandExecute, CanCloseProfCommandExecuted);
            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);
        }

        #endregion Конструкторы

        #region Методы класса

        /// <summary>
        /// Отображение окна
        /// </summary>
        public void ShowProfWindow()
        {
            _ProfWindow = new ProfWindow()
            {
                DataContext = this
            };
            _ProfWindow.ShowDialog();
        }

        #endregion Методы класса
    }
}