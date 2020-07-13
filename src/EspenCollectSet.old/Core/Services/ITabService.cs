namespace EspenCollectSet.Windows.Tabs
{
    using System;
    using Catel.MVVM;

    /// <summary>
    ///     The tab service that allows us to handle AvalonDock tabs.
    /// </summary>
    public interface ITabService
    {
        #region Methods


        /// <summary>
        ///     Shows the anchorable.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="iconSource">The icon source.</param>
        void ShowAnchorable(IViewModel viewModel, string iconSource = null);

        /// <summary>
        ///     Shows the anchorable.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of the view model. </typeparam>
        void ShowAnchorable<TViewModel>(string iconSource = null)
            where TViewModel : IViewModel, new();

        /// <summary>
        ///     Shows the document in the main shell.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="iconSource">The icon source.</param>
        /// <param name="tag">The tag.</param>
        void ShowDocument<TViewModel>(string iconSource = null, object tag = null)
            where TViewModel : IViewModel;

        /// <summary>
        ///     Shows the document in the main shell.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="viewModel">The view model to show which will automatically be resolved to a view.</param>
        /// <param name="iconSource">The icon source.</param>
        /// <param name="tag">The tag.</param>
        /// <exception cref="ArgumentNullException">
        ///     The
        ///     <paramref name="viewModel" />
        ///     is
        ///     <c>null</c>
        ///     .
        /// </exception>
        void ShowDocument<TViewModel>(TViewModel viewModel, string iconSource = null, object tag = null)
            where TViewModel : IViewModel;

        #endregion
    }
}
