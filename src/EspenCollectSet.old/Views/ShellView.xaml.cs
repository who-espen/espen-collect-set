namespace EspenCollectSet.Views
{
    using Catel.IoC;
    using Catel.Windows;
    using Orchestra.Services;
    using Orchestra.Views;
    using Services;

    /// <summary>
    /// Interaction logic for ShellWindow.xaml.
    /// </summary>
    public partial class ShellView : IShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellWindow"/> class.
        /// </summary>
        public ShellView()
        {
            var serviceLocator = ServiceLocator.Default;

            InitializeComponent();

            serviceLocator.RegisterInstance(this);
            serviceLocator.RegisterInstance(pleaseWaitProgressBar, "pleaseWaitService");
            serviceLocator.RegisterInstance(DockingManager);
            serviceLocator.RegisterInstance(LayoutDocumentPane);
            serviceLocator.RegisterInstance(LayoutAnchorablePane);

            var statusService = serviceLocator.ResolveType<IStatusService>();
            //statusService.Initialize(statusTextBlock);

            var dependencyResolver = this.GetDependencyResolver();
            var ribbonService = dependencyResolver.Resolve<IRibbonService>();

            var ribbonContent = ribbonService.GetRibbon();
            if (ribbonContent != null)
            {
                ribbonContentControl.SetCurrentValue(ContentProperty, ribbonContent);

                var ribbon = ribbonContent.FindVisualDescendantByType<Fluent.Ribbon>();
                if (ribbon != null)
                {
                    serviceLocator.RegisterInstance<Fluent.Ribbon>(ribbon);
                }
            }

            var statusBarContent = ribbonService.GetStatusBar();
            if (statusBarContent != null)
            {
                customStatusBarItem.SetCurrentValue(ContentProperty, statusBarContent);
            }

            var mainView = ribbonService.GetMainView();
            //contentControl.Content = mainView;

            ShellDimensionsHelper.ApplyDimensions(this, mainView);
        }
    }
}
