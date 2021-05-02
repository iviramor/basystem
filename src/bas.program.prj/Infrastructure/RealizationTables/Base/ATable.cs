using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Infrastructure.RealizationTables.Base
{
    public abstract class ATable
    {

        #region Свойства

        /// <summary>
        ///  Таблица с данными Клиента
        /// </summary>
        public DataTable _DataTable { get; set; }

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public BankDbContext _BankDbContext { get; set; }

        private string _NameTable;

        #endregion Свойства

        //public abstract void DelFromTable();

        //public abstract void AddToTable();

        //public abstract void EditInTable();

       private void SetColumnTable()
        {

            /// Тип данной таблицы
            var entityTypeCurrentTable = _BankDbContext.Model.GetEntityTypes()
                .SingleOrDefault(table => table.DisplayName() == _NameTable).ClrType;

            var PropColumn = TypeDescriptor.GetProperties(entityTypeCurrentTable);

            foreach (PropertyDescriptor prop in PropColumn)
            {
                if (prop.DisplayName != null)
                    _DataTable.Columns.Add(prop.DisplayName);
            }

        }

        public abstract DataTable GetFullTable();

        public ATable(string name)
        {
            _NameTable = name;
            _DataTable = new();
            _BankDbContext = new();


            SetColumnTable();

        }

    }

}
