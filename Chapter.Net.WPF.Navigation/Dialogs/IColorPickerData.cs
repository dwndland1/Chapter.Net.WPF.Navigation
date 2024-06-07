// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IColorPickerData.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Drawing;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <summary>
///     Represents a common dialog box that displays available colors along with controls that enable the user to define
///     custom colors. See <see cref="System.Windows.Forms.ColorDialog" />.
/// </summary>
public interface IColorPickerData
{
    /// <summary>
    ///     Gets or sets a value indicating whether the user can use the dialog box to define custom colors.
    /// </summary>
    /// <returns>true if the user can define custom colors; otherwise, false.</returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool AllowFullOpen { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box displays all available colors in the set of basic colors.
    /// </summary>
    /// <returns>
    ///     true if the dialog box displays all available colors in the set of basic colors; otherwise, false.
    /// </returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool AnyColor { get; set; }

    /// <summary>
    ///     Gets or sets the color selected by the user.
    /// </summary>
    /// <returns>The color selected by the user.</returns>
    /// <value>Default: Color.Black.</value>
    [DefaultValue(typeof(Color), "Black")]
    Color Color { get; set; }

    /// <summary>
    ///     Gets or sets the set of custom colors shown in the dialog box.
    /// </summary>
    /// <returns>A set of custom colors shown by the dialog box.</returns>
    /// <value>Default: Color.Black.</value>
    [DefaultValue(null)]
    int[] CustomColors { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the controls used to create custom colors are visible when the dialog box
    ///     is opened.
    /// </summary>
    /// <returns>
    ///     true if the custom color controls are available when the dialog box is opened; otherwise, false.
    /// </returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool FullOpen { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box will restrict users to selecting solid colors only.
    /// </summary>
    /// <returns>true if users can select only solid colors; otherwise, false..</returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool SolidColorOnly { get; set; }
}