// -----------------------------------------------------------------------------------------------------------------
// <copyright file="StackingStrategy.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.WPF.Navigation.Presenters;

/// <summary>
///     Defines the strategy for the <see cref="StackedNavigationPresenter" />.
/// </summary>
public enum StackingStrategy
{
    /// <summary>
    ///     The contents shall be overlapped.
    /// </summary>
    Overlapping,

    /// <summary>
    ///     The contents shall be arranged vertically.
    /// </summary>
    Vertical,

    /// <summary>
    ///     The contents shall be arranged horizontally.
    /// </summary>
    Horizontal
}