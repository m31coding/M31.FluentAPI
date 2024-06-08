using System;
using System.Collections.Generic;
using System.Linq;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration.LoopHandling;
using M31.FluentApi.Generator.Commons;
using Xunit;

namespace M31.FluentApi.Tests.Components;

public class TarjansSccAlgorithmTests
{
    [Fact]
    public void CanGetComponentsOfEmptyGraph()
    {
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                Array.Empty<Vertex>(), v => Array.Empty<Vertex>(), false);

        Assert.Equal(0, components.Count);
    }

    [Fact]
    public void CanGetComponentsOfSingleVertexGraph()
    {
        Vertex vertex = new Vertex(0);
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                new Vertex[] { vertex }, v => Array.Empty<Vertex>(), true);

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> expectedComponents = new List<List<Vertex>>()
        {
            new() { vertex },
        };

        AssertEquals(expectedComponents, components);
    }

    [Fact]
    public void CanGetZeroComponentsOfSingleVertexGraph()
    {
        Vertex vertex = new Vertex(0);
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                new Vertex[] { vertex }, v => Array.Empty<Vertex>(), false);

        Assert.Equal(0, components.Count);
    }

    [Fact]
    public void CanGetComponentsOfGraph1()
    {
        // Test from https://en.wikipedia.org/wiki/Strongly_connected_component.
        Vertex vertexA = new Vertex(1);
        Vertex vertexB = new Vertex(2);
        Vertex vertexC = new Vertex(3);

        Vertex[] vertices = new Vertex[] { vertexA, vertexB, vertexC };

        ListDictionary<Vertex, Vertex> successors = new ListDictionary<Vertex, Vertex>()
        {
            [vertexA] = new() { vertexB },
            [vertexB] = new() { vertexA },
            [vertexC] = new() { vertexA },
        };

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                vertices, v => successors[v], false);

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> expectedComponents = new List<List<Vertex>>()
        {
            new() { vertexA, vertexB },
        };

        AssertEquals(expectedComponents, components);
    }

    [Fact]
    public void CanGetComponentsOfGraph2()
    {
        // Test from https://en.wikipedia.org/wiki/Strongly_connected_component.
        Vertex vertexA = new Vertex(1);
        Vertex vertexB = new Vertex(2);
        Vertex vertexC = new Vertex(3);
        Vertex vertexD = new Vertex(4);

        Vertex[] vertices = new Vertex[] { vertexA, vertexB, vertexC };

        ListDictionary<Vertex, Vertex> successors = new ListDictionary<Vertex, Vertex>()
        {
            [vertexA] = new() { vertexB },
            [vertexB] = new() { vertexC },
            [vertexC] = new() { vertexA },
            [vertexD] = new() { },
        };

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                vertices, v => successors[v], false);

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> expectedComponents = new List<List<Vertex>>()
        {
            new() { vertexA, vertexB, vertexC },
        };

        AssertEquals(expectedComponents, components);
    }

    [Fact]
    public void CanGetComponentsOfGraph3()
    {
        // Test from https://en.wikipedia.org/wiki/Strongly_connected_component.
        Vertex vertexA = new Vertex(1);
        Vertex vertexB = new Vertex(2);
        Vertex vertexC = new Vertex(3);
        Vertex vertexD = new Vertex(4);
        Vertex vertexE = new Vertex(5);
        Vertex vertexF = new Vertex(6);
        Vertex vertexG = new Vertex(7);
        Vertex vertexH = new Vertex(8);

        Vertex[] vertices = new Vertex[] { vertexA, vertexB, vertexC, vertexD, vertexE, vertexF, vertexG, vertexH };

        ListDictionary<Vertex, Vertex> successors = new ListDictionary<Vertex, Vertex>()
        {
            [vertexA] = new() { vertexB },
            [vertexB] = new() { vertexE, vertexF },
            [vertexC] = new() { vertexD, vertexG },
            [vertexD] = new() { vertexC, vertexH },
            [vertexE] = new() { vertexA, vertexF },
            [vertexF] = new() { vertexG },
            [vertexG] = new() { vertexF },
            [vertexH] = new() { vertexD, vertexG }
        };

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> components =
            TarjansSccAlgorithm<Vertex>.GetStronglyConnectedComponents(
                vertices, v => successors[v], false);

        IReadOnlyCollection<IReadOnlyCollection<Vertex>> expectedComponents = new List<List<Vertex>>()
        {
            new() { vertexA, vertexB, vertexE },
            new() { vertexC, vertexD, vertexH },
            new() { vertexF, vertexG },
        };

        AssertEquals(expectedComponents, components);
    }

    private static void AssertEquals(
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> expectedComponents,
        IReadOnlyCollection<IReadOnlyCollection<Vertex>> actualComponents)
    {
        Assert.Equal(expectedComponents.Count, actualComponents.Count);
        foreach (var components in Sort(expectedComponents).Zip(Sort(actualComponents)))
        {
            Assert.Equal(components.First, components.Second);
        }

        static IReadOnlyCollection<IReadOnlyCollection<Vertex>> Sort(
            IReadOnlyCollection<IReadOnlyCollection<Vertex>> components)
        {
            return components.Select(c => c.OrderBy(v => v.Value).ToArray()).OrderBy(c => c.First().Value)
                .ToArray();
        }
    }

    private class Vertex
    {
        public Vertex(int value)
        {
            Value = value;
        }

        internal int Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}