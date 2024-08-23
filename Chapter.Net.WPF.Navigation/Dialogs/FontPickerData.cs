// -----------------------------------------------------------------------------------------------------------------
// <copyright file="FontPickerData.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <inheritdoc />
public class FontPickerData : IFontPickerData
{
    /// <summary>
    ///     Creates a new instance of FontPickerData.
    /// </summary>
    public FontPickerData()
    {
        AllowScriptChange = true;
        AllowSimulations = true;
        AllowVectorFonts = true;
        AllowVerticalFonts = true;
        Color = Color.Black;
        FixedPitchOnly = false;
        Font = null;
        FontMustExist = false;
        MaxSize = 0;
        MinSize = 0;
        ScriptsOnly = false;
        ShowApply = false;
        ShowColor = false;
        ShowEffects = true;
    }

    /// <inheritdoc />
    public bool AllowScriptChange { get; set; }

    /// <inheritdoc />
    public bool AllowSimulations { get; set; }

    /// <inheritdoc />
    public bool AllowVectorFonts { get; set; }

    /// <inheritdoc />
    public bool AllowVerticalFonts { get; set; }

    /// <inheritdoc />
    public Color Color { get; set; }

    /// <inheritdoc />
    public bool FixedPitchOnly { get; set; }

    /// <inheritdoc />
    public Font Font { get; set; }

    /// <inheritdoc />
    public bool FontMustExist { get; set; }

    /// <inheritdoc />
    public int MaxSize { get; set; }

    /// <inheritdoc />
    public int MinSize { get; set; }

    /// <inheritdoc />
    public bool ScriptsOnly { get; set; }

    /// <inheritdoc />
    public bool ShowApply { get; set; }

    /// <inheritdoc />
    public bool ShowColor { get; set; }

    /// <inheritdoc />
    public bool ShowEffects { get; set; }

    /// <inheritdoc />
    public event EventHandler Apply;

    /// <inheritdoc />
    public void OnApply(object sender, EventArgs e)
    {
        Apply?.Invoke(sender, e);
    }
}