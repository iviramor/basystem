﻿using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels
{
    public class HelloWindowViewModel : ViewModel
    {
        /// <summary>
        /// ViewModel главного окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Окно HelloWindow
        /// </summary>
        private HelloWindow _HelloWindow;

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
                .SingleOrDefault(u => u.User_login == Login && u.User_password == Password);

            if (user != null)
            {
                _workSpaceWindowViewModel.UserName = $"{user.User_name} {user.User_patronymic}";
                _workSpaceWindowViewModel.User.User = user;
                _workSpaceWindowViewModel.User.Session = true;
                if (user.User_status_to_system == 1) _workSpaceWindowViewModel.AdminStatus = true;
                _HelloWindow.Close();
            }
            else MessageBox.Show("Пользователь не найден", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion Авторизация

        #endregion Команды

        /// <summary>
        /// Конструктор ViewModel для HelloWindow
        /// </summary>
        /// <param name="workWindow">ViewModel Главного окна</param>
        public HelloWindowViewModel(WorkSpaceWindowViewModel workWindow)
        {
            _workSpaceWindowViewModel = workWindow;
            SignInCommand = new ActionCommand(OnSignInCommandExecute, CanSignInCommandExecuted);
        }

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
    }
}