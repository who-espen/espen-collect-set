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
           var sessionId = await _authenticationService.Authenticate(Username, Password).ConfigureAwait(true);

            await OnLoginExecuteFinished(sessionId).ConfigureAwait(false);
        }

        protected bool CanSaveCommandExecute()
        {
            return Username != null && Password != null;
        }

        protected async Task OnCancelCommandExecuteAsync()
        {
            await CloseViewModelAsync(true).ConfigureAwait(false);
        }
        private async Task OnLoginExecuteFinished(string sessionId)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                await CloseViewModelAsync(true).ConfigureAwait(false);
            }
            else await _messageService.ShowErrorAsync("Username or password incorrect").ConfigureAwait(false);
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
            var value = !result;
            if (value == null || (bool)value)
            {
                Application.Current.Shutdown(-1);
            }

            await base.OnClosedAsync(result).ConfigureAwait(false);
        }
    }
}
