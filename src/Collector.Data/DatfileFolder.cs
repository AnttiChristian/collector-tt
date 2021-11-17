using System.Collections.Generic;

namespace Collector.Data
{
    public record DatfileFolder
    {
        public string ShortName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<ReleaseRecord> Releases { get; set; } = new();
    }
}
