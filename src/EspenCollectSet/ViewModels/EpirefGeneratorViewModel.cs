namespace EspenCollectSet.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Catel.MVVM;
    using EspenCollect.Core;

    public class EpirefGeneratorViewModel: ViewModelBase
    {

        #region Constructors
        public EpirefGeneratorViewModel()
        {
            Download = new TaskCommand(OnExecuteDownload, CanExecuteDownload);

            var subCollection = new List<MetabaseCollection>
            {
                new MetabaseCollection { Name = "2019" },
                new MetabaseCollection { Name = "2020" }
            };

            MetabaseCollections = new ObservableCollection<MetabaseCollection>
            {
                new MetabaseCollection { Name = "Ghana", MetabaseInnerCollections = subCollection },
                new MetabaseCollection { Name = "Nigeria" }
            };
        }

        #endregion

        #region Properties
        /// <summary>
        ///     Gets the title of the view model.
        /// </summary>
        /// <value> The title. </value>
        public override string Title => "EPIRF Generator";

        public string SelectedEpirfFile { get; set; }

        public ObservableCollection<MetabaseCollection> MetabaseCollections { get; set; }

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
