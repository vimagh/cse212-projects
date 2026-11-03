public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.
    /// The item is always added to the back of the queue regardless
    /// of the priority.
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Start by assuming the first item has the highest priority.
        var highPriorityIndex = 0;

        // Check every remaining item in the queue.
        for (int index = 1; index < _queue.Count; index++)
        {
            // Only change the selected item when a STRICTLY higher
            // priority is found. This preserves FIFO for equal priorities.
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
        }

        // Save the value before removing the item.
        var value = _queue[highPriorityIndex].Value;

        // A dequeue operation must actually remove the selected item.
        _queue.RemoveAt(highPriorityIndex);

        return value;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY THE CODE IN THIS METHOD
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}