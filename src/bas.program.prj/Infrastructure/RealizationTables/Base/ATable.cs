using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.UserTables;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.Infrastructure.RealizationTables.Base
{
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

        #endregion Свойства

        #region Команды

        #region Удаление

        private bool CanRemoveProfCommandExecuted(object p) => true;

        public abstract void OnRemoveProfCommandExecute(object p);

        public void ShowRemoveError()
        {
            MessageBox.Show("Выберите элемент в таблице!", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        #endregion

        #endregion Команды

        public ICommand GetRemoveFromTabeleCommand()
        {
            return new ActionCommand(OnRemoveProfCommandExecute, CanRemoveProfCommandExecuted);
        }

        public abstract void SetSelected(DataRowView selectedItem);

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

        public abstract DataTable GetFullTable();

        public ATable(Bank_user_access bank_User_Access)
        {
            _Bank_user_access = bank_User_Access;
            DataTable = new();
            BankDbContext = new();


            SetColumnTable();

        }

    }

}
