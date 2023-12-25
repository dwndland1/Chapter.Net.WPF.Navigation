﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBoxProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation.MessageBoxes;

/// <summary>
///     Provides possibilities to show message boxes.
/// </summary>
public interface IMessageBoxProvider
{
    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(string messageBoxText);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(Window owner, string messageBoxText);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(string messageBoxText, string caption);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption, MessageBoxButton button);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

    /// <summary>
    ///     Shows the message box.
    /// </summary>
    /// <param name="owner">The owner window of the message box.</param>
    /// <param name="messageBoxText">The text show in the message box.</param>
    /// <param name="caption">The message box caption.</param>
    /// <param name="button">The buttons show in the message box.</param>
    /// <param name="options">The additional options for the message box.</param>
    /// <returns>The result of the message box after closing.</returns>
    Task<MessageBoxResult> Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);
}