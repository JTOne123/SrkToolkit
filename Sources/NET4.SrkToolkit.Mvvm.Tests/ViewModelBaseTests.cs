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

namespace SrkToolkit.Mvvm.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SrkToolkit.Mvvm;

    [TestClass]
    public class ViewModelBaseTests
    {
        [TestClass]
        public class PropertyChangedEvent
        {
            [TestMethod, TestCategory(Category.Unit)]
            public void ChangingPropertyValueViaSetValueNotifiesChange()
            {
                // prepare
                var target = new ViewModelA();
                bool notified = false;
                target.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "ViaSetValue")
                        notified = true;
                };

                // execute
                target.ViaSetValue = true;

                // verify
                Assert.AreEqual(true, target.ViaSetValue);
                Assert.IsTrue(notified);
            }

            [TestMethod, TestCategory(Category.Unit)]
            public void ChangingPropertyValueViaRaisePropertyChangedNotifiesChange()
            {
                // prepare
                var target = new ViewModelA();
                bool notified = false;
                target.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "ViaRaisePropertyChanged")
                        notified = true;
                };

                // execute
                target.ViaRaisePropertyChanged = true;

                // verify
                Assert.AreEqual(true, target.ViaRaisePropertyChanged);
                Assert.IsTrue(notified);
            }
        }

        [TestClass]
        public class DispatchMethod
        {
            [TestMethod]
            public void DoesNotThrowWhenDispatcherIsNull()
            {
                var target = new ViewModelA();
                target.Dispatcher = null;
                target.Dispatch(() => { });
            }
        }

        public class ViewModelA : ViewModelBase
        {
            private bool viaSetValue;
            private bool viaRaisePropertyChanged;

            public bool ViaSetValue
            {
                get { return this.viaSetValue; }
                set { this.SetValue(ref this.viaSetValue, value, "ViaSetValue"); }
            }

            public bool ViaRaisePropertyChanged
            {
                get { return this.viaRaisePropertyChanged; }
                set
                {
                    if (this.viaRaisePropertyChanged != value)
                    {
                        this.viaRaisePropertyChanged = value;
                        this.RaisePropertyChanged("ViaRaisePropertyChanged");
                    }
                }
            }
        }
    }
}
