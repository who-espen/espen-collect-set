namespace EspenCollectSet.Helpers
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// </summary>
    public static class PasswordHelper
    {
        #region Fields

        /// <summary>
        ///     The password property
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
                typeof(string), typeof(PasswordHelper),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.
                        BindsTwoWayByDefault,
                    OnPasswordPropertyChanged));

        /// <summary>
        ///     The is updating property
        /// </summary>
        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
                typeof(PasswordHelper));

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <param name="dependencyObject"> The dependency object. </param>
        /// <returns> </returns>
        public static string GetPassword(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(PasswordProperty);

        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <param name="dependencyObject"> The dependency object. </param>
        /// <param name="value"> The value. </param>
        public static void SetPassword(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(PasswordProperty, value);
        }

        /// <summary>
        ///     Gets the is updating.
        /// </summary>
        /// <param name="dependencyObject"> The dependency object. </param>
        /// <returns> </returns>
        private static bool GetIsUpdating(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(IsUpdatingProperty);

        /// <summary>
        ///     Sets the is updating.
        /// </summary>
        /// <param name="dependencyObject"> The dependency object. </param>
        /// <param name="value">
        ///     if set to <c>true</c> value.
        /// </param>
        private static void SetIsUpdating(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsUpdatingProperty, value);
        }

        /// <summary>
        ///     Called when password property changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data.
        /// </param>
        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(passwordBox))
                passwordBox.Password = (string)dependencyPropertyChangedEventArgs.NewValue;
            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        ///     Passwords the changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="routedEventArgs">
        ///     The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.
        /// </param>
        private static void PasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            if (passwordBox == null)
                return;
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }

        #endregion
    }
}
