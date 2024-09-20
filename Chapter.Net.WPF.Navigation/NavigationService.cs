// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;
using Chapter.Net.WPF.Navigation.Dialogs;
using Chapter.Net.WPF.Navigation.MessageBoxes;
using Chapter.Net.WPF.Navigation.Presenters;
using Chapter.Net.WPF.Navigation.Windows;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Provides ways to show windows, user controls, dialogs and more.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IDialogProvider _dialogProvider;
    private readonly IMessageBoxProvider _messageBoxProvider;
    private readonly INavigationPresenterProvider _navigationPresenterProvider;
    private readonly IWindowProvider _windowProvider;

    /// <summary>
    ///     Creates a new instance of NavigationService.
    /// </summary>
    /// <param name="windowProvider">The provider of new windows or user controls.</param>
    /// <param name="messageBoxProvider">The provider to display message boxes.</param>
    /// <param name="dialogProvider">The provider to display system dialogs.</param>
    /// <param name="navigationPresenterProvider">The provider for navigation presenters.</param>
    /// <exception cref="ArgumentNullException">windowProvider is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxProvider is null.</exception>
    /// <exception cref="ArgumentNullException">pleaseWaitProvider is null.</exception>
    /// <exception cref="ArgumentNullException">dialogProvider is null.</exception>
    /// <exception cref="ArgumentNullException">navigationPresenterProvider is null.</exception>
    public NavigationService(
        IWindowProvider windowProvider,
        IMessageBoxProvider messageBoxProvider,
        IDialogProvider dialogProvider,
        INavigationPresenterProvider navigationPresenterProvider)
    {
        _windowProvider = windowProvider ?? throw new ArgumentNullException(nameof(windowProvider));
        _messageBoxProvider = messageBoxProvider ?? throw new ArgumentNullException(nameof(messageBoxProvider));
        _dialogProvider = dialogProvider ?? throw new ArgumentNullException(nameof(dialogProvider));
        _navigationPresenterProvider = navigationPresenterProvider ?? throw new ArgumentNullException(nameof(navigationPresenterProvider));
    }

    /// <inheritdoc />
    public Task ShowWindow(object windowKey, object viewModel)
    {
        ArgumentNullException.ThrowIfNull(windowKey);
        ArgumentNullException.ThrowIfNull(viewModel);

        return ShowWindowImpl(null, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task ShowWindow(object ownerWindowKey, object windowKey, object viewModel)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(windowKey);
        ArgumentNullException.ThrowIfNull(viewModel);

        return ShowWindowImpl(ownerWindowKey, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task<bool?> ShowModalWindow(object windowKey, object viewModel)
    {
        ArgumentNullException.ThrowIfNull(windowKey);
        ArgumentNullException.ThrowIfNull(viewModel);

        var mainWindow = _windowProvider.GetMainWindow();
        return ShowModalWindowImpl(mainWindow, windowKey, viewModel);
    }

    /// <inheritdoc />
    public Task<bool?> ShowModalWindow(object ownerWindowKey, object windowKey, object viewModel)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(windowKey);
        ArgumentNullException.ThrowIfNull(viewModel);

        return ShowModalWindowImpl(ownerWindowKey, windowKey, viewModel);
    }

    /// <inheritdoc />
    public void SetDialogResult(object windowKey, bool? dialogResult)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.DialogResult = dialogResult;
    }

    /// <inheritdoc />
    public void Close(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Close();
    }

    /// <inheritdoc />
    public void HideWindow(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Hide();
    }

    /// <inheritdoc />
    public void ShowHiddenWindow(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Show();
    }

    /// <inheritdoc />
    public Point GetWindowPosition(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Point(window.Left, window.Top);
    }

    /// <inheritdoc />
    public void MoveWindow(object windowKey, Point position)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Left = position.X;
        window.Top = position.Y;
    }

    /// <inheritdoc />
    public Size GetWindowSize(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Size(window.ActualWidth, window.ActualHeight);
    }

    /// <inheritdoc />
    public void ResizeWindow(object windowKey, Size size)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Width = size.Width;
        window.Height = size.Height;
    }

    /// <inheritdoc />
    public Rect GetWindowPositionAndSize(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Rect(new Point(window.Left, window.Top), new Size(window.ActualWidth, window.ActualHeight));
    }

    /// <inheritdoc />
    public void MoveAndResizeWindow(object windowKey, Rect rect)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Left = rect.Left;
        window.Top = rect.Top;
        window.Width = rect.Width;
        window.Height = rect.Height;
    }

    /// <inheritdoc />
    public WindowState GetWindowState(object windowKey)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        return window.WindowState;
    }

    /// <inheritdoc />
    public void SetWindowState(object windowKey, WindowState state)
    {
        ArgumentNullException.ThrowIfNull(windowKey);

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.WindowState = state;
    }

    /// <inheritdoc />
    public async Task<bool> ShowControl(object hostId, object controlKey, object viewModel)
    {
        ArgumentNullException.ThrowIfNull(hostId);
        ArgumentNullException.ThrowIfNull(controlKey);
        ArgumentNullException.ThrowIfNull(viewModel);

        var host = _navigationPresenterProvider.GetNavigationPresenter(hostId);
        if (!await host.CanSetContent())
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
        ArgumentNullException.ThrowIfNull(hostId);
        ArgumentNullException.ThrowIfNull(viewModel);

        if (viewModel is IEditable editable &&
            !await editable.TryLeave())
            return false;

        var host = _navigationPresenterProvider.GetNavigationPresenter(hostId);
        return host.ClearContent(viewModel);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText);
        return _messageBoxProvider.Show(mainWindow, messageBoxText);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(messageBoxText);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption, button);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption, button);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption, button);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);
        ArgumentNullException.ThrowIfNull(options);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText, caption, button, options);
        return _messageBoxProvider.Show(mainWindow, messageBoxText, caption, button, options);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);
        ArgumentNullException.ThrowIfNull(options);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText, caption, button, options);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IOpenFileData openFileData)
    {
        ArgumentNullException.ThrowIfNull(openFileData);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(openFileData);
        return _dialogProvider.Show(mainWindow, openFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IOpenFileData openFileData)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(openFileData);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, openFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(ISaveFileData saveFileData)
    {
        ArgumentNullException.ThrowIfNull(saveFileData);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(saveFileData);
        return _dialogProvider.Show(mainWindow, saveFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, ISaveFileData saveFileData)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(saveFileData);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, saveFileData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IBrowseFolderData browseFolderData)
    {
        ArgumentNullException.ThrowIfNull(browseFolderData);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(browseFolderData);
        return _dialogProvider.Show(mainWindow, browseFolderData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IBrowseFolderData browseFolderData)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(browseFolderData);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, browseFolderData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IColorPickerData colorPickerData)
    {
        ArgumentNullException.ThrowIfNull(colorPickerData);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(colorPickerData);
        return _dialogProvider.Show(mainWindow, colorPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IColorPickerData colorPickerData)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(colorPickerData);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, colorPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(IFontPickerData fontPickerData)
    {
        ArgumentNullException.ThrowIfNull(fontPickerData);

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(fontPickerData);
        return _dialogProvider.Show(mainWindow, fontPickerData);
    }

    /// <inheritdoc />
    public Task<bool> ShowDialog(object ownerWindowKey, IFontPickerData fontPickerData)
    {
        ArgumentNullException.ThrowIfNull(ownerWindowKey);
        ArgumentNullException.ThrowIfNull(fontPickerData);

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, fontPickerData);
    }

    /// <inheritdoc />
    public bool IsWindowKnown(object windowKey)
    {
        return _windowProvider.IsWindowRegistered(windowKey);
    }

    /// <inheritdoc />
    public bool IsControlKnown(object controlKey)
    {
        return _windowProvider.IsControlRegistered(controlKey);
    }

    /// <inheritdoc />
    public bool IsHostKnown(object hostId)
    {
        return _navigationPresenterProvider.TryNavigationPresenter(hostId, out _);
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
}