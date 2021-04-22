using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.ViewModels.ChildWindows
{
    public class ProfWindowViewModel : ViewModel
    {

        #region Свойства

        #region Класс

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

        #endregion

        #endregion

        #region Команды


        #endregion

        public ProfWindowViewModel(AdministratorViewModel adminVM)
        {
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
