using System;

using System.Collections;
using System.Linq;

using FluentAssertions.Execution;

namespace FluentAssertions.Equivalency
{
    internal class EnumerableEquivalencyStep : IEquivalencyStep
    {
        /// <summary>
        /// Gets a value indicating whether this step can handle the verificationScope subject and/or expectation.
        /// </summary>
        public bool CanHandle(EquivalencyValidationContext context, IEquivalencyAssertionOptions config)
        {
            Type subjectType = GetSubjectType(context, config);

            return IsCollection(subjectType);
        }

        internal static Type GetSubjectType(EquivalencyValidationContext context, IEquivalencyAssertionOptions config)
        {
            bool useRuntimeType = ShouldUseRuntimeType(config);

            return useRuntimeType ? context.RuntimeType : context.CompileTimeType;
        }

        private static bool ShouldUseRuntimeType(IEquivalencyAssertionOptions config)
        {
            return config.SelectionRules.Any(selectionRule => selectionRule is AllRuntimePublicPropertiesSelectionRule);
        }

        /// <summary>
        /// Applies a step as part of the task to compare two objects for structural equality.
        /// </summary>
        /// <value>
        /// Should return <c>true</c> if the subject matches the expectation or if no additional assertions
        /// have to be executed. Should return <c>false</c> otherwise.
        /// </value>
        /// <remarks>
        /// May throw when preconditions are not met or if it detects mismatching data.
        /// </remarks>
        public bool Handle(EquivalencyValidationContext context, IEquivalencyValidator parent, IEquivalencyAssertionOptions config)
        {
            if (AssertExpectationIsCollection(context.Expectation))
            {
                var validator = new EnumerableEquivalencyValidator(parent, context)
                {
                    Recursive = context.IsRoot || config.IsRecursive,
                    OrderingRules = config.OrderingRules
                };

                validator.Execute(ToArray(context.Subject), ToArray(context.Expectation));
            }

            return true;
        }

        private static bool AssertExpectationIsCollection(object expectation)
        {
            return AssertionScope.Current
                .ForCondition(IsCollection(expectation.GetType()))
                .FailWith("{context:Subject} is a collection and cannot be compared with a non-collection type.");
        }

        private static bool IsCollection(Type type)
        {
            return !typeof(string).IsAssignableFrom(type) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        internal static object[] ToArray(object value)
        {
            return ((IEnumerable)value).Cast<object>().ToArray();
        }
    }
}