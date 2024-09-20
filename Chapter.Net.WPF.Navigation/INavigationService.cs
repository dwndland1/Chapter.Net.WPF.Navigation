// -----------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationService.cs" company="dwndland">
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
public interface INavigationService
{
    /// <summary>
    ///     Shows a non-modal window.
    /// </summary>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    Task ShowWindow(object windowKey, object viewModel);

    /// <summary>
    ///     Shows a non-modal window.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the open owner window.</param>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    Task ShowWindow(object ownerWindowKey, object windowKey, object viewModel);

    /// <summary>
    ///     Shows a modal window.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="windowKey">The key of the window to generate.</param>
    /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
    /// <returns>The task to await with the DialogResult.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    Task<bool?> ShowModalWindow(object windowKey, object viewModel);

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
    Task<bool?> ShowModalWindow(object ownerWindowKey, object windowKey, object viewModel);

    /// <summary>
    ///     Sets the dialog result of the open modal window by its key.
    ///     That does not work for non-modal windows.
    /// </summary>
    /// <param name="windowKey">The key of the window which dialog result to set.</param>
    /// <param name="dialogResult">The dialog result to set.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void SetDialogResult(object windowKey, bool? dialogResult);

    /// <summary>
    ///     Closes the open window known by its key.
    ///     If the window was modal, the DialogResult will be null.
    /// </summary>
    /// <param name="windowKey">The key of the window to close.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void Close(object windowKey);

    /// <summary>
    ///     Hides the open window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to hide.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void HideWindow(object windowKey);

    /// <summary>
    ///     Shows the window back again by its key which got hidden before.
    /// </summary>
    /// <param name="windowKey">The key of the window to show.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void ShowHiddenWindow(object windowKey);

    /// <summary>
    ///     Reads the actual position of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which position to read.</param>
    /// <returns>The window position.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    /// <returns>The window position.</returns>
    Point GetWindowPosition(object windowKey);

    /// <summary>
    ///     Moves the window to a specific position by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to move.</param>
    /// <param name="position">The new position the window has to move to.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void MoveWindow(object windowKey, Point position);

    /// <summary>
    ///     Reads the actual size of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which size to read.</param>
    /// <returns>The window size.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    Size GetWindowSize(object windowKey);

    /// <summary>
    ///     Resizes the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to resize.</param>
    /// <param name="size">The new size of the window.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void ResizeWindow(object windowKey, Size size);

    /// <summary>
    ///     Reads the actual size and position of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which size and position to read.</param>
    /// <returns>The size and position of the window.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    /// <returns>The size and position of the window.</returns>
    Rect GetWindowPositionAndSize(object windowKey);

    /// <summary>
    ///     Moves and resizes the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window to move and resize.</param>
    /// <param name="rect">The new position and size of the window.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void MoveAndResizeWindow(object windowKey, Rect rect);

    /// <summary>
    ///     Reads the actual state of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which state to read.</param>
    /// <returns>The window state.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    WindowState GetWindowState(object windowKey);

    /// <summary>
    ///     Sets the state of the window by its key.
    /// </summary>
    /// <param name="windowKey">The key of the window which state to set.</param>
    /// <param name="state">The new window state.</param>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given key.</exception>
    void SetWindowState(object windowKey, WindowState state);

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
    Task<bool> ShowControl(object hostId, object controlKey, object viewModel);

    /// <summary>
    ///     Removes a specific user control by its control key into the <see cref="NavigationPresenter" /> known by its id.
    /// </summary>
    /// <param name="hostId">The ID of the <see cref="NavigationPresenter" /> where to remove the user control from.</param>
    /// <param name="viewModel">The ViewModel of the user control to remove.</param>
    /// <returns>True if the user control got removed; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">hostId is null.</exception>
    /// <exception cref="ArgumentNullException">viewModel is null.</exception>
    /// <exception cref="InvalidOperationException">There is no navigation presenter registered with the given hostId.</exception>
    Task<bool> RemoveControl(object hostId, object viewModel);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    Task<MessageBoxResult> ShowMessageBox(string messageBoxText);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption);

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
    Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption);

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
    Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button);

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
    Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button);

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
    Task<MessageBoxResult> ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

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
    Task<MessageBoxResult> ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    Task<bool> ShowDialog(IOpenFileData openFileData);

    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<bool> ShowDialog(object ownerWindowKey, IOpenFileData openFileData);

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    Task<bool> ShowDialog(ISaveFileData saveFileData);

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<bool> ShowDialog(object ownerWindowKey, ISaveFileData saveFileData);

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    Task<bool> ShowDialog(IBrowseFolderData browseFolderData);

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<bool> ShowDialog(object ownerWindowKey, IBrowseFolderData browseFolderData);

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    Task<bool> ShowDialog(IColorPickerData colorPickerData);

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<bool> ShowDialog(object ownerWindowKey, IColorPickerData colorPickerData);

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <remarks>The NavigationService asks the <see cref="IWindowProvider" /> for the main window and uses that if not null.</remarks>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    Task<bool> ShowDialog(IFontPickerData fontPickerData);

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <param name="ownerWindowKey">The owner window key of the dialog.</param>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    /// <exception cref="InvalidOperationException">There is no open window with the given owner key.</exception>
    Task<bool> ShowDialog(object ownerWindowKey, IFontPickerData fontPickerData);

    /// <summary>
    ///     Checks if a window is registered by the given window key.
    /// </summary>
    /// <param name="windowKey">The key of the window to look for.</param>
    /// <returns>True if the window is known by its key; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">windowKey is null.</exception>
    bool IsWindowKnown(object windowKey);

    /// <summary>
    ///     Checks if a user control is registered by the given control key.
    /// </summary>
    /// <param name="controlKey">The key of the user control to look for.</param>
    /// <returns>True if the user control is known by its key; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">controlKey is null.</exception>
    bool IsControlKnown(object controlKey);

    /// <summary>
    ///     Checks if a navigation presenter is registered by the given host ID.
    /// </summary>
    /// <param name="hostId">The ID of the navigation presenter to look for.</param>
    /// <returns>True if the navigation presenter is known by its key; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">hostId is null.</exception>
    bool IsHostKnown(object hostId);
}