using Collector.Data;

namespace Collector.Collection;

public class Dump : EntityRecord
{
    public List<Blob> Blobs { get; set; } = new List<Blob>();
}