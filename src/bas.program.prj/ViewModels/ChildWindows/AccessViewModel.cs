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

        private Bank_tables_info _SelectAllAccess;
        /// <summary>
        /// Выделенный доступ пользователя в листе
        /// </summary>
        public Bank_tables_info SelectAllAccess
        {
            get => _SelectAllAccess;
            set
            {
                if (Equals(_SelectAllAccess, value)) return;
                _SelectAllAccess = value;
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

        private AccessForTable _SelectComboBoxAccess;

        /// <summary>
        /// Выделенный Бокс с доступами извинением к таблице
        /// </summary>
        public AccessForTable SelectComboBoxAccess
        {
            get => _SelectComboBoxAccess;
            set
            {
                if (Equals(_SelectComboBoxAccess, value)) return;
                _SelectComboBoxAccess = value;
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
        /// Свойство Бокса с доступами извинением к таблице
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

        #region Удалить доступ
        public ICommand DelAccessCommand { get; }

        private bool CanDelAccessCommandExecuted(object p) => true;

        private void OnDelAccessCommandExecute(object p)
        {
            if (_SelectAccessUser == null)
            {
                MessageBox.Show("Выделите доступ!", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return;
            }

            /// Обновление данных
            _UserDataSession.DataBase.Bank_user_access.Remove(_SelectAccessUser);
            _UserDataSession.DataBase.SaveChanges();

            SetSourceAccessUser();
        }

        #endregion

        #region Добавить доступ

        public ICommand AddAccessCommand { get; }

        private bool CanAddAccessCommandExecuted(object p) => true;

        private void OnAddAccessCommandExecute(object p)
        {

            /// Проверка выделены ли элементы
            if (_SelectAllAccess == null)
            {
                MessageBox.Show("Выделите таблицу!", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (_SelectComboBoxAccess == null)
            {
                MessageBox.Show("Выделите тип доступа к таблице", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Bank_user_access bua = new()
            {
                Access_user_status = _Bank_User_Status.Status_id,
                Access_name_table = _SelectAllAccess.Tables_id,
                Access_modification = _SelectComboBoxAccess.KeyAccess

            };

            /// Обновление данных
            _UserDataSession.DataBase.Bank_user_access.Add(bua);
            _UserDataSession.DataBase.SaveChanges();

            SetSourceAccessUser();
        }

        #endregion

        #endregion

        #region Конструкторы

        public AccessViewModel(Bank_user_status user_Status, UserDataSession userDataSession)
        {
            _Bank_User_Status = user_Status;
            _UserDataSession = userDataSession;

            SetSourceAccessUser();
            SetSourceAllAccess();

            DelAccessCommand = new ActionCommand(OnDelAccessCommandExecute, CanDelAccessCommandExecuted);
            AddAccessCommand = new ActionCommand(OnAddAccessCommandExecute, CanAddAccessCommandExecuted);

        }

        #endregion

        #region Методы

        private void SetSourceAccessUser()
        {
            ListWithAccessUser = _UserDataSession.DataBase.Bank_user_access
                .Where(table => table.Access_user_status == _Bank_User_Status.Status_id)
                .ToList();
        }

        private void SetSourceAllAccess()
        {
            ListWithAllAccess = _UserDataSession.DataBase.Bank_tables_info
                .ToList();
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
