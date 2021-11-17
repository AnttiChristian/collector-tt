using System.Collections.Generic;

namespace Collector.Data
{
    public record BlobRecord
    {
        public string Name { get; set; } = string.Empty;

        public uint Size { get; set; }

        public Dictionary<string, string> Hashes { get; } = new();

        public BlobRercordState State { get; set; } = BlobRercordState.Ok;
    }
}
