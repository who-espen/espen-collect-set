namespace EspenCollectSet.Windows.Tabs
{
    using System.Linq;
    using Xceed.Wpf.AvalonDock.Layout;

    /// <summary>
    ///     Layout initializer for layout update strategy.
    /// </summary>
    public class LayoutInitializer : ILayoutUpdateStrategy
    {
        #region ILayoutUpdateStrategy Members

        /// <summary>
        ///     Before the insert anchorable.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="anchorableToShow">The anchorable to show.</param>
        /// <param name="destinationContainer">The destination container.</param>
        /// <returns></returns>
        public virtual bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow,
            ILayoutContainer destinationContainer)
        {
            //AD wants to add the anchorable into destinationContainer
            //just for test provide a new anchorablepane 
            //if the pane is floating let the manager go ahead
            if (destinationContainer != null &&
                destinationContainer.FindParent<LayoutFloatingWindow>() != null)
            {
                return false;
            }

            var toolsPane =
                layout.Descendents()
                    .OfType<LayoutAnchorablePane>()
                    .FirstOrDefault();

            if (toolsPane == null) return false;
            toolsPane.Children.Add(anchorableToShow);
            return true;
        }

        /// <summary>
        ///     Afters the insert anchorable.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="anchorableShown">The anchorable shown.</param>
        public virtual void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
        }

        /// <summary>
        ///     Before the insert document.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="anchorableToShow">The anchorable to show.</param>
        /// <param name="destinationContainer">The destination container.</param>
        /// <returns></returns>
        public virtual bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow,
            ILayoutContainer destinationContainer) => false;

        /// <summary>
        ///     Afters the insert document.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <param name="anchorableShown">The anchorable shown.</param>
        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
        }

        #endregion
    }
}
