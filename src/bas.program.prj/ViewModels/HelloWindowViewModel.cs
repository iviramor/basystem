using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;

namespace bas.program.ViewModels
{
    public class HelloWindowViewModel : ViewModel
    {

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

        public string _Password;

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




    }
}
