using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> _elements = new List<(T, float)>();

    public int Count => _elements.Count;

    public void Enqueue(T item, float priority)
    {
        _elements.Add((item, priority));
    }

    public T Dequeue()
    {
        var bestIndex = 0;

        for (var i = 1; i < _elements.Count; i++)
        {
            if (_elements[i].priority < _elements[bestIndex].priority)
                bestIndex = i;
        }

        var bestItem = _elements[bestIndex];
        _elements.RemoveAt(bestIndex);
        return bestItem.item;
    }

    public bool Contains(T item)
    {
        foreach (var element in _elements)
        {
            if (EqualityComparer<T>.Default.Equals(element.item, item))
                return true;
        }
        return false;
    }
}