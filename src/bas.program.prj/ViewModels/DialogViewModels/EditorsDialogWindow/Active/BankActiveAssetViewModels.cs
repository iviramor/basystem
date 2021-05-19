using bas.program.Infrastructure.Commands;
using bas.program.Models.Tables.Active;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base;
using bas.program.Views.DialogViews;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Active
{
    public class BankActiveAssetViewModels : AB5Window
    {

        /// <summary>
        /// Данные для изменения
        /// </summary>
        private readonly Bank_active_asset _Bank_data;

        public override bool FindMatch()
        {
            return _DataBase.Bank_active_asset.Any(i => i.Ass_name == Name);
        }

        public override void OnUpdateDataCommandExecute(object p)
        {

            var data = _DataBase.Bank_active_asset.SingleOrDefault(d => d.Ass_name == _Bank_data.Ass_name);

            #region Смена изменений в сессии пользователя

            data.Ass_name = _Name;
            data.Ass_cash = Summa;
            data.Ass_type = SelectCurrency.Currency_id;
            data.Bank_currency = SelectCurrency;

            #endregion Смена изменений в сессии пользователя

            /// Изменение данных в базе данных
            _DataBase.Bank_active_asset.Update(data);
            _DataBase.SaveChanges();

            /// Уведомление об успешной операции
            MessageBox.Show("Операция выполнена, \n Данные изменены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            _workSpaceWindowViewModel.SetUpdateTabel();

        }

        public override void OnAddDataCommandExecute(object p)
        {

            /// Новые данные
            Bank_active_asset NewData = new();

            #region Смена изменений в сессии пользователя

            if (_Name == null ||
                SelectCurrency == null)
            {
                MessageBox.Show("Проверьте данные! Вы могли пропустить поле.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewData.Ass_name = _Name;
            NewData.Ass_cash = Summa;
            NewData.Ass_type = SelectCurrency.Currency_id;

            #endregion 

            /// Добавление в базу данных 
            _DataBase.Bank_active_asset.Add(NewData);
            _DataBase.SaveChanges();

            /// Обновление таблицы 
            _workSpaceWindowViewModel.SetUpdateTabel();

            MessageBox.Show("Добавлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            _BankWindow.Close();

        }

        #region Конструкторы

        /// <summary>
        /// Конструктор для редактирования
        /// </summary>
        public BankActiveAssetViewModels(WorkSpaceWindowViewModel workVM, Bank_active_asset bank_data)
            : base(workVM)
        {

            /// Заголовок окна с именем
            Title = $"Изменить: {bank_data.Ass_name}";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Ass_name;
            Summa = bank_data.Ass_cash;

            Currency = workVM.User.DataBase.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ass_type);

            #endregion Значение свойство

        }

        /// <summary>
        /// Конструктор для добавления
        /// </summary>
        public BankActiveAssetViewModels(WorkSpaceWindowViewModel workVM, string actionName)
            : base(workVM, actionName)
        {

            Currency = workVM.User.DataBase.Bank_currency.ToList();
        }

        /// <summary>
        /// Конструктор для Просмотра данных
        /// </summary>
        public BankActiveAssetViewModels(Bank_active_asset bank_data, BankDbContext dbContext)
            : base(dbContext)
        {

            /// Заголовок окна с именем
            Title = $"Просмотр: {bank_data.Ass_name} ";

            /// Данные
            _Bank_data = bank_data;

            #region Значение свойство

            Name = bank_data.Ass_name;
            Summa = bank_data.Ass_cash;

            Currency = dbContext.Bank_currency.ToList();

            SelectCurrency = Currency.SingleOrDefault(c => c.Currency_id == bank_data.Ass_type);

            #endregion Значение свойство

        }

        #endregion Конструкторы

    }
}
