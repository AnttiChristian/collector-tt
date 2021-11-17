using System;
using System.Collections.Generic;

namespace Collector.Data
{
    public class DatfileRecord : EntityRecord
    {
        public Guid HardwareId { get; set; }

        public string Version { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;

        public List<DatfileFolder> Folders { get; set; } = new();

        public List<ReleaseRecord> Releases { get; set; } = new();
    }
}
