namespace EspenCollectSet.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.Reflection;
    using Catel.Services;
    using EspenCollectSet.Windows.Tabs;
    using Orchestra.ViewModels;

    public class RibbonViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly ITabService _tabService;

        public RibbonViewModel(INavigationService navigationService, IUIVisualizerService uiVisualizerService, IDependencyResolver dependencyResolver)
        {
            Argument.IsNotNull(() => navigationService);
            Argument.IsNotNull(() => uiVisualizerService);
            Argument.IsNotNull(() => dependencyResolver);

            _navigationService = navigationService;
            _uiVisualizerService = uiVisualizerService;
            _tabService = dependencyResolver.Resolve<ITabService>();

            ShowKeyboardMappings = new TaskCommand(OnShowKeyboardMappingsExecuteAsync);

            var assembly = AssemblyHelper.GetEntryAssembly();
            Title = assembly.Title();
        }

        #region Commands
        /// <summary>
        /// Gets the ShowKeyboardMappings command.
        /// </summary>
        public TaskCommand ShowKeyboardMappings { get; private set; }

        /// <summary>
        /// Method to invoke when the ShowKeyboardMappings command is executed.
        /// </summary>
        private async Task OnShowKeyboardMappingsExecuteAsync()
        {
            //PleaseWaitService.Show(
            //               () => _tabService.ShowDocument<EpirefGeneratorViewModel>(),
            //               "Chargement des tarifs");

            _tabService.ShowDocument<EpirefGeneratorViewModel>();
            //await _uiVisualizerService.ShowDialogAsync<KeyboardMappingsCustomizationViewModel>();
        }
        #endregion

        #region Methods
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: Write initialization code here and subscribe to events
        }

        protected override Task CloseAsync()
        {
            // TODO: Unsubscribe from events

            return base.CloseAsync();
        }
        #endregion
    }
}
