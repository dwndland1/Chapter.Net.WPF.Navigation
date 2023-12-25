// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBoxProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation.MessageBoxes;

/// <summary>
///     Provides possibilities to show message boxes.
/// </summary>
public sealed class MessageBoxProvider : IMessageBoxProvider
{
    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    public Task<MessageBoxResult> Show(string messageBoxText)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var result = MessageBox.Show(messageBoxText);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var result = MessageBox.Show(owner, messageBoxText);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    public Task<MessageBoxResult> Show(string messageBoxText, string caption)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(messageBoxText, caption);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(owner, messageBoxText, caption);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(messageBoxText, caption, button);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(owner, messageBoxText, caption, button);
        return Task.FromResult(result);
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="ArgumentNullException">options is null.</exception>
    public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        if (options.Icon != null && options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value));
        if (options.Icon != null)
            return Task.FromResult(MessageBox.Show(messageBoxText, caption, button, options.Icon.Value));
        if (options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value));
        return Task.FromResult(MessageBox.Show(messageBoxText, caption, button));
    }

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
    /// <exception cref="ArgumentNullException">caption is null.</exception>
    /// <exception cref="ArgumentNullException">options is null.</exception>
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        if (options.Icon != null && options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value));
        if (options.Icon != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value));
        if (options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value));
        return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button));
    }
}