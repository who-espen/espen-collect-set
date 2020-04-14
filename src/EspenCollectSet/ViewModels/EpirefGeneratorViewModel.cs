namespace EspenCollectSet.ViewModels
{
    using System.Threading.Tasks;
    using Catel.MVVM;
    using EspenCollect.Data.Repositories;

    public class EpirefGeneratorViewModel: ViewModelBase
    {
        private readonly IOnchoEpirfRepository _onchoEpirfRepository;

        #region Constructors
        public EpirefGeneratorViewModel(IOnchoEpirfRepository onchoEpirf)
        {
            _onchoEpirfRepository = onchoEpirf;
            Download = new TaskCommand(OnExecuteDownload, CanExecuteDownload);
        }

        #endregion

        #region Properties
        /// <summary>
        ///     Gets the title of the view model.
        /// </summary>
        /// <value> The title. </value>
        public override string Title => "EPIRF Generator";

        public string SelectedEpirfFile { get; set; }

        public TaskCommand Download { get; private set; }

        #endregion

        #region Methods

        protected bool CanExecuteDownload() => SelectedEpirfFile != null || SelectedEpirfFile != "";

        protected async Task OnExecuteDownload()
        {
            var epirf = await _onchoEpirfRepository.GetAllEpirfOnchoAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
