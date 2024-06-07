// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SingleNavigationPresenter.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Represents a host where a single user control will be placed by the <see cref="NavigationService" />.
/// </summary>
public class SingleNavigationPresenter : NavigationPresenter
{
    internal static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(SingleNavigationPresenter), new PropertyMetadata(null));

    static SingleNavigationPresenter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SingleNavigationPresenter), new FrameworkPropertyMetadata(typeof(SingleNavigationPresenter)));
    }

    internal object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <inheritdoc />
    public override async Task<bool> CanSetContent()
    {
        var dataContext = (Content as FrameworkElement)?.DataContext;
        if (dataContext is IEditable asyncEditable)
            return await asyncEditable.TryLeave();
        return true;
    }

    /// <inheritdoc />
    public override void SetContent(FrameworkElement control)
    {
        if (!EnableUIPersistence && DisposeViewModel && Content is FrameworkElement { DataContext: IDisposable disposable })
            disposable.Dispose();

        Content = control;
    }

    /// <inheritdoc />
    public override bool ClearContent(object viewModel)
    {
        if ((Content as FrameworkElement)?.DataContext == viewModel)
        {
            SetContent(null);
            return true;
        }

        return false;
    }
}