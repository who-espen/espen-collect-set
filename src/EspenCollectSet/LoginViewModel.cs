namespace EspenCollectSet
{
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.MVVM;
    using Catel.Services;
    using EspenCollect.Services;

    public class LoginViewModel : ViewModelBase
    {

        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly IMessageService _messageService;

        #endregion

        #region Constructors

        public LoginViewModel(IAuthenticationService authenticationService, IMessageService messageService)
        {
            _authenticationService = authenticationService;
            _messageService = messageService;

            SaveCommand = new TaskCommand(OnSaveCommandExecuteAsync, CanSaveCommandExecute);

            CancelCommand = new TaskCommand(OnCancelCommandExecuteAsync);
        }

        #endregion

        #region Commands

        public TaskCommand SaveCommand { get; private set; }

        public TaskCommand CancelCommand { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the title of the view model.
        /// </summary>
        /// <value> The title. </value>
        public override string Title
        {
            get { return "Authentication"; }
        }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        /// <value>
        ///     The user name.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the property value.
        /// </summary>
        public string LoginStatus { get; set; }

        #endregion


        protected async Task OnSaveCommandExecuteAsync()
        {
            await _authenticationService.Authenticate(Username, Password, LoginSuccess, LoginFailure).ConfigureAwait(true);

            //OnLoginExecuteFinished();
        }

        protected bool CanSaveCommandExecute()
        {
            return Username != null && Password != null;
        }

        protected async Task OnCancelCommandExecuteAsync()
        {
            await CloseViewModelAsync(true);
        }

        private async void LoginSuccess(string roleOfAuthenticatedUser)
        {
            Argument.IsNotNullOrWhitespace("roleOfAuthenticatedUser", roleOfAuthenticatedUser);

            LoginStatus = "Succeed";

            //_authorizationService.GetAllowedModules(
            //    roleOfAuthenticatedUser,
            //    LoadCorrespondingModules);

            //await CloseViewModel(true).ConfigureAwait(true);

            await CloseViewModelAsync(true).ConfigureAwait(true);
        }


        private void LoginFailure(string errorMessage)
        {
            Argument.IsNotNullOrWhitespace("errorMessage", errorMessage);

            LoginStatus = "Failed";
            _messageService.ShowErrorAsync(errorMessage);
        }

        /// <summary>
        ///     Called when the view model has just been closed.
        ///     <para />
        ///     This method also raises the <see cref="E:Catel.MVVM.ViewModelBase.Closed" /> event.
        /// </summary>
        /// <param name="result">
        ///     The result to pass to the view. This will, for example, be used as <c>DialogResult</c>.
        /// </param>
        protected async override Task OnClosedAsync(bool? result)
        {
           await base.OnClosedAsync(result);

            var b = !result;
            if (b != null && (bool)b)
            {
                Application.Current.Shutdown();
            }
        }

    }
}
