﻿using bas.program.Infrastructure.RealizationTables.Tables;
using bas.program.Infrastructure.RealizationTables.Tables.Active;
using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bas.program.Infrastructure.RealizationTables.Base
{
    public class Tables
    {

        #region Свойства

        /// <summary>
        /// Выделенный Объект в ComboBox
        /// </summary>
        public Bank_user_access SelectTableItem { get; set; }

        /// <summary>
        /// Класс с таблицей
        /// </summary>
        private ATable Table { get; set; }

        /// <summary>
        /// Поле с ViewModel главного окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        #endregion Свойства

        #region Методы

        /// <summary>
        /// Установка таблицы с учетом текущей выделенной таблицы в combobox
        /// </summary>
        /// <param name="bank_User_Access">Объект выделенной таблицы</param>
        private void SetCurrendTabel(Bank_user_access bank_User_Access)
        {
            string name = bank_User_Access.Bank_tables_info.Tables_key;

            switch (name)
            {
                case "Bank_client":
                    Table = new TBankClient(bank_User_Access, _workSpaceWindowViewModel);
                    return;
                case "Bank_client_company":
                    Table = new TBankClientCompany(bank_User_Access, _workSpaceWindowViewModel);
                    return;
                case "Bank_client_history":
                    Table = new TBankClientHistory(bank_User_Access, _workSpaceWindowViewModel);
                    return;
                case "Bank_currency":
                    Table = new TBankCurrency(bank_User_Access, _workSpaceWindowViewModel);
                    return;
                case "Bank_active_authorized_capital":
                    Table = new TBankActiveAuthorizedCapital(bank_User_Access, _workSpaceWindowViewModel);
                    return;

            }
        }

        #endregion Методы

        #region Реализация 

        /// <summary>
        /// Устанавливает таблицу, выделенную в ComboBox
        /// </summary>
        /// <param name="bank_User_Access">Выделенный статус</param>
        public void SetTable(Bank_user_access bank_User_Access)
        {
            SelectTableItem = bank_User_Access;
            SetCurrendTabel(bank_User_Access);
        }

        /// <summary>
        /// Выдает полную таблицу(выделенную в ComboBox, Установленную в SetTable)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            return Table.GetFullTable();
        }

        public void SetSelectedItem(DataRowView selectedItem)
        {
            Table.SetSelected(selectedItem);
        }

        #region Команды таблицы

        /// <summary>
        /// Отдает команду На удаление, с учетом текущей таблицы
        /// </summary>
        /// <returns></returns>
        public ICommand GetRemoveCommand()
        {
            return Table.GetRemoveFromTabeleCommand();
        }

        /// <summary>
        /// Отдает команду На Добавление в таблицу
        /// </summary>
        /// <returns></returns>
        public ICommand GetAddCommand()
        {
            return Table.GetAddFromTabeleCommand();
        }

        /// <summary>
        /// Отдает команду На Изменение в таблице
        /// </summary>
        /// <returns></returns>
        public ICommand GetEditCommand()
        {
            return Table.GetEditFromTabeleCommand();
        }

        /// <summary>
        /// Отдает команду Просмотр данных элемента
        /// </summary>
        /// <returns></returns>
        public ICommand GetShowCommand()
        {
            return Table.GetShowFromTabeleCommand();
        }

        #endregion Команды таблицы

        #region Элементы главного окна

        /// <summary>
        /// Функция отдает значения Функции Фильтра
        /// </summary>
        /// <returns></returns>
        public bool GetBoolFilterTable()
        {
            return Table.Filter;
        }
        /// <summary>
        /// Функция отдает значения Функции Расчета
        /// </summary>
        /// <returns></returns>
        public bool GetBoolMathsTable()
        {
            return Table.Maths;
        }

        #endregion Элементы главного окна

        #endregion Реализация 

        #region Конструктор

        public Tables(WorkSpaceWindowViewModel workVM)
        {
            _workSpaceWindowViewModel = workVM;
        }

        #endregion 

    }
}