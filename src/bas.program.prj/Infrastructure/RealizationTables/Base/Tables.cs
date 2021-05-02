using bas.program.Infrastructure.RealizationTables.Tables;
using bas.program.Models.Tables.UserTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.Infrastructure.RealizationTables.Base
{
    public class Tables
    {

        #region Свойства

        private string _NameTable { get; set; }

        private Bank_user_access _SelectTableItem { get; set; }

        /// <summary>
        /// Класс с таблицей
        /// </summary>
        private ATable _Table { get; set; }

        #endregion Свойства

        #region Методы

        private void SetCurrendTabel(string name)
        {
            switch (name)
            {
                case "Bank_client":
                    _Table = new TBankClient(name);
                    return;
                case "Bank_client_company":
                    _Table = new TBankClientCompany(name);
                    return;
                case "Bank_client_history":
                    _Table = new TBankClientHistory(name);
                    return;
                case "Bank_currency":
                    _Table = new TBankCurrency(name);
                    return;

            }
        }

        #endregion Методы

        #region Реализация 

        public DataTable GetTabel(string name)
        {
            SetCurrendTabel(name);
            return _Table.GetFullTable();
        }

        #endregion Реализация 

        #region Конструктор

        public Tables()
        {

        }

        #endregion 


    }
}
