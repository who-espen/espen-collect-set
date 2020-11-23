namespace EspenCollectSet
{
    using Catel.MVVM;
    using Catel.Services;

    public class LoginViewModel : ViewModelBase
    {

        #region Fields

        //private readonly IAuthorizationService _authorizationService;
        //private readonly IAuthenticationService<IUser> _authenticationService;
        private readonly IMessageService _messageService;

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
    }
}
