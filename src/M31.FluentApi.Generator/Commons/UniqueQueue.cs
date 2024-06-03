namespace M31.FluentApi.Generator.Commons;

internal class UniqueQueue<T>
{
    private readonly Queue<T> queue;
    private readonly HashSet<T> hashSet;

    public UniqueQueue()
    {
        queue = new Queue<T>();
        hashSet = new HashSet<T>();
    }

    public bool IsEmpty => queue.Count == 0;

    public bool EnqueueIfNotPresent(T item)
    {
        bool added = hashSet.Add(item);

        if (added)
        {
            queue.Enqueue(item);
        }

        return added;
    }

    public T Dequeue()
    {
        T item = queue.Dequeue();
        hashSet.Remove(item);
        return item;
    }
}