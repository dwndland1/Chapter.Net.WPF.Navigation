// -----------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationPresenter.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation.Presenters;

/// <summary>
///     Interface for the host where the <see cref="NavigationService" /> shows its user control.
/// </summary>
public interface INavigationPresenter
{
    /// <summary>
    ///     Gets the potential cached user control by its viewmodel.
    /// </summary>
    /// <param name="viewModel">The viewmodel of the user control.</param>
    /// <returns>The user control if any; otherwise null.</returns>
    FrameworkElement GetCached(object viewModel);

    /// <summary>
    ///     Stores a user control for a later use if <see cref="NavigationPresenter.EnableUIPersistence" /> is on.
    /// </summary>
    /// <param name="viewModel">The viewmodel of the user control.</param>
    /// <param name="control">The user control to store.</param>
    void StoreCached(object viewModel, FrameworkElement control);

    /// <summary>
    ///     Checks if the current displayed user control can be replaced by a different.
    /// </summary>
    /// <returns>The task to await.</returns>
    Task<bool> CanSetContent();

    /// <summary>
    ///     Replaces the current displayed user control by a different one.
    /// </summary>
    /// <param name="control">The control to set.</param>
    void SetContent(FrameworkElement control);

    /// <summary>
    ///     Removes the content by its viewmodel.
    /// </summary>
    /// <param name="viewModel">The viewmodel of the content to remove.</param>
    bool ClearContent(object viewModel);
}