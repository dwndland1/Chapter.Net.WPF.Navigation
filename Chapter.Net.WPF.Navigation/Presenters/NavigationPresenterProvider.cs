// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationPresenterProvider.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter.Net.WPF.Navigation.Presenters;

/// <inheritdoc />
public sealed class NavigationPresenterProvider : INavigationPresenterProvider
{
    private static readonly Dictionary<object, WeakReference> _navigationPresenter = new();

    /// <inheritdoc />
    public INavigationPresenter GetNavigationPresenter(object hostId)
    {
        if (TryNavigationPresenter(hostId, out var presenter))
            return presenter;

        throw new InvalidOperationException($"For the ID '{hostId}' no INavigationPresenter is registered");
    }

    /// <inheritdoc />
    public bool TryNavigationPresenter(object hostId, out INavigationPresenter navigationPresenter)
    {
        RemoveDeadNavigationPresenter();
        if (!_navigationPresenter.TryGetValue(hostId, out var reference))
        {
            navigationPresenter = null;
            return false;
        }

        navigationPresenter = (INavigationPresenter)reference.Target;
        return true;
    }

    internal static void UnregisterPresenter(object id)
    {
        RemoveDeadNavigationPresenter();
        _navigationPresenter.Remove(id);
    }

    internal static void RegisterPresenter(object id, NavigationPresenter control)
    {
        RemoveDeadNavigationPresenter();
        _navigationPresenter[id] = new WeakReference(control);
    }

    private static void RemoveDeadNavigationPresenter()
    {
        var dead = _navigationPresenter.Where(x => !x.Value.IsAlive).ToList();
        foreach (var pair in dead)
            _navigationPresenter.Remove(pair.Key);
    }
}