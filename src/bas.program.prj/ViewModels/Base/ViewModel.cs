using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace bas.program.ViewModels.Base
{
    /// <summary>
    /// Класс реализующий OnPropertyChanged для свойств
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
