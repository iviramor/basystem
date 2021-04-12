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
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        private HelloWindow _HelloWindow;

        #region Логин

        private string _Login = "";

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

        public ICommand SignInCommand { get; }

        private bool CanSignInCommandExecuted(object p) => true;

        private void OnSignInCommandExecute(object p)
        {
            var user = _workSpaceWindowViewModel.User.DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .SingleOrDefault(u => u.User_login == Login && u.User_password == Password);

            if (user != null)
            {
                _workSpaceWindowViewModel.UserName = $"В системе: {user.User_name} {user.User_patronymic}";
                _workSpaceWindowViewModel.User.User = user;
                _workSpaceWindowViewModel.User.Session = true;
                if (user.User_status_to_system == 1) _workSpaceWindowViewModel.AdminStatus = true;
                _HelloWindow.Close();
            }
            else MessageBox.Show("Пользователь не найден", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion Авторизация

        #endregion Команды

        public HelloWindowViewModel(WorkSpaceWindowViewModel workWindow)
        {
            _workSpaceWindowViewModel = workWindow;
            SignInCommand = new ActionCommand(OnSignInCommandExecute, CanSignInCommandExecuted);
        }

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