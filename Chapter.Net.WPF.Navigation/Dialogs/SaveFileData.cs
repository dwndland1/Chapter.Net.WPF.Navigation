// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SaveFileData.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <inheritdoc />
public class SaveFileData : ISaveFileData
{
    /// <summary>
    ///     Creates a new instance of SaveFileData.
    /// </summary>
    public SaveFileData()
    {
        CheckFileExists = false;
        CheckPathExists = true;
        CreatePrompt = false;
        DefaultExt = string.Empty;
        FileName = string.Empty;
        Filter = string.Empty;
        FilterIndex = 1;
        InitialDirectory = string.Empty;
        OverwritePrompt = true;
        Title = string.Empty;
        ValidateNames = true;
    }

    /// <inheritdoc />
    public bool CheckFileExists { get; set; }

    /// <inheritdoc />
    public bool CheckPathExists { get; set; }

    /// <inheritdoc />
    public bool CreatePrompt { get; set; }

    /// <inheritdoc />
    public string DefaultExt { get; set; }

    /// <inheritdoc />
    public string FileName { get; set; }

    /// <inheritdoc />
    public string[] FileNames { get; set; }

    /// <inheritdoc />
    public string Filter { get; set; }

    /// <inheritdoc />
    public int FilterIndex { get; set; }

    /// <inheritdoc />
    public string InitialDirectory { get; set; }

    /// <inheritdoc />
    public bool OverwritePrompt { get; set; }

    /// <inheritdoc />
    public string Title { get; set; }

    /// <inheritdoc />
    public bool ValidateNames { get; set; }
}