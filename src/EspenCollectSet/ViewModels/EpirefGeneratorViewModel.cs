namespace EspenCollectSet.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;
    using EspenCollect.Core;
    using EspenCollect.Services;

    public class EpirefGeneratorViewModel: ViewModelBase
    {
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IRestApi _restApi;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        #region Constructors
        public EpirefGeneratorViewModel(IPleaseWaitService pleaseWaitService, IRestApi restApi)
        {
            Argument.IsNotNull(() => pleaseWaitService);
            Argument.IsNotNull(() => restApi);

            _pleaseWaitService = pleaseWaitService;
            _restApi = restApi;

            Download = new TaskCommand(OnExecuteDownload, CanExecuteDownload);

            LoadEpirfTitle = new TaskCommand(OnExecuteLoadEpirfTitle);

            MetabaseCollections = new FastObservableCollection<MetabaseCollection>();

        }

        #endregion

        #region Properties
        /// <summary>
        ///     Gets the title of the view model.
        /// </summary>
        /// <value> The title. </value>
        public override string Title => "EPIRF Generator";

        public string SelectedEpirfFile { get; set; }
        public MetabaseCollection SelectedItem { get; set; }

        public FastObservableCollection<MetabaseCollection> MetabaseCollections { get; set; }

        public FastObservableCollection<EpirfList> EpirfLists { get; set; }
        //public FastObservableCollection<EpirfList> EpirfsToGenerate { get; set; }

        public TaskCommand Download { get; private set; }

        public TaskCommand LoadEpirfTitle { get; private set; }

        #endregion

        #region Methods

        protected bool CanExecuteDownload() => !string.IsNullOrWhiteSpace(SelectedEpirfFile);

        protected async Task OnExecuteDownload()
        {
           
            //await _epirfGenerator.GenerateEpirfAsync(SelectedEpirfFile);
        }
        
        protected async Task OnExecuteLoadEpirfTitle()
        {
            Log.Info("Started loading EPIRF list");
            if (SelectedItem != null)
            {
                var results = await LoadCollectionItem(SelectedItem).ConfigureAwait(true);

                EpirfLists = new FastObservableCollection<EpirfList>(results.Select(i => new EpirfList { Name = i.Name }));
            }

        }

        protected override async Task InitializeAsync()
        {
            try
            {
                await base.InitializeAsync();

                _pleaseWaitService.Show(async () => {

                    var collections = await PopulateAsync().ConfigureAwait(false);

                    MetabaseCollections.AddItems(collections);

                }, "Loading Metabase collections");
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }

        private async Task<IEnumerable<MetabaseCollection>> PopulateAsync()
        {
            var metabaseCollections = new List<MetabaseCollection>();

            var collections = await _restApi.GetAllCollection();

            var maxSubCollection = 1;

            collections.ForEach(c =>
            {
                var count = c.Location.Count(l => l == '/');

                maxSubCollection = maxSubCollection < count ? count : maxSubCollection;
            });

            var i = maxSubCollection;

            var treatedCollection = new List<MetabaseCollection>();
            while (i >= 1)
            {
                var collectionParrents = collections.Where(c => c.Location.Count(l => l == '/') == i);

                if (treatedCollection.Any()) { 

                    collectionParrents.ForEach(p => {
                        treatedCollection.ForEach(c => {
                            var ParentIds = c.Location.Split('/');

                            if (ParentIds.Length > 1 && p.Id == ParentIds[ParentIds.Length - 2])
                            {
                                if (p.MetabaseInnerCollections is null)
                                {
                                    p.MetabaseInnerCollections = new List<MetabaseCollection>{ c };
                                }
                                else
                                {
                                    p.MetabaseInnerCollections.Add(c);
                                }
                            }

                        });
                    });
                }

                treatedCollection.AddRange(collectionParrents);
                i--;
            }

            metabaseCollections.AddRange(treatedCollection);

            return metabaseCollections.Where(c => c.Location == "/");
        }

        private Task<IEnumerable<CollectionItem>> LoadCollectionItem(MetabaseCollection selectedCollection)
        {
            //Argument.IsNotNull(() => selectedCollection);

            var results = _restApi.GetAllCollectionItem(selectedCollection.Id);

            return results;
        }

        #endregion
    }
}
