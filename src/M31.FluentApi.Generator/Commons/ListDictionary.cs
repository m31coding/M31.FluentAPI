using System.Runtime.Serialization;

namespace M31.FluentApi.Generator.Commons;

internal class ListDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>
    where TKey : notnull
{
    public ListDictionary()
    {
    }

    public ListDictionary(IDictionary<TKey, List<TValue>> dictionary)
        : base(dictionary)
    {
    }

    public ListDictionary(IEqualityComparer<TKey> comparer)
        : base(comparer)
    {
    }

    public ListDictionary(int capacity)
        : base(capacity)
    {
    }

    public ListDictionary(IDictionary<TKey, List<TValue>> dictionary, IEqualityComparer<TKey> comparer)
        : base(dictionary, comparer)
    {
    }

    public ListDictionary(int capacity, IEqualityComparer<TKey> comparer)
        : base(capacity, comparer)
    {
    }

    protected ListDictionary(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public void AddItem(TKey key, TValue item)
    {
        if (TryGetValue(key, out List<TValue>? items))
        {
            items.Add(item);
        }
        else
        {
            this[key] = new List<TValue>() { item };
        }
    }

    public bool RemoveItem(TKey key, TValue item)
    {
        if (!TryGetValue(key, out List<TValue>? items))
        {
            return false;
        }

        return items.Remove(item);
    }
}