﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IDialogProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <summary>
///     Provides way how to show system dialogs for files and folders.
/// </summary>
public interface IDialogProvider
{
    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    Task<bool> Show(IOpenFileData openFileData);

    /// <summary>
    ///     Shows the open file dialog.
    /// </summary>
    /// <param name="owner">The owner window of the dialog.</param>
    /// <param name="openFileData">The open file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">openFileData is null.</exception>
    Task<bool> Show(Window owner, IOpenFileData openFileData);

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    Task<bool> Show(ISaveFileData saveFileData);

    /// <summary>
    ///     Shows the save file dialog.
    /// </summary>
    /// <param name="owner">The owner window of the dialog.</param>
    /// <param name="saveFileData">The save file dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
    Task<bool> Show(Window owner, ISaveFileData saveFileData);

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    Task<bool> Show(IBrowseFolderData browseFolderData);

    /// <summary>
    ///     Shows the browse folder dialog.
    /// </summary>
    /// <param name="owner">The owner window of the dialog.</param>
    /// <param name="browseFolderData">The browse folder dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
    Task<bool> Show(Window owner, IBrowseFolderData browseFolderData);

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    Task<bool> Show(IColorPickerData colorPickerData);

    /// <summary>
    ///     Shows the color picker dialog.
    /// </summary>
    /// <param name="owner">The owner window of the dialog.</param>
    /// <param name="colorPickerData">The color picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
    Task<bool> Show(Window owner, IColorPickerData colorPickerData);

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    Task<bool> Show(IFontPickerData fontPickerData);

    /// <summary>
    ///     Shows the font picker dialog.
    /// </summary>
    /// <param name="owner">The owner window of the dialog.</param>
    /// <param name="fontPickerData">The font picker dialog data.</param>
    /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">owner is null.</exception>
    /// <exception cref="ArgumentNullException">fontPickerData is null.</exception>
    Task<bool> Show(Window owner, IFontPickerData fontPickerData);
}