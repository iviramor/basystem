using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.Infrastructure.Commands.HelloWindowCommands;
using bas.program.ViewModels.Base;
using bas.program.Views;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace bas.program.ViewModels
{
    public class HelloWindowViewModel : ViewModel
    {

        private BankDbContext _DataBase = new BankDbContext();

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
            var user = _DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .SingleOrDefault(u => u.User_login == Login && u.User_password == Password);

            if (user != null) MessageBox.Show($"Hello! {user.User_name} {user.User_patronymic} {user.Bank_user_status.Status_name}");
            else MessageBox.Show("Пользователь не найден");

            new WorkSpaceWindow().Show();

        }

        #endregion

        #endregion


        public HelloWindowViewModel()
        {
            SignInCommand = new ActionCommand(OnSignInCommandExecute, CanSignInCommandExecuted);
        }


    }
}
