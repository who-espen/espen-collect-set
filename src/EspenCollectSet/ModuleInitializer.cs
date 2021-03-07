using Catel.Data;
using Catel.IoC;
using EspenCollect.Infrastructure;
using Orc.FluentValidation;

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


        var rootContainer = new RootContainer();

        ServiceLocator.Default.RegisterType<IValidatorProvider, FluentValidatorProvider>();


        rootContainer.Initialize(serviceLocator);
    }
}
