﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WindowProvider.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Chapter.Net.WPF.Navigation.Windows;

/// <inheritdoc />
public sealed class WindowProvider : IWindowProvider
{
    private static readonly DependencyProperty InstanceProperty =
        DependencyProperty.RegisterAttached("Instance", typeof(Guid), typeof(WindowProvider), new PropertyMetadata(default(Guid)));

    private readonly Dictionary<object, Type> _controls;
    private readonly Dictionary<Guid, Tuple<object, Window>> _openWindows;
    private readonly Dictionary<object, Type> _windows;
    private object _mainWindowKey;

    /// <summary>
    ///     Creates a new instance of WindowProvider.
    /// </summary>
    public WindowProvider()
    {
        _windows = new Dictionary<object, Type>();
        _controls = new Dictionary<object, Type>();
        _openWindows = new Dictionary<Guid, Tuple<object, Window>>();
    }

    /// <inheritdoc />
    public Window GetNewWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        if (!_windows.TryGetValue(windowKey, out var windowType))
            throw new InvalidOperationException($"For the windowKey '{windowKey}' no window is registered");

        var window = (Window)Activator.CreateInstance(windowType);
        var instance = Guid.NewGuid();
        SetInstance(window, instance);
        _openWindows[instance] = Tuple.Create(windowKey, window);
        window!.Closed += HandleWindowClosed;
        return window;
    }

    /// <inheritdoc />
    public Window GetOpenWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = TryGetOpenWindow(windowKey);
        if (window == null)
            throw new InvalidOperationException($"There is no open window with the window key '{windowKey}'");

        return window;
    }

    /// <inheritdoc />
    public Window TryGetOpenWindow(object windowKey)
    {
        var pair = _openWindows.FirstOrDefault(x => Equals(x.Value.Item1, windowKey));
        return pair.Value?.Item2;
    }

    /// <inheritdoc />
    public Window GetMainWindow()
    {
        return _mainWindowKey == null ? null : TryGetOpenWindow(_mainWindowKey);
    }

    /// <inheritdoc />
    public UserControl GetNewControl(object controlKey)
    {
        if (controlKey == null)
            throw new ArgumentNullException(nameof(controlKey));

        if (!_controls.TryGetValue(controlKey, out var controlType))
            throw new InvalidOperationException($"For the control key '{controlKey}' no user control is registered");

        return (UserControl)Activator.CreateInstance(controlType);
    }

    private void HandleWindowClosed(object sender, EventArgs e)
    {
        var window = (Window)sender;
        window.Closed -= HandleWindowClosed;
        var instance = GetInstance(window);
        _openWindows.Remove(instance);
    }

    /// <summary>
    ///     Registers a window type for a window key.
    /// </summary>
    /// <typeparam name="TWindow">The type of the window to register.</typeparam>
    /// <param name="windowKey">The window key.</param>
    /// <param name="isMainWindow">Registers the window key additionally as the main window.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    public void RegisterWindow<TWindow>(object windowKey, bool isMainWindow = false) where TWindow : Window
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        if (isMainWindow)
            _mainWindowKey = windowKey;

        _windows[windowKey] = typeof(TWindow);
    }

    /// <summary>
    ///     Registers a user control type by a key.
    /// </summary>
    /// <typeparam name="TControl">The type of the user control to register.</typeparam>
    /// <param name="controlKey">The user control key.</param>
    /// <exception cref="ArgumentNullException">controlKey is null.</exception>
    public void RegisterControl<TControl>(object controlKey) where TControl : UserControl
    {
        if (controlKey == null)
            throw new ArgumentNullException(nameof(controlKey));

        _controls[controlKey] = typeof(TControl);
    }

    private static Guid GetInstance(DependencyObject obj)
    {
        return (Guid)obj.GetValue(InstanceProperty);
    }

    private static void SetInstance(DependencyObject obj, Guid value)
    {
        obj.SetValue(InstanceProperty, value);
    }
}