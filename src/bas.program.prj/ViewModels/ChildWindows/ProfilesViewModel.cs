using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ProfilesViewModel : ViewModel
    {

        #region Поля и Свойства

        private ProfilesWindow _ProfilesWindow;
        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;
        private BankDbContext _DataBase;
        private Bank_user _SelectedItem;

        public Bank_user SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (Equals(_SelectedItem, value)) return;
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_user> _Bank_users;

        public List<Bank_user> Bank_users
        {

            get
            {
                return _Bank_users;
            }
            set
            {
                if (Equals(_Bank_users, value)) return;
                _Bank_users = value;
                OnPropertyChanged();
            }

        }

        #endregion

        #region Команды

        #region Удалить

        public ICommand RemoveDataCommand { get; }

        private bool CanRemoveDataCommandExecuted(object p) => true;

        private void OnRemoveDataCommandExecute(object p)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.User_id != _workSpaceWindowViewModel.User.User.User_id)
                {
                    if (SelectedItem.User_status_to_system != 2)
                    {
                        _DataBase.Remove(SelectedItem);
                        _DataBase.SaveChanges();

                        Bank_users = _DataBase.Bank_user
                            .Include(u => u.Bank_user_status)
                            .ToList();

                        MessageBox.Show($"Успех");
                        return;
                    }

                    MessageBox.Show("Удалять Профиль со статусом - Администатор, запрещено", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;

                }

                MessageBox.Show("Удалять собственный Профиль, запрещено", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;

            }

            MessageBox.Show("Выделите Сотрудника в таблице", "Ошибка ввода", MessageBoxButton.OK,
                MessageBoxImage.Error);


        }

        #endregion

        #region Закрыть окно

        public ICommand CloseProfilesCommand { get; }


        private bool CanCloseProfilesCommandExecuted(object p) => true;

        private void OnCloseProfilesCommandExecute(object p)
        {
            _ProfilesWindow.Close();
        }

        #endregion

        #endregion




        public ProfilesViewModel(WorkSpaceWindowViewModel workVM)
        {
            _workSpaceWindowViewModel = workVM;

            _DataBase = workVM.User.DataBase;

            _Bank_users = _DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .ToList();


            RemoveDataCommand = new ActionCommand(OnRemoveDataCommandExecute, CanRemoveDataCommandExecuted);
            CloseProfilesCommand = new ActionCommand(OnCloseProfilesCommandExecute, CanCloseProfilesCommandExecuted);
        }




        public void ShowProfilesWindow()
        {

            _ProfilesWindow = new ProfilesWindow();
            _ProfilesWindow.DataContext = this;
            _ProfilesWindow.ShowDialog();

        }

    }
}
