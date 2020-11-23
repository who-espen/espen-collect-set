namespace EspenCollect.Infrastructure
{
    using Catel;
    using Catel.IoC;
    using EspenCollect.Services;

    /// <summary>
    /// The class for registering all type and instances into the catel service locator.
    /// </summary>
    public class RootContainer : IServiceLocatorInitializer
    {
        /// <summary>
        ///     Initializes the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="serviceLocator"/> is <c>null</c>.</exception>
        public void Initialize(IServiceLocator serviceLocator)
        {
            Argument.IsNotNull(nameof(serviceLocator), serviceLocator);

            serviceLocator.RegisterType<IOnchoEpirfInit, OnchoEpirfInit>();
            serviceLocator.RegisterType<ILfEpirfInit, LfEpirfInit>();
            serviceLocator.RegisterType<ISthEpirfInit, SthEpirfInit>();
            serviceLocator.RegisterType<ISchEpirfInit, SchEpirfInit>();
            serviceLocator.RegisterType<IEpirfGenerator, EpirfGenerator>();
            serviceLocator.RegisterType<IRestApi, RestApi>();
            serviceLocator.RegisterType<IAuthenticationService, AuthenticationService>();
        }
    }
}
