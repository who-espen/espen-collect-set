namespace EspenCollect.Services
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRestApi _restApi;

        public AuthenticationService(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public async Task Authenticate(string userName, string password, Action<string> successCallback, Action<string> failureCallback)
        {

            Argument.IsNotNullOrWhitespace("userName", userName);
            Argument.IsNotNullOrWhitespace("password", password);

            var session = await _restApi.Authenticate(userName, password);

            ServiceLocator.Default.RegisterInstance(session);
        }
    }
}
