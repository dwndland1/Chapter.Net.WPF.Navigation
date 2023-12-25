﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBoxOptions.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace Chapter.Net.WPF.Navigation.MessageBoxes;

/// <summary>
///     Provides additional data for the message box.
/// </summary>
public interface IMessageBoxOptions
{
    /// <summary>
    ///     [Optional] The icon to show in the message box.
    /// </summary>
    MessageBoxImage? Icon { get; set; }

    /// <summary>
    ///     [Optional] The default result of the message box.
    /// </summary>
    MessageBoxResult? DefaultResult { get; set; }
}