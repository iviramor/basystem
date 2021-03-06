using bas.program.Infrastructure.Commands;
using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models;
using bas.program.Models.Items;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.DialogViewModels;
using bas.program.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels
{
    public class WorkSpaceWindowViewModel : ViewModel
    {
        #region Блоки окна

        #region Действия с окном

        private bool _Visibility = false;

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

                _SelectedItemMainTable = null;
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

        #endregion Combo box с таблицами

        #region Главная таблица

        private DataRowView _SelectedItemMainTable = null;

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

                Tables.SetSelectedItem(value);

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

        #endregion Доступ к элементам окна

        #region Статус бар

        private int _CountRows;

        /// <summary>
        /// Кол-во записей в таблице
        /// В статус баре
        /// </summary>
        public int CountRows
        {
            get => _CountRows;
            set
            {
                if (Equals(_CountRows, value)) return;
                _CountRows = value;

                OnPropertyChanged();
            }
        }

        private string _NameCurrentTable;

        /// <summary>
        /// Название таблицы
        /// В статус баре
        /// </summary>
        public string NameCurrentTable
        {
            get => _NameCurrentTable;
            set
            {
                if (Equals(_NameCurrentTable, value)) return;
                _NameCurrentTable = value;

                OnPropertyChanged();
            }
        }

        #endregion Статус бар

        #region Боковые блоки

        private bool _MathVisibility = false;

        /// <summary>
        /// Видимость Расчетов
        /// </summary>
        public bool MathVisibility
        {
            get => _MathVisibility;
            set
            {
                if (Equals(_MathVisibility, value)) return;
                _MathVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _FilterVisibility = false;

        /// <summary>
        /// Видимость Фильтра
        /// </summary>
        public bool FilterVisibility
        {
            get => _FilterVisibility;
            set
            {
                if (Equals(_FilterVisibility, value)) return;
                _FilterVisibility = value;
                OnPropertyChanged();
            }
        }

        #region Элементы в фильтре

        private SearchComboBox _SelecedSearchComboBox = null;

        /// <summary>
        /// Колонки для поиска
        /// </summary>
        public SearchComboBox SelecedSearchComboBox
        {
            get
            {
                return _SelecedSearchComboBox;
            }
            set
            {
                if (Equals(_SelecedSearchComboBox, value)) return;
                if (value == null)
                {
                    IsEnableSearch = false;
                }
                else
                {
                    IsEnableSearch = true;
                }
                _SelecedSearchComboBox = value;
                OnPropertyChanged();
            }
        }

        private List<SearchComboBox> _ListSearchComboBox;

        /// <summary>
        /// Лист с колонками для поиска
        /// </summary>
        public List<SearchComboBox> ListSearchComboBox
        {
            get => _ListSearchComboBox;
            set
            {
                if (Equals(_ListSearchComboBox, value)) return;
                _ListSearchComboBox = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEnableSearch = false;

        /// <summary>
        /// Активный фильтр
        /// </summary>
        public bool IsEnableSearch
        {
            get => _IsEnableSearch;
            set
            {
                if (Equals(_IsEnableSearch, value)) return;
                _IsEnableSearch = value;
                OnPropertyChanged();
            }
        }

        #endregion Элементы в фильтре

        #endregion Боковые блоки

        #endregion Главная таблица

        #region Данные

        private Tables Tables { get; set; }

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
            var que = MessageBox.Show("Вы точно хотите выйти из Профиля?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (que == MessageBoxResult.Yes)
            {
                workSpaceWindow.OnClose = true;
                workSpaceWindow.Close();
                helloWindowViewModel.Authorization();
                return;
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
            _ = new ProfileViewModel(this);
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

        #endregion Администратор

        #region Работа с таблицей

        #region Удаление из таблицы

        private ICommand _RemoveFromTabeleCommand;

        /// <summary>
        /// Команда для удаления
        /// </summary>
        public ICommand RemoveFromTabeleCommand
        {
            get => _RemoveFromTabeleCommand;
            set
            {
                if (Equals(_RemoveFromTabeleCommand, value)) return;
                _RemoveFromTabeleCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion Удаление из таблицы

        #region Добавление в таблицы

        private ICommand _AddFromTabeleCommand;

        /// <summary>
        /// Команда для добавления
        /// </summary>
        public ICommand AddFromTabeleCommand
        {
            get => _AddFromTabeleCommand;
            set
            {
                if (Equals(_AddFromTabeleCommand, value)) return;
                _AddFromTabeleCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion Добавление в таблицы

        #region Добавление в таблицы

        private ICommand _EditFromTabeleCommand;

        /// <summary>
        /// Команда для добавления
        /// </summary>
        public ICommand EditFromTabeleCommand
        {
            get => _EditFromTabeleCommand;
            set
            {
                if (Equals(_EditFromTabeleCommand, value)) return;
                _EditFromTabeleCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion Добавление в таблицы

        #region Просмотр в таблице

        private ICommand _ShowFromTabeleCommand;

        /// <summary>
        /// Команда для Просмотра элемента в таблице
        /// </summary>
        public ICommand ShowFromTabeleCommand
        {
            get => _ShowFromTabeleCommand;
            set
            {
                if (Equals(_ShowFromTabeleCommand, value)) return;
                _ShowFromTabeleCommand = value;
                OnPropertyChanged();
            }
        }

        #endregion Просмотр в таблице

        #region Расчеты

        public ICommand ShowMathsCommand { get; }

        private bool CanShowMathsCommandExecuted(object p) => true;

        private void OnShowMathsCommandExecute(object p)
        {
            MathVisibility = true;
        }

        public ICommand CloseMathsCommand { get; }

        private bool CanCloseMathsCommandExecuted(object p) => true;

        private void OnCloseMathsCommandExecute(object p)
        {
            MathVisibility = false;
        }

        #endregion Расчеты

        #region Фильтры

        public ICommand ShowFilterCommand { get; }

        private bool CanShowFilterCommandExecuted(object p) => true;

        private void OnShowFilterCommandExecute(object p)
        {
            FilterVisibility = true;
        }

        public ICommand CloseFilterCommand { get; }

        private bool CanCloseFilterCommandExecuted(object p) => true;

        private void OnCloseFilterCommandExecute(object p)
        {
            FilterVisibility = false;
        }

        #endregion Фильтры

        #endregion Работа с таблицей

        #endregion Команды

        #region Конструктор

        /// <summary>
        /// ViewModel окна приветствие
        /// </summary>
        private HelloWindowViewModel helloWindowViewModel;

        /// <summary>
        /// Рабочее окно
        /// </summary>
        private WorkSpaceWindow workSpaceWindow;

        /// <summary>
        /// Конструктор Главного окна
        /// </summary>
        public WorkSpaceWindowViewModel(HelloWindowViewModel helloWindow)
        {
            helloWindowViewModel = helloWindow;

            Tables = new(this);

            SetNullAccess();

            #region Команды

            ShowSignOutCommand = new ActionCommand(OnShowSignOutCommandExecute, CanShowSignOutCommandExecuted);
            ShowProfileCommand = new ActionCommand(OnShowProfileExecute, CanShowProfileExecuted);
            ShowProfilesCommand = new ActionCommand(OnShowProfilesExecute, CanShowProfilesExecuted);
            ShowAdministratorCommand = new ActionCommand(OnShowAdministratorCommandExecute, CanShowAdministratorCommandExecuted);

            ShowMathsCommand = new ActionCommand(OnShowMathsCommandExecute, CanShowMathsCommandExecuted);
            CloseMathsCommand = new ActionCommand(OnCloseMathsCommandExecute, CanCloseMathsCommandExecuted);
            ShowFilterCommand = new ActionCommand(OnShowFilterCommandExecute, CanShowFilterCommandExecuted);
            CloseFilterCommand = new ActionCommand(OnCloseFilterCommandExecute, CanCloseFilterCommandExecuted);

            #endregion Команды
        }

        #endregion Конструктор

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

        #endregion Ограничение доступа

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

        public void SetItemsTable()
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

        #endregion Установка листа со доступами

        #region Работа с таблицей

        /// <summary>
        /// Заполняет рабочее пространство таблицы
        /// - Команды действий
        /// - Видимость фильтров/расчетов
        /// - Статус бар
        /// </summary>
        private void SetCurrentTable()
        {
            if (SelectTableItemComboBox == null) return;

            MathVisibility = false;
            FilterVisibility = false;

            Tables.SetTable(SelectTableItemComboBox);

            RemoveFromTabeleCommand = Tables.GetRemoveCommand();
            AddFromTabeleCommand = Tables.GetAddCommand();
            EditFromTabeleCommand = Tables.GetEditCommand();
            ShowFromTabeleCommand = Tables.GetShowCommand();

            FilterIsEnabled = Tables.GetBoolFilterTable();
            MathsIsEnabled = Tables.GetBoolMathsTable();

            MainTable = Tables.GetTable();
            CountRows = MainTable.Rows.Count;
            NameCurrentTable = _SelectTableItemComboBox.Bank_tables_info.Tables_name;
        }

        /// <summary>
        /// Заполняет новыми данными текущую таблицу
        /// </summary>
        public void SetUpdateTabel()
        {
            SelectedItemMainTable = null;
            MainTable = Tables.GetTable();
            CountRows = MainTable.Rows.Count;
            NameCurrentTable = _SelectTableItemComboBox.Bank_tables_info.Tables_name;
        }

        public void SetWorkTableSpace()
        {
            Tables = new(this);

            SetItemsTable();
        }

        #endregion Работа с таблицей

        #region Отображение рабочего окна

        public void ShowMainWindow()
        {
            workSpaceWindow = new()
            {
                DataContext = this
            };
            workSpaceWindow.Show();
        }

        #endregion Отображение рабочего окна

        #endregion Методы
    }
}