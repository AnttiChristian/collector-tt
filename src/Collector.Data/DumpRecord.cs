using System.Collections.Generic;

namespace Collector.Data
{
    public record DumpRecord
    {
        public string Name { get; set; } = string.Empty;

        public List<BlobRecord> Blobs { get; set; } = new();

        //public List<ModRecord> Mods { get; set; } = new();
    }
}
