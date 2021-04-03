using System.Windows;
using System.Windows.Input;
using bas.program.Infrastructure.Commands;
using bas.program.Models;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.ChildWindows;


namespace bas.program.ViewModels
{
    public class WorkSpaceWindowViewModel : ViewModel
    {

        #region Блоки окна

        #region Действия с окном

        private bool _Visibility = true;

        public bool Visibility
        {
            get => _Visibility;
            set
            {
                if (Equals(_Visibility, value)) return;
                _Visibility = value;
                OnPropertyChanged();
            }
        }

        private bool _IsEnabled = true;

        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                if (Equals(_IsEnabled, value)) return;
                _IsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _AdminStatus;

        public bool AdminStatus
        {
            get
            {
                return _AdminStatus;
            }
            set
            {
                if (Equals(_AdminStatus, value)) return;
                _AdminStatus = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #endregion

        #region Данные

        public UserDataSession User = new UserDataSession();

        private string _UserName = null;

        public string UserName
        {
            get => _UserName;
            set
            {
                if (Equals(_UserName, value)) return;
                _UserName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Команды

        #region Показать Меню авторизации

        public ICommand ShowSignOutCommand { get; }

        private bool CanShowSignOutCommandExecuted(object p) => true;

        private void OnShowSignOutCommandExecute(object p)
        {
            
            var que = MessageBox.Show("Вы точно хотите выйти из Системы?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (que == MessageBoxResult.Yes)
            {
                User.Session = false;
                Application.Current.Shutdown();
            }

            return;
        }

        #endregion

        #region Заблокировать окно

        public ICommand EnableWindowCommand { get; }

        private bool CanEnableWindowCommandExecuted(object p) => true;

        private void OnEnableWindowCommandExecute(object p)
        {
            new HelloWindow().Show();
        }

        #endregion

        #region Профиль 
        public ICommand ShowProfileCommand { get; }

        private bool CanShowProfileExecuted(object p) => true;

        private void OnShowProfileExecute(object p)
        {
            var profile = new ProfileViewModel(this);
            profile.ShowProfileWindow();
        }

        #endregion

        #endregion


        public WorkSpaceWindowViewModel()
        {

            if (!User.Session)
            {
                if (!AuthorizeHelloWindow())
                {
                    Application.Current.Shutdown();
                }
            }


            ShowSignOutCommand = new ActionCommand(OnShowSignOutCommandExecute, CanShowSignOutCommandExecuted);
            EnableWindowCommand = new ActionCommand(OnEnableWindowCommandExecute, CanEnableWindowCommandExecuted);
            ShowProfileCommand = new ActionCommand(OnShowProfileExecute, CanShowProfileExecuted);
        }




        #region Диалоговые окна


        #region Окно авторизации
        private bool AuthorizeHelloWindow()
        {
            new HelloWindowViewModel(this).ShowHelloWindow();
            if (User.Session) return true;
            else return false;

        }
        #endregion



        #endregion

    }
}
