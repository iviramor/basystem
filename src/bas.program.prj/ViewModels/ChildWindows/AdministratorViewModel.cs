using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Data;
using System.Windows.Input;

namespace bas.program.ViewModels.ChildWindows
{
    public class AdministratorViewModel : ViewModel
    {
        /// <summary>
        /// Окно администратора
        /// </summary>
        private AdministratorWindow _administratorWindow;

        #region Свойства

        #region Свойства элементов

        private DataTable _ProfTable;
        /// <summary>
        /// Таблица с должностями
        /// </summary>
        public DataTable ProfTable
        {
            get
            {
                return _ProfTable;
            }
            set
            {
                if (Equals(_ProfTable, value)) return;
                _ProfTable = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Команды

        #region Выйти из окна Админ

        public ICommand CloseAdminCommand { get; }
        private bool CanCloseAdminCommandExecuted(object p) => true;
        private void OnNameCloseAdminCommandExecute(object p)
        {
            _administratorWindow.Close();
        }

        #endregion

        #endregion

        public AdministratorViewModel()
        {

            CloseAdminCommand = new ActionCommand(OnNameCloseAdminCommandExecute, CanCloseAdminCommandExecuted);
        }


        private void UpdateProfTable()
        {

        }

        public void ShowAdministratorWindow()
        {
            _administratorWindow = new()
            {
                DataContext = this
            };
            _administratorWindow.ShowDialog();
        }
    }
}