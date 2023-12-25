// -----------------------------------------------------------------------------------------------------------------
// <copyright file="StackedNavigationPresenter.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chapter.Net.WPF.Navigation;

/// <summary>
///     Represents a host where multiple user controls can be displayed.
/// </summary>
public class StackedNavigationPresenter : NavigationPresenter
{
    /// <summary>
    ///     The DependencyProperty for the Strategy property.
    /// </summary>
    public static readonly DependencyProperty StrategyProperty =
        DependencyProperty.Register(nameof(Strategy), typeof(StackingStrategy), typeof(StackedNavigationPresenter), new PropertyMetadata(StackingStrategy.Overlapping));

    /// <summary>
    ///     The DependencyProperty for the HorizontalOffset property.
    /// </summary>
    public static readonly DependencyProperty HorizontalOffsetProperty =
        DependencyProperty.Register(nameof(HorizontalOffset), typeof(double), typeof(StackedNavigationPresenter), new PropertyMetadata(0d));

    /// <summary>
    ///     The DependencyProperty for the VerticalOffset property.
    /// </summary>
    public static readonly DependencyProperty VerticalOffsetProperty =
        DependencyProperty.Register(nameof(VerticalOffset), typeof(double), typeof(StackedNavigationPresenter), new PropertyMetadata(0d));

    /// <summary>
    ///     The DependencyProperty for the IsReverted property.
    /// </summary>
    public static readonly DependencyProperty IsRevertedProperty =
        DependencyProperty.Register(nameof(IsReverted), typeof(bool), typeof(StackedNavigationPresenter), new PropertyMetadata(false));

    private ItemsControl _items;

    static StackedNavigationPresenter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedNavigationPresenter), new FrameworkPropertyMetadata(typeof(StackedNavigationPresenter)));
    }

    /// <summary>
    ///     Gets or sets the strategy how multiple items shall be arranged.
    /// </summary>
    public StackingStrategy Strategy
    {
        get => (StackingStrategy)GetValue(StrategyProperty);
        set => SetValue(StrategyProperty, value);
    }

    /// <summary>
    ///     Gets or sets the horizontal offset. Left or right depending on the <see cref="IsReverted" /> property.
    /// </summary>
    public double HorizontalOffset
    {
        get => (double)GetValue(HorizontalOffsetProperty);
        set => SetValue(HorizontalOffsetProperty, value);
    }

    /// <summary>
    ///     Gets or sets the horizontal offset. Top or down depending on the <see cref="IsReverted" /> property.
    /// </summary>
    public double VerticalOffset
    {
        get => (double)GetValue(VerticalOffsetProperty);
        set => SetValue(VerticalOffsetProperty, value);
    }

    /// <summary>
    ///     Gets or set if the later items shall be added on the end or at the top. That also modifies the usage of the offset
    ///     properties.
    /// </summary>
    public bool IsReverted
    {
        get => (bool)GetValue(IsRevertedProperty);
        set => SetValue(IsRevertedProperty, value);
    }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        _items = (ItemsControl)GetTemplateChild("PART_Items");
    }

    /// <inheritdoc />
    public override Task<bool> CanSetContent()
    {
        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public override void SetContent(FrameworkElement control)
    {
        var multiply = 1;
        if (Strategy == StackingStrategy.Overlapping)
            multiply = _items.Items.Count;

        if (IsReverted)
        {
            control.Margin = new Thickness(0, 0, HorizontalOffset * multiply, VerticalOffset * multiply);
            _items.Items.Insert(0, control);
        }
        else
        {
            control.Margin = new Thickness(HorizontalOffset * multiply, VerticalOffset * multiply, 0, 0);
            _items.Items.Add(control);
        }
    }

    /// <inheritdoc />
    public override bool ClearContent(object viewModel)
    {
        var item = _items.Items.Cast<FrameworkElement>().FirstOrDefault(x => x.DataContext == viewModel);
        if (item != null && !EnableUIPersistence && DisposeViewModel && viewModel is IDisposable disposable)
            disposable.Dispose();
        if (item != null)
        {
            _items.Items.Remove(item);
            if (Strategy == StackingStrategy.Overlapping)
            {
                var multiply = 0;
                foreach (FrameworkElement itemLeft in _items.Items)
                    itemLeft.Margin = IsReverted ? new Thickness(0, 0, HorizontalOffset * multiply, VerticalOffset * multiply++) : new Thickness(HorizontalOffset * multiply, VerticalOffset * multiply++, 0, 0);
            }

            return true;
        }

        return false;
    }
}