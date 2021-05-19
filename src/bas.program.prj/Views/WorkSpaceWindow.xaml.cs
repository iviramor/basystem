using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bas.program.Views
{
    /// <summary>
    /// Interaction logic for WorkSpaceWindow.xaml
    /// </summary>
    public partial class WorkSpaceWindow : Window
    {

        /// <summary>
        /// Поле отвечает за закрытые окна в виде:
        /// true - значит окно закроется без отключения программы
        /// false - с выключением программы
        /// </summary>
        public bool OnClose;

        public WorkSpaceWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// при закрытии окна предупреждение
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (OnClose) return;

            var ans = MessageBox.Show("Вы точно хотите выйти из системы?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (ans == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                return;
            }

            e.Cancel = true;

        }

    }
}
