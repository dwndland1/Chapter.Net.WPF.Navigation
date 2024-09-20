// -----------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationPresenterProvider.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

namespace Chapter.Net.WPF.Navigation.Presenters;

/// <summary>
///     Provides all currently available navigation providers.
/// </summary>
public interface INavigationPresenterProvider
{
    /// <summary>
    ///     Provides the available navigation presenter by its host ID.
    /// </summary>
    /// <param name="hostId">The ID of the navigation presenter.</param>
    /// <returns>The navigation presenter.</returns>
    /// <exception cref="InvalidOperationException">There is no known navigation presenter for the given host ID.</exception>
    INavigationPresenter GetNavigationPresenter(object hostId);

    /// <summary>
    ///     Tries to provide the available navigation presenter by its host ID.
    /// </summary>
    /// <param name="hostId">The ID of the navigation presenter.</param>
    /// <param name="navigationPresenter">The navigation presenter.</param>
    /// <returns>True if the navigation presenter was provided; otherwise false.</returns>
    bool TryNavigationPresenter(object hostId, out INavigationPresenter navigationPresenter);
}