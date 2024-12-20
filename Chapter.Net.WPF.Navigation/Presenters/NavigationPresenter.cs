﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationPresenter.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chapter.Net.WPF.Navigation.Presenters;

/// <summary>
///     Base class for the host where the <see cref="NavigationService" /> shows its user control.
/// </summary>
public abstract class NavigationPresenter : Control, INavigationPresenter
{
    /// <summary>
    ///     The DependencyProperty for the ID property.
    /// </summary>
    public static readonly DependencyProperty IDProperty =
        DependencyProperty.Register(nameof(ID), typeof(object), typeof(NavigationPresenter), new PropertyMetadata(OnIDChanged));

    /// <summary>
    ///     The DependencyProperty for the EnableUIPersistence property.
    /// </summary>
    public static readonly DependencyProperty EnableUIPersistenceProperty =
        DependencyProperty.Register(nameof(EnableUIPersistence), typeof(bool), typeof(NavigationPresenter), new PropertyMetadata(false));

    /// <summary>
    ///     The DependencyProperty for the DisposeViewModel property.
    /// </summary>
    public static readonly DependencyProperty DisposeViewModelProperty =
        DependencyProperty.Register(nameof(DisposeViewModel), typeof(bool), typeof(NavigationPresenter), new PropertyMetadata(false));

    private readonly Dictionary<WeakReference, FrameworkElement> _cache = new();

    /// <summary>
    ///     The ID of the presenter how it registers in the <see cref="NavigationPresenter" />.
    /// </summary>
    /// <value>Default: null.</value>
    [DefaultValue(null)]
    public object ID
    {
        get => GetValue(IDProperty);
        set => SetValue(IDProperty, value);
    }

    /// <summary>
    ///     Sets or sets a value indicating if the UI element shall be persisted if the content changed.
    /// </summary>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    public bool EnableUIPersistence
    {
        get => (bool)GetValue(EnableUIPersistenceProperty);
        set => SetValue(EnableUIPersistenceProperty, value);
    }

    /// <summary>
    ///     Defines if the viewmodel shall be disposed if they implement the IDisposable and the ViewModel changed.
    /// </summary>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    public bool DisposeViewModel
    {
        get => (bool)GetValue(DisposeViewModelProperty);
        set => SetValue(DisposeViewModelProperty, value);
    }

    /// <inheritdoc />
    public virtual FrameworkElement GetCached(object viewModel)
    {
        var dead = _cache.Where(x => !x.Key.IsAlive);
        foreach (var pair in dead)
            _cache.Remove(pair.Key);

        return _cache.FirstOrDefault(x => Equals(x.Key.Target, viewModel)).Value;
    }

    /// <inheritdoc />
    public virtual void StoreCached(object viewModel, FrameworkElement control)
    {
        if (!EnableUIPersistence)
            return;

        var dead = _cache.Where(x => !x.Key.IsAlive);
        foreach (var pair in dead)
            _cache.Remove(pair.Key);

        _cache[new WeakReference(viewModel)] = control;
    }

    /// <inheritdoc />
    public abstract Task<bool> CanSetContent();

    /// <inheritdoc />
    public abstract void SetContent(FrameworkElement control);

    /// <inheritdoc />
    public abstract bool ClearContent(object viewModel);

    private static void OnIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (NavigationPresenter)d;
        if (e.OldValue != null)
            NavigationPresenterProvider.UnregisterPresenter(e.OldValue);
        if (e.NewValue != null)
            NavigationPresenterProvider.RegisterPresenter(e.NewValue, control);
    }
}