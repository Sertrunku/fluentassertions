using System;

using FluentAssertions.Collections;

#if !OLD_MSTEST && !NUNIT 
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#elif NUNIT
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestCaseAttribute;
using AssertFailedException = NUnit.Framework.AssertionException;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace FluentAssertions.Specs
{
    [TestClass]
    public class AndWhichConstraintSpecs
    {
        [TestMethod]
        public void When_many_objects_are_provided_accesing_which_should_throw_a_descriptive_exception()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var continuation = new AndWhichConstraint<StringCollectionAssertions, string>(null, new[] {"hello", "world"});

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () =>
            {
                var item = continuation.Which;
            };

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.ShouldThrow<AssertFailedException>()
                .WithMessage(
                    "More than one object found.  FluentAssertions cannot determine which object is meant.*")
                .WithMessage("*Found objects:\r\n\t\"hello\"\r\n\t\"world\"");
        }
    }
}
