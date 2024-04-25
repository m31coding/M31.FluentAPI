using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.Generics;

internal class GenericTypeConstraint
{
    internal GenericTypeConstraint(
        bool referenceTypeConstraint,
        bool nullableReferenceTypeConstraint,
        bool valueTypeConstraint,
        bool notNullConstraint,
        bool constructorConstraint,
        bool unmanagedTypeConstraint,
        IReadOnlyCollection<string> constraintTypesForCodeGeneration)
    {
        ReferenceTypeConstraint = referenceTypeConstraint;
        NullableReferenceTypeConstraint = nullableReferenceTypeConstraint;
        ValueTypeConstraint = valueTypeConstraint;
        NotNullConstraint = notNullConstraint;
        ConstructorConstraint = constructorConstraint;
        UnmanagedTypeConstraint = unmanagedTypeConstraint;
        ConstraintTypesForCodeGeneration = constraintTypesForCodeGeneration;
    }

    internal static GenericTypeConstraint Create(ITypeParameterSymbol typeParameter)
    {
        bool referenceTypeConstraint = typeParameter.HasReferenceTypeConstraint &&
                                       typeParameter.ReferenceTypeConstraintNullableAnnotation !=
                                       NullableAnnotation.Annotated;
        bool nullableReferenceTypeConstraint = typeParameter.HasReferenceTypeConstraint &&
                                               typeParameter.ReferenceTypeConstraintNullableAnnotation ==
                                               NullableAnnotation.Annotated;
        bool valueTypeConstraint = typeParameter.HasValueTypeConstraint;
        bool notNullConstraint = typeParameter.HasNotNullConstraint;
        bool constructorConstraint = typeParameter.HasConstructorConstraint;
        bool unmanagedTypeConstraint = typeParameter.HasUnmanagedTypeConstraint;
        IReadOnlyCollection<string> constraintTypesForCodeGeneration =
            typeParameter.ConstraintTypes.Select(ToConstraintTypeForCodeGeneration).ToArray();

        return new GenericTypeConstraint(
            referenceTypeConstraint, nullableReferenceTypeConstraint, valueTypeConstraint, notNullConstraint,
            constructorConstraint, unmanagedTypeConstraint, constraintTypesForCodeGeneration);

        string ToConstraintTypeForCodeGeneration(ITypeSymbol constraintType, int index)
        {
            string typeForCodeGeneration = CodeTypeExtractor.GetTypeForCodeGeneration(constraintType);

            return typeParameter.ConstraintNullableAnnotations[index] == NullableAnnotation.Annotated
                ? $"{typeForCodeGeneration}?"
                : typeForCodeGeneration;
        }
    }

    internal bool ReferenceTypeConstraint { get; }
    internal bool NullableReferenceTypeConstraint { get; }
    internal bool ValueTypeConstraint { get; }
    internal bool NotNullConstraint { get; }
    internal bool ConstructorConstraint { get; }
    internal bool UnmanagedTypeConstraint { get; }
    internal IReadOnlyCollection<string> ConstraintTypesForCodeGeneration { get; }

    internal IEnumerable<string> GetConstraintsForCodeGeneration()
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
        if (ReferenceTypeConstraint) yield return "class";
        if (NullableReferenceTypeConstraint) yield return "class?";
        if (ValueTypeConstraint) yield return "struct";
        if (NotNullConstraint) yield return "notnull";
        if (UnmanagedTypeConstraint) yield return "unmanaged";

        foreach (string constraintTypeForCodeGeneration in ConstraintTypesForCodeGeneration)
        {
            yield return constraintTypeForCodeGeneration;
        }

        if (ConstructorConstraint) yield return "new()";
    }

    protected bool Equals(GenericTypeConstraint other)
    {
        return ReferenceTypeConstraint == other.ReferenceTypeConstraint &&
               NullableReferenceTypeConstraint == other.NullableReferenceTypeConstraint &&
               ValueTypeConstraint == other.ValueTypeConstraint &&
               NotNullConstraint == other.NotNullConstraint &&
               ConstructorConstraint == other.ConstructorConstraint &&
               UnmanagedTypeConstraint == other.UnmanagedTypeConstraint &&
               ConstraintTypesForCodeGeneration.SequenceEqual(other.ConstraintTypesForCodeGeneration);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GenericTypeConstraint)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(ReferenceTypeConstraint, NullableReferenceTypeConstraint)
            .Add(ValueTypeConstraint, NotNullConstraint, ConstructorConstraint, UnmanagedTypeConstraint)
            .AddSequence(ConstraintTypesForCodeGeneration);
    }
}