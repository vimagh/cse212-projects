using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
    // Store words we have already encountered.
    var seen = new HashSet<string>();

    // Store the matching pairs.
    var pairs = new List<string>();

    foreach (var word in words)
    {
        // Reverse the two characters.
        string reversed = $"{word[1]}{word[0]}";

        // Ignore words like "aa" that reverse to themselves.
        if (word != reversed && seen.Contains(reversed))
        {
            pairs.Add($"{word} & {reversed}");
        }

        // Remember the current word.
        seen.Add(word);
    }

    return pairs.ToArray();
  }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
    var degrees = new Dictionary<string, int>();

    foreach (var line in File.ReadLines(filename))
    {
        var fields = line.Split(",");

        // The fourth column contains the education category.
        string degree = fields[3].Trim();

        // If this degree already exists, increase its count.
        if (degrees.ContainsKey(degree))
        {
            degrees[degree]++;
        }
        else
        {
            // First occurrence of this degree.
            degrees[degree] = 1;
        }
    }

    return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    
public static bool IsAnagram(string word1, string word2)
{
    // Store the number of times each letter appears.
    var letterCounts = new Dictionary<char, int>();

    // Count letters in the first word.
    foreach (char letter in word1)
    {
        if (letter == ' ')
            continue;

        char currentLetter = char.ToLowerInvariant(letter);

        if (letterCounts.ContainsKey(currentLetter))
        {
            letterCounts[currentLetter]++;
        }
        else
        {
            letterCounts[currentLetter] = 1;
        }
    }

    // Check the letters in the second word.
    foreach (char letter in word2)
    {
        if (letter == ' ')
            continue;

        char currentLetter = char.ToLowerInvariant(letter);

        // If the letter is missing, the words are not anagrams.
        if (!letterCounts.ContainsKey(currentLetter))
            return false;

        // Reduce the count for each matching letter.
        letterCounts[currentLetter]--;

        // Remove the letter when its count reaches zero.
        if (letterCounts[currentLetter] == 0)
        {
            letterCounts.Remove(currentLetter);
        }
    }

    // If nothing remains, all letters matched.
    return letterCounts.Count == 0;
}


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    
public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, uri);

        using var jsonStream = client.Send(getRequestMessage)
            .Content.ReadAsStream();

        using var reader = new StreamReader(jsonStream);

        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Convert the JSON data into C# objects.
        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();

        if (featureCollection == null)
        {
            return results.ToArray();
        }

        // Go through every earthquake in the JSON data.
        foreach (var earthquake in featureCollection.Features)
        {
            string place = earthquake.Properties.Place ?? "Unknown location";
            double? magnitude = earthquake.Properties.Mag;

            // Format the earthquake information.
            string description = $"{place} - Mag {magnitude}";

            results.Add(description);
        }

        return results.ToArray();
    }

}