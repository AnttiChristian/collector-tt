using System.Collections.Generic;

namespace Collector.Data
{
    public class ProductRecord : EntityRecord
    {
        public string Manufacturer { get; set; } = string.Empty;

        public List<ReleaseRecord> Releases { get; set; } = new();
    }
}
