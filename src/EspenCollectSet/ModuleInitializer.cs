﻿using Catel.IoC;
using EspenCollect.Infrastructure;

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

        rootContainer.Initialize(serviceLocator);
    }
}
