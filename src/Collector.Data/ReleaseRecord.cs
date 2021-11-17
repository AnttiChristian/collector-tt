using System.Collections.Generic;

namespace Collector.Data
{
    public class ReleaseRecord : EntityRecord
    {
        public string Title { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public WeakDate ReleaseDate { get; set; } = new();

        public List<DumpRecord> Dumps { get; set; } = new();
    }
}
