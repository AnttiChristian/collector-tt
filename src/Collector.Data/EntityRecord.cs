// Collector.TT project

using System;

namespace Collector.Data
{
    public abstract class EntityRecord
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = string.Empty;
    }
}
