using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogWindows
{
    public class HelloWindowViewModel : ViewModel
    {
        #region Классы

        /// <summary>
        /// ViewModel главного окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Окно HelloWindow
        /// </summary>
        private HelloWindow _HelloWindow;

        #endregion

        #region Логин

        private string _Login = "";
        /// <summary>
        /// Свойство поля ввода Логина
        /// </summary>
        public string Login
        {
            get => _Login;
            set
            {
                if (Equals(_Login, value)) return;
                _Login = value;
                OnPropertyChanged();
            }
        }

        #endregion Логин

        #region Пароль

        public string _Password = "";
        /// <summary>
        /// Свойство поля ввода Пароля
        /// </summary>
        public string Password
        {
            get => _Password;
            set
            {
                if (Equals(_Password, value)) return;
                _Password = value;
                OnPropertyChanged();
            }
        }

        #endregion Пароль

        #region Команды

        #region Авторизация

        /// <summary>
        /// Команда для авторизации в системе, если не нашел, 
        /// то выводит сообщение
        /// если найден, то в сессию помещает пользователя 
        /// и открывает Главное окно, в соответствии с ролью
        /// </summary>
        public ICommand SignInCommand { get; }

        private bool CanSignInCommandExecuted(object p) => true;

        private void OnSignInCommandExecute(object p)
        {
            var user = _workSpaceWindowViewModel.User.DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .Include(u => u.Bank_user_status.Bank_user_access)
                .SingleOrDefault(u => u.User_login == Login && u.User_password == Password);

           /// Процесс заполнения данных 
           /// Открытия элементов доступа
            if (user != null)
            {
                _workSpaceWindowViewModel.UserName = $"{user.User_name} {user.User_patronymic}";
                _workSpaceWindowViewModel.User.User = user;
                _workSpaceWindowViewModel.User.Session = true;

                var accessTable = user.Bank_user_status.Bank_user_access
                    .Where(us => us.Access_user_status == user.User_status_to_system)
                    .ToList();

                if (user.Bank_user_status.Status_full_access)
                {
                    _workSpaceWindowViewModel.AdminBlock = true;
                    _workSpaceWindowViewModel.ProfilesBlock = true;
                }

                /// Поиск доступа к блокам
                /// Профиля и Админ
                foreach (var access in accessTable)
                {
                    var tableInfo = _workSpaceWindowViewModel.User.DataBase
                        .Bank_tables_info
                        .SingleOrDefault(ti => ti.Tables_id == access.Access_name_table);

                    if (tableInfo.Tables_key == "Bank_user")
                    {
                        _workSpaceWindowViewModel.ProfilesBlock = true;
                    }
                    if (tableInfo.Tables_key == "Bank_user_status")
                    {
                        _workSpaceWindowViewModel.AdminBlock = true;
                    }
                }

                _HelloWindow.Close();
            }
            else MessageBox.Show("Пользователь не найден", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion Авторизация

        #endregion Команды

        #region Конструкторы

        /// <summary>
        /// Конструктор ViewModel для HelloWindow
        /// </summary>
        /// <param name="workWindow">ViewModel Главного окна</param>
        public HelloWindowViewModel(WorkSpaceWindowViewModel workWindow)
        {
            _workSpaceWindowViewModel = workWindow;
            SignInCommand = new ActionCommand(OnSignInCommandExecute, CanSignInCommandExecuted);
        }

        #endregion

        #region Методы класса

        /// <summary>
        /// Отображает окно HelloWindow
        /// </summary>
        public void ShowHelloWindow()
        {
            _HelloWindow = new HelloWindow
            {
                DataContext = this,
            };
            _HelloWindow.ShowDialog();
        }

        #endregion
    }
}