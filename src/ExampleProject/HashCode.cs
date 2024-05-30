using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public struct HashCode
{
    private int hash;

    public HashCode()
    {
        hash = 17;
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void Add<T>(T value)
    {
        unchecked
        {
            hash = hash * 23 + value?.GetHashCode() ?? 0;
        }
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void Add<T1, T2>(T1 value1, T2 value2)
    {
        unchecked
        {
            hash = hash * 23 + value1?.GetHashCode() ?? 0;
            hash = hash * 23 + value2?.GetHashCode() ?? 0;
        }
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void Add<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
    {
        unchecked
        {
            hash = hash * 23 + value1?.GetHashCode() ?? 0;
            hash = hash * 23 + value2?.GetHashCode() ?? 0;
            hash = hash * 23 + value3?.GetHashCode() ?? 0;
        }
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void Add<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
    {
        unchecked
        {
            hash = hash * 23 + value1?.GetHashCode() ?? 0;
            hash = hash * 23 + value2?.GetHashCode() ?? 0;
            hash = hash * 23 + value3?.GetHashCode() ?? 0;
            hash = hash * 23 + value4?.GetHashCode() ?? 0;
        }
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void Add<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
    {
        unchecked
        {
            hash = hash * 23 + value1?.GetHashCode() ?? 0;
            hash = hash * 23 + value2?.GetHashCode() ?? 0;
            hash = hash * 23 + value3?.GetHashCode() ?? 0;
            hash = hash * 23 + value4?.GetHashCode() ?? 0;
            hash = hash * 23 + value5?.GetHashCode() ?? 0;
        }
    }

    [FluentMethod(0)]
    [FluentContinueWith(0)]
    public void AddSequence<T>(IEnumerable<T> items)
    {
        unchecked
        {
            foreach (T item in items)
            {
                hash = hash * 23 + item?.GetHashCode() ?? 0;
            }

        }
    }

    [FluentMethod(0)]
    [FluentReturn]
    public int Value()
    {
        return hash;
    }
}