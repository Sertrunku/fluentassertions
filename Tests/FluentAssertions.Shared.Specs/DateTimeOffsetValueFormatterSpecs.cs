﻿using System;

using FluentAssertions.Formatting;

#if !OLD_MSTEST && !NUNIT
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
    public class DateTimeOffsetValueFormatterSpecs
    {
        [TestMethod]
        public void When_time_is_not_relevant_it_should_not_be_included_in_the_output()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeOffsetValueFormatter();

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
            var formatter = new DateTimeOffsetValueFormatter();

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
            var formatter = new DateTimeOffsetValueFormatter();

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
            var formatter = new DateTimeOffsetValueFormatter();

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

        [TestMethod]
        public void
            When_a_DateTime_is_used_it_should_format_the_same_as_a_DateTimeOffset()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var formatter = new DateTimeOffsetValueFormatter();

            var dateOnly = ToUtcWithoutChangingTime(new DateTime(1973, 9, 20));
            var timeOnly = ToUtcWithoutChangingTime(1.January(0001).At(08, 20, 01));
            var witoutMilliseconds = ToUtcWithoutChangingTime(1.May(2012).At(20, 15, 30));
            var withMilliseconds = ToUtcWithoutChangingTime(1.May(2012).At(20, 15, 30, 318));

            //-----------------------------------------------------------------------------------------------------------
            // Act / Assert
            //-----------------------------------------------------------------------------------------------------------
            formatter.ToString(dateOnly, false).Should().Be(formatter.ToString((DateTimeOffset) dateOnly, false));
            formatter.ToString(timeOnly, false).Should().Be(formatter.ToString((DateTimeOffset) timeOnly, false));
            formatter.ToString(witoutMilliseconds, false).Should().Be(formatter.ToString((DateTimeOffset) witoutMilliseconds, false));
            formatter.ToString(withMilliseconds, false).Should().Be(formatter.ToString((DateTimeOffset) withMilliseconds, false));
        }

        private static DateTime ToUtcWithoutChangingTime(DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }
    }
}