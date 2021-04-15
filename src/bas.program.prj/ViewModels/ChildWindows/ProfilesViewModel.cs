﻿using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.Messages;
using bas.program.Views.ChildViews;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfilesViewModel : ViewModel
    {

        #region Поля и Свойства

        /// <summary>
        /// Окно Профилей
        /// </summary>
        private ProfilesWindow _ProfilesWindow;

        /// <summary>
        /// ViewModel главного меню
        /// </summary>
        public readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// База данных или же Контекст моделей
        /// </summary>
        private readonly BankDbContext _DataBase;

        private Bank_user _SelectedItem;
        /// <summary>
        /// Свойство - Выделенный Профиль в таблице всех профилей
        /// </summary>
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
        /// <summary>
        /// Данные пользователя
        /// </summary>
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

        #endregion Поля и Свойства

        #region Команды

        #region Удалить


        /// <summary>
        /// Команда для удаления профиля
        /// </summary>
        public ICommand RemoveDataCommand { get; }

        private bool CanRemoveDataCommandExecuted(object p) => true;

        private void OnRemoveDataCommandExecute(object p)
        {
            /// Проверяет выделен ли профиль в таблице
            /// если нет, то показывает уведомление ошибкой
            if (SelectedItem != null)
            {
                /// Проверяет, является ли выделенный Профиль
                /// уже авторизованным в данной сессии
                if (SelectedItem.User_id != _workSpaceWindowViewModel.User.User.User_id)
                {
                    /// Проверяет, является ли профиль Администратором
                    if (SelectedItem.User_status_to_system != 1)
                    {
                        /// Сообщение, для подтверждения пароля
                        var PasswordWindow = new ConfirmPasswordViewModel();
                        /// Отображение сообщения и запись вводимого в
                        /// окне пароля
                        var password =  PasswordWindow.ShowMessagePassword();
                        
                        /// Проверка есть ли пароль
                        if (password == null) return;
                        /// Сравнивает введенный пароль с паролем пользователя
                        else if (password == _workSpaceWindowViewModel.User.User.User_password)
                        {
                            _DataBase.Remove(SelectedItem);
                            _DataBase.SaveChanges();

                            UpdateTable();

                            MessageBox.Show($"Успех");
                            return;
                        }
                        /// если пароль не подходить, то сообщение об ошибке Ввода
                        else
                            MessageBox.Show("Неверный пароль!", "Ошибка ввода", MessageBoxButton.OK,
                            MessageBoxImage.Error);

                        return;
                    }

                    MessageBox.Show("Удалять Профиль со статусом - Администратор, запрещено", "Ошибка ввода", MessageBoxButton.OK,
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

        #endregion Удалить

        #region Закрыть окно

        public ICommand CloseProfilesCommand { get; }

        private bool CanCloseProfilesCommandExecuted(object p) => true;

        private void OnCloseProfilesCommandExecute(object p)
        {
            _ProfilesWindow.Close();
        }

        #endregion Закрыть окно

        #region Окно редактировать

        public ICommand EditDataCommand { get; }

        private bool CanEditDataCommandExecuted(object p) => true;

        private void OnEditDataCommandExecute(object p)
        {
            if (_SelectedItem == null)
            {
                MessageBox.Show("Выделите Сотрудника в таблице", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            var profile = new ProfileViewModel(this, _SelectedItem);
            profile.ShowProfileWindow();
        }

        #endregion Окно редактировать

        #region Окно добавить

        public ICommand AddDataCommand { get; }

        private bool CanAddDataCommandExecuted(object p) => true;

        private void OnAddDataCommandExecute(object p)
        {
            var profile = new ProfileViewModel(this, "Добавить");
            profile.ShowProfileWindow();
        }

        #endregion Окно добавить

        #endregion Команды

        public ProfilesViewModel(WorkSpaceWindowViewModel workVM)
        {
            _workSpaceWindowViewModel = workVM;

            _DataBase = workVM.User.DataBase;

            _Bank_users = _DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .ToList();

            RemoveDataCommand = new ActionCommand(OnRemoveDataCommandExecute, CanRemoveDataCommandExecuted);
            EditDataCommand = new ActionCommand(OnEditDataCommandExecute, CanEditDataCommandExecuted);
            AddDataCommand = new ActionCommand(OnAddDataCommandExecute, CanAddDataCommandExecuted);
            CloseProfilesCommand = new ActionCommand(OnCloseProfilesCommandExecute, CanCloseProfilesCommandExecuted);
        }

        public void UpdateTable()
        {
            Bank_users = _DataBase.Bank_user
                .Include(u => u.Bank_user_status)
                .ToList();
        }

        public void ShowProfilesWindow()
        {
            _ProfilesWindow = new ProfilesWindow
            {
                DataContext = this
            };
            _ProfilesWindow.ShowDialog();
        }
    }
}