// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ISaveFileData.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace Chapter.Net.WPF.Navigation.Dialogs
{
    /// <summary>
    ///     Provides the data to be used for the save file dialog.
    /// </summary>
    public interface ISaveFileData
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that
        ///     does not exist.
        /// </summary>
        /// <value>Default: false.</value>
        [DefaultValue(false)]
        bool CheckFileExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does
        ///     not exist.
        /// </summary>
        /// <value>Default: true.</value>
        [DefaultValue(true)]
        bool CheckPathExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user
        ///     specifies a file that does not exist.
        /// </summary>
        /// <value>Default: false.</value>
        [DefaultValue(false)]
        bool CreatePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the default file extension.
        /// </summary>
        /// <value>Default: "".</value>
        [DefaultValue("")]
        string DefaultExt { get; set; }

        /// <summary>
        ///     Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        /// <value>Default: null.</value>
        [DefaultValue(null)]
        string FileName { get; set; }

        /// <summary>
        ///     Gets the file names of all selected files in the dialog box.
        /// </summary>
        /// <value>Default: null.</value>
        [DefaultValue(null)]
        string[] FileNames { get; set; }

        /// <summary>
        ///     Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file
        ///     type" or "Files of type" box in the dialog box.
        /// </summary>
        /// <value>Default: "".</value>
        [DefaultValue("")]
        string Filter { get; set; }

        /// <summary>
        ///     Gets or sets the index of the filter currently selected in the file dialog box.
        /// </summary>
        /// <value>Default: 1.</value>
        [DefaultValue(1)]
        int FilterIndex { get; set; }

        /// <summary>
        ///     Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        /// <value>Default: "".</value>
        [DefaultValue("")]
        string InitialDirectory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name
        ///     that already exists.
        /// </summary>
        /// <value>Default: true.</value>
        [DefaultValue(true)]
        bool OverwritePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the file dialog box title.
        /// </summary>
        /// <value>Default: "".</value>
        [DefaultValue("")]
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        /// </summary>
        /// <value>Default: true.</value>
        [DefaultValue(true)]
        bool ValidateNames { get; set; }
    }
}