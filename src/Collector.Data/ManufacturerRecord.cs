namespace Collector.Data;

public class ManufacturerRecord : EntityRecord
{
    public string Name { get; set; } = string.Empty;

    public override string ToString() => $"{Name} ({Code})";
}
