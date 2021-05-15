﻿using bas.program.ViewModels.Base;
using bas.program.Views.DialogViews;
using bas.website.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.DialogViewModels.EditorsDialogWindow.Base
{
    public abstract class ABAC3Window : ViewModel
    {

        #region Поля и свойства

        #region Классы

        /// <summary>
        /// Окно View
        /// </summary>
        public BAC3Window _BankWindow;

        /// <summary>
        /// ViewModel главного рабочего окна
        /// </summary>
        public WorkSpaceWindowViewModel _workSpaceWindowViewModel;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public BankDbContext _DataBase;

        #endregion Классы

        #region Видимость элементов

        private string _TName = "Наименование";
        /// <summary>
        /// Заголовок блока 
        /// </summary>
        public string TName
        {
            get
            {
                return _TName;
            }
            set
            {
                if (Equals(_TName, value)) return;
                _TName = value;
                OnPropertyChanged();
            }
        }

        private string _TBankClient = "Наименование";
        /// <summary>
        /// Заголовок списка дебитора
        /// </summary>
        public string TBankClient
        {
            get
            {
                return _TBankClient;
            }
            set
            {
                if (Equals(_TBankClient, value)) return;
                _TBankClient = value;
                OnPropertyChanged();
            }
        }

        private string _Title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (Equals(_Title, value)) return;
                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _NameAction = "Изменить";
        /// <summary>
        /// Название операции
        /// </summary>
        public string NameAction
        {
            get => _NameAction;
            set
            {
                if (Equals(_NameAction, value)) return;
                _NameAction = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEnabled = false;
        /// <summary>
        /// Блокировка элементов
        /// </summary>
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                if (Equals(_IsEnabled, value)) return;
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisibility = true;
        /// <summary>
        /// Блокировка элементов
        /// </summary>
        public bool IsVisibility
        {
            get => _IsVisibility;
            set
            {
                if (Equals(_IsVisibility, value)) return;
                _IsVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion Видимость элементов

        #region Свойства элементов

        public string _Name;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get => _Name;
            set
            {
                if (Equals(_Name, value)) return;

                if (value.Length < 2)
                {
                    MessageBox.Show("Имя не может быть:\n" +
                                    "-> Меньше 2 символов\n", "Ошибка ввода", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _Description;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get => _Description;
            set
            {
                if (Equals(_Description, value)) return;
                _Description = value;
                OnPropertyChanged();
            }
        }

        private decimal _Credit;
        /// <summary>
        /// Кредит
        /// </summary>
        public decimal Credit
        {
            get => _Credit;
            set
            {
                if (Equals(_Credit, value)) return;

                _Credit = value;
                OnPropertyChanged();
            }
        }

        #region Дебитор

        private List<Bank_client> _BankClient;

        /// <summary>
        /// Список с дебитором
        /// </summary>
        public List<Bank_client> BankClient
        {
            get => _BankClient;
            set
            {
                if (Equals(_BankClient, value)) return;
                _BankClient = value;
                OnPropertyChanged();
            }
        }

        private Bank_client _SelectedBankClient;

        /// <summary>
        /// Выделенный в списке дебитор
        /// </summary>
        public Bank_client SelectedBankClient
        {
            get => SelectedBankClient;
            set
            {
                if (Equals(_SelectedBankClient, value)) return;

                _SelectedBankClient = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Средства

        private List<Bank_currency> _Currency;

        /// <summary>
        /// Список 
        /// </summary>
        public List<Bank_currency> Currency
        {
            get => _Currency;
            set
            {
                if (Equals(_Currency, value)) return;
                _Currency = value;
                OnPropertyChanged();
            }
        }

        private Bank_currency _SelectCurrency;

        /// <summary>
        /// Выделенный в списке
        /// </summary>
        public Bank_currency SelectCurrency
        {
            get => _SelectCurrency;
            set
            {
                if (Equals(_SelectCurrency, value)) return;

                _SelectCurrency = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion Свойства пользователя

        #endregion Поля и свойства

        #region Команды

        #region Обновление

        /// <summary>
        /// Команда обновления данных
        /// </summary>
        public ICommand UpdateDataCommand { get; }

        #region Изменение данных

        private bool CanUpdateDataCommandExecuted(object p) => true;

        public abstract void OnUpdateDataCommandExecute(object p);

        #endregion Изменение данных

        #region Добавление данных

        private bool CanAddDataCommandExecuted(object p) => true;

        public abstract void OnAddDataCommandExecute(object p);

        #endregion Добавление данных

        #endregion Обновление

        #region Закрыть окно

        /// <summary>
        /// Команда закрытия окна
        /// </summary>
        public ICommand CloseCommand { get; }

        private bool CanCloseWindowCommandExecuted(object p) => true;

        private void OnCloseWindowCommandExecute(object p)
        {
            _BankWindow.Close();
        }

        #endregion

        #endregion Команды


        public void ShowWindow()
        {
            _BankWindow = new()
            {
                DataContext = this
            };
            _BankWindow.ShowDialog();
        }

    }
}