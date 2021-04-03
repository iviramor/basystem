using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.Infrastructure.Commands.HelloWindowCommands;
using bas.program.Models;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace bas.program.ViewModels
{
    public class HelloWindowViewModel : ViewModel
    {


        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;

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

        #endregion

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

        #endregion

        #region Команды

        #region Авторизация

        public ICommand SignInCommand { get; }

        private bool CanSignInCommandExecuted(object p) => true;

        private void OnSignInCommandExecute(object p)
        {
            var user =  _workSpaceWindowViewModel.User.DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .SingleOrDefault(u => u.User_login == Login && u.User_password == Password); 

            if (user != null)
            {
                MessageBox.Show($"Hello! {user.User_name} {user.User_patronymic} {user.Bank_user_status.Status_name}");
                _workSpaceWindowViewModel.UserName = user.User_name;
                _workSpaceWindowViewModel.User.User = user;
                _workSpaceWindowViewModel.User.Session = true;
                if (user.User_status_to_system == 2) _workSpaceWindowViewModel.AdminStatus = true;
                _HelloWindow.Close();
            }

            else MessageBox.Show("Пользователь не найден");


        }

        #endregion


        #endregion


        public HelloWindowViewModel(WorkSpaceWindowViewModel workWindow)
        {
            _workSpaceWindowViewModel = workWindow;
            SignInCommand = new ActionCommand(OnSignInCommandExecute, CanSignInCommandExecuted);
        }


        public void ShowHelloWindow()
        {
            _HelloWindow = new HelloWindow();
            _HelloWindow.DataContext = this;
            _HelloWindow.ShowDialog();
        }


    }
}
