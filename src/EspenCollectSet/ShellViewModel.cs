namespace EspenCollectSet
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Collections;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;
    using EspenCollect.Core;
    using EspenCollect.Services;

    public class ShellViewModel : ViewModelBase
    {
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IRestApi _restApi;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly ISaveFileService _saveFileService;
        private readonly IMessageService _messageService;
        private readonly IEpirfGenerator _epirfGenerator;
        private readonly IUIVisualizerService _visualizerService;

        #region Constructors
        public ShellViewModel(IPleaseWaitService pleaseWaitService, IRestApi restApi, ISaveFileService saveFileService,
            IMessageService messageService, IEpirfGenerator epirfGenerator, IUIVisualizerService visualizerService)
        {
            Argument.IsNotNull(() => pleaseWaitService);
            Argument.IsNotNull(() => restApi);
            Argument.IsNotNull(() => saveFileService);
            Argument.IsNotNull(() => messageService);
            Argument.IsNotNull(() => epirfGenerator);
            Argument.IsNotNull(() => visualizerService);

            _pleaseWaitService = pleaseWaitService;
            _restApi = restApi;
            _saveFileService = saveFileService;
            _messageService = messageService;
            _epirfGenerator = epirfGenerator;
            _visualizerService = visualizerService;

            Download = new TaskCommand(OnExecuteDownload, CanExecuteDownload);

            LoadEpirfTitle = new TaskCommand(OnExecuteLoadEpirfTitle);

            CheckEpirf = new Command(OnCheckEpirf, CanOnCheckEpirf);

            UncheckEpirf = new Command(OnUncheckEpirf, CanOnUncheckEpirf);

            GenerateEpirf = new TaskCommand(OnGenerateEpirfAsync, CanGenerateEpirf);

            GenerateEpirfForEdit = new TaskCommand(OnGenerateEpirfForEditAsync, CanGenerateEpirf);

            MetabaseCollections = new FastObservableCollection<MetabaseCollection>();

            EpirfsToGenerate = new FastObservableCollection<EpirfSpec>();

            InitializeDataAsync();
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

        public FastObservableCollection<EpirfSpec> EpirfLists { get; set; }
        public FastObservableCollection<EpirfSpec> EpirfsToGenerate { get; set; }
        public EpirfSpec SelectedEpirfToGenerate { get; set; }
        public EpirfSpec SelectedEpirf { get; set; }

        public TaskCommand Download { get; private set; }

        public TaskCommand LoadEpirfTitle { get; private set; }

        public Command CheckEpirf { get; private set; }
        public Command UncheckEpirf { get; private set; }

        public TaskCommand GenerateEpirf { get; private set; }
        public TaskCommand GenerateEpirfForEdit { get; private set; }

        public bool IsLoading { get; private set; }

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
                var results = await LoadCollectionItem(SelectedItem).ConfigureAwait(false);

                EpirfLists = new FastObservableCollection<EpirfSpec>(results.Select(i => new EpirfSpec { Name = i.Name, Id = i.Id, CollectionName = SelectedItem.Name }));
            }

        }

        protected async Task OnGenerateEpirfAsync()
        {
            try
            {
                IsLoading = true;
                var collectionCount = (from x in EpirfsToGenerate select x.CollectionName).Distinct().Count();

                if (collectionCount > 1)
                {
                    if (await _messageService.ShowAsync("Are you sure to generate an EPIRF for more than one collection?",
                        "Are you sure?", MessageButton.YesNo, MessageImage.Question) == MessageResult.No)
                    {
                        IsLoading = false;
                        return;
                    }
                }



                var fileToSave = await _saveFileService.DetermineFileAsync(new DetermineSaveFileContext
                {
                    Filter = "Excel Macro-enabled Workbook|*.xlsm",
                    Title = "Save EPIRF as"
                }).ConfigureAwait(false);

                if (fileToSave.Result)
                {
                    //_pleaseWaitService.Show();

                    var results = await _epirfGenerator.GenerateEpirfAsync(EpirfsToGenerate, fileToSave.FileName).ConfigureAwait(false);

                    //_pleaseWaitService.Hide();
                }

                IsLoading = false;
            }
            catch (System.Exception e)
            {
                await _messageService.ShowAsync($"Error {e.Message} has occurred, please contact the administrator",
                                        "Error", MessageButton.OK, MessageImage.Error);
            }
        }


        protected async Task OnGenerateEpirfForEditAsync()
        {
            try
            {
                IsLoading = true;
                var collectionCount = (from x in EpirfsToGenerate select x.CollectionName).Distinct().Count();

                if (collectionCount > 1)
                {
                    if (await _messageService.ShowAsync("Are you sure to generate an EPIRF for more than one collection?",
                        "Are you sure?", MessageButton.YesNo, MessageImage.Question) == MessageResult.No)
                    {
                        IsLoading = false;
                        return;
                    }
                }



                var fileToSave = await _saveFileService.DetermineFileAsync(new DetermineSaveFileContext
                {
                    Filter = "Excel Macro-enabled Workbook|*.xlsm",
                    Title = "Save EPIRF as"
                }).ConfigureAwait(false);

                if (fileToSave.Result)
                {
                    var results = await _epirfGenerator.GenerateEpirfForEditAsync(EpirfsToGenerate, fileToSave.FileName).ConfigureAwait(false);
                }

                IsLoading = false;
            }
            catch (System.Exception e)
            {
                await _messageService.ShowAsync($"Error {e.Message} has occurred, please contact the administrator",
                                        "Error", MessageButton.OK, MessageImage.Error);
            }
        }

        protected async Task InitializeDataAsync()
        {
            try
            {
                var typeFactory = this.GetTypeFactory();

                var loginViewModel = typeFactory.CreateInstance<LoginViewModel>();

                var r = await _visualizerService.ShowDialogAsync(loginViewModel).ConfigureAwait(false);

                //_pleaseWaitService.Show(async () =>
                //{

                //    var collections = await PopulateAsync().ConfigureAwait(false);

                //    MetabaseCollections.AddItems(collections);

                //}, "Loading Metabase collections");

                if (r == true)
                {
                    var collections = await PopulateAsync().ConfigureAwait(false);

                    MetabaseCollections.AddItems(collections);
                }
            }
            catch (System.Exception e)
            {

                throw e;
            }

            //return Task.CompletedTask;
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

                if (treatedCollection.Any())
                {

                    collectionParrents.ForEach(p =>
                    {
                        treatedCollection.ForEach(c =>
                        {
                            var ParentIds = c.Location.Split('/');

                            if (ParentIds.Length > 1 && p.Id == ParentIds[ParentIds.Length - 2])
                            {
                                if (p.MetabaseInnerCollections is null)
                                {
                                    p.MetabaseInnerCollections = new List<MetabaseCollection> { c };
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

        private async Task<IEnumerable<CollectionItem>> LoadCollectionItem(MetabaseCollection selectedCollection)
        {
            //Argument.IsNotNull(() => selectedCollection);

            var results = await _restApi.GetAllCollectionItem(selectedCollection.Id).ConfigureAwait(false);

            return results;
        }

        private bool CanOnCheckEpirf() => SelectedEpirf != null;

        private bool CanOnUncheckEpirf() => SelectedEpirfToGenerate != null;

        private void OnCheckEpirf()
        {
            if (EpirfsToGenerate.Any())
            {
                var item = EpirfsToGenerate.FirstOrDefault(i => i.Id == SelectedEpirf.Id);

                if (item != null)
                {
                    EpirfsToGenerate[EpirfsToGenerate.IndexOf(item)] = SelectedEpirf;
                }
                else
                {
                    EpirfsToGenerate.Add(SelectedEpirf);
                }
            }
            else
            {
                EpirfsToGenerate.Add(SelectedEpirf);
            }
        }

        private void OnUncheckEpirf()
        {
            if (EpirfsToGenerate.Any())
            {
                EpirfsToGenerate.Remove(SelectedEpirfToGenerate);
            }
        }

        private bool CanGenerateEpirf() => EpirfsToGenerate.Count > 0;

        #endregion
    }
}
