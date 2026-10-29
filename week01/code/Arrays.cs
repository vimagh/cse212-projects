public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number'
    /// followed by multiples of 'number'.
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start

        // Step 1: Create a new double array that has the size specified by length.
        // Step 2: Loop through every index in the new array, starting at index 0.
        // Step 3: For each index, calculate the correct multiple of number.
        //         Since array indexes start at 0, use (index + 1) so that
        //         the first value will be number * 1, the second will be
        //         number * 2, and so on.
        // Step 4: Store each calculated multiple in its correct position in the array.
        // Step 5: After all positions have been filled, return the completed array.

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.
    /// For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3,
    /// the list after the function runs should be
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing
    /// data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start

        // Step 1: Find the index where the last 'amount' values begin.
        //         This is found by subtracting amount from the total number
        //         of items in the list.
        // Step 2: Copy the last 'amount' values into a temporary list
        //         using GetRange.
        // Step 3: Remove those values from their original position
        //         using RemoveRange.
        // Step 4: Insert the saved values at index 0 using InsertRange.
        // Step 5: The original list is now modified so that the last
        //         'amount' values have been moved to the front.

        int startIndex = data.Count - amount;

        List<int> valuesToMove = data.GetRange(startIndex, amount);

        data.RemoveRange(startIndex, amount);

        data.InsertRange(0, valuesToMove);
    }
}