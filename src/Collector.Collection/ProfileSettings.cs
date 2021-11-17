namespace Collector.Collection;

/// <summary>
/// Class containing settings for each profile in collection. 
/// These settings can be applied to each node in collection tree inlcuding categories.
/// </summary>
public class ProfileSettings
{
    /// <summary>
    /// Gets or sets the folder where files for specific node are stored.
    /// </summary>
    public string CollectionFolder { get; set; } = string.Empty;
}