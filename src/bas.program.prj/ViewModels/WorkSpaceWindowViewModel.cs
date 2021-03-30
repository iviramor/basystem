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
    public class WorkSpaceWindowViewModel : ViewModel
    {

        private string _User = "User";

        public string User
        {
            get => _User;
            set
            {
                if (Equals(_User, value)) return;
                _User = value;
                OnPropertyChanged();
            }
        }

        #region Команды

        #region Показать Меню авторизации

        public ICommand ShowSignInCommand { get; }

        private bool CanShowSignInCommandExecuted(object p) => true;

        private void OnShowSignInCommandExecute(object p)
        {
            new HelloWindow().Show();
        }

        #endregion

        #endregion


        public WorkSpaceWindowViewModel()
        {
            ShowSignInCommand = new ActionCommand(OnShowSignInCommandExecute, CanShowSignInCommandExecuted);
        }
    }
}
