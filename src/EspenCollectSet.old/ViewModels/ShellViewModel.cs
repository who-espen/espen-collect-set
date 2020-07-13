namespace EspenCollectSet.ViewModels
{
    using System;
    using Catel.ExceptionHandling;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;

    public class ShellViewModel : ViewModelBase
    {
        //private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ShellViewModel():base()
        {
            //var dependencyResolver = this.GetDependencyResolver();
            //var exceptionService = dependencyResolver.Resolve<IExceptionService>();

            //exceptionService.Register<Exception>(async exception =>
            //{
            //    Log.Error(exception);
            //    await dependencyResolver.Resolve<IMessageService>().ShowErrorAsync("An unknown exception occurred, please contact the developers") ;
            //});
        }
    }
}
