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
        throw new System.NotImplementedException();
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new System.NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new System.NotImplementedException();
    }

    public bool IsReadOnly { get; } = false;

    public void Add(TKey key, TValue value)
    {
        throw new System.NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        throw new System.NotImplementedException();
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        throw new System.NotImplementedException();
    }

    public TValue this[TKey key]
    {
        get => throw new System.NotImplementedException();
        set => throw new System.NotImplementedException();
    }

    public ICollection<TKey> Keys { get; } = new List<TKey>();
    public ICollection<TValue> Values { get; } = new List<TValue>();
}