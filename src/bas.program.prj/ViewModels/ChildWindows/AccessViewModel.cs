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

        private int Access;

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

        private string _Title;
        /// <summary>
        /// Название действия окна
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (Equals(_Title, value)) return;
                _Title = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства элементов Окна

        private string _AddOrEditButton = "Добавить";
        /// <summary>
        /// Кнопка добавить или удалить Доступ
        /// </summary>
        public string AddOrEditButton
        {
            get => _AddOrEditButton;
            set
            {
                if (Equals(_AddOrEditButton, value)) return;
                _AddOrEditButton = value;
                OnPropertyChanged();
            }
        }

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
                Bank_user_access bua = GetUserAccess(value);

                /// Если не найдено "Добавить"
                /// если есть то "Изменить"
                if (bua == null)
                {
                    if (Access == 3)
                    {
                        EditIsEnabled = false;
                    }
                    AddOrEditButton = "Добавить";
                }
                else
                {
                    if (Access == 3)
                    {
                        EditIsEnabled = true;
                    }
                    AddOrEditButton = "Изменить";
                }

                /// блокировка изменения таблицы "Пользовательский доступ", 
                /// если нет полного доступа
                if (value.Tables_key == "Bank_user_access" && Access != -1)
                {
                    EditIsEnabled = false;
                }

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

        #region Доступ к элементам окна

        private bool _AccessListIsEnabled = true;
        /// <summary>
        /// Доступ к добавлению
        /// </summary>
        public bool AccessListIsEnable
        {
            get => _AccessListIsEnabled;
            set
            {
                if (Equals(_AccessListIsEnabled, value)) return;
                _AccessListIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _EditIsEnabled = true;
        /// <summary>
        /// Доступ к Редактированию
        /// </summary>
        public bool EditIsEnabled
        {
            get => _EditIsEnabled;
            set
            {
                if (Equals(_EditIsEnabled, value)) return;
                _EditIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _DelIsEnabled = true;
        /// <summary>
        /// Доступ к Редактированию
        /// </summary>
        public bool DelIsEnabled
        {
            get => _DelIsEnabled;
            set
            {
                if (Equals(_DelIsEnabled, value)) return;
                _DelIsEnabled = value;
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
                MessageBox.Show("Выделите таблицу!", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                return;
            }

            /// Обновление данных
            _UserDataSession.DataBase.Bank_user_access.Remove(_SelectAccessUser);
            _UserDataSession.DataBase.SaveChanges();

            SetSourceAccessUser();
            SetSourceAllAccess();
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

            Bank_user_access bua = GetUserAccess(_SelectAllAccess);

            /// Если не найдено то
            /// создает новый
            if (bua == null)
            {
                bua = new()
                {
                    Access_user_status = _Bank_User_Status.Status_id,
                    Access_name_table = _SelectAllAccess.Tables_id,
                    Access_modification = _SelectComboBoxAccess.KeyAccess

                };

            }
            else
            {
                bua.Access_modification = _SelectComboBoxAccess.KeyAccess;
            }

            _UserDataSession.DataBase.Bank_user_access.Update(bua);
            _UserDataSession.DataBase.SaveChanges();

            AddOrEditButton = "Изменить";
            SetSourceAccessUser();
        }

        /// <summary>
        /// Выборка статуса на изменения
        /// </summary>
        /// <param name="bank_Tables_Info"></param>
        /// <returns></returns>
        public Bank_user_access GetUserAccess(Bank_tables_info bank_Tables_Info)
        {
            Bank_user_access bua = _UserDataSession.DataBase.Bank_user_access
                        .SingleOrDefault(ua => ua.Access_name_table == bank_Tables_Info.Tables_id &&
                                         ua.Access_user_status == _Bank_User_Status.Status_id);
            return bua;
        }

        #endregion

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна Профиля
        /// </summary>
        public ICommand CloseAccessCommand { get; }

        private bool CanCloseAccessCommandExecuted(object p) => true;

        private void OnCloseAccessCommandExecute(object p)
        {
            _AccessWindow.Close();
        }

        #endregion 

        #endregion

        #region Конструкторы

        public AccessViewModel(Bank_user_status user_Status, UserDataSession userDataSession)
        {

            _Bank_User_Status = user_Status;
            _UserDataSession = userDataSession;

            _Title = $"Должность: {user_Status.Status_name}";

            SetSourceAccessUser();
            SetSourceAllAccess();

            DelAccessCommand = new ActionCommand(OnDelAccessCommandExecute, CanDelAccessCommandExecuted);
            AddAccessCommand = new ActionCommand(OnAddAccessCommandExecute, CanAddAccessCommandExecuted);
            CloseAccessCommand = new ActionCommand(OnCloseAccessCommandExecute, CanCloseAccessCommandExecuted);

            #region Доступ к элементам

            ///Поиск информации таблицы Пользователей
            var tableInfo = userDataSession.DataBase.Bank_tables_info
                .SingleOrDefault(ti => ti.Tables_key == "Bank_user_access");

            /// Поиск доступа к таблице Bank_user_access
            var tableAccess = userDataSession.User.Bank_user_status.Bank_user_access
                .SingleOrDefault(ua => ua.Access_name_table == tableInfo.Tables_id);


            if (userDataSession.User.Bank_user_status.Status_full_access)
            {
                Access = -1;
                return;
            }
            else if (tableAccess.Access_modification == 2)
            {
                _AccessListIsEnabled = false;
                _EditIsEnabled = false;
                _DelIsEnabled = false;
                Access = 2;
            }
            else if (tableAccess.Access_modification == 3)
            {
                _EditIsEnabled = false;
                _DelIsEnabled = false;
                Access = 3;
            }

            #endregion

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
