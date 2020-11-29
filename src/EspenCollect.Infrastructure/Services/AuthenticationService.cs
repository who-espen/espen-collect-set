﻿namespace EspenCollect.Services
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

        public async Task<string> Authenticate(string username, string password)
        {

            Argument.IsNotNullOrWhitespace("userName", username);
            Argument.IsNotNullOrWhitespace("password", password);

            var session = await _restApi.Authenticate(username, password);

            ServiceLocator.Default.RegisterInstance(session);

            return session.Id;
        }
    }
}
