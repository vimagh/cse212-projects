
using System.Text.Json.Serialization;

public class FeatureCollection
{
    // The JSON contains a list of earthquake features.
    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = new();
}

public class Feature
{
    // Each feature contains earthquake properties.
    [JsonPropertyName("properties")]
    public EarthquakeProperties Properties { get; set; } = new();
}

public class EarthquakeProperties
{
    // Location of the earthquake.
    [JsonPropertyName("place")]
    public string? Place { get; set; }

    // Magnitude of the earthquake.
    [JsonPropertyName("mag")]
    public double? Mag { get; set; }
}
