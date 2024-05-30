// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi("CreateTree")]
public class Node<T>
{
    private Node()
    {
    }

    public Node(T value, Node<T>? left, Node<T>? right)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    public T Value { get; private set; }
    public Node<T>? Left { get; private set; }
    public Node<T>? Right { get; private set; }

    [FluentMethod(0)]
    private void Root(T value)
    {
        Value = value;
    }

    [FluentMethod(1, "Left")]
    private void SetLeftNode(T value, Func<CreateTree<T>.ILeftLeftNull, Node<T>>? createTree = null)
    {
        if (createTree == null)
        {
            Left = new Node<T>(value, null, null);
        }
        else
        {
            Left = createTree(CreateTree<T>.InitialStep().Root(value));
        }
    }

    [FluentMethod(1)]
    private void LeftNull()
    {
        Left = null;
    }

    [FluentMethod(2, "Right")]
    private void SetRightNode(T value, Func<CreateTree<T>.ILeftLeftNull, Node<T>>? createTree = null)
    {
        if (createTree == null)
        {
            Right = new Node<T>(value, null, null);
        }
        else
        {
            Right = createTree(CreateTree<T>.InitialStep().Root(value));
        }
    }

    [FluentMethod(2)]
    private void RightNull()
    {
        Right = null;
    }
}