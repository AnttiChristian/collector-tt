namespace Collector.Collection;

public class Collection
{
    public string Name { get; set; } = "Empty collection";

    public string Version { get; set; } = "0.0.0.0";

    public List<Manufacturer> Manufacturers { get; } = new();

    public override string ToString() => $"{Name} ({Version})";
}
