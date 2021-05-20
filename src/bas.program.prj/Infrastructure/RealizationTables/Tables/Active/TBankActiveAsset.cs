using bas.program.Infrastructure.RealizationTables.Base;
using bas.program.Models.Tables.Active;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Windows;

namespace bas.program.Infrastructure.RealizationTables.Tables.Active
{
    public class TBankActiveAsset : ATable
    {
        #region Свойства

        private Bank_active_asset Bank_data;

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

            var data = BankDbContext.Bank_active_asset
                .Include(aac => aac.Bank_currency)
                .ToList();

            DataTable.Clear();

            foreach (var item in data)
                DataTable.Rows.Add(
                    item.Ass_name,
                    item.Ass_cash,
                    item.Bank_currency.Currency_name
                    );
        }

        #endregion Работа с таблицей

        #endregion Методы

        #region Команды

        #region Удалить

        public override void OnRemoveCommandExecute(object p)
        {
            if (HasNullObject()) return;
            if (CheckUserPassword())
            {
                BankDbContext.Bank_active_asset.Remove(Bank_data);
                BankDbContext.SaveChanges();
                MessageBox.Show($"{Bank_data.Ass_name} - Удалено");
                UpdateDataInTable();
                return;
            }
        }

        #endregion Удалить

        #region Добавить

        public override void OnAddCommandExecute(object p)
        {
            BankActiveAssetViewModels clientViewModel = new(workSpaceWindowViewModel, "Добавить");
            clientViewModel.ShowWindow();
        }

        #endregion Добавить

        #region Изменить

        public override void OnEditCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveAssetViewModels clientViewModel = new(workSpaceWindowViewModel, Bank_data);
            clientViewModel.ShowWindow();
        }

        #endregion Изменить

        #region Просмотр

        public override void OnShowCommandExecute(object p)
        {
            if (HasNullObject()) return;

            BankActiveAssetViewModels clientViewModel = new(Bank_data, workSpaceWindowViewModel.User.DataBase);
            clientViewModel.ShowWindow();
        }

        #endregion Просмотр

        #endregion Команды

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

            Bank_data = BankDbContext.Bank_active_asset
                .SingleOrDefault(item =>
                            item.Ass_name == (string)selectedItem[0] &&
                            item.Ass_cash == decimal.Parse((string)selectedItem[1]));
        }

        public override DataTable GetFullTable()
        {
            SetValuesTable();
            return DataTable;
        }

        public TBankActiveAsset(Bank_user_access bank_User_Access, WorkSpaceWindowViewModel workVM) : base(bank_User_Access, workVM)
        {
        }
    }
}