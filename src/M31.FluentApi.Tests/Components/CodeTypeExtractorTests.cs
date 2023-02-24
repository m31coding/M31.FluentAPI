using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Tests.Components.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace M31.FluentApi.Tests.Components;

public class CodeTypeExtractorTests
{
    private readonly TypeSymbolProvider typeSymbolProvider;

    const string Code =
        @"
        using System;
        using System.Collections;
        using System.Collections.Generic;

        namespace TestClasses;

        public class TestCollections
        {
            public int Int1 { get; set; }
            public Int32 Int2 { get; set; }
            public System.Int32 Int3 { get; set; }

            public string String1 { get; set; }
            public String String2 { get; set; }
            public System.String String3 { get; set; }
            public string? NullableString { get; set; }

            public string[] StringArray { get; set; }
            public List<string> ListString { get; set; }
            public HashSet<string> HashSetString { get; set; }
            public IEnumerable<string> IEnumerableString { get; set; }
            public IReadOnlyCollection<string> IReadOnlyCollectionString { get; set; }
            public IList IList { get; set; }
            public List<List<string>> ListListString { get; set; }
            public List<int[,,]> ListInt3DArray { get; set; }

            public Student Student { get; set; }
            public Calculus<int> CalculusInt { get; set; }
        }

        public class Student
        {
            public string Name { get; set; }
        }

        public class Calculus<T>
        {
        }
        ";

    public CodeTypeExtractorTests()
    {
        typeSymbolProvider = TypeSymbolProvider.Create(Code);
    }

    [Theory]
    [InlineData("Int1", "int")]
    [InlineData("Int2", "int")]
    [InlineData("Int3", "int")]
    [InlineData("String1", "string")]
    [InlineData("String2", "string")]
    [InlineData("String3", "string")]
    [InlineData("NullableString", "string?")]
    [InlineData("StringArray", "string[]")]
    [InlineData("ListString", "System.Collections.Generic.List<string>")]
    [InlineData("HashSetString", "System.Collections.Generic.HashSet<string>")]
    [InlineData("IEnumerableString", "System.Collections.Generic.IEnumerable<string>")]
    [InlineData("IReadOnlyCollectionString", "System.Collections.Generic.IReadOnlyCollection<string>")]
    [InlineData("IList", "System.Collections.IList")]
    [InlineData("ListListString", "System.Collections.Generic.List<System.Collections.Generic.List<string>>")]
    [InlineData("ListInt3DArray", "System.Collections.Generic.List<int[,,]>")]
    [InlineData("Student", "TestClasses.Student")]
    [InlineData("CalculusInt", "TestClasses.Calculus<int>")]
    public void CanExtractCorrectCodeType(string propertyName, string expectedTypeForCode)
    {
        ITypeSymbol typeSymbol = typeSymbolProvider.GetTypeSymbol(propertyName);
        string typeForCode = CodeTypeExtractor.GetTypeForCodeGeneration(typeSymbol);
        Assert.Equal(expectedTypeForCode, typeForCode);
    }
}