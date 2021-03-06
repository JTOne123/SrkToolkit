﻿
namespace SrkToolkit.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SrkToolkit.DataAnnotations;
    using System.ComponentModel.DataAnnotations;

    [TestClass]
    public class EmailAddressAttributeTests
    {
        [TestClass]
        public class SingleValidation
        {
            [TestMethod]
            public void NullValue_IsValid()
            {
                string value = null, name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void EmptyValue_IsValid()
            {
                string value = "", name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void InvalidAddress_IsInvalid()
            {
                string value = "test", name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNotNull(result);
                Assert.AreEqual("A valid email address is required.", result.ErrorMessage);
                Assert.AreEqual(1, result.MemberNames.Count());
                Assert.AreEqual(name, result.MemberNames.Single());
            }

            [TestMethod]
            public void ValidAddress_IsValid()
            {
                string value = "test@test.com", name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ValidTagAddress_IsValid()
            {
                string value = "test+test@test.com", name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ValidParisAddress_IsValid()
            {
                string value = "test@test.paris", name = "Email";
                var attr = new EmailAddressAttribute();
                var context = new ValidationContext(new object(), null, null);
                context.MemberName = name;
                var result = attr.GetValidationResult(value, context);

                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class MultiValidation
        {
            [TestMethod]
            public void NullValue_IsValid()
            {
                string value = null;
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
                //Assert.AreEqual(result.ErrorMessage, "A valid email address is required.");
            }

            [TestMethod]
            public void EmptyValue_IsValid()
            {
                string value = "";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
                //Assert.AreEqual(result.ErrorMessage, "A valid email address is required.");
            }

            [TestMethod]
            public void InvalidAddress_IsValid()
            {
                string value = "test";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ValidAddress_IsValid()
            {
                string value = "test@test.com";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void MultiValidAddress_IsValid()
            {
                string value = "test@test.com test@test.com test@test.com cool";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ValidTagAddress_IsValid()
            {
                string value = "test+test@test.com";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void InvalidAddressAndMin1_IsInvalid()
            {
                string value = "test";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                    MinimumAddresses = 1,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.ErrorMessage.Contains("minimum of"));
            }

            [TestMethod]
            public void MultiValidAddressAndMax1_IsValid()
            {
                string value = "test@test.com test@test.com test@test.com cool";
                var attr = new EmailAddressAttribute()
                {
                    AllowMultiple = true,
                    MaximumAddresses = 1,
                };
                var result = attr.GetIsValid(value, null);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.ErrorMessage.Contains("maximum of"));
            }
        }
    }
}
