namespace EspenCollectSet.Windows.Tabs
{
    using System;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using EspenCollectSet.Core;

    public class TabService : ITabService
    {
        #region Fields

        private readonly ITypeFactory _typeFactory;
        private readonly IViewLocator _viewLocator;
        private readonly IViewModelManager _viewModelManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabService" /> class.
        /// </summary>
        /// <param name="typeFactory">The type factory.</param>
        /// <param name="viewModelManager">The view model manager.</param>
        /// <param name="viewLocator">The view locator.</param>
        public TabService(ITypeFactory typeFactory,
            IViewModelManager viewModelManager, IViewLocator viewLocator)
        {
            _typeFactory = typeFactory;
            _viewModelManager = viewModelManager;
            _viewLocator = viewLocator;
        }

        #endregion

        #region Properties


        #endregion

        #region Methods

        /// <summary>
        ///     Shows the anchorable.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="iconSource">The icon source.</param>
        public virtual void ShowAnchorable(IViewModel viewModel, string iconSource = null)
        {
            Argument.IsNotNull(nameof(viewModel), viewModel);

            var viewModeltype = viewModel.GetType();

            var viewType = _viewLocator.ResolveView(viewModeltype);

            var anchorable = AvalonDockHelper.FindAnchorable(viewType);

            if (anchorable == null)
            {
                var view = ViewHelper.ConstructViewWithViewModel(viewType,
                    _viewModelManager.GetFirstOrDefaultInstance(
                        viewModeltype));
                anchorable = AvalonDockHelper.CreateAnchorable(view, iconSource);
            }

            AvalonDockHelper.ActivateAnchorable(anchorable);
        }

        /// <summary>
        ///     Shows the anchorable.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of the view model. </typeparam>
        public virtual void ShowAnchorable<TViewModel>(string iconSource = null) where TViewModel : IViewModel, new()
        {
            var viewModel = _typeFactory.CreateInstanceWithParametersAndAutoCompletion<TViewModel>();

            ShowAnchorable(viewModel, iconSource);
        }

        /// <summary>
        ///     Shows the document in the main shell.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="iconSource">The icon source.</param>
        /// <param name="tag">The tag.</param>
        public virtual void ShowDocument<TViewModel>(string iconSource = null, object tag = null)
            where TViewModel : IViewModel
        {
            var viewModel = _typeFactory.CreateInstanceWithParametersAndAutoCompletion<TViewModel>();

            ShowDocument(viewModel, iconSource, tag);
        }

        /// <summary>
        ///     Shows the document in the main shell.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="viewModel">The view model to show which will automatically be resolved to a view.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="iconSource">The icon source.</param>
        /// <exception cref="ArgumentNullException">
        ///     The
        ///     <paramref name="viewModel" />
        ///     is
        ///     <c>null</c>
        ///     .
        /// </exception>
        public virtual void ShowDocument<TViewModel>(TViewModel viewModel, string iconSource = null, object tag = null)
            where TViewModel : IViewModel
        {
            Argument.IsNotNull(nameof(viewModel), viewModel);

            var viewType = _viewLocator.ResolveView(viewModel.GetType());

            var document = AvalonDockHelper.FindDocument(viewType, tag);
            if (document == null)
            {
                var view = ViewHelper.ConstructViewWithViewModel(viewType, viewModel);
                document = AvalonDockHelper.CreateDocument(view, tag, iconSource);
            }

            AvalonDockHelper.ActivateDocument(document);
        }

        #endregion
    }
}
