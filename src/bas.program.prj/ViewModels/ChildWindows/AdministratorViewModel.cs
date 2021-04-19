using bas.program.ViewModels.Base;
using bas.program.Views;

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