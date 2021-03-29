using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bas.program.Infrastructure.Commands.Base;

namespace bas.program.Infrastructure.Commands.HelloWindowCommands
{
    public class CloseSystemCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
