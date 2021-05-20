using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.Messages;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace bas.program.Infrastructure.RealizationTables.Base
{
    /// <summary>
    /// Абстрактный класс описывающий действия таблиц
    /// (удаление, добавление, обновление и т.д.)
    /// </summary>
    public abstract class ATable
    {
        #region Свойства

        /// <summary>
        ///  Таблица с данными Клиента
        /// </summary>
        public DataTable DataTable { get; set; }

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public BankDbContext BankDbContext { get; set; }

        public readonly Bank_user_access _Bank_user_access;

        public readonly WorkSpaceWindowViewModel workSpaceWindowViewModel;

        #endregion Свойства

        #region Элементы главного окна

        /// <summary>
        /// Свойства фильтра, если true, то Фильтр для таблицы применим
        /// </summary>
        public abstract bool Filter { get; }

        /// <summary>
        /// Свойства фильтра, если true, то Расчеты для таблицы применимы
        /// </summary>
        public abstract bool Maths { get; }

        #endregion Элементы главного окна

        #region Команды

        #region Удаление

        private bool CanRemoveCommandExecuted(object p) => true;

        public abstract void OnRemoveCommandExecute(object p);

        /// <summary>
        /// Функция Вызывает окно с подтверждением пароля
        /// </summary>
        /// <returns>True - если пароль принят</returns>
        public bool CheckUserPassword()
        {
            /// Сообщение, для подтверждения пароля
            var PasswordWindow = new ConfirmPasswordViewModel();
            /// Отображение сообщения и запись вводимого в
            /// окне пароля
            var password = PasswordWindow.ShowMessagePassword();

            /// Проверка есть ли пароль
            if (password == null) return false;
            /// Сравнивает введенный пароль с паролем пользователя
            else if (password == workSpaceWindowViewModel.User.User.User_password)
            {
                return true;
            }
            /// если пароль не подходить, то сообщение об ошибке Ввода
            else
                MessageBox.Show("Неверный пароль!", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            return false;
        }

        #endregion Удаление

        #region Добавление

        private bool CanAddCommandExecuted(object p) => true;

        public abstract void OnAddCommandExecute(object p);

        #endregion Добавление

        #region Изменить

        private bool CanEditCommandExecuted(object p) => true;

        public abstract void OnEditCommandExecute(object p);

        #endregion Изменить

        #region Просмотр

        private bool CanShowCommandExecuted(object p) => true;

        public abstract void OnShowCommandExecute(object p);

        #endregion Просмотр

        public static void ShowNullObjectError()
        {
            MessageBox.Show("Выберите элемент в таблице!", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        #endregion Команды

        #region Выдача команд

        public ICommand GetRemoveFromTabeleCommand()
        {
            return new ActionCommand(OnRemoveCommandExecute, CanRemoveCommandExecuted);
        }

        public ICommand GetAddFromTabeleCommand()
        {
            return new ActionCommand(OnAddCommandExecute, CanAddCommandExecuted);
        }

        public ICommand GetEditFromTabeleCommand()
        {
            return new ActionCommand(OnEditCommandExecute, CanEditCommandExecuted);
        }

        public ICommand GetShowFromTabeleCommand()
        {
            return new ActionCommand(OnShowCommandExecute, CanShowCommandExecuted);
        }

        #endregion Выдача команд

        /// <summary>
        /// Ищет выделенный ряд в базе данных и устанавливает в поле
        /// </summary>
        /// <param name="selectedItem"></param>
        public abstract void SetSelected(DataRowView selectedItem);

        /// <summary>
        /// Устанавливает колонки в табличку
        /// </summary>
        private void SetColumnTable()
        {
            /// Тип данной таблицы
            var entityTypeCurrentTable = BankDbContext.Model.GetEntityTypes()
                .SingleOrDefault(table => table.DisplayName() == _Bank_user_access.Bank_tables_info.Tables_key).ClrType;

            var PropColumn = TypeDescriptor.GetProperties(entityTypeCurrentTable);

            foreach (PropertyDescriptor prop in PropColumn)
            {
                if (prop.DisplayName != null)
                    DataTable.Columns.Add(prop.DisplayName);
            }
        }

        /// <summary>
        /// Обновляет данные в таблице
        /// </summary>
        public void UpdateDataInTable()
        {
            workSpaceWindowViewModel.SetUpdateTabel();
        }

        /// <summary>
        /// Выдает полностью заполненную таблицу
        /// </summary>
        /// <returns></returns>
        public abstract DataTable GetFullTable();

        public ATable(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM)
        {
            workSpaceWindowViewModel = workVM;

            _Bank_user_access = bank_User_Access;
            DataTable = new();
            BankDbContext = new();

            SetColumnTable();
        }

        #region Дополнительные методы

        public abstract bool HasNullObject();

        /// <summary>
        /// Метод обновляет свойство  BankDbContext(= new())
        /// </summary>
        public void UpdateDBContext()
        {
            BankDbContext = new();
        }

        #endregion Дополнительные методы
    }
}