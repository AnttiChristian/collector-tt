namespace Collector.Collection;

public class Manufacturer : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<Hardware> Hardware { get; } = new();

    public override string ToString() => $"{Name} ({Code})";
}