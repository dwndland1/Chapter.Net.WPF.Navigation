// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IFontPickerData.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <summary>
///     Prompts the user to choose a font from among those installed on the local computer. See
///     <see cref="System.Windows.Forms.FontDialog" />.
/// </summary>
public interface IFontPickerData
{
    /// <summary>
    ///     Gets or sets a value indicating whether the user can change the character set specified in the Script combo box to
    ///     display a character set other than the one currently displayed.
    /// </summary>
    /// <returns>
    ///     true if the user can change the character set specified in the Script combo box; otherwise, false.
    /// </returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool AllowScriptChange { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box allows graphics device interface (GDI) font simulations.
    /// </summary>
    /// <returns>true if font simulations are allowed; otherwise, false.</returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool AllowSimulations { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box allows vector font selections.
    /// </summary>
    /// <returns>true if vector fonts are allowed; otherwise, false.</returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool AllowVectorFonts { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box displays both vertical and horizontal fonts or only
    ///     horizontal fonts.
    /// </summary>
    /// <returns>true if both vertical and horizontal fonts are allowed; otherwise, false.</returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool AllowVerticalFonts { get; set; }

    /// <summary>
    ///     Gets or sets the selected font color.
    /// </summary>
    /// <returns>The color of the selected font.</returns>
    /// <value>Default: Color.Black.</value>
    [DefaultValue(typeof(Color), "Black")]
    Color Color { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box allows only the selection of fixed-pitch fonts.
    /// </summary>
    /// <returns>true if only fixed-pitch fonts can be selected; otherwise, false.</returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool FixedPitchOnly { get; set; }

    /// <summary>
    ///     Gets or sets the selected font.
    /// </summary>
    /// <returns>The selected font.</returns>
    /// <value>Default: undefined.</value>
    Font Font { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box specifies an error condition if the user attempts to select
    ///     a font or style that does not exist.
    /// </summary>
    /// <returns>
    ///     true if the dialog box specifies an error condition when the user tries to select a font or style that does
    ///     not exist; otherwise, false.
    /// </returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool FontMustExist { get; set; }

    /// <summary>
    ///     Gets or sets the maximum point size a user can select.
    /// </summary>
    /// <returns>The maximum point size a user can select.</returns>
    /// <value>Default: 0.</value>
    [DefaultValue(0)]
    int MaxSize { get; set; }

    /// <summary>
    ///     Gets or sets the minimum point size a user can select.
    /// </summary>
    /// <returns>The minimum point size a user can select.</returns>
    /// <value>Default: 0.</value>
    [DefaultValue(0)]
    int MinSize { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box allows selection of fonts for all non-OEM and Symbol
    ///     character sets, as well as the ANSI character set.
    /// </summary>
    /// <returns>
    ///     true if selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set, is
    ///     allowed; otherwise, false.
    /// </returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool ScriptsOnly { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box contains an Apply button.
    /// </summary>
    /// <returns>true if the dialog box contains an Apply button; otherwise, false.</returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool ShowApply { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box displays the color choice.
    /// </summary>
    /// <returns>true if the dialog box displays the color choice; otherwise, false.</returns>
    /// <value>Default: false.</value>
    [DefaultValue(false)]
    bool ShowColor { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the dialog box contains controls that allow the user to specify
    ///     strikethrough, underline, and text color options.
    /// </summary>
    /// <returns>
    ///     true if the dialog box contains controls to set strikethrough, underline, and text color options; otherwise,
    ///     false.
    /// </returns>
    /// <value>Default: true.</value>
    [DefaultValue(true)]
    bool ShowEffects { get; set; }

    /// <summary>
    ///     Occurs when the user clicks the Apply button in the font dialog box.
    /// </summary>
    event EventHandler Apply;

    /// <summary>
    ///     Raises the <see cref="Apply" /> event.
    /// </summary>
    /// <param name="sender">The sender dialog.</param>
    /// <param name="e">Empty.</param>
    void OnApply(object sender, EventArgs e);
}