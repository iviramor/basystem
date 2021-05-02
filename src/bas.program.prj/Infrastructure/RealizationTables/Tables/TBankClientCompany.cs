using bas.program.Infrastructure.RealizationTables.Base;
using bas.website.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Infrastructure.RealizationTables.Tables
{
    public class TBankClientCompany : ATable
    {

        #region Методы

        #region Работа с таблицей

        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {
            var data = _BankDbContext.Bank_client_company
                .ToList();

            _DataTable.Clear();

            foreach (var item in data)
                _DataTable.Rows.Add(
                    item.Clcomp_name,
                    item.Clcomp_descr,
                    item.Clcomp_type
                    );
        }

        #endregion

        #endregion Методы

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return _DataTable;
        }

        public TBankClientCompany(string name) : base(name)
        {

        }

    }
}
