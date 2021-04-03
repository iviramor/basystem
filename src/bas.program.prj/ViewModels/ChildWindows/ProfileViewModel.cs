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


        public Bank_user _User = new Bank_user();
        public Bank_user User
        {
            get => _User;
            set
            {
                if (Equals(_User, value)) return;
                _User = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды

        #region Обновление

        public ICommand UpdateDataCommand { get; }

        private bool CanUpdateDataCommandExecuted(object p) => true;

        private void OnUpdateDataCommandExecute(object p)
        {
            /// Смена имени пользователя в главом окне
            _workSpaceWindowViewModel.UserName = User.User_name;

            /// Смена обновленных данных польоватея в сессии
            _workSpaceWindowViewModel.User.User.ChangeUser(_workSpaceWindowViewModel.User.User, User);

            /// Изменение данных в базе данных
            _workSpaceWindowViewModel.User.DataBase.Update(_workSpaceWindowViewModel.User.User);
            _workSpaceWindowViewModel.User.DataBase.SaveChanges();

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
            workVM.User.User.ChangeUser(User, workVM.User.User);
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
