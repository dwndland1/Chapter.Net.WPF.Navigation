// -----------------------------------------------------------------------------------------------------------------
// <copyright file="BrowseFolderData.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <inheritdoc />
public class BrowseFolderData : IBrowseFolderData
{
    /// <summary>
    ///     Creates a new instance of BrowseFolderData.
    /// </summary>
    public BrowseFolderData()
    {
        Description = string.Empty;
        RootFolder = Environment.SpecialFolder.Desktop;
        SelectedPath = string.Empty;
        ShowNewFolderButton = true;
        Data = null;
    }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public Environment.SpecialFolder? RootFolder { get; set; }

    /// <inheritdoc />
    public string SelectedPath { get; set; }

    /// <inheritdoc />
    public bool ShowNewFolderButton { get; set; }

    /// <inheritdoc />
    public object Data { get; set; }
}