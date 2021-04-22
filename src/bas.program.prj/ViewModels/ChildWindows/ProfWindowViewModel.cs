using bas.program.Models.Tables.UserTables;
using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfWindowViewModel : ViewModel
    {

        #region Свойства

        #region Класс

        private Bank_user_status UserStatus;

        /// <summary>
        /// Окно Свойств Профессии
        /// </summary>
        private ProfWindow _ProfWindow;

        /// <summary>
        /// ViewModel Главного окна
        /// </summary>
        private readonly WorkSpaceWindowViewModel _WorkSpaceWindowViewModel;

        /// <summary>
        /// ViewModel администратора
        /// </summary>
        private readonly AdministratorViewModel _AdministratorViewModel;

        #endregion

        #region Свойства окна

        private string _NameAction;
        /// <summary>
        /// Название действия окна
        /// </summary>
        public string NameAction
        {
            get
            {
                return _NameAction;
            }
            set
            {
                if (Equals(_NameAction, value)) return;
                _NameAction = value;
                OnPropertyChanged();
            }
        }

        private string _Title;
        /// <summary>
        /// Название действия окна
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

        private bool _IsVisibility = true;
        /// <summary>
        /// Видимость элементов (Полный доступ)
        /// </summary>
        public bool IsVisibility
        {
            get
            {
                return _IsVisibility;
            }
            set
            {
                if (Equals(_IsVisibility, value)) return;
                _IsVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Свойства элементов

        /// <summary>
        /// Название статуса(Должности)
        /// </summary>
        private string _ProfName;
        public string ProfName
        {
            get
            {
                return _ProfName;
            }
            set
            {
                if (Equals(_ProfName, value)) return;
                _ProfName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Описание статуса(Должности)
        /// </summary>
        private string _ProfDescription;
        public string ProfDescription
        {
            get
            {
                return _ProfDescription;
            }
            set
            {
                if (Equals(_ProfDescription, value)) return;
                _ProfDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Полный доступ статуса(Должности)
        /// </summary>
        private bool _ProfFullAccess;
        public bool ProfFullAccess
        {
            get
            {
                return _ProfFullAccess;
            }
            set
            {
                if (Equals(_ProfFullAccess, value)) return;
                _ProfFullAccess = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Команды

        #endregion

        /// <summary>
        /// Конструктор "Изменить"
        /// </summary>
        /// <param name="adminVM">ViewModel окна администратора</param>
        /// <param name="user_Status">Объект с данными о Статусе(Должности)</param>
        /// <param name="workSpace">ViewModel главного окна</param>
        public ProfWindowViewModel(AdministratorViewModel adminVM, 
                                   Bank_user_status user_Status, 
                                   ref WorkSpaceWindowViewModel workSpace)
        {
            /// Запись ряда с данными профессии
            UserStatus = user_Status;

            ///Название действия 
            NameAction = "Изменить";
            /// Название окна
            Title = $"{NameAction}: {UserStatus.Status_name}";

            #region Добавления значений в свойства

            _ProfName = user_Status.Status_name;
            _ProfDescription = user_Status.Status_describ;
            _ProfFullAccess = user_Status.Status_full_access;

            /// Скрыть CheckBox Полного доступа, если Высшего доступа
            if (!workSpace.User.User.Bank_user_status.Status_higher |
                workSpace.User.User.Bank_user_status.Status_id == user_Status.Status_id)
                _IsVisibility = false;

            #endregion


            _WorkSpaceWindowViewModel = workSpace;
            _AdministratorViewModel = adminVM;
        }

        public void ShowProfWindow()
        {
            _ProfWindow = new ProfWindow()
            {
                DataContext = this
            };
            _ProfWindow.ShowDialog();
        }

    }
}
