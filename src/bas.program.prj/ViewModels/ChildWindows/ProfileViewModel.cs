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

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfileViewModel : ViewModel
    {

        private ProfileWindow _ProfileWindow;
        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        public Bank_user User { get; private set; }


        #region Команды

        #region Обновление

        public ICommand UpdateDataCommand { get; }

        private bool CanUpdateDataCommandExecuted(object p) => true;

        private void OnUpdateDataCommandExecute(object p)
        {
            _workSpaceWindowViewModel.UserName = User.User_name;
            MessageBox.Show("Данные обновлены");
            _workSpaceWindowViewModel.User.DataBase.Update(User);
            _workSpaceWindowViewModel.User.DataBase.SaveChanges();

        }

        #endregion


        #endregion



        public ProfileViewModel(WorkSpaceWindowViewModel workVM)
        {
            _workSpaceWindowViewModel = workVM;
            User = workVM.User.User;
            UpdateDataCommand = new ActionCommand(OnUpdateDataCommandExecute, CanUpdateDataCommandExecuted);

        }



        public void ShowProfileWindow()
        {
            
            _ProfileWindow = new ProfileWindow();
            _ProfileWindow.DataContext = this;
            _ProfileWindow.ShowDialog();

        }
    }
}
