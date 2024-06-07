namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration.LoopHandling;

// Tarjan's strongly connected components algorithm, see also
// https://en.wikipedia.org/wiki/Tarjan%27s_strongly_connected_components_algorithm.
internal static class TarjansSccAlgorithm<T>
    where T : notnull
{
    internal static IReadOnlyCollection<IReadOnlyCollection<T>> GetStronglyConnectedComponents(
        IReadOnlyCollection<T> values, Func<T, IEnumerable<T>> getSuccessors, bool includeSingleVertices)
    {
        IReadOnlyCollection<Vertex> vertices = values.Select(e => new Vertex(e)).ToArray();
        Dictionary<T, Vertex> valueToVertex = vertices.ToDictionary(v => v.Value, v => v);
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            GetStronglyConnectedComponents(vertices, valueToVertex, getSuccessors, includeSingleVertices);
        return components.Select(c => c.Select(v => v.Value).ToArray()).ToArray();
    }

    private static IReadOnlyCollection<IReadOnlyCollection<Vertex>> GetStronglyConnectedComponents(
        IReadOnlyCollection<Vertex> vertices,
        Dictionary<T, Vertex> valueToVertex,
        Func<T, IEnumerable<T>> getSuccessors,
        bool includeSingleVertices)
    {
        List<List<Vertex>> components = new List<List<Vertex>>();
        int index = 0;
        Stack<Vertex> stack = new Stack<Vertex>();

        foreach (Vertex vertex in vertices)
        {
            if (!vertex.Index.HasValue)
            {
                StrongConnect(vertex);
            }
        }

        void StrongConnect(Vertex v)
        {
            v.Index = index;
            v.LowLink = index++;
            stack.Push(v);
            v.OnStack = true;

            foreach (Vertex w in getSuccessors(v.Value).Select(value => valueToVertex[value]))
            {
                if (!w.Index.HasValue)
                {
                    StrongConnect(w);
                    v.LowLink = Math.Min(v.LowLink, w.LowLink);
                }
                else if (w.OnStack)
                {
                    v.LowLink = Math.Min(v.LowLink, w.Index.Value);
                }
            }

            if (v.LowLink == v.Index)
            {
                List<Vertex> component = new List<Vertex>();

                while (true)
                {
                    Vertex w = stack.Pop();
                    w.OnStack = false;
                    component.Add(w);

                    if (w == v)
                    {
                        break;
                    }
                }

                if (component.Count > 1 || includeSingleVertices)
                {
                    components.Add(component);
                }
            }
        }

        return components;
    }

    private class Vertex
    {
        internal T Value { get; }
        internal int? Index { get; set; }
        internal int LowLink { get; set; }
        internal bool OnStack { get; set; }

        internal Vertex(T value)
        {
            Value = value;
            Index = null;
            LowLink = 0;
            OnStack = false;
        }
    }
}