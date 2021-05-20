using bas.program.Infrastructure.Commands.Base;
using System.Windows;

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
