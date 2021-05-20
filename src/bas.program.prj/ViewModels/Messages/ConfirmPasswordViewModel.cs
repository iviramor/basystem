using bas.program.Infrastructure.Commands;
using bas.program.ViewModels.Base;
using bas.program.Views.Messages;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels.Messages
{
    public class ConfirmPasswordViewModel : ViewModel
    {
        private ConfirmPassword _ConfirmPasswordWindow;

        #region Свойства окна

        private string _Password;

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (Equals(_Password, value)) return;
                _Password = value;
                OnPropertyChanged();
            }
        }

        #endregion Свойства окна

        #region Команды

        #region Закрыть окно

        public ICommand CloseMessageCommand { get; }

        private bool CanCloseMessageCommandExecuted(object p) => true;

        private void OnCloseMessageCommandExecute(object p)
        {
            _Password = null;
            _ConfirmPasswordWindow.Close();
        }

        #endregion Закрыть окно

        #region Подтвердить

        public ICommand ConfirmMessageCommand { get; }

        private bool CanConfirmMessageCommandExecuted(object p) => true;

        private void OnConfirmMessageCommandExecute(object p)
        {
            if (_Password == null)
            {
                MessageBox.Show("Заполните поле", "Ошибка ввода", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            _ConfirmPasswordWindow.Close();
        }

        #endregion Подтвердить

        #endregion Команды

        public ConfirmPasswordViewModel()
        {
            ConfirmMessageCommand = new ActionCommand(OnConfirmMessageCommandExecute, CanConfirmMessageCommandExecuted);
            CloseMessageCommand = new ActionCommand(OnCloseMessageCommandExecute, CanCloseMessageCommandExecuted);
        }

        public string ShowMessagePassword()
        {
            _ConfirmPasswordWindow = new()
            {
                DataContext = this
            };

            _ConfirmPasswordWindow.ShowDialog();

            return Password;
        }
    }
}