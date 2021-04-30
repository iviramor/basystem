using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.Messages;
using bas.program.Views;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.ChildWindows
{
    public class AdministratorViewModel : ViewModel
    {
        #region Классы

        /// <summary>
        /// Окно администратора
        /// </summary>
        private AdministratorWindow _administratorWindow;

        /// <summary>
        /// Поле с ViewModel главного окна
        /// </summary>
        private WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// База данных или же Контекст моделей
        /// </summary>
        private readonly BankDbContext _DataBase;

        #endregion

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

        private bool _AccessTableIsEnabled = true;
        /// <summary>
        /// Доступ к Таблицам доступа
        /// </summary>
        public bool AccessTableIsEnabled
        {
            get => _AccessTableIsEnabled;
            set
            {
                if (Equals(_AccessTableIsEnabled, value)) return;
                _AccessTableIsEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства

        private DataRowView _SelectedItem;

        /// <summary>
        /// Свойство - Выделенная профессия в таблице всех Профессий
        /// </summary>
        public DataRowView SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (Equals(_SelectedItem, value)) return;
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        #region Свойства элементов

        private DataTable _ProfTable = new();
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

        #region Изменить Должность

        /// <summary>
        /// Команда Открытия окна на изменения Должности
        /// </summary>
        public ICommand EditProfCommand { get; }

        private bool CanEditProfCommandExecuted(object p) => true;

        private void OnEditProfCommandExecute(object p)
        {
            /// Проверка выделен ли ряд в таблице
            if (_SelectedItem == null)
            {
                MessageBox.Show("Выделите Сотрудника в таблице", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            /// Поиск совпадения
            Bank_user_status user_Status = 
                _DataBase.Bank_user_status
                   .SingleOrDefault(st => st.Status_name == (string)_SelectedItem[0] &
                                          st.Status_describ == (string)_SelectedItem[1]);

            /// Передача данных Профессии в ProfWindowViewModel
            var profile = new ProfWindowViewModel(user_Status, ref _workSpaceWindowViewModel);
            profile.ShowProfWindow();
            UpdateProfTable();
        }

        #endregion

        #region Добавить Должность

        public ICommand AddProfCommand { get; }

        private bool CanAddProfCommandExecuted(object p) => true;

        private void OnAddProfCommandExecute(object p)
        {

            /// Передача данных Профессии в ProfWindowViewModel
            var profile = new ProfWindowViewModel(ref _workSpaceWindowViewModel);
            profile.ShowProfWindow();
            UpdateProfTable();

        }

        #endregion

        #region Открыть Доступы

        public ICommand ShowAccessCommand { get; }

        private bool CanShowAccessCommandExecuted(object p) => true;

        private void OnShowAccessCommandExecute(object p)
        {
            if (_SelectedItem == null)
            {
                MessageBox.Show("Выберете Должность в таблице", "Предупреждение", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                return;
            }
            /// Поиск совпадения
            Bank_user_status user_Status =
                _DataBase.Bank_user_status
                   .SingleOrDefault(st => st.Status_name == (string)_SelectedItem[0] &
                                          st.Status_describ == (string)_SelectedItem[1]);

            new AccessViewModel(user_Status, _workSpaceWindowViewModel.User).ShowAccessWindow();
            UpdateProfTable();
        }

        #endregion

        #region Удаление Должность

        /// <summary>
        /// Команда для удаления выделенного элемента Должности
        /// </summary>
        public ICommand RemoveProfCommand { get; }

        private bool CanRemoveProfCommandExecuted(object p) => true;

        private void OnRemoveProfCommandExecute(object p)
        {

            /// Проверка выделен ли ряд в таблице
            if (_SelectedItem == null)
            {
                MessageBox.Show("Выделите Должность в таблице", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            /// Поиск совпадения
            Bank_user_status user_Status =
                _DataBase.Bank_user_status
                   .SingleOrDefault(st => st.Status_name == (string)_SelectedItem[0] &
                                          st.Status_describ == (string)_SelectedItem[1]);

            /// Проверка, является ли профиль высшим
            if (user_Status.Status_higher)
            {
                MessageBox.Show("Должность имеет высший доступ", "Ошибка удаления", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            /// Проверка, есть ли с данной должностью Профили
            if (_DataBase.Bank_user
                .Any(user => user.User_status_to_system == user_Status.Status_id))
            {
                MessageBox.Show("С такой должностью существует Профиль!", "Ошибка удаления", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            /// Сообщение, для подтверждения пароля
            var PasswordWindow = new ConfirmPasswordViewModel();
            /// Отображение сообщения и запись вводимого в
            /// окне пароля
            var password = PasswordWindow.ShowMessagePassword();

            /// Проверка есть ли пароль
            if (password == null) return;
            /// Сравнивает введенный пароль с паролем пользователя
            else if (password == _workSpaceWindowViewModel.User.User.User_password)
            {

                /// Удаление Должности
                _DataBase.Bank_user_status.Remove(user_Status);
                _DataBase.SaveChanges();

                UpdateProfTable();

                MessageBox.Show($"Успех");
                return;
            }
            /// если пароль не подходить, то сообщение об ошибке Ввода
            else
                MessageBox.Show("Неверный пароль!", "Ошибка ввода", MessageBoxButton.OK,
                MessageBoxImage.Error);

            return;
        }

        #endregion

        #region Просмотр Должность

        public ICommand ShowProfCommand { get; }

        private bool CanShowProfCommandExecuted(object p) => true;

        private void OnShowProfCommandExecute(object p)
        {
            if (_SelectedItem == null) return;
            /// Поиск совпадения
            Bank_user_status user_Status =
                _DataBase.Bank_user_status
                   .SingleOrDefault(st => st.Status_name == (string)_SelectedItem[0] &
                                          st.Status_describ == (string)_SelectedItem[1]);

            /// Передача данных Профессии в ProfWindowViewModel
            var profile = new ProfWindowViewModel(user_Status);
            profile.ShowProfWindow();

        }

        #endregion

        #endregion

        public AdministratorViewModel(WorkSpaceWindowViewModel workVM)
        {

            _workSpaceWindowViewModel = workVM;

            _DataBase = workVM.User.DataBase;

            CreateProfTable();
            UpdateProfTable();


            EditProfCommand = new ActionCommand(OnEditProfCommandExecute, CanEditProfCommandExecuted);
            AddProfCommand = new ActionCommand(OnAddProfCommandExecute, CanAddProfCommandExecuted);
            RemoveProfCommand = new ActionCommand(OnRemoveProfCommandExecute, CanRemoveProfCommandExecuted);
            ShowProfCommand = new ActionCommand(OnShowProfCommandExecute, CanShowProfCommandExecuted);
            ShowAccessCommand = new ActionCommand(OnShowAccessCommandExecute, CanShowAccessCommandExecuted);
            CloseAdminCommand = new ActionCommand(OnNameCloseAdminCommandExecute, CanCloseAdminCommandExecuted);

            #region Доступ к элементам

            ///Поиск информации таблицы Пользователей
            var tableInfos = _DataBase.Bank_tables_info
                .ToList();

            #region Работа с Bank_user_status, блокировка элементов

            /// Поиск Bank_user_status
            var tableAccess = workVM.User.User.Bank_user_status.Bank_user_access
                    .SingleOrDefault(ua => 
                    /// Сравнение с выданной таблицей доступных таблиц в системе
                    ua.Access_name_table == tableInfos
                            .SingleOrDefault(ti => 
                                    ti.Tables_key == "Bank_user_status").Tables_id);

            if (workVM.User.User.Bank_user_status.Status_full_access) return;
            else if (tableAccess.Access_modification == 2)
            {
                _AddIsEnabled = false;
                _EditIsEnabled = false;
                _DelIsEnabled = false;
            }
            else if (tableAccess.Access_modification == 3)
            {
                _AddIsEnabled = false;
                _DelIsEnabled = false;
            }

            #endregion

            #region Работа с Bank_user_access, блокировка элементов

            /// Поиск Bank_user_status
            tableAccess = workVM.User.User.Bank_user_status.Bank_user_access
                    .SingleOrDefault(ua =>
                    /// Сравнение с выданной таблицей доступных таблиц в системе
                    ua.Access_name_table == tableInfos
                            .SingleOrDefault(ti =>
                                    ti.Tables_key == "Bank_user_access").Tables_id);

            if (tableAccess == null) _AccessTableIsEnabled = false;

            #endregion



            #endregion

        }

        #region Методы класса

        /// <summary>
        /// Создает структуру таблицы (Колонки)
        /// </summary>
        private void CreateProfTable()
        {
            var PropColumn = TypeDescriptor.GetProperties(typeof(Bank_user_status));

            foreach (PropertyDescriptor prop in PropColumn)
            {
                if (prop.DisplayName != null)
                    _ProfTable.Columns.Add(prop.DisplayName);
                    
            }
        }

        /// <summary>
        /// Обновляет таблицу данными
        /// </summary>
        public void UpdateProfTable()
        {
            var user_Accesses = _DataBase.Bank_user_status
                .Include(ac => ac.Bank_user_access)
                .ToList();

            _ProfTable.Clear();

            foreach (var item in user_Accesses)
                _ProfTable.Rows.Add(
                    item.Status_name,
                    item.Status_describ,
                    (item.Status_full_access) ? "Полный доступ" : item.Bank_user_access.Count
                    );

        }


        /// <summary>
        /// Показывает окно Администратора
        /// </summary>
        public void ShowAdministratorWindow()
        {
            _administratorWindow = new()
            {
                DataContext = this
            };
            _administratorWindow.ShowDialog();
        }

        #endregion
    }
}