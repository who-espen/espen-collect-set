using Catel.IoC;
using EspenCollect.Data.Repositories;
using EspenCollectSet.Services;
using EspenCollectSet.Windows.Tabs;
using Orchestra.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IRibbonService, RibbonService>();
        serviceLocator.RegisterType<ITabService, TabService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationService>();

        serviceLocator.RegisterType<IRepository, RepositoryBase>();
        serviceLocator.RegisterType<IOnchoEpirfRepository, OnchoEpirfRepository>();
    }
}
