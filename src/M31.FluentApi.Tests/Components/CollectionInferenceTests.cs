using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;
using M31.FluentApi.Generator.SourceGenerators.Collections;
using M31.FluentApi.Tests.Components.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace M31.FluentApi.Tests.Components;

public class CollectionInferenceTests
{
    private readonly TypeSymbolProvider typeSymbolProvider;

    private const string Code = @"
        using System;
        using System.Collections;
        using System.Collections.Generic;

        namespace Collections;

        public class TestCollections
        {
            public string[] StringArray { get; set; }
            public Array Array { get; set; }
            public IEnumerable IEnumerable { get; set; }
            public IEnumerable<string> IEnumerableString { get; set; }
            public IReadOnlyList<string> IReadOnlyListString { get; set; }
            public IReadOnlyCollection<string> IReadOnlyCollectionString { get; set; }
            public List<string> ListString { get; set; }
            public IList IList { get; set; }
            public IList<string> IListString { get; set; }
            public ICollection<string> ICollectionString { get; set; }
            public ICollection ICollection { get; set; }
            public HashSet<string> HashSetString { get; set; }
            public ISet<string> ISetString { get; set; }
            public IReadOnlySet<string> IReadOnlySetString { get; set; }
            public List<List<string>> ListListString { get; set; }
            public List<int[,,]> ListInt3DArray { get; set; }
        }";

    public CollectionInferenceTests()
    {
        typeSymbolProvider = TypeSymbolProvider.Create(Code);
    }

    [Theory]
    [InlineData("StringArray", GeneratedCollection.Array, "string")]
    [InlineData("Array", GeneratedCollection.Array, "object")]
    [InlineData("IEnumerable", GeneratedCollection.Array, "object")]
    [InlineData("IEnumerableString", GeneratedCollection.Array, "string")]
    [InlineData("IReadOnlyListString", GeneratedCollection.Array, "string")]
    [InlineData("IReadOnlyCollectionString", GeneratedCollection.Array, "string")]
    internal void TestTypesThatWillBeCoveredByAnArray(
        string propertyName,
        GeneratedCollection expectedCollection,
        string genericTypeArgument)
    {
        TestType(propertyName, expectedCollection, genericTypeArgument);
    }

    [Theory]
    [InlineData("ListString", GeneratedCollection.List, "string")]
    [InlineData("IList", GeneratedCollection.List, "object")]
    [InlineData("IListString", GeneratedCollection.List, "string")]
    [InlineData("ICollectionString", GeneratedCollection.List, "string")]
    [InlineData("ICollection", GeneratedCollection.List, "object")]
    internal void TestTypesThatWillBeCoveredByAList(
        string propertyName,
        GeneratedCollection expectedCollection,
        string genericTypeArgument)
    {
        TestType(propertyName, expectedCollection, genericTypeArgument);
    }

    [Theory]
    [InlineData("HashSetString", GeneratedCollection.HashSet, "string")]
    [InlineData("ISetString", GeneratedCollection.HashSet, "string")]
    [InlineData("IReadOnlySetString", GeneratedCollection.HashSet, "string")]
    internal void TestTypesThatWillBeCoveredByAHashSet(
        string propertyName,
        GeneratedCollection expectedCollection,
        string genericTypeArgument)
    {
        TestType(propertyName, expectedCollection, genericTypeArgument);
    }

    [Theory]
    [InlineData("ListListString", GeneratedCollection.List, "System.Collections.Generic.List<string>")]
    [InlineData("ListInt3DArray", GeneratedCollection.List, "int[,,]")]
    internal void TestAdditionalTypes(
        string propertyName,
        GeneratedCollection expectedCollection,
        string genericTypeArgument)
    {
        TestType(propertyName, expectedCollection, genericTypeArgument);
    }

    private void TestType(string propertyName, GeneratedCollection expectedCollection, string genericTypeArgument)
    {
        ITypeSymbol typeSymbol = typeSymbolProvider.GetTypeSymbol(propertyName);
        CollectionType? actual = CollectionInference.InferCollectionType(typeSymbol);
        CollectionType expected = new CollectionType(expectedCollection, genericTypeArgument, null);
        Assert.Equal(expected, actual);
    }
}