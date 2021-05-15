using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Active;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables.Active
{
    public class TBankActiveDocs : ATable
    {

        #region Свойства

        private Bank_active_docs Bank_data;

        #endregion Свойства

        #region Элементы главного окна

        public override bool Filter { get; } = true;
        public override bool Maths { get; } = false;

        #endregion Элементы главного окна

        #region Методы

        #region Работа с таблицей

        /// <summary>
        /// Заполняет таблицу данными
        /// </summary>
        private void SetValuesTable()
        {

            UpdateDBContext();

            var data = BankDbContext.Bank_active_docs
                .Include(aac => aac.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Docs_name,
                    item.Docs_type_doc,
                    item.Docs_cash,
                    item.Bank_currency.Currency_name
                    );
        }

        #endregion

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;
            if (CheckUserPassword())
            {
                BankDbContext.Bank_active_docs.Remove(Bank_data);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_data.Docs_name} - Удалено");
                UpdateDataInTable();
                return;
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankActiveDocsViewModel clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveDocsViewModel clientViewModel = new(workSpaceWindowViewModel, Bank_data);
            clientViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveDocsViewModel clientViewModel = new(Bank_data, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowWindow();
        }

        #endregion

        #endregion

        public override bool HasNullObject()
        {
            if (Bank_data == null)
            {
                ShowNullObjectError();
                return true;
            }
            return false;
        }

        public override void SetSelected(DataRowView selectedItem)
        {
            if (selectedItem == null)
            {
                Bank_data = null;
                return;
            }

            Bank_data = BankDbContext.Bank_active_docs
                .SingleOrDefault(item =>
                            item.Docs_name == (string)selectedItem[0] &&
                            item.Docs_type_doc == (string)selectedItem[1]);
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankActiveDocs(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {

        }

    }
}
