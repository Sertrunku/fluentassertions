﻿using System;

using FluentAssertions.Formatting;

#if WINRT
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif NUNIT
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestCaseAttribute;
using AssertFailedException = NUnit.Framework.AssertionException;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace FluentAssertions.Specs
{
    [TestClass]
    public class DateTimeFormatterSpecs
    {
        [TestMethod]
        public void When_time_is_not_relevant_it_should_not_be_included_in_the_output()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeValueFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            string result = formatter.ToString(new DateTime(1973, 9, 20), false);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            result.Should().Be("<1973-09-20>");
        }

        [TestMethod]
        public void When_date_is_not_relevant_it_should_not_be_included_in_the_output()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeValueFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            DateTime emptyDate = 1.January(0001);
            var dateTime = emptyDate.At(08, 20, 01);
            string result = formatter.ToString(dateTime, false);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            result.Should().Be("<08:20:01>");
        }

        [TestMethod]
        public void When_a_full_date_and_time_is_specified_all_parts_should_be_included_in_the_output()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeValueFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            var dateTime = 1.May(2012).At(20, 15, 30, 318);
            string result = formatter.ToString(dateTime, false);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            result.Should().Be(dateTime.ToString("<yyyy-MM-dd HH:mm:ss.fff>"));
        }

        [TestMethod]
        public void When_milliseconds_are_not_relevant_they_should_not_be_included_in_the_output()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeValueFormatter();

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            var dateTime = 1.May(2012).At(20, 15, 30);
            string result = formatter.ToString(dateTime, false);

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            result.Should().Be("<2012-05-01 20:15:30>");
        }
    }
}