namespace EspenCollectSet
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using EspenCollectSet.Core;
    using Xceed.Wpf.AvalonDock;
    using Xceed.Wpf.AvalonDock.Layout;

    /// <summary>
    ///     Helper class for avalon dock.
    /// </summary>
    public class AvalonDockHelper
    {
        #region Fields

        /// <summary>
        ///     The layout document pane.
        /// </summary>
        private static readonly LayoutDocumentPane LayoutDocumentPane;

        /// <summary>
        ///     The layout anchorable pane.
        /// </summary>
        private static readonly LayoutAnchorablePane LayoutAnchorablePane;

        /// <summary>
        ///     The service locator
        /// </summary>
        private static readonly IServiceLocator ServiceLocator;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <remarks></remarks>
        static AvalonDockHelper()
        {
            ServiceLocator = Catel.IoC.ServiceLocator.Default;

            var dockingManager = ServiceLocator.ResolveType<DockingManager>();
            dockingManager.DocumentClosed += OnDockingManagerDocumentClosed;

            LayoutDocumentPane = ServiceLocator.ResolveType<LayoutDocumentPane>();

            LayoutAnchorablePane = ServiceLocator.ResolveType<LayoutAnchorablePane>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the document.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>
        ///     The found document or <c>null</c> if no document was found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="viewType" /> is <c>null</c>.
        /// </exception>
        public static LayoutDocument FindDocument(Type viewType, object tag = null)
        {
            Argument.IsNotNull(nameof(viewType), viewType);

            // TODO: Add tag options

            return
                LayoutDocumentPane.Children.Where(
                    document =>
                        document is LayoutDocument && ObjectHelper.AreEqual(document.Content.GetType(), viewType))
                    .Cast<LayoutDocument>().FirstOrDefault();
        }

        /// <summary>
        ///     Finds the anchorable.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public static LayoutAnchorable FindAnchorable(Type viewType, object tag = null)
        {
            Argument.IsNotNull(nameof(viewType), viewType);

            // TODO: Add tag options

            return
                LayoutAnchorablePane.Children
                    .FirstOrDefault(anchorable => !ObjectHelper.IsNull(anchorable) &&
                        ObjectHelper.AreEqual(anchorable.Content.GetType(), viewType));
        }

        /// <summary>
        ///     Activates the document in the docking manager, which makes it the active document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="document" /> is <c>null</c>.
        /// </exception>
        public static void ActivateDocument(LayoutDocument document)
        {
            Argument.IsNotNull(nameof(document), document);

            LayoutDocumentPane.SelectedContentIndex = LayoutDocumentPane.IndexOfChild(document);
        }

        /// <summary>
        ///     Activates the anchorable.
        /// </summary>
        /// <param name="anchorable">The anchorable.</param>
        public static void ActivateAnchorable(LayoutAnchorable anchorable)
        {
            Argument.IsNotNull(nameof(anchorable), anchorable);

            LayoutAnchorablePane.SelectedContentIndex = LayoutAnchorablePane.IndexOfChild(anchorable);
        }

        /// <summary>
        ///     Creates the document.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="iconSource">The icon source.</param>
        /// <returns>
        ///     The created layout document.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="view" /> is <c>null</c>.
        /// </exception>
        public static LayoutDocument CreateDocument(FrameworkElement view, object tag = null, string iconSource = null)
        {
            Argument.IsNotNull(nameof(view), view);

            var layoutDocument = WrapViewInLayoutDocument(view, tag);

            if (!string.IsNullOrWhiteSpace(iconSource))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(iconSource);
                bitmapImage.EndInit();

                layoutDocument.IconSource = bitmapImage;
            }

            LayoutDocumentPane.Children.Add(layoutDocument);

            return layoutDocument;
        }

        /// <summary>
        ///     Creates the anchorable.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="iconSource">The icon source.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public static LayoutAnchorable CreateAnchorable(FrameworkElement view, string iconSource = null,
            object tag = null)
        {
            Argument.IsNotNull(nameof(view), view);

            var layoutAnchorable = WrapViewInLayoutAnchorable(view, tag);

            if (!string.IsNullOrWhiteSpace(iconSource))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(iconSource);
                bitmapImage.EndInit();

                layoutAnchorable.IconSource = bitmapImage;
            }

            LayoutAnchorablePane.Children.Add(layoutAnchorable);

            return layoutAnchorable;
        }

        /// <summary>
        ///     Wraps the view in a layout document.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>A wrapped layout document.</returns>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="view" /> is <c>null</c>.
        /// </exception>
        private static LayoutDocument WrapViewInLayoutDocument(FrameworkElement view, object tag = null)
        {
            Argument.IsNotNull(nameof(view), view);

            var layoutDocument = new LayoutDocument
            {
                CanFloat = false,
                Title = ((IViewModel)view.DataContext).Title,
                Content = view
            };

            // TODO: Make bindable => automatic updates

            view.SetCurrentValue(FrameworkElement.TagProperty, tag);

            return layoutDocument;
        }

        /// <summary>
        ///     Wraps the view in layout anchorable.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        private static LayoutAnchorable WrapViewInLayoutAnchorable(FrameworkElement view, object tag = null)
        {
            Argument.IsNotNull(nameof(view), view);

            var layoutAnchorable = new LayoutAnchorable
            {
                CanFloat = false,
                Title = ((IViewModel)view.DataContext).Title,
                Content = view
            };

            // TODO: Make bindable => automatic updates

            view.SetCurrentValue(FrameworkElement.TagProperty, tag);

            return layoutAnchorable;
        }

        /// <summary>
        ///     Called when the docking manager has just closed a document.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="DocumentClosedEventArgs" /> instance containing the event data.
        /// </param>
        private static void OnDockingManagerDocumentClosed(object sender, DocumentClosedEventArgs e)
        {
            var containerView = e.Document;
            var view = containerView.Content as IDocumentView;
            view?.CloseDocument();
        }

        #endregion
    }
}
