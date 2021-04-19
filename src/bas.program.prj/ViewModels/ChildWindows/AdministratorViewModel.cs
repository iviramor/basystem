using bas.program.ViewModels.Base;
using bas.program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bas.program.ViewModels.ChildWindows
{
    public class AdministratorViewModel : ViewModel
    {



        public AdministratorViewModel()
        {

        }

        public void ShowAdministratorWindow()
        {
            AdministratorWindow administratorWindow = new()
            {
                DataContext = this
            };
            administratorWindow.ShowDialog();
        }
    }
}
