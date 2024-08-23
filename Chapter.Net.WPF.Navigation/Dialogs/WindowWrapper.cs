// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WindowWrapper.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Interop;
using IWin32Window = System.Windows.Forms.IWin32Window;

namespace Chapter.Net.WPF.Navigation.Dialogs;

internal class WindowWrapper : IWin32Window
{
    public WindowWrapper(Window window)
    {
        Handle = new WindowInteropHelper(window).Handle;
    }

    public IntPtr Handle { get; }
}