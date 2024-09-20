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
        ArgumentNullException.ThrowIfNull(messageBoxText);

        var result = MessageBox.Show(messageBoxText);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(messageBoxText);

        var result = MessageBox.Show(owner, messageBoxText);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText, string caption)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var result = MessageBox.Show(messageBoxText, caption);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var result = MessageBox.Show(owner, messageBoxText, caption);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var result = MessageBox.Show(messageBoxText, caption, button);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);

        var result = MessageBox.Show(owner, messageBoxText, caption, button);
        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
    {
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);
        ArgumentNullException.ThrowIfNull(options);

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
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(messageBoxText);
        ArgumentNullException.ThrowIfNull(caption);
        ArgumentNullException.ThrowIfNull(options);

        if (options.Icon != null && options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value));
        if (options.Icon != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value));
        if (options.DefaultResult != null)
            return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value));
        return Task.FromResult(MessageBox.Show(owner, messageBoxText, caption, button));
    }
}