// -----------------------------------------------------------------------------------------------------------------
// <copyright file="OpenFileData.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <inheritdoc />
public class OpenFileData : IOpenFileData
{
    /// <summary>
    ///     Creates a new instance of OpenFileData.
    /// </summary>
    public OpenFileData()
    {
        CheckFileExists = true;
        CheckPathExists = true;
        DefaultExt = string.Empty;
        FileName = string.Empty;
        Filter = string.Empty;
        FilterIndex = 1;
        InitialDirectory = string.Empty;
        MultiSelect = false;
        Title = string.Empty;
        ValidateNames = true;
    }

    /// <inheritdoc />
    public bool CheckFileExists { get; set; }

    /// <inheritdoc />
    public bool CheckPathExists { get; set; }

    /// <inheritdoc />
    public string DefaultExt { get; set; }

    /// <inheritdoc />
    public string FileName { get; set; }

    /// <inheritdoc />
    public string[] FileNames { get; set; }

    /// <inheritdoc />
    public string SafeFileName { get; set; }

    /// <inheritdoc />
    public string[] SafeFileNames { get; set; }

    /// <inheritdoc />
    public string Filter { get; set; }

    /// <inheritdoc />
    public int FilterIndex { get; set; }

    /// <inheritdoc />
    public string InitialDirectory { get; set; }

    /// <inheritdoc />
    public bool MultiSelect { get; set; }

    /// <inheritdoc />
    public string Title { get; set; }

    /// <inheritdoc />
    public bool ValidateNames { get; set; }
}