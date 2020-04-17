namespace EspenCollectSet.ViewModels
{
    using System.Threading.Tasks;
    using Catel.MVVM;
    using EspenCollect.Data.Services;

    public class EpirefGeneratorViewModel: ViewModelBase
    {
        private readonly IEpirfGenerator _epirfGenerator;

        #region Constructors
        public EpirefGeneratorViewModel(IEpirfGenerator epirfGenerator)
        {
            _epirfGenerator = epirfGenerator;
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

        protected bool CanExecuteDownload() => !string.IsNullOrWhiteSpace(SelectedEpirfFile);

        protected async Task OnExecuteDownload()
        {
            await _epirfGenerator.GenerateEpirfAsync(SelectedEpirfFile);
        }

        #endregion
    }
}
