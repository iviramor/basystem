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


        #endregion

        #endregion

        #region Команды



        #endregion

        public ProfWindowViewModel(AdministratorViewModel adminVM, Bank_user_status user_Status)
        {
            /// Запись ряда с данными профессии
            UserStatus = user_Status;

            ///Название действия 
            NameAction = "Изменить";
            /// Название окна
            Title = $"{NameAction}: {UserStatus.Status_name}";

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
