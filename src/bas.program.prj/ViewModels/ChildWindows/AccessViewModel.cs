using bas.program.Infrastructure.Commands;
using bas.program.Models;
using bas.program.Models.Elements;
using bas.program.Models.Tables;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.ChildWindows
{
    public class AccessViewModel : ViewModel
    {

        #region Свойства

        #region Классы

        /// <summary>
        /// Окно Доступа
        /// </summary>
        private AccessWindow _AccessWindow;

        /// <summary>
        /// Статус выделенного пользователя
        /// </summary>
        private Bank_user_status _Bank_User_Status;

        /// <summary>
        /// Сессия пользователя
        /// </summary>
        private UserDataSession _UserDataSession;

        #endregion

        #region Свойства окна


        #endregion

        #region Свойства элементов Окна

        private Bank_user_access _SelectAccessUser;
        /// <summary>
        /// Выделенный доступ пользователя в листе
        /// </summary>
        public Bank_user_access SelectAccessUser
        {
            get => _SelectAccessUser;
            set
            {
                if (Equals(_SelectAccessUser, value)) return;
                _SelectAccessUser = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_user_access> _ListWithAccessUser;

        /// <summary>
        /// Свойство Листа Доступа пользователя
        /// </summary>
        public List<Bank_user_access> ListWithAccessUser
        {
            get => _ListWithAccessUser;
            set
            {
                if (Equals(_ListWithAccessUser, value)) return;
                _ListWithAccessUser = value;
                OnPropertyChanged();
            }
        }

        private List<Bank_tables_info> _ListWithAllAccess;

        /// <summary>
        /// Свойство Листа Всех таблицы
        /// </summary>
        public List<Bank_tables_info> ListWithAllAccess
        {
            get => _ListWithAllAccess;
            set
            {
                if (Equals(_ListWithAllAccess, value)) return;
                _ListWithAllAccess = value;
                OnPropertyChanged();
            }
        }

        private List<AccessForTable> _ComboBoxAccess = new() 
        { 
            new AccessForTable() { NameAccess = "Полный" , KeyAccess = 1},
            new AccessForTable() { NameAccess = "Только чтение", KeyAccess = 2 },
            new AccessForTable() { NameAccess = "Чтение и редактирование", KeyAccess = 3 }

        };

        /// <summary>
        /// Свойство Бокса с доступами измения к таблице
        /// </summary>
        public List<AccessForTable> ComboBoxAccess
        {
            get => _ComboBoxAccess;
            set
            {
                if (Equals(_ComboBoxAccess, value)) return;
                _ComboBoxAccess = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Команды


        public ICommand ShowAccessCommand { get; }

        private bool CanShowAccessCommandExecuted(object p) => true;

        private void OnShowAccessCommandExecute(object p)
        {
            MessageBox.Show($"{_SelectAccessUser.Bank_tables_info.Tables_name}");
        }


        #endregion

        #region Конструкторы

        public AccessViewModel(Bank_user_status user_Status, UserDataSession userDataSession)
        {
            _Bank_User_Status = user_Status;
            _UserDataSession = userDataSession;

            SetSourceAccessUser();
            SetSourceAllAccess();

            ShowAccessCommand = new ActionCommand(OnShowAccessCommandExecute, CanShowAccessCommandExecuted);

        }

        #endregion

        #region Методы

        private void SetSourceAccessUser()
        {
            ListWithAccessUser = _Bank_User_Status.Bank_user_access;
        }

        private void SetSourceAllAccess()
        {
            ListWithAllAccess = _UserDataSession.DataBase.Bank_tables_info.ToList();
        }

        public void ShowAccessWindow()
        {
            _AccessWindow = new()
            {
                DataContext = this
            };
            _AccessWindow.ShowDialog();
            
        }

        #endregion

    }
}
