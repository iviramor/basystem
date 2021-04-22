using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
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

        #region Изменить профиль

        /// <summary>
        /// Команда Открытия окна на изменения Профессии
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
            var profile = new ProfWindowViewModel(this, user_Status, ref _workSpaceWindowViewModel);
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
            CloseAdminCommand = new ActionCommand(OnNameCloseAdminCommandExecute, CanCloseAdminCommandExecuted);
        }

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
    }
}