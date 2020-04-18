namespace EspenCollectSet.ViewModels
{
    using System.Threading.Tasks;
    using Catel.MVVM;

    public class EpirefGeneratorViewModel: ViewModelBase
    {

        #region Constructors
        public EpirefGeneratorViewModel()
        {
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
            //await _epirfGenerator.GenerateEpirfAsync(SelectedEpirfFile);
        }

        #endregion
    }
}
