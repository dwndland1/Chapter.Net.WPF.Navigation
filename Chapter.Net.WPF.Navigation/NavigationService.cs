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
    ///     Creates a new instance of <see cref="NavigationService" />.
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

    /// <summary>
    ///     Shows a non modal window.
    /// </summary>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    public Task ShowWindow(object windowKey, object viewModel)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        return ShowWindowImpl(null, windowKey, viewModel);
    }

    /// <summary>
    ///     Shows a non modal window.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the open owner window.</param>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
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

    /// <summary>
    ///     Shows a modal window.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await with the DialogResult.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    public Task<bool?> ShowModalWindow(object windowKey, object viewModel)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));

        var mainWindow = _windowProvider.GetMainWindow();
        return ShowModalWindowImpl(mainWindow, windowKey, viewModel);
    }

    /// <summary>
    ///     Shows a modal window.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the open owner window.</param>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await with the DialogResult.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
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

    /// <summary>
    ///     Sets the dialog result of the open modal window by its key.
    ///     That does not work for non modal windows.
    /// </summary>
    /// <param name="windowKey">The key of the window which dialog result to set.</param>
    /// <param name="dialogResult">The dialog result to set.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void SetDialogResult(object windowKey, bool? dialogResult)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.DialogResult = dialogResult;
    }

    /// <summary>
    ///     Closes the open window known by its key.
    ///     If the window was modal, the DialogResult will be null.
    /// </summary>
    /// <param name="windowKey">The key of the window to close.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void Close(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Close();
    }

    /// <summary>
    ///     Hides the open window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to hide.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void HideWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Hide();
    }

    /// <summary>
    ///     Shows the window back again by its key which got hidden before.
    /// </summary>
    /// <param name="windowKey">The key of the window to show.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void ShowHiddenWindow(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Show();
    }

    /// <summary>
    ///     Reads the actual position of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which position to read.</param>
    /// <returns>The window position.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public Point GetWindowPosition(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Point(window.Left, window.Top);
    }

    /// <summary>
    ///     Moves the window to a specific position by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to move.</param>
    /// <param name="position">The new position the window has to move to.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void MoveWindow(object windowKey, Point position)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Left = position.X;
        window.Top = position.Y;
    }

    /// <summary>
    ///     Reads the actual size of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which size to read.</param>
    /// <returns>The window size.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public Size GetWindowSize(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Size(window.ActualWidth, window.ActualHeight);
    }

    /// <summary>
    ///     Resizes the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to resize.</param>
    /// <param name="size">The new size of the window.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void ResizeWindow(object windowKey, Size size)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.Width = size.Width;
        window.Height = size.Height;
    }

    /// <summary>
    ///     Reads the actual size and position of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which size and position to read.</param>
    /// <returns>The size and position of the window.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public Rect GetWindowPositionAndSize(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return new Rect(new Point(window.Left, window.Top), new Size(window.ActualWidth, window.ActualHeight));
    }

    /// <summary>
    ///     Moves and resizes the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to move and resize.</param>
    /// <param name="rect">The new position and size of the window.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
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

    /// <summary>
    ///     Reads the actual state of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which state to read.</param>
    /// <returns>The window state.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public WindowState GetWindowState(object windowKey)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        return window.WindowState;
    }

    /// <summary>
    ///     Sets the state of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which state to set.</param>
    /// <param name="state">The new window state.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    public void SetWindowState(object windowKey, WindowState state)
    {
        if (windowKey == null)
            throw new ArgumentNullException(nameof(windowKey));

        var window = _windowProvider.GetOpenWindow(windowKey);
        window.WindowState = state;
    }

    /// <summary>
    ///     Shows a new user control by its control key into the <see cref="NavigationPresenter" /> known by its id.
    /// </summary>
    /// <param name="hostId">The ID of the <see cref="NavigationPresenter" /> where to display the user control.</param>
    /// <param name="controlKey">The ID of the user control to create.</param>
    /// <param name="viewModel">The ViewModel which will be set into the DataContext of newly created user control.</param>
    /// <returns>
    ///     The task to await before or after the user control is shown. True if it was successfully (
    ///     <see cref="IEditable" />); otherwise false.
    /// </returns>
    /// <exception cref="ArgumentNullException">hostId is null.</exception>
    /// <exception cref="ArgumentNullException">controlKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    /// <exception cref="InvalidOperationException">There is no navigation presenter registered with the given hostId.</exception>
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

    /// <summary>
    ///     Removes a specific user control by its control key into the <see cref="NavigationPresenter" /> known by its id.
    /// </summary>
    /// <param name="hostId">The ID of the <see cref="NavigationPresenter" /> where to remove the user control from.</param>
    /// <param name="viewModel">The ViewModel of the user control to remove.</param>
    /// <returns>True if the user control got removed; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">hostId is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    /// <exception cref="InvalidOperationException">There is no navigation presenter registered with the given hostId.</exception>
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
        return host.ClearContent(viewModel);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    public Task<MessageBoxResult> ShowMessageBox(string messageBoxText)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _messageBoxProvider.Show(messageBoxText);
        return _messageBoxProvider.Show(mainWindow, messageBoxText);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    public Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _messageBoxProvider.Show(window, messageBoxText);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
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

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
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

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
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

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
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

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="ArgumentNullException">options is null.</exception>
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

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="ArgumentNullException">options is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
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

    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    public Task<bool> ShowDialog(IOpenFileData openFileData)
    {
        if (openFileData == null)
            throw new ArgumentNullException(nameof(openFileData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(openFileData);
        return _dialogProvider.Show(mainWindow, openFileData);
    }

    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    public Task<bool> ShowDialog(object ownerWindowKey, IOpenFileData openFileData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (openFileData == null)
            throw new ArgumentNullException(nameof(openFileData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, openFileData);
    }

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    public Task<bool> ShowDialog(ISaveFileData saveFileData)
    {
        if (saveFileData == null)
            throw new ArgumentNullException(nameof(saveFileData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(saveFileData);
        return _dialogProvider.Show(mainWindow, saveFileData);
    }

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    public Task<bool> ShowDialog(object ownerWindowKey, ISaveFileData saveFileData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (saveFileData == null)
            throw new ArgumentNullException(nameof(saveFileData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, saveFileData);
    }

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    public Task<bool> ShowDialog(IBrowseFolderData browseFolderData)
    {
        if (browseFolderData == null)
            throw new ArgumentNullException(nameof(browseFolderData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(browseFolderData);
        return _dialogProvider.Show(mainWindow, browseFolderData);
    }

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    public Task<bool> ShowDialog(object ownerWindowKey, IBrowseFolderData browseFolderData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (browseFolderData == null)
            throw new ArgumentNullException(nameof(browseFolderData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, browseFolderData);
    }

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    public Task<bool> ShowDialog(IColorPickerData colorPickerData)
    {
        if (colorPickerData == null)
            throw new ArgumentNullException(nameof(colorPickerData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(colorPickerData);
        return _dialogProvider.Show(mainWindow, colorPickerData);
    }

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    public Task<bool> ShowDialog(object ownerWindowKey, IColorPickerData colorPickerData)
    {
        if (ownerWindowKey == null)
            throw new ArgumentNullException(nameof(ownerWindowKey));
        if (colorPickerData == null)
            throw new ArgumentNullException(nameof(colorPickerData));

        var window = _windowProvider.GetOpenWindow(ownerWindowKey);
        return _dialogProvider.Show(window, colorPickerData);
    }

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    public Task<bool> ShowDialog(IFontPickerData fontPickerData)
    {
        if (fontPickerData == null)
            throw new ArgumentNullException(nameof(fontPickerData));

        var mainWindow = _windowProvider.GetMainWindow();
        if (mainWindow == null)
            return _dialogProvider.Show(fontPickerData);
        return _dialogProvider.Show(mainWindow, fontPickerData);
    }

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
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