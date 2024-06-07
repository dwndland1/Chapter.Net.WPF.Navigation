// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBoxOptions.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace Chapter.Net.WPF.Navigation.MessageBoxes;

/// <inheritdoc />
public class MessageBoxOptions : IMessageBoxOptions
{
    /// <summary>
    ///     Creates a new instance of MessageBoxOptions.
    /// </summary>
    public MessageBoxOptions()
    {
    }

    /// <summary>
    ///     Creates a new instance of MessageBoxOptions.
    /// </summary>
    /// <param name="icon">The icon to show in the message box.</param>
    public MessageBoxOptions(MessageBoxImage icon)
    {
        Icon = icon;
    }

    /// <summary>
    ///     Creates a new instance of MessageBoxOptions.
    /// </summary>
    /// <param name="defaultResult">The default result of the message box.</param>
    public MessageBoxOptions(MessageBoxResult defaultResult)
    {
        DefaultResult = defaultResult;
    }

    /// <summary>
    ///     Creates a new instance of MessageBoxOptions.
    /// </summary>
    /// <param name="icon">The icon to show in the message box.</param>
    /// <param name="defaultResult">The default result of the message box.</param>
    public MessageBoxOptions(MessageBoxImage icon, MessageBoxResult defaultResult)
    {
        Icon = icon;
        DefaultResult = defaultResult;
    }

    /// <inheritdoc />
    public MessageBoxImage? Icon { get; set; }

    /// <inheritdoc />
    public MessageBoxResult? DefaultResult { get; set; }
}