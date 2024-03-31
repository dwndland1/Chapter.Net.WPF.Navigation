// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IWindowProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;

namespace Chapter.Net.WPF.Navigation.Windows
{
    /// <summary>
    ///     Provides possibilities to create and display windows and user controls.
    /// </summary>
    public interface IWindowProvider
    {
        /// <summary>
        ///     Creates a new window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to create.</param>
        /// <returns>The newly created window.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="InvalidOperationException">For the windowKey no window is registered.</exception>
        Window GetNewWindow(object windowKey);

        /// <summary>
        ///     Returns the open window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to return.</param>
        /// <returns>The open window know by the key.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
        Window GetOpenWindow(object windowKey);

        /// <summary>
        ///     Returns the open window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to return.</param>
        /// <returns>The open window know by the key if known; otherwise null.</returns>
        Window TryGetOpenWindow(object windowKey);

        /// <summary>
        ///     Returns the main window.
        /// </summary>
        /// <returns>The main window if registered and open; otherwise null.</returns>
        Window GetMainWindow();

        /// <summary>
        ///     Creates a new user control by the key.
        /// </summary>
        /// <param name="controlKey">The key of the user control to create.</param>
        /// <returns>The newly created user control.</returns>
        /// <exception cref="ArgumentNullException">controlKey is null.</exception>
        /// <exception cref="InvalidOperationException">For the control key no user control is registered</exception>
        UserControl GetNewControl(object controlKey);
    }
}