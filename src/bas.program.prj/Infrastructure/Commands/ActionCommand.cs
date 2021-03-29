using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bas.program.Infrastructure.Commands.Base;

namespace bas.program.Infrastructure.Commands
{
    public class ActionCommand : Command
    {

        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecut;

        public ActionCommand(Action<object> Execute, Func<object, bool> CanExecut = null)
        {
            _Execute = Execute;
            _CanExecut = CanExecut;
        }

        public override bool CanExecute(object parameter) => _CanExecut?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
