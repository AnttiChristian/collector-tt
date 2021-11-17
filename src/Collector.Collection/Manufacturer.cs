using Collector.Data;

namespace Collector.Collection;

public class Manufacturer : ManufacturerRecord
{
    public List<Hardware> Hardware { get; } = new();
}