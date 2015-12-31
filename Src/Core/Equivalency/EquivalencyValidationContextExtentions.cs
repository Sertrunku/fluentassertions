using System;
using FluentAssertions.Common;

namespace FluentAssertions.Equivalency
{
    internal static class EquivalencyValidationContextExtentions
    {
        internal static IEquivalencyValidationContext CreateForNestedMember(this IEquivalencyValidationContext context,
            SelectedMemberInfo nestedMember, SelectedMemberInfo matchingProperty)
        {
            string memberDescription = nestedMember.Name;
            string propertyPath = (context.SelectedMemberDescription.Length == 0) ? "member " : context.SelectedMemberDescription + ".";

            return new EquivalencyValidationContext
            {
                SelectedMemberInfo = nestedMember,
                Subject = nestedMember.GetValue(context.Subject, null),
                Expectation = matchingProperty.GetValue(context.Expectation, null),
                SelectedMemberPath = context.SelectedMemberPath.Combine(memberDescription, "."),
                SelectedMemberDescription = propertyPath + memberDescription,
                Reason = context.Reason,
                ReasonArgs = context.ReasonArgs,
                CompileTimeType = nestedMember.MemberType,
                RootIsCollection = context.RootIsCollection
            };
        }

        internal static IEquivalencyValidationContext CreateForCollectionItem<T>(this IEquivalencyValidationContext context,
            string index, T subject, object expectation)
        {
            string memberDescription = "[" + index + "]";
            string propertyPath = (context.SelectedMemberDescription.Length == 0) ? "item" : context.SelectedMemberDescription + String.Empty;

            return new EquivalencyValidationContext
            {
                SelectedMemberInfo = context.SelectedMemberInfo,
                Subject = subject,
                Expectation = expectation,
                SelectedMemberPath = context.SelectedMemberPath.Combine(memberDescription, String.Empty),
                SelectedMemberDescription = propertyPath + memberDescription,
                Reason = context.Reason,
                ReasonArgs = context.ReasonArgs,
                CompileTimeType = typeof (T),
                RootIsCollection = context.RootIsCollection
            };
        }

        internal static IEquivalencyValidationContext CreateForDictionaryItem<TKey, TValue>(
            this IEquivalencyValidationContext context,
            TKey key,
            TValue subject,
            object expectation)
        {
            string memberDescription = "[" + key + "]";
            string propertyPath = (context.SelectedMemberDescription.Length == 0) ? "pair" : context.SelectedMemberDescription + String.Empty;

            return new EquivalencyValidationContext
            {
                SelectedMemberInfo = context.SelectedMemberInfo,
                Subject = subject,
                Expectation = expectation,
                SelectedMemberPath = context.SelectedMemberPath.Combine(memberDescription, String.Empty),
                SelectedMemberDescription = propertyPath + memberDescription,
                Reason = context.Reason,
                ReasonArgs = context.ReasonArgs,
                CompileTimeType = typeof (TValue),
                RootIsCollection = context.RootIsCollection
            };
        }

        internal static IEquivalencyValidationContext CreateWithDifferentSubject(this IEquivalencyValidationContext context,
            object subject, Type compileTimeType)
        {
            return new EquivalencyValidationContext
            {
                CompileTimeType = compileTimeType,
                Expectation = context.Expectation,
                SelectedMemberDescription = context.SelectedMemberDescription,
                SelectedMemberInfo = context.SelectedMemberInfo,
                SelectedMemberPath = context.SelectedMemberPath,
                Reason = context.Reason,
                ReasonArgs = context.ReasonArgs,
                Subject = subject
            };
        }
    }
}