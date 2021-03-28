using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;

namespace bas.program.ViewModels
{
    public class HelloWindowViewModel : ViewModel
    {



        #region Команды

        #region Закрыть приложение
        public ICommand CloseSystemCommand { get; }

        private void OnCloseSystemCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseSystemCommandExecuted(object p) => true;

        #endregion

        #endregion

        public HelloWindowViewModel()
        {
            #region Команды

            CloseSystemCommand = new ActionCommand(OnCloseSystemCommandExecuted, CanCloseSystemCommandExecuted);

            #endregion
        }



    }
}
