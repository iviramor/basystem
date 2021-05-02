using bas.program.Infrastructure.Commands;
using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models;
using bas.program.Models.Tables;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.DialogViewModels;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels
{
    public class WorkSpaceWindowViewModel : ViewModel
    {

        #region Блоки окна

        #region Действия с окном

        private bool _Visibility = true;
        /// <summary>
        /// Отображение и скрытие нужных элементов, Используется для статуса "Администратор"
        /// </summary>
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
        /// <summary>
        /// Свойство для блокирования элементов в Главном меню
        /// </summary>
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

        private bool _AdminBlock = false;
        /// <summary>
        /// Свойство, в котором храниться доступ к разделу админ
        /// пользователь является администратором
        /// если false, то обычный пользователь
        /// </summary>
        public bool AdminBlock
        {
            get
            {
                return _AdminBlock;
            }
            set
            {
                if (Equals(_AdminBlock, value)) return;
                _AdminBlock = value;
                OnPropertyChanged();
            }
        }

        private bool _ProfilesBlock = false;
        /// <summary>
        /// Свойство, в котором храниться доступ к разделу профилей
        /// пользователь является администратором
        /// если false, то обычный пользователь
        /// </summary>
        public bool ProfilesBlock
        {
            get
            {
                return _ProfilesBlock;
            }
            set
            {
                if (Equals(_ProfilesBlock, value)) return;
                _ProfilesBlock = value;
                OnPropertyChanged();
            }
        }

        #endregion Действия с окном

        #region Combo box с таблицами

        private Bank_user_access _SelectTableItemComboBox;

        public Bank_user_access SelectTableItemComboBox
        {
            get
            {
                return _SelectTableItemComboBox;
            }
            set
            {
                if (Equals(_SelectTableItemComboBox, value)) return;
                _SelectTableItemComboBox = value;

                if (value == null) SetNullAccess();
                else if (value.Access_modification == 1) SetFullAccess();
                else if (value.Access_modification == 2) SetReadOnlyAccess();
                else if (value.Access_modification == 3) SetEditAndReadOnlyAccess();

                SetCurrentTable();

                OnPropertyChanged();
            }
        }


        private List<Bank_user_access> _ItemsTableComboBox = new();

        public List<Bank_user_access> ItemsTableComboBox
        {
            get
            {
                return _ItemsTableComboBox;
            }
            set
            {
                if (Equals(_ItemsTableComboBox, value)) return;
                _ItemsTableComboBox = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Главная таблица

        private DataRowView _SelectedItemMainTable;
        /// <summary>
        /// Выделенный ряд в таблице
        /// </summary>
        public DataRowView SelectedItemMainTable
        {
            get => _SelectedItemMainTable;
            set
            {
                if (Equals(_SelectedItemMainTable, value)) return;
                _SelectedItemMainTable = value;
                OnPropertyChanged();
            }
        }

        private DataTable _MainTable = new();
        /// <summary>
        /// Главная таблица
        /// </summary>
        public DataTable MainTable
        {
            get
            {
                return _MainTable;
            }
            set
            {
                if (Equals(_MainTable, value)) return;
                _MainTable = value;
                OnPropertyChanged();
            }
        }

        # endregion Главная таблица

        #region Доступ к элементам окна

        private bool _AddIsEnabled = true;
        /// <summary>
        /// Доступ к добавлению
        /// </summary>
        public bool AddIsEnabled
        {
            get => _AddIsEnabled;
            set
            {
                if (Equals(_AddIsEnabled, value)) return;
                _AddIsEnabled = value;
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

        private bool _FilterIsEnabled = true;
        /// <summary>
        /// Доступ к Фильтру
        /// </summary>
        public bool FilterIsEnabled
        {
            get => _FilterIsEnabled;
            set
            {
                if (Equals(_FilterIsEnabled, value)) return;
                _FilterIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _MathsIsEnabled = true;
        /// <summary>
        /// Доступ к Расчету
        /// </summary>
        public bool MathsIsEnabled
        {
            get => _MathsIsEnabled;
            set
            {
                if (Equals(_MathsIsEnabled, value)) return;
                _MathsIsEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion Блоки окна

        #region Данные

        private Tables _Tables { get; set; }

        /// <summary>
        /// Поле с данными пользователя в текущей сессии.
        /// </summary>
        public UserDataSession User = new();

        private string _UserName = null;
        /// <summary>
        /// Свойство блока Имя и Фамилия для правого верхнего угла "В системе: "
        /// </summary>
        public string UserName
        {
            get => _UserName;
            set
            {
                if (Equals(_UserName, value)) return;
                _UserName = value;
                OnPropertyChanged();
            }
        }

        #endregion Данные

        #region Команды

        #region Показать Меню авторизации

        /// <summary>
        /// Команда кнопки "Выйти из системы", выполняет выходи из системы
        /// </summary>
        public ICommand ShowSignOutCommand { get; }

        private bool CanShowSignOutCommandExecuted(object p) => true;

        private void OnShowSignOutCommandExecute(object p)
        {
            var que = MessageBox.Show("Вы точно хотите выйти из Системы?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (que == MessageBoxResult.Yes)
            {
                User.Session = false;
                Application.Current.Shutdown();
            }

            return;
        }

        #endregion Показать Меню авторизации

        #region Профиль

        /// <summary>
        /// Команда для отображения окна "Профиля" текущего пользователя в системе
        /// </summary>

        public ICommand ShowProfileCommand { get; }

        private bool CanShowProfileExecuted(object p) => true;

        private void OnShowProfileExecute(object p)
        {
            var profile = new ProfileViewModel(this);
        }

        #endregion Профиль

        #region Профили

        /// <summary>
        /// Отображение окна "Профили"
        /// </summary>
        public ICommand ShowProfilesCommand { get; }

        private bool CanShowProfilesExecuted(object p) => true;

        private void OnShowProfilesExecute(object p)
        {
            var profiles = new ProfilesViewModel(this);
            profiles.ShowProfilesWindow();
        }

        #endregion Профили

        #region Администратор
        public ICommand ShowAdministratorCommand { get; }

        private bool CanShowAdministratorCommandExecuted(object p) => true;

        private void OnShowAdministratorCommandExecute(object p)
        {
            AdministratorViewModel adminVM = new(this);
            adminVM.ShowAdministratorWindow();
        }

        #endregion

        #endregion Команды

        #region Конструктор

        /// <summary>
        /// Конструктор Главного окна
        /// </summary>
        public WorkSpaceWindowViewModel()
        {
            AuthorizeHelloWindow();

            if (!User.Session)
            {
                Application.Current.Shutdown();
                return;
            }

            _Tables = new();

            SetItemsTable();

            if (_SelectTableItemComboBox == null)
            {
                SetNullAccess();
            }


            #region Команды 

            ShowSignOutCommand = new ActionCommand(OnShowSignOutCommandExecute, CanShowSignOutCommandExecuted);
            ShowProfileCommand = new ActionCommand(OnShowProfileExecute, CanShowProfileExecuted);
            ShowProfilesCommand = new ActionCommand(OnShowProfilesExecute, CanShowProfilesExecuted);
            ShowAdministratorCommand = new ActionCommand(OnShowAdministratorCommandExecute, CanShowAdministratorCommandExecuted);

            #endregion
        }

        #endregion

        #region Методы

        #region Ограничение доступа

        private void SetNullAccess()
        {
            AddIsEnabled = false;
            EditIsEnabled = false;
            DelIsEnabled = false;
            FilterIsEnabled = false;
            MathsIsEnabled = false;
        }

        private void SetReadOnlyAccess()
        {
            AddIsEnabled = false;
            EditIsEnabled = false;
            DelIsEnabled = false;
            FilterIsEnabled = true;
            MathsIsEnabled = true;
        }

        private void SetFullAccess()
        {
            AddIsEnabled = true;
            EditIsEnabled = true;
            DelIsEnabled = true;
            FilterIsEnabled = true;
            MathsIsEnabled = true;
        }

        private void SetEditAndReadOnlyAccess()
        {
            AddIsEnabled = false;
            EditIsEnabled = true;
            DelIsEnabled = false;
            FilterIsEnabled = true;
            MathsIsEnabled = true;
        }

        public void CleanAccess()
        {
            SetNullAccess();
            SetItemsTable();
            SelectTableItemComboBox = null;
            MainTable = null;
        }

        #endregion

        #region Установка листа со доступами

        private List<Bank_user_access> GetFullListAccess()
        {
            List<Bank_user_access> bank_User_Accesses = new();

            var AllTableInfoIsNotSys = User.DataBase.Bank_tables_info
                .Where(ti => !ti.Tables_isSystem)
                .ToList();

            foreach (var tableInfo in AllTableInfoIsNotSys)
            {
                bank_User_Accesses
                    .Add(

                        new Bank_user_access()
                        {
                            Access_name_table = tableInfo.Tables_id,
                            Access_modification = 1,
                            Access_user_status = User.User.Bank_user_status.Status_id,
                            Bank_tables_info = tableInfo
                        }
                    );
            }

            return bank_User_Accesses;
        }

        private void SetItemsTable()
        {
            /// Таблица с статусом текущего Пользователя системы
            var Statuses = User.User.Bank_user_status;

            /// Вывод всех данных, если полный доступ
            if (Statuses.Status_full_access)
            {
                ItemsTableComboBox = GetFullListAccess();
                return;
            }

            var AllStatusAccess = User.DataBase.Bank_user_access
                    .Include(ua => ua.Bank_tables_info)
                    .Where(ua => ua.Access_user_status == Statuses.Status_id &
                          !ua.Bank_tables_info.Tables_isSystem)
                    .ToList();

            ItemsTableComboBox = AllStatusAccess;

        }

        #endregion

        #region Работа с таблицей

        private void SetCurrentTable()
        {
            if (_SelectTableItemComboBox == null) return;

            /// Текущий ключ таблицы
            var currentNameTable = _SelectTableItemComboBox.Bank_tables_info.Tables_key;


            MainTable = _Tables.GetTabel(currentNameTable);

        }

        #endregion Работа с таблицей

        #endregion

        #region Диалоговые окна

        #region Окно авторизации

        /// <summary>
        /// Создает и отображает Окно авторизации
        /// </summary>
        /// <returns> 
        /// True - если пользователь прошел авторизацию
        /// False - если не закрыл окно и не прошел регистрацию
        /// </returns>
        private void AuthorizeHelloWindow()
        {
            new HelloWindowViewModel(this).ShowHelloWindow();
        }

        #endregion Окно авторизации

        #endregion Диалоговые окна

    }
}