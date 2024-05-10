namespace M31.FluentApi.Generator.CodeBuilding;

internal class Generics
{
    internal Generics()
    {
        Parameters = new GenericParameters();
        Constraints = new GenericConstraints();
    }

    internal Generics(Generics generics)
    {
        Parameters = new GenericParameters(generics.Parameters);
        Constraints = new GenericConstraints(generics.Constraints);
    }

    internal GenericParameters Parameters { get; }
    internal GenericConstraints Constraints { get; }

    internal void AddGenericParameter(string parameter, IEnumerable<string> constraints)
    {
        Parameters.Add(parameter);
        IReadOnlyCollection<string> constraintsCollection = constraints.ToArray();
        if (constraintsCollection.Count > 0)
        {
            Constraints.Add(parameter, constraintsCollection);
        }
    }
}