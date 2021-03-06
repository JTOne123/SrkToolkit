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

namespace SrkToolkit.Mvvm
{
    using System;
    using SrkToolkit.Mvvm.Tools;

    /// <summary>
    /// Higher-level ViewModel base with tasks and MessageBox abstraction.
    /// </summary>
    public partial class InteractionViewModelBase : ViewModelBase
    {
        private readonly BusyTaskCollection tasks = new BusyTaskCollection();
        private InteractionViewModelBase baseViewModel;
#if SILVERLIGHT || WPF
        private IMessageBoxService mbox;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionViewModelBase"/> class.
        /// Make sure you instantiate this in the UI thread so that the dispatcher can attach.
        /// </summary>
        public InteractionViewModelBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionViewModelBase"/> class.
        /// Make sure you instantiate this in the UI thread so that the dispatcher can attach.
        /// </summary>
        public InteractionViewModelBase(InteractionViewModelBase interactionViewModelBase)
            : base()
        {
            this.baseViewModel = interactionViewModelBase;
        }

        /// <summary>
        /// This collection contains tasks that are being processed.
        /// Nice properties are Tasks.IsBusy and Tasks.IsProcessing.
        /// Access tasks from the view with 
        ///   - {Binding Tasks[AutoLogin].IsProcessing}
        ///   - {Binding Tasks[AutoLogin].Message}
        /// </summary>
        public BusyTaskCollection Tasks
        {
            get { return tasks; }
        }

#if SILVERLIGHT || WPF
        /// <summary>
        /// MessageBox abstraction.
        /// You can replace this for unit-testing.
        /// </summary>
        protected IMessageBoxService Mbox
        {
            get { return mbox ?? (mbox = new MessageBoxService()); }
            set { mbox = value; }
        }
#endif

        /// <summary>
        /// Initialize a task.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isGlobal"></param>
        protected BusyTask CreateTask(string key, bool isGlobal)
        {
            var task = Tasks[key];
            if (task != null)
            {
                throw new ArgumentException("a task with this key already exists", "key");
            }
            else
            {
                task = new BusyTask
                {
                    IsGlobal = isGlobal,
                    Key = key,
                };
                this.Tasks.Add(task);
                return task;
            }
        }

        /// <summary>
        /// Initialize a task.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isGlobal"></param>
        /// <param name="name">the display name</param>
        protected BusyTask CreateTask(string key, bool isGlobal, string name)
        {
            var task = Tasks[key];
            if (task != null)
            {
                throw new ArgumentException("a task with this key already exists", "key");
            }
            else
            {
                task = new BusyTask
                {
                    IsGlobal = isGlobal,
                    Key = key,
                    Name = name,
                };
                this.Tasks.Add(task);
                return task;
            }
        }

        /// <summary>
        /// Update a task status.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isProcessing"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        protected void UpdateTask(string key, bool isProcessing = false, string message = null, BusyTaskType type = BusyTaskType.Default)
        {
            var task = this.Tasks[key];
            if (task != null)
            {
                this.Tasks.Update(key, message, isProcessing, type);
            }
            else
            {
                task = new BusyTask
                {
                    Key = key,
                    IsGlobal = false,
                    IsProcessing = isProcessing,
                    Message = message,
                };
                this.Tasks.Add(task);
            }
        }

        /// <summary>
        /// Update a task status.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isProcessing"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        protected void UpdateTask(string key, string message = null, bool isProcessing = false, BusyTaskType type = BusyTaskType.Default)
        {
            var task = Tasks[key];
            if (task != null)
            {
                Tasks.Update(key, message, isProcessing, type);
            }
            else
            {
                task = new BusyTask
                {
                    Key = key,
                    IsGlobal = false,
                    IsProcessing = isProcessing,
                    Message = message,
                };
                Tasks.Add(task);
            }
        }

        /// <summary>
        /// Update a task status with an exception message.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="isProcessing">if set to <c>true</c> [is processing].</param>
        /// <param name="type">The type.</param>
        protected void UpdateTask(string key, Exception exception, string message = null, bool isProcessing = false, BusyTaskType type = BusyTaskType.Error)
        {
            if (exception != null)
            {
#if DEBUG
                UpdateTask(key, isProcessing, message ?? exception.Message, type);
#else
                UpdateTask(key, isProcessing, message ?? "An error occured. ", type);
#endif
            }
            else
            {
                UpdateTask(key, isProcessing, message, BusyTaskType.Default);
            }
        }
    }
}
