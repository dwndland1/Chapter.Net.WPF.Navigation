// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPickerData.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Drawing;

namespace Chapter.Net.WPF.Navigation.Dialogs;

/// <inheritdoc />
public class ColorPickerData : IColorPickerData
{
    /// <summary>
    ///     Creates a new instance of ColorPickerData.
    /// </summary>
    public ColorPickerData()
    {
        AllowFullOpen = true;
        AnyColor = false;
        Color = Color.Black;
        CustomColors = null;
        FullOpen = false;
        SolidColorOnly = false;
    }

    /// <inheritdoc />
    public bool AllowFullOpen { get; set; }

    /// <inheritdoc />
    public bool AnyColor { get; set; }

    /// <inheritdoc />
    public Color Color { get; set; }

    /// <inheritdoc />
    public int[] CustomColors { get; set; }

    /// <inheritdoc />
    public bool FullOpen { get; set; }

    /// <inheritdoc />
    public bool SolidColorOnly { get; set; }
}