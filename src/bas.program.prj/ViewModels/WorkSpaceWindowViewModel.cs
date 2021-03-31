using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using bas.program.Models.Tables.UserTables;


namespace bas.program.ViewModels
{
    public class WorkSpaceWindowViewModel : ViewModel
    {

        private BankDbContext _DataBase = new BankDbContext();

        #region Блоки окна

        #region Действия с окном

        private bool _Visibility = true;

        public bool Visibility
        {
            get => _Visibility;
            set
            {
                if (Equals(_Visibility, value)) return;
                _Visibility = value;
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

        #endregion

        #endregion

        #region Данные

        public bool Session { get; set; } = false;

        private Bank_user _UserData = null;

        public Bank_user UserData
        {
            get => _UserData;
            set
            {
                if (Equals(_UserData, value)) return;
                _UserData = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды

        #region Показать Меню авторизации

        public ICommand ShowSignInCommand { get; }

        private bool CanShowSignInCommandExecuted(object p) => true;

        private void OnShowSignInCommandExecute(object p)
        {
            Session = false;
            Application.Current.Shutdown();
        }

        #endregion

        #region Заблокировать окно

        public ICommand EnableWindowCommand { get; }

        private bool CanEnableWindowCommandExecuted(object p) => true;

        private void OnEnableWindowCommandExecute(object p)
        {
            new HelloWindow().Show();
        }

        #endregion

        #endregion


        public WorkSpaceWindowViewModel()
        {

            if (!Session)
            {
                if (!AuthorizeHelloWindow())
                {
                    Application.Current.Shutdown();
                }
            }


            ShowSignInCommand = new ActionCommand(OnShowSignInCommandExecute, CanShowSignInCommandExecuted);
            EnableWindowCommand = new ActionCommand(OnEnableWindowCommandExecute, CanEnableWindowCommandExecuted);
        }




        #region Диалоговые окна

        

        private bool AuthorizeHelloWindow()
        {
            new HelloWindowViewModel(this).ShowHelloWindow();
            if (Session) return true;
            else return false;

        }

        #endregion








    }
}
