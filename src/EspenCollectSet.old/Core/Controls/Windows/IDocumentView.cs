namespace EspenCollectSet.Core
{

    using Catel.MVVM.Views;

    /// <summary>
    ///     Interface defining a document view.
    /// </summary>
    public interface IDocumentView : IView
    {
        /// <summary>
        ///     Closes the document.
        /// </summary>
        void CloseDocument();
    }
}
