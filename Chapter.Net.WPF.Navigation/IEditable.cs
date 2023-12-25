﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IEditable.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Represents a ViewModel which can block the navigation with pending changes, input validations and other.
/// </summary>
public interface IEditable
{
    /// <summary>
    ///     The callback called if the user wants to navigate to another user control or window.
    /// </summary>
    /// <returns>True if the page is save to leave; otherwise False.</returns>
    Task<bool> TryLeave();
}