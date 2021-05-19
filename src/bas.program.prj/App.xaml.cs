using bas.program.ViewModels;
using bas.program.ViewModels.DialogViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace bas.program
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void AppStartup(object sender, StartupEventArgs e)
        {

            HelloWindowViewModel helloWindowViewModel = new();
            helloWindowViewModel.Authorization();

        }

    }
}
