using System;

namespace Collector.Data
{
    public record HardwareMappingRecord
    {
        public string Code { get; set; } = string.Empty;

        public Guid HardwareId { get; set; } = Guid.Empty;
    }
}
