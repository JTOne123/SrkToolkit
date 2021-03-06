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

namespace System.Windows
{
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// Extension methods for <see cref="DependencyObject"/>s.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Gets the visual children of a <see cref="DependencyObject"/> with the specified type.
        /// Stops recursion on object found.
        /// </summary>
        /// <typeparam name="T">the type of object to find</typeparam>
        /// <param name="control">The control.</param>
        /// <param name="stopRecursionOnObjectFound">if set to <c>true</c> stops recursion on object found; otherwise <b>false</b>.</param>
        /// <returns>
        /// a collection of objects of the specified type
        /// </returns>
        public static IEnumerable<T> GetChildrenRecursive<T>(this DependencyObject control, bool stopRecursionOnObjectFound = true)
            where T : DependencyObject
        {
            // for each child
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                var child = VisualTreeHelper.GetChild(control, i);

                // child found?
                if (child is T)
                {
                    yield return (T)child;

                    // stop recursion on found?
                    if (stopRecursionOnObjectFound)
                        break;
                }

                // handle children of child
                foreach (var item in child.GetChildrenRecursive<T>())
                {
                    yield return item;
                }
            }
        }

        public static T GetParent<T>(this DependencyObject control)
            where T : DependencyObject
        {
            DependencyObject parent = control;
            while ((parent = VisualTreeHelper.GetParent(parent)) != null)
            {
                if (parent is T)
                    return (T)parent;
            }

            return null;
        }
    }
}
