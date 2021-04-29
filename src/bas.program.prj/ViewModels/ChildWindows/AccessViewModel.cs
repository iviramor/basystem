using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.ViewModels.ChildWindows
{
    public class AccessViewModel : ViewModel
    {

        #region Свойства

        #region Классы

        /// <summary>
        /// Окно Доступа
        /// </summary>
        private AccessWindow _AccessWindow;

        #endregion

        #endregion

        #region Конструкторы

        public AccessViewModel()
        {

        }

        #endregion

        #region Методы
        public void ShowAccessWindow()
        {
            _AccessWindow = new()
            {
                DataContext = this
            };
            _AccessWindow.ShowDialog();
            
        }

        #endregion

    }
}
