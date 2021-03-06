using System;
using System.Diagnostics;
using System.Reflection;
using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace FluentAssertions.Types
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="MethodInfo"/> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class MethodInfoAssertions :
        MethodBaseAssertions<MethodInfo, MethodInfoAssertions>
    {
        public MethodInfoAssertions(MethodInfo methodInfo)
        {
            Subject = methodInfo;
        }

        /// <summary>
        /// Asserts that the selected method is virtual.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<MethodInfoAssertions> BeVirtual(
            string because = "", params object[] reasonArgs)
        {
            string failureMessage = "Expected method " +
                                    SubjectDescription +
                                    " to be virtual{reason}, but it is not virtual.";

            Execute.Assertion
                .ForCondition(!Subject.IsNonVirtual())
                .BecauseOf(because, reasonArgs)
                .FailWith(failureMessage);

            return new AndConstraint<MethodInfoAssertions>(this);
        }

        /// <summary>
        /// Asserts that the selected method is async. 
        /// </summary>
        /// <param name="because">A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])"/> explaining why the assertion
        ///             is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.</param>
        /// <param name="reasonArgs">Zero or more objects to format using the placeholders in <see cref="!:because"/>.</param>
        public AndConstraint<MethodInfoAssertions> BeAsync(string because = "", params object[] reasonArgs)
        {
            string failureMessage = "Expected subject " +
                        SubjectDescription +
                        " to be async{reason}, but it is not.";

            Execute.Assertion
                .ForCondition(Subject.IsAsync())
                .BecauseOf(because, reasonArgs)
                .FailWith(failureMessage);

            return new AndConstraint<MethodInfoAssertions>(this);
        }

        /// <summary>
        /// Asserts that the selected MethodInfo returns void.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> ReturnVoid(string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(typeof(void) == Subject.ReturnType)
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected the return type of method " + Subject.Name + " to be void {reason}, but it is {0}.",
                    Subject.ReturnType.FullName);

            return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
        }

        /// <summary>
        /// Asserts that the selected MethodInfo returns <paramref name="returnType"/>.
        /// </summary>
        /// <param name="returnType">The expected return type.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> Return(Type returnType, string because = "", params object[] reasonArgs)
        {
            Execute.Assertion.ForCondition(returnType == Subject.ReturnType)
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected the return type of method " + Subject.Name + " to be {0} {reason}, but it is {1}.",
                    returnType, Subject.ReturnType.FullName);

            return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
        }
        
        /// <summary>
        /// Asserts that the selected MethodInfo returns <typeparamref name="TReturn"/>.
        /// </summary>
        /// <typeparam name="TReturn">The expected return type.</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> Return<TReturn>(string because = "", params object[] reasonArgs)
        {
            return Return(typeof (TReturn), because, reasonArgs);
        }


        internal static string GetDescriptionFor(MethodInfo method)
        {
            string returnTypeName = method.ReturnType.Name;

            return String.Format("{0} {1}.{2}", returnTypeName,
                method.DeclaringType, method.Name);
        }

        internal override string SubjectDescription
        {
            get { return GetDescriptionFor(Subject); }
        }

        protected override string Context
        {
            get { return "method"; }
        }
    }
}