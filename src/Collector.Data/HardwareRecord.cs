using Collector.Data;

using System;
using System.Collections.Generic;

namespace Collector.Database;

public class HardwareRecord : EntityRecord
{
    public string Name { get; set; } = "unknown";

    public Guid ManufacturerId { get; set; }

    public Dictionary<string, string> ExternalCodes { get; set; } = new Dictionary<string, string>();

    public override string ToString() => $"{Name} ({Code})";
}
