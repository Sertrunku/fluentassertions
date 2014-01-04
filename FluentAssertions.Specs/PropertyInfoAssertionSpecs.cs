﻿using System;
using System.Reflection;

using FluentAssertions.Types;


#if WINRT
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif NUNIT
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestCaseAttribute;
using AssertFailedException = NUnit.Framework.AssertionException;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using Assert = NUnit.Framework.Assert;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace FluentAssertions.Specs
{
    [TestClass]
    public class PropertyInfoAssertionSpecs
    {
        #region PropertyInfo assertions

        [TestMethod]
        public void When_a_virtual_property_is_expected_to_be_virtual_it_should_succeed()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithAllPropertiesVirtual).GetRuntimeProperty("PublicVirtualProperty");
#else
            PropertyInfo propertyInfo = typeof(ClassWithAllPropertiesVirtual).GetProperty("PublicVirtualProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfo.Should().BeVirtual();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void When_a_non_virtual_property_is_expected_to_be_virtual_it_should_throw()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithNonVirtualPublicProperties).GetRuntimeProperty("PublicNonVirtualProperty");
#else
            PropertyInfo propertyInfo = typeof(ClassWithNonVirtualPublicProperties).GetProperty("PublicNonVirtualProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfo.Should().BeVirtual("we want to test the error {0}", "message");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>()
               .WithMessage(
                   "Expected property String FluentAssertions.Specs.ClassWithNonVirtualPublicProperties.PublicNonVirtualProperty" +
                       " to be virtual because we want to test the error message," +
                       " but it is not virtual.");
        }

        [TestMethod]
        public void When_asserting_a_property_is_decorated_with_attribute_and_it_is_it_should_succeed()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithAllPropertiesDecoratedWithDummyAttribute).GetRuntimeProperty("PublicProperty");
#else
            PropertyInfo propertyInfo = typeof(ClassWithAllPropertiesDecoratedWithDummyAttribute).GetProperty("PublicProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfo.Should().BeDecoratedWith<DummyPropertyAttribute>();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void When_asserting_a_property_is_decorated_with_attribute_and_it_is_it_should_throw_with_descriptive_message()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute).GetRuntimeProperty("PublicProperty");
#else
            PropertyInfo propertyInfo =
                typeof(ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute).GetProperty("PublicProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfo.Should().BeDecoratedWith<DummyPropertyAttribute>("because we want to test the error message");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>()
               .WithMessage("Expected property String " +
                   "FluentAssertions.Specs.ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute.PublicProperty to be decorated with " +
                   "FluentAssertions.Specs.DummyPropertyAttribute because we want to test the error message, but that attribute was not found.");
        }

        #endregion

        [TestMethod]
        public void When_asserting_properties_are_virtual_and_they_are_it_should_succeed()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithAllPropertiesVirtual));

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should().BeVirtual();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void When_asserting_properties_are_virtual_but_non_virtual_properties_are_found_it_should_throw()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithNonVirtualPublicProperties));

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should().BeVirtual();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void
            When_asserting_properties_are_virtual_but_non_virtual_properties_are_found_it_should_throw_with_descriptive_message()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithNonVirtualPublicProperties));

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should().BeVirtual("we want to test the error {0}", "message");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>()
               .WithMessage("Expected all selected properties" +
                   " to be virtual because we want to test the error message," +
                   " but the following properties are not virtual:\r\n" +
                   "String FluentAssertions.Specs.ClassWithNonVirtualPublicProperties.PublicNonVirtualProperty\r\n" +
                   "String FluentAssertions.Specs.ClassWithNonVirtualPublicProperties.InternalNonVirtualProperty\r\n" +
                   "String FluentAssertions.Specs.ClassWithNonVirtualPublicProperties.ProtectedNonVirtualProperty");
        }

        [TestMethod]
        public void When_asserting_properties_are_decorated_with_attribute_and_they_are_it_should_succeed()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithAllPropertiesDecoratedWithDummyAttribute));

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should().BeDecoratedWith<DummyPropertyAttribute>();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void When_asserting_properties_are_decorated_with_attribute_and_they_are_not_it_should_throw()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute))
                .ThatArePublicOrInternal;

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should().BeDecoratedWith<DummyPropertyAttribute>();

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void
            When_asserting_properties_are_decorated_with_attribute_and_they_are_not_it_should_throw_with_descriptive_message()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
            var propertyInfoSelector = new PropertyInfoSelector(typeof(ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute));

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action act = () =>
                propertyInfoSelector.Should()
                                    .BeDecoratedWith<DummyPropertyAttribute>("because we want to test the error {0}", "message");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>()
               .WithMessage("Expected all selected properties to be decorated with" +
                   " FluentAssertions.Specs.DummyPropertyAttribute because we want to test the error message," +
                   " but the following properties are not:\r\n" +
                   "String FluentAssertions.Specs.ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute.PublicProperty\r\n" +
                   "String FluentAssertions.Specs.ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute.InternalProperty\r\n" +
                   "String FluentAssertions.Specs.ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute.ProtectedProperty");
        }

        [TestMethod]
        public void When_a_read_only_property_is_expected_to_be_writable_it_should_throw()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithReadOnlyProperty).GetRuntimeProperty("ReadOnlyProperty");
#else
            PropertyInfo propertyInfo = typeof(ClassWithReadOnlyProperty).GetProperty("ReadOnlyProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action action = () => propertyInfo.Should().BeWritable("that's required");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            action
                .ShouldThrow<AssertFailedException>()
                .WithMessage("Expected property ReadOnlyProperty to have a setter because that's required.");
        }
        
        [TestMethod]
        public void When_a_read_write_property_is_expected_to_be_writable_it_should_not_throw()
        {
            //-------------------------------------------------------------------------------------------------------------------
            // Arrange
            //-------------------------------------------------------------------------------------------------------------------
#if WINRT
            PropertyInfo propertyInfo = typeof(ClassWithReadOnlyProperty).GetRuntimeProperty("ReadWriteProperty");
#else
            PropertyInfo propertyInfo = typeof(ClassWithReadOnlyProperty).GetProperty("ReadWriteProperty");
#endif

            //-------------------------------------------------------------------------------------------------------------------
            // Act
            //-------------------------------------------------------------------------------------------------------------------
            Action action = () => propertyInfo.Should().BeWritable("that's required");

            //-------------------------------------------------------------------------------------------------------------------
            // Assert
            //-------------------------------------------------------------------------------------------------------------------
            action.ShouldNotThrow();
        }
    }

    #region Internal classes used in unit tests

    internal class ClassWithAllPropertiesVirtual
    {
        public virtual string PublicVirtualProperty { get; set; }

        internal virtual string InternalVirtualProperty { get; set; }

        protected virtual string ProtectedVirtualProperty { get; set; }
    }

    internal interface IInterfaceWithProperty
    {
        string PublicNonVirtualProperty { get; set; }
    }

    internal class ClassWithNonVirtualPublicProperties : IInterfaceWithProperty
    {
        public string PublicNonVirtualProperty { get; set; }

        internal string InternalNonVirtualProperty { get; set; }

        protected string ProtectedNonVirtualProperty { get; set; }
    }

    internal class ClassWithReadOnlyProperty
    {
        public string ReadOnlyProperty { get { return ""; } }
        public string ReadWriteProperty { get { return ""; } set { } }
    }

    internal class ClassWithAllPropertiesDecoratedWithDummyAttribute
    {
        [DummyProperty]
        public string PublicProperty { get; set; }

        [DummyProperty]
        internal string InternalProperty { get; set; }

        [DummyProperty]
        protected string ProtectedProperty { get; set; }
    }

    internal class ClassWithPropertiesThatAreNotDecoratedWithDummyAttribute
    {
        public string PublicProperty { get; set; }

        internal string InternalProperty { get; set; }

        protected string ProtectedProperty { get; set; }
    }

    #endregion
}