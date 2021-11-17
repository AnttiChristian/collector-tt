namespace Collector.Collection;

public class Blob
{
    public string Filename { get; init; } = String.Empty;

    public uint Size { get; init; }

    public Dictionary<string, string> Hashes { get; } = new Dictionary<string, string>();

    public BlobQuality Quality { get; set; } = BlobQuality.Unknown;
}