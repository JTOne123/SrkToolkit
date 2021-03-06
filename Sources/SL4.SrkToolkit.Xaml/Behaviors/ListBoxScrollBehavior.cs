﻿// 
// Copyright 2014 SandRock
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

namespace SrkToolkit.Xaml.Behaviors
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    /// This behaviors helps detect a <see cref="ListBox"/> is scrolled to the bottom by executing a <see cref="ICommand"/>.
    /// </summary>
    public class ListBoxScrollBehavior : Behavior<ListBox>
    {
        /// <summary>
        /// The <see cref="ScrollViewer"/> found within the ListBox.
        /// </summary>
        private ScrollViewer scrollViewer;

        #region Attached property ReachedBottomCommand

        /// <summary>
        /// Gets the "reached bottom" command.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>the command</returns>
        public static ICommand GetReachedBottomCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ReachedBottomCommandProperty);
        }

        /// <summary>
        /// Sets the "reached bottom" command.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetReachedBottomCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ReachedBottomCommandProperty, value);
        }

        /// <summary>
        /// The ReachedBottomCommand attached property.
        /// </summary>
        public static readonly DependencyProperty ReachedBottomCommandProperty =
            DependencyProperty.RegisterAttached("ReachedBottomCommand", typeof(ICommand), typeof(ListBoxScrollBehavior), new PropertyMetadata(null));

        #endregion

        #region Attached property ReachedBottomCommandParameter

        /// <summary>
        /// Gets the reached bottom command parameter.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static object GetReachedBottomCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(ReachedBottomCommandParameterProperty);
        }

        /// <summary>
        /// Sets the reached bottom command parameter.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetReachedBottomCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ReachedBottomCommandParameterProperty, value);
        }

        /// <summary>
        /// The ReachedBottomCommandParameter attached property.
        /// </summary>
        public static readonly DependencyProperty ReachedBottomCommandParameterProperty =
            DependencyProperty.RegisterAttached("ReachedBottomCommandParameter", typeof(object), typeof(ListBoxScrollBehavior), new PropertyMetadata(null));

        #endregion

        #region Dependency property: StickToBottom

        public static bool GetStickToBottom(DependencyObject obj)
        {
            return (bool)obj.GetValue(StickToBottomProperty);
        }

        public static void SetStickToBottom(DependencyObject obj, bool value)
        {
            obj.SetValue(StickToBottomProperty, value);
        }

        public static readonly DependencyProperty StickToBottomProperty =
            DependencyProperty.RegisterAttached("StickToBottom", typeof(bool), typeof(ListBoxScrollBehavior), new PropertyMetadata(false));

        #endregion

        #region Attached property ListBoxScrollBehavior

        public static ListBoxScrollBehavior GetListBoxScrollBehavior(DependencyObject obj)
        {
            return (ListBoxScrollBehavior)obj.GetValue(ListBoxScrollBehaviorProperty);
        }

        public static void SetListBoxScrollBehavior(DependencyObject obj, ListBoxScrollBehavior value)
        {
            obj.SetValue(ListBoxScrollBehaviorProperty, value);
        }

        /// <summary>
        /// The ListBoxScrollBehavior attached property.
        /// </summary>
        public static readonly DependencyProperty ListBoxScrollBehaviorProperty =
            DependencyProperty.RegisterAttached("ListBoxScrollBehavior", typeof(ListBoxScrollBehavior), typeof(ListBoxScrollBehavior), new PropertyMetadata(null));

        #endregion

        #region Attached property ScrollViewerVerticalOffset on ListBox

        /// <summary>
        /// Gets the scroll viewer vertical offset.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns></returns>
        public static double GetScrollViewerVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(ScrollViewerVerticalOffsetProperty);
        }

        /// <summary>
        /// Sets the scroll viewer vertical offset.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetScrollViewerVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(ScrollViewerVerticalOffsetProperty, value);
        }

        /// <summary>
        /// The ScrollViewerVerticalOffset attached property.
        /// </summary>
        public static readonly DependencyProperty ScrollViewerVerticalOffsetProperty =
            DependencyProperty.RegisterAttached("ScrollViewerVerticalOffset", typeof(double), typeof(ListBox), new PropertyMetadata(double.NaN, OnVerticalOffsetPropertyChanged));

        /// <summary>
        /// Called when the scrollviewer's vertical offset property changed.
        /// </summary>
        /// <param name="d">The listbox.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnVerticalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listbox = (ListBox)d;
            var behavior = listbox.GetValue(ListBoxScrollBehaviorProperty) as ListBoxScrollBehavior;
            behavior.OnVerticalOffsetChanged();
        }

        #endregion

        #region Attached property ScrollViewerScrollableHeightProperty on ListBox

        public static double GetScrollViewerScrollableHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ScrollViewerScrollableHeightProperty);
        }

        public static void SetScrollViewerScrollableHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ScrollViewerScrollableHeightProperty, value);
        }

        public static readonly DependencyProperty ScrollViewerScrollableHeightProperty =
            DependencyProperty.RegisterAttached("ScrollViewerScrollableHeight", typeof(double), typeof(ListBox), new PropertyMetadata(double.NaN, OnScrollableHeightPropertyChanged));

        private static void OnScrollableHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listbox = (ListBox)d;
            var behavior = listbox.GetValue(ListBoxScrollBehaviorProperty) as ListBoxScrollBehavior;
            behavior.OnScrollableHeightChanged();
        }

        #endregion

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            if (this.AssociatedObject == null)
            {
                throw new InvalidOperationException("Could not find associated ListBox");
            }

            this.AssociatedObject.SetValue(ListBoxScrollBehaviorProperty, this);
            this.AssociatedObject.Loaded += this.OnListboxLoaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.Loaded -= this.OnListboxLoaded;
                this.AssociatedObject.ClearValue(ListBoxScrollBehaviorProperty);
            }

            this.scrollViewer = null;
        }

        private void OnListboxLoaded(object sender, RoutedEventArgs e)
        {
            this.TryAttach();
        }

        public void TryAttach()
        {
            if (this.scrollViewer != null)
                return;

            this.scrollViewer = this.AssociatedObject.GetChildrenRecursive<ScrollViewer>().FirstOrDefault();
            if (this.scrollViewer != null)
            {
                this.AssociatedObject.Loaded -= this.OnListboxLoaded;

                {
                    var binding = new Binding();
                    binding.Source = this.scrollViewer;
                    binding.Path = new PropertyPath("VerticalOffset", new object[0]);
                    binding.Mode = BindingMode.OneWay;
                    this.AssociatedObject.SetBinding(ScrollViewerVerticalOffsetProperty, binding);
                }

                {
                    var binding = new Binding();
                    binding.Source = this.scrollViewer;
                    binding.Path = new PropertyPath("ScrollableHeight", new object[0]);
                    binding.Mode = BindingMode.OneWay;
                    this.AssociatedObject.SetBinding(ScrollViewerScrollableHeightProperty, binding);
                }
            }
            else
            {
                Trace.TraceWarning("ListBoxScrollBehavior: Could not find ScrollViever in associated ListBox");
                //throw new InvalidOperationException("Could not find ScrollViever in associated ListBox");
                return;
            }
        }

        /// <summary>
        /// Called when the scrollviewer's vertical offset change.
        /// </summary>
        private void OnVerticalOffsetChanged()
        {
            // scrollable test
            if (this.scrollViewer.ScrollableHeight > 0)
            {
                // end of scrollviewer test
                // when using a virtualized stackpanel, VerticalOffset never gets greater than ScrollableHeight
                // this is why I use an arbitrary margin
                double margin = this.scrollViewer.ScrollableHeight / 20D;
                if (this.scrollViewer.VerticalOffset >= this.scrollViewer.ScrollableHeight
                 || this.scrollViewer.VerticalOffset > margin
                 && (this.scrollViewer.VerticalOffset + margin) > this.scrollViewer.ScrollableHeight)
                {
                    // command not-null test
                    var command = GetReachedBottomCommand(this);
                    if (command != null)
                    {
                        // command invocation
                        command.Execute(GetReachedBottomCommandParameter(this.AssociatedObject));
                    }
                }
            }
        }

        private void OnScrollableHeightChanged()
        {
            if (GetStickToBottom(this.AssociatedObject))
            {
                double diff = this.scrollViewer.ScrollableHeight - this.scrollViewer.VerticalOffset;
                if (Math.Abs(diff) > 1D)
                {
                    this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.ScrollableHeight);
                }
            }
        }
    }
}
