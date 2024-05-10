using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.Generics;

internal class GenericTypeParameter
{
    internal static GenericTypeParameter Create(ITypeParameterSymbol typeParameter)
    {
        string parameterName = CodeTypeExtractor.GetTypeForCodeGeneration(typeParameter);
        GenericTypeConstraints constraints = GenericTypeConstraints.Create(typeParameter);
        return new GenericTypeParameter(parameterName, typeParameter.Ordinal, constraints);
    }

    private GenericTypeParameter(string parameterName, int parameterPosition, GenericTypeConstraints constraints)
    {
        ParameterName = parameterName;
        ParameterPosition = parameterPosition;
        Constraints = constraints;
    }

    internal string ParameterName { get; }
    internal int ParameterPosition { get; }
    internal GenericTypeConstraints Constraints { get; }

    protected bool Equals(GenericTypeParameter other)
    {
        return ParameterName == other.ParameterName && Constraints.Equals(other.Constraints);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GenericTypeParameter)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(ParameterName).Add(Constraints);
    }
}