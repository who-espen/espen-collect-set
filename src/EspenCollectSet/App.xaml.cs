namespace EspenCollectSet
{
    using Catel.IoC;
    using Catel.Windows.Controls;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

#if DEBUG
            Catel.Logging.LogManager.AddDebugListener();
#endif
            //This is an alternate way to initialize MaterialDesignInXAML if you don't use the MaterialDesignResourceDictionary in App.xaml
            //Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
            //Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            //ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, accentColor);

            //Resources.SetTheme(theme);


            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                        XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            //var serviceLocator = ServiceLocator.Default;

            //serviceLocator.RegisterType<ISnackbarMessageQueue, SnackbarMessageQueue>();


            PerformanceTuning();

            base.OnStartup(e);
        }

        protected void PerformanceTuning()
        {
            UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;
        }
    }
}
