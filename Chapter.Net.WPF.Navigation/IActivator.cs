// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IActivator.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Provides an async activate method for a ViewModel which can get called when switch between ViewModels without
///     disposing them before. For example custom tab controls.
/// </summary>
public interface IActivator
{
    /// <summary>
    ///     Refreshes the data in the ViewModel async as soon the ViewModel got visible again.
    /// </summary>
    /// <returns>The task to await.</returns>
    Task Activate();
}