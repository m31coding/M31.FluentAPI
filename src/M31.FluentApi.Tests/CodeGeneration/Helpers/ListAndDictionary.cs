using System;
using System.Collections.Generic;

namespace M31.FluentApi.Tests.CodeGeneration.Helpers;

/// <summary>
/// Dummy class to test generic constraints, inherits from a class and implements an interface.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class ListAndDictionary<TKey, TValue> : List<TKey>, IDictionary<TKey, TValue>
{
    public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        throw new NotSupportedException();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        throw new NotSupportedException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotSupportedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotSupportedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotSupportedException();
    }

    public bool IsReadOnly { get; } = false;

    public void Add(TKey key, TValue value)
    {
        throw new NotSupportedException();
    }

    public bool ContainsKey(TKey key)
    {
        throw new NotSupportedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new NotSupportedException();
    }

    public TValue this[TKey key]
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public ICollection<TKey> Keys { get; } = new List<TKey>();
    public ICollection<TValue> Values { get; } = new List<TValue>();
}