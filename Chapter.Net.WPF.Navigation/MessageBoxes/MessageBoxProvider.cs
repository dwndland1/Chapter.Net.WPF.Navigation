// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBoxProvider.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation.MessageBoxes;

/// <inheritdoc />
public sealed class MessageBoxProvider : IMessageBoxProvider
{
    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var result = MessageBox.Show(messageBoxText);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText)
    {
        if (owner == null)
            throw new ArgumentNullException(nameof(owner));
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));

        var result = MessageBox.Show(owner, messageBoxText);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText, string caption)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(messageBoxText, caption);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button)
    {
        if (messageBoxText == null)
            throw new ArgumentNullException(nameof(messageBoxText));
        if (caption == null)
            throw new ArgumentNullException(nameof(caption));

        var result = MessageBox.Show(messageBoxText, caption, button);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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