using bas.program.Infrastructure.Commands;
using bas.program.Models;
using bas.program.ViewModels.Base;
using bas.program.ViewModels.ChildWindows;
using System.Windows;
using System.Windows.Input;

namespace bas.program.ViewModels
{
    public class WorkSpaceWindowViewModel : ViewModel
    {
        #region Блоки окна

        #region Действия с окном

        private bool _Visibility = true;
        /// <summary>
        /// Отображение и скрытие нужных элементов, Используется для статуса "Администратор"
        /// </summary>
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
        /// <summary>
        /// Свойство для блокирования элементов в Главном меню
        /// </summary>
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
        /// <summary>
        /// Свойство, в котором храниться статус Администратор, если true то 
        /// пользователь является администратором
        /// если false, то обычный пользователь
        /// </summary>
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

        #endregion Действия с окном

        #endregion Блоки окна

        #region Данные

        /// <summary>
        /// Поле с данными пользователя в текущей сессии.
        /// </summary>
        public UserDataSession User = new();

        private string _UserName = null;
        /// <summary>
        /// Свойство блока Имя и Фамилия для правого верхнего угла "В системе: "
        /// </summary>
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

        #endregion Данные

        #region Команды

        #region Показать Меню авторизации

        /// <summary>
        /// Команда кнопки "Выйти из системы", выполняет выходи из системы
        /// </summary>
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

        #endregion Показать Меню авторизации

        #region Профиль

        /// <summary>
        /// Команда для отображения окна "Профиля" текущего пользователя в системе
        /// </summary>
        public ICommand ShowProfileCommand { get; }

        private bool CanShowProfileExecuted(object p) => true;

        private void OnShowProfileExecute(object p)
        {
            var profile = new ProfileViewModel(this);
            profile.ShowProfileWindow();
        }

        #endregion Профиль

        #region Профили

        /// <summary>
        /// Отображение окна "Профили"
        /// </summary>
        public ICommand ShowProfilesCommand { get; }

        private bool CanShowProfilesExecuted(object p) => true;

        private void OnShowProfilesExecute(object p)
        {
            var profiles = new ProfilesViewModel(this);
            profiles.ShowProfilesWindow();
        }

        #endregion Профили

        #region Администратор
        public ICommand ShowAdministratorCommand { get; }

        private bool CanShowAdministratorCommandExecuted(object p) => true;

        private void OnShowAdministratorCommandExecute(object p)
        {
            AdministratorViewModel adminVM = new();
            adminVM.ShowAdministratorWindow();

        }

        #endregion

        #endregion Команды

        #region Конструктор

        /// <summary>
        /// Конструктор Главного окна
        /// </summary>
        public WorkSpaceWindowViewModel()
        {
            
            /// Авторизация
            if (!AuthorizeHelloWindow())
            {
                Application.Current.Shutdown();
            }

            #region Команды 

            ShowSignOutCommand = new ActionCommand(OnShowSignOutCommandExecute, CanShowSignOutCommandExecuted);
            ShowProfileCommand = new ActionCommand(OnShowProfileExecute, CanShowProfileExecuted);
            ShowProfilesCommand = new ActionCommand(OnShowProfilesExecute, CanShowProfilesExecuted);
            ShowAdministratorCommand = new ActionCommand(OnShowAdministratorCommandExecute, CanShowAdministratorCommandExecuted);

            #endregion
        }

        #endregion

        #region Диалоговые окна

        #region Окно авторизации

        /// <summary>
        /// Создает и отображает Окно авторизации
        /// </summary>
        /// <returns> 
        /// True - если пользователь прошел авторизацию
        /// False - если не закрыл окно и не прошел регистрацию
        /// </returns>
        private bool AuthorizeHelloWindow()
        {
            new HelloWindowViewModel(this).ShowHelloWindow();
            if (User.Session) return true;
            else return false;
        }

        #endregion Окно авторизации

        #endregion Диалоговые окна
    }
}