// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Chapter.Net.WPF.Navigation.Dialogs;
using Chapter.Net.WPF.Navigation.MessageBoxes;
using Chapter.Net.WPF.Navigation.Windows;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Provides ways to show windows, user controls, dialogs and more.
/// </summary>
public class NavigationService : INavigationService
{
    private static readonly Dictionary<object, WeakReference> _navigationPresenter = new();
    private readonly IDialogProvider _dialogProvider;
    private readonly IMessageBoxProvider _messageBoxProvider;
    private readonly IWindowProvider _windowProvider;

    /// <summary>
    ///     Creates a new instance of NavigationService.
    /// </summary>
    /// <param name="windowProvider">The provider of new windows or user controls.</param>
    /// <param name="messageBoxProvider">The provider to display message boxes.</param>
    /// <param name="dialogProvider">The provider to display system dialogs.</param>
    /// <exception cref="ArgumentNullException">windowProvider is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxProvider is null.</exception>
    /// <exception cref="ArgumentNullException">pleaseWaitProvider is null.</exception>
    /// <exception cref="ArgumentNullException">dialogProvider is null.</exception>
    public NavigationService(IWindowProvider windowProvider,
        IMessageBoxProvider messageBoxProvider,
        IDialogProvider dialogProvider)
    {
        _windowProvider = windowProvider ?? throw new ArgumentNullException(nameof(windowProvider));
        _messageBoxProvider = messageBoxProvider ?? throw new ArgumentNullException(nameof(messageBoxProvider));
        _dialogProvider = dialogProvider ?? throw new ArgumentNullException(nameof(dialogProvider));
    }

    /// <inheritdoc />
    public Task ShowWindow(object windowKey, object viewModel)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        return ShowWindowImpl(null, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task ShowWindow(object ownerWindowKey, object windowKey, object viewModel)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        return ShowWindowImpl(ownerWindowKey, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task<bool?> ShowModalWindow(object windowKey, object viewModel)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        var mainWindow = _windowProvider.GetMainWindow();
        return ShowModalWindowImpl(mainWindow, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task<bool?> ShowModalWindow(object ownerWindowKey, object windowKey, object viewModel)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        return ShowModalWindowImpl(ownerWindowKey, windowKey, viewModel);
    }

    /// <inheritdoc />
    public void SetDialogResult(object windowKey, bool? dialogResult)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.DialogResult = dialogResult;
    }

    /// <inheritdoc />
    public void Close(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Close();
    }

    /// <inheritdoc />
    public void HideWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Hide();
    }

    /// <inheritdoc />
    public void ShowHiddenWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Show();
    }

    /// <inheritdoc />
    public Point GetWindowPosition(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Point(window.Left, window.Top);
    }

    /// <inheritdoc />
    public void MoveWindow(object windowKey, Point position)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Left = position.X;
        window.Top = position.Y;
    }

    /// <inheritdoc />
    public Size GetWindowSize(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Size(window.ActualWidth, window.ActualHeight);
    }

    /// <inheritdoc />
    public void ResizeWindow(object windowKey, Size size)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Width = size.Width;
        window.Height = size.Height;
    }

    /// <inheritdoc />
    public Rect GetWindowPositionAndSize(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Rect(new Point(window.Left, window.Top), new Size(window.ActualWidth, window.ActualHeight));
    }

    /// <inheritdoc />
    public void MoveAndResizeWindow(object windowKey, Rect rect)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Left = rect.Left;
        window.Top = rect.Top;
        window.Width = rect.Width;
        window.Height = rect.Height;
    }

    /// <inheritdoc />
    public WindowState GetWindowState(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return window.WindowState;
    }

    /// <inheritdoc />
    public void SetWindowState(object windowKey, WindowState state)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.WindowState = state;
    }

    /// <inheritdoc />
    public async Task<bool> ShowControl(object hostId, object controlKey, object viewModel)
    {
        if (hostId == null)
            throw new ArgumentNullException(nameof(hostId));
        if (controlKey == null)
            throw new ArgumentNullException(nameof(controlKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        RemoveDeadNavigationPresenter();
        if (!_navigationPresenter.TryGetValue(hostId, out var reference))
            throw new InvalidOperationException($"For the ID '{hostId}' no INavigationPresenter is registered");

        var host = (NavigationPresenter)reference.Target;
        if (!await host!.CanSetContent())
            return false;

        var control = host.GetCached(viewModel);
        if (control == null)
        {
            control = _windowProvider.GetNewControl(controlKey);
            control.DataContext = viewModel;
        }

        host.StoreCached(viewModel, control);

        host.SetContent(control);
        if (viewModel is ILoader asyncLoader)
            await asyncLoader.Load();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveControl(object hostId, object viewModel)
    {
        if (hostId == null)
            throw new ArgumentNullException(nameof(hostId));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        RemoveDeadNavigationPresenter();
        if (!_navigationPresenter.TryGetValue(hostId, out var reference))
            throw new InvalidOperationException($"For the ID '{hostId}' no INavigationPresenter is registered");

        if (viewModel is IEditable editable &&
            !await editable.TryLeave())
            return false;

        var host = (NavigationPresenter)reference.Target;
        return host!.ClearContent(viewModel);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText);
        return _messageBoxProvider.Show(mainWindow, messageBoxText);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption, button);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption, button);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption, button);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption, button, options);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption, button, options);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption, button, options);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IOpenFileData openFileData)
    {
        if (openFileData == null)
            throw new ArgumentNullException(nameof(openFileData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(openFileData);
        return _dialogProvider.Show(mainWindow, openFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IOpenFileData openFileData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (openFileData == null)
            throw new ArgumentNullException(nameof(openFileData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, openFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(ISaveFileData saveFileData)
    {
        if (saveFileData == null)
            throw new ArgumentNullException(nameof(saveFileData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(saveFileData);
        return _dialogProvider.Show(mainWindow, saveFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, ISaveFileData saveFileData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (saveFileData == null)
            throw new ArgumentNullException(nameof(saveFileData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, saveFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IBrowseFolderData browseFolderData)
    {
        if (browseFolderData == null)
            throw new ArgumentNullException(nameof(browseFolderData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(browseFolderData);
        return _dialogProvider.Show(mainWindow, browseFolderData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IBrowseFolderData browseFolderData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (browseFolderData == null)
            throw new ArgumentNullException(nameof(browseFolderData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, browseFolderData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IColorPickerData colorPickerData)
    {
        if (colorPickerData == null)
            throw new ArgumentNullException(nameof(colorPickerData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(colorPickerData);
        return _dialogProvider.Show(mainWindow, colorPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IColorPickerData colorPickerData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (colorPickerData == null)
            throw new ArgumentNullException(nameof(colorPickerData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, colorPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IFontPickerData fontPickerData)
    {
        if (fontPickerData == null)
            throw new ArgumentNullException(nameof(fontPickerData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(fontPickerData);
        return _dialogProvider.Show(mainWindow, fontPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IFontPickerData fontPickerData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (fontPickerData == null)
            throw new ArgumentNullException(nameof(fontPickerData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, fontPickerData);
    }

    private async Task ShowWindowImpl(object ownerWindowKey, object windowKey, object viewModel)
    {
        var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
        window.Show();
        if (viewModel is ILoader asyncLoader)
            await asyncLoader.Load();
    }

    private Task<bool?> ShowModalWindowImpl(object ownerWindowKey, object windowKey, object viewModel)
    {
        var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
        if (viewModel is ILoader asyncLoader)
            asyncLoader.Load().FireAndForget();
        return Task.FromResult(window.ShowDialog());
    }

    private Window CreateWindow(object ownerWindowKey, object windowKey, object viewModel)
    {
        var window = _windowProvider.GetNewWindow(windowKey);
        window.DataContext = viewModel;
        if (ownerWindowKey != null)
            window.Owner = _windowProvider.GetOpenWindow(ownerWindowKey);
        window.Closed += HandleWindowClosed;
        return window;
    }

    private void HandleWindowClosed(object sender, EventArgs e)
    {
        var window = (Window)sender;
        window.Closed -= HandleWindowClosed;
        (window.DataContext as IDisposable)?.Dispose();
    }

    internal static void UnregisterPresenter(object id)
    {
        RemoveDeadNavigationPresenter();
        _navigationPresenter.Remove(id);
    }

    private static void RemoveDeadNavigationPresenter()
    {
        var dead = _navigationPresenter.Where(x => !x.Value.IsAlive).ToList();
        foreach (var pair in dead)
            _navigationPresenter.Remove(pair.Key);
    }

    internal static void RegisterPresenter(object id, NavigationPresenter control)
    {
        RemoveDeadNavigationPresenter();
        _navigationPresenter[id] = new WeakReference(control);
    }
}