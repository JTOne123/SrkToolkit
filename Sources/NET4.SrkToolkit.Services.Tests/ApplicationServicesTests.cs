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

namespace SrkToolkit.Services.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class ApplicationServicesTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            ApplicationServices.Clear();
        }

        [TestMethod, TestCategory(Category.Unit)]
        public void RegisterInstanceAndResolve()
        {
            // prepare
            ClassA subject = new ClassA();
            object resolved = null;

            // execute
            ApplicationServices.Register<InterfaceA, ClassA>(subject);
            resolved = ApplicationServices.Resolve<InterfaceA>();

            // verify
            Assert.AreEqual(subject, resolved);
        }

        [TestMethod, TestCategory(Category.Unit), ExpectedException(typeof(ArgumentException))]
        public void CannotRegisterAgain()
        {
            // prepare
            ClassA subject1 = new ClassA();
            ClassA subject2 = new ClassA();

            // execute
            ApplicationServices.Register<InterfaceA, ClassA>(subject1);
            ApplicationServices.Register<InterfaceA, ClassA>(subject2);
        }

        [TestMethod, TestCategory(Category.Unit)]
        public void RegisterNewAndResolve()
        {
            // prepare
            object resolved = null;
            object resolved1 = null;

            // execute
            ApplicationServices.Register<InterfaceA, ClassA>();
            resolved = ApplicationServices.Resolve<InterfaceA>();

            // verify
            Assert.IsNotNull(resolved);
            Assert.IsInstanceOfType(resolved, typeof(ClassA));

            // execute again
            resolved1 = ApplicationServices.Resolve<InterfaceA>();

            // verify
            Assert.AreSame(resolved1, resolved);
        }

        [TestMethod, TestCategory(Category.Unit)]
        public void RegisterClassInstanceAndResolve()
        {
            // prepare
            ClassA instance = new ClassA();
            object resolved = null;
            object resolved1 = null;

            // execute
            ApplicationServices.Register(instance);
            resolved = ApplicationServices.Resolve<ClassA>();

            // verify
            Assert.IsNotNull(resolved);
            Assert.IsInstanceOfType(resolved, typeof(ClassA));
            Assert.AreSame(instance, resolved);

            // execute again
            resolved1 = ApplicationServices.Resolve<ClassA>();

            // verify
            Assert.AreSame(instance, resolved1);
        }

        [TestMethod, TestCategory(Category.Unit)]
        public void RegisterFactoryAndResolve()
        {
            // prepare
            Func<ClassA> subject = () => new ClassA();
            object resolved = null;
            object resolved1 = null;

            // execute
            ApplicationServices.Register<InterfaceA>(subject);
            resolved = ApplicationServices.Resolve<InterfaceA>();

            // verify
            Assert.IsNotNull(resolved);
            Assert.IsInstanceOfType(resolved, typeof(ClassA));

            // execute again
            resolved1 = ApplicationServices.Resolve<InterfaceA>();

            // verify
            Assert.AreSame(resolved1, resolved);
        }

        [TestClass]
        public class ExecuteIfRegisteredMethod
        {
            [TestCleanup]
            public void Cleanup()
            {
                ApplicationServices.Clear();
            }

            [TestMethod]
            public void DoesNothingIfNotRegistered()
            {
                // prepare
                bool result = false;

                // execute
                result = ApplicationServices.ExecuteIfRegistered<InterfaceA>(a => a.Run());

                // verify
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void WorksIfRegistered()
            {
                // prepare
                var instance = new ClassA();
                Func<InterfaceA> subject = () => instance;
                bool result = false;

                // execute
                ApplicationServices.Register<InterfaceA>(subject);
                result = ApplicationServices.ExecuteIfRegistered<InterfaceA>(a => a.Run());

                // verify
                Assert.IsTrue(result);
                Assert.IsTrue(instance.Ran);
            }

            [TestMethod]
            public void WorksIfInstantiated()
            {
                // prepare
                var instance = new ClassA();
                Func<InterfaceA> subject = () => instance;
                bool result = false;

                // execute
                ApplicationServices.Register<InterfaceA>(subject);
                var resolved = ApplicationServices.Resolve<InterfaceA>();
                result = ApplicationServices.ExecuteIfRegistered<InterfaceA>(a => a.Run());

                // verify
                Assert.IsTrue(result);
                Assert.IsTrue(instance.Ran);
            }
        }

        [TestClass]
        public class ExecuteIfReadyMethod
        {
            [TestCleanup]
            public void Cleanup()
            {
                ApplicationServices.Clear();
            }

            [TestMethod]
            public void DoesNothingIfNotRegistered()
            {
                // prepare
                bool result = false;

                // execute
                result = ApplicationServices.ExecuteIfReady<InterfaceA>(a => a.Run());

                // verify
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void DoesNothingIfRegisteredAndNotReady()
            {
                // prepare
                var instance = new ClassA();
                Func<InterfaceA> subject = () => instance;
                bool result = false;

                // execute
                ApplicationServices.Register<InterfaceA>(subject);
                result = ApplicationServices.ExecuteIfReady<InterfaceA>(a => a.Run());

                // verify
                Assert.IsFalse(result);
                Assert.IsFalse(instance.Ran);
            }

            [TestMethod]
            public void WorksIfInstantiated()
            {
                // prepare
                var instance = new ClassA();
                Func<InterfaceA> subject = () => instance;
                bool result = false;

                // execute
                ApplicationServices.Register<InterfaceA>(subject);
                var resolved = ApplicationServices.Resolve<InterfaceA>();
                result = ApplicationServices.ExecuteIfReady<InterfaceA>(a => a.Run());

                // verify
                Assert.IsTrue(result);
                Assert.IsTrue(instance.Ran);
            }
        }

        [TestClass]
        public class RegisterFactoryMethod
        {
            [TestCleanup]
            public void Cleanup()
            {
                ApplicationServices.Clear();
            }

            [TestMethod]
            public void ResolvesNewInstanceAtEachResolveCall()
            {
                // prepare
                Func<ClassA> subject = () => new ClassA();
                object resolved = null;
                object resolved1 = null;

                // execute
                ApplicationServices.RegisterFactory<InterfaceA>(subject);
                resolved = ApplicationServices.Resolve<InterfaceA>();

                // verify
                Assert.IsNotNull(resolved);
                Assert.IsInstanceOfType(resolved, typeof(ClassA));

                // execute again
                resolved1 = ApplicationServices.Resolve<InterfaceA>();

                // verify
                Assert.AreNotSame(resolved1, resolved);
            }
        }

        [TestClass]
        public class SyncRootMethod
        {
            [TestCleanup]
            public void Cleanup()
            {
                ApplicationServices.Clear();
            }

            [TestMethod]
            public void AllowThreadSafeRegistration()
            {
                // prepare
                var taskDelegate = new Action(() =>
                {
                    using (ApplicationServices.SyncRoot())
                    {
                        if (!ApplicationServices.IsRegistered<InterfaceA>())
                            ApplicationServices.Register<InterfaceA>(new ClassA());
                    }
                });

                // execute
                var tasks = Enumerable.Range(0, 100).Select(i => new Task(taskDelegate)).ToArray();
                for (int i = 0; i < tasks.Length; i++)
                    tasks[i].Start();
                Task.WaitAll(tasks);

                // verify
                var resolved1 = ApplicationServices.Resolve<InterfaceA>();
                Assert.IsNotNull(resolved1);
            }
        }

        interface InterfaceA {
            void Run();
        }
        interface InterfaceB { }
        class ClassA : InterfaceA
        {
            private bool ran;
            internal bool Ran { get { return this.ran; } }
            public void Run()
            {
                this.ran = true;
            }
        }
        class ClassB : InterfaceB { }
    }
}
