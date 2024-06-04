using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace M31.FluentApi.Tests.CodeGeneration;

internal class TestDataProvider : IEnumerable<object[]>
{
    private readonly List<object[]> testClasses =
        Filter(new string[] { "SkippableLoopClass" },
            new List<object[]>
            {
                new object[] { "Abstract", "AliasNamespaceClass", "Student" },
                new object[] { "Abstract", "CollectionInterfaceMemberClass", "Student" },
                new object[] { "Abstract", "CollectionMemberClass", "Student" },
                new object[] { "Abstract", "CollectionNullableArrayClass", "Student" },
                new object[] { "Abstract", "ContinueWithAfterCompoundClass", "Student" },
                new object[] { "Abstract", "ContinueWithOfOverloadedMethodClass", "Student" },
                new object[] { "Abstract", "ContinueWithSelfClass", "Student" },
                new object[] { "Abstract", "CustomFluentMethodNameClass", "Student" },
                new object[] { "Abstract", "DefaultFluentMethodNameClass", "Student" },
                new object[] { "Abstract", "EmptyClass", "Student" },
                new object[] { "Abstract", "FluentDefaultMemberClass", "Student" },
                new object[] { "Abstract", "FluentLambdaClass", "Student|Address" },
                new object[] { "Abstract", "FluentLambdaClassInDifferentNamespace", "Student|Address" },
                new object[] { "Abstract", "FluentLambdaNullablePropertyClass", "Student|Address" },
                new object[] { "Abstract", "FluentLambdaRecursiveClass", "Student" },
                new object[] { "Abstract", "FluentLambdaSingleStepClass", "Student|Address" },
                new object[] { "Abstract", "FluentMethodClass", "Student" },
                new object[] { "Abstract", "FluentMethodDefaultValuesClass", "Student" },
                new object[] { "Abstract", "FluentMethodParameterModifiersClass", "Student" },
                new object[] { "Abstract", "FluentNullableClass", "Student" },
                new object[] { "Abstract", "FluentNullableNoNullableAnnotationClass", "Student" },
                new object[] { "Abstract", "FluentNullableNoNullableAnnotationPrivateSetClass", "Student" },
                new object[] { "Abstract", "FluentReturnMultiStepClass", "Student" },
                new object[] { "Abstract", "FluentReturnMultiStepPrivateMethodsClass", "Student" },
                new object[] { "Abstract", "FluentReturnSingleStepClass", "Student" },
                new object[] { "Abstract", "FluentReturnSingleStepPrivateMethodsClass", "Student" },
                new object[] { "Abstract", "ForkClass", "Student" },
                new object[] { "Abstract", "FullyQualifiedTypeClass", "Student" },
                new object[] { "Abstract", "GenericClass", "Student" },
                new object[] { "Abstract", "GenericClassPrivateConstructor", "Student" },
                new object[] { "Abstract", "GenericClassWithConstraints", "Student" },
                new object[] { "Abstract", "GenericClassWithGenericMethods", "Student" },
                new object[] { "Abstract", "GenericClassWithPrivateGenericMethods", "Student" },
                new object[] { "Abstract", "GenericMethodWithConstraintsClass", "Student" },
                new object[] { "Abstract", "GenericOverloadedMethodClass", "Student" },
                new object[] { "Abstract", "GenericOverloadedPrivateMethodClass", "Student" },
                new object[] { "Abstract", "GetInitPropertyClass", "Student" },
                new object[] { "Abstract", "GetPrivateInitPropertyClass", "Student" },
                new object[] { "Abstract", "GetPrivateSetPropertyClass", "Student" },
                new object[] { "Abstract", "InternalPropertyClass", "Student" },
                new object[] { "Abstract", "InternalClass", "Student" },
                new object[] { "Abstract", "NonGenericCollectionMemberClass", "Student" },
                new object[] { "Abstract", "NullablePredicateAndCollectionClass", "Student" },
                new object[] { "Abstract", "OneMemberClass", "Student" },
                new object[] { "Abstract", "OverloadedMethodClass", "Student" },
                new object[] { "Abstract", "PartialClass", "Student1|Student2" },
                new object[] { "Abstract", "PredicateClass", "Student" },
                new object[] { "Abstract", "PredicatePrivateFieldClass", "Student" },
                new object[] { "Abstract", "PrivateConstructorClass", "Student" },
                new object[] { "Abstract", "PrivateFieldClass", "Student" },
                new object[] { "Abstract", "PrivateFluentMethodClass", "Student" },
                new object[] { "Abstract", "PrivateFluentMethodNullableParameterClass", "Student" },
                new object[] { "Abstract", "PrivateFluentMethodParameterModifiersClass", "Student" },
                new object[] { "Abstract", "PrivateReadonlyFieldClass", "Student" },
                new object[] { "Abstract", "PrivateUnderscoreFieldClass", "Student" },
                new object[] { "Abstract", "PublicFieldClass", "Student" },
                new object[] { "Abstract", "PublicReadonlyFieldClass", "Student" },
                new object[] { "Abstract", "SameNameMemberClass", "Student" },
                new object[] { "Abstract", "SkippableFirstMemberClass", "Student" },
                new object[] { "Abstract", "SkippableLoopClass", "Student" },
                new object[] { "Abstract", "SkippableMemberClass", "Student" },
                new object[] { "Abstract", "SkippableSeveralMembersClass", "Student" },
                new object[] { "Abstract", "ThreeMemberClass", "Student" },
                new object[] { "Abstract", "ThreeMemberRecord", "Student" },
                new object[] { "Abstract", "ThreeMemberStruct", "Student" },
                new object[] { "Abstract", "ThreePrivateMembersClass", "Student" },
                new object[] { "Abstract", "ThreeMemberRecordStruct", "Student" },
                new object[] { "Abstract", "TryBreakFluentApiClass1", "Student" },
                new object[] { "Abstract", "TryBreakFluentApiClass2", "Student" },
                new object[] { "Abstract", "TwoMemberClass", "Student" },
                new object[] { "Abstract", "TwoParameterCompoundClass", "Student" },
                new object[] { "Abstract", "TwoParameterCompoundClassReversedParameters", "Student" },
                new object[] { "PersonClass", "Person" },
                new object[] { "StudentClass", "Student" }
            }).Select(l => new string[] { "..", "..", "..", "CodeGeneration", "TestClasses" }
            .Concat(l).Reverse().ToArray()).ToList(); // reversed for better readability in the unit test panel

    public IEnumerator<object[]> GetEnumerator() => testClasses.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static List<object[]> Filter(string[] testsToFilter, List<object[]> tests)
    {
        return testsToFilter.Length == 0 ? tests : tests.Where(t => testsToFilter.Contains((string)t[1])).ToList();
    }
}