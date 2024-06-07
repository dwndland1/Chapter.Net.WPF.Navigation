// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ILoader.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Provides an async load method for a ViewModel which gets called directly after a window or user control is
///     displayed by the <see cref="NavigationService" />.
/// </summary>
public interface ILoader
{
    /// <summary>
    ///     Loads the data in the ViewModel async as soon the window or user control is displayed.
    /// </summary>
    /// <returns>The task to await.</returns>
    Task Load();
}