using bas.program.Infrastructure.Commands.Base;
using System;

namespace bas.program.Infrastructure.Commands
{
    /// <summary>
    /// Команда
    /// </summary>
    public class ActionCommand : Command
    {

        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecut;

        /// <summary>
        /// Принимает OnCommand n CanCommand
        /// </summary>
        /// <param name="Execute"></param>
        /// <param name="CanExecut"></param>
        public ActionCommand(Action<object> Execute, Func<object, bool> CanExecut = null)
        {
            _Execute = Execute;
            _CanExecut = CanExecut;
        }

        public override bool CanExecute(object parameter) => _CanExecut?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
