using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities. The highest-priority
    // item is added last.
    // Expected Result: "High" should be dequeued first because it has priority 10.
    // Test Result: Initially failed. After fixing PriorityQueue.Dequeue, the test passed.
    // Defect(s) Found: The loop stopped before checking the last item in the queue,
    // so an item at the end could be ignored even when it had the highest priority.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Add multiple items that have the same highest priority.
    // Expected Result: "First" should be dequeued before "Second" because equal
    // priorities must follow FIFO order.
    // Test Result: Initially failed. After changing the priority comparison,
    // the test passed.
    // Defect(s) Found: The >= comparison caused a later item with the same priority
    // to replace the earlier item as the dequeue choice, violating FIFO.
    public void TestPriorityQueue_SamePriorityUsesFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Low", 1);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Scenario: Add two items, dequeue the highest-priority item, and then dequeue again.
    // Expected Result: The first dequeue should return "First" and the second should
    // return "Second", proving that the first item was actually removed.
    // Test Result: Initially failed. After removing the dequeued item from the list,
    // the test passed.
    // Defect(s) Found: Dequeue returned the highest-priority item's value but did not
    // remove the item from the queue.
    public void TestPriorityQueue_DequeueRemovesItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 1);

        var firstResult = priorityQueue.Dequeue();
        var secondResult = priorityQueue.Dequeue();

        Assert.AreEqual("First", firstResult);
        Assert.AreEqual("Second", secondResult);
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with the
    // exact message "The queue is empty."
    // Test Result: Passed. The correct exception type and message were already implemented.
    // Defect(s) Found: None.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                $"Unexpected exception of type {e.GetType()} caught: {e.Message}"
            );
        }
    }
}