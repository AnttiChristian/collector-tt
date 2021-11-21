namespace Collector.Collection;

public class Hardware : Entity
{
    public string Name { get; set; } = "unknown";

    public Guid ManufacturerId { get; set; }

    public Dictionary<string, string> ExternalCodes { get; set; } = new Dictionary<string, string>();

    public override string ToString() => $"{Name} ({Code})";

}