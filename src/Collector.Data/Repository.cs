using Collector.Database;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Collector.Data
{
    public static class Repository
    {
        public static readonly List<ManufacturerRecord> Manufacturers = new();

        public static readonly List<HardwareRecord> Hardware = new();

        public static readonly List<CategoryRecord> Categories = new();

        public static readonly List<HardwareMappingRecord> HardwareMapping = new();

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static void Load(string path)
        {
            Manufacturers.Clear();
            Hardware.Clear();
            HardwareMapping.Clear();

            if (File.Exists(Path.Combine(path, "manufacturers.json")))
            {
                Manufacturers.AddRange(
                    JsonSerializer.Deserialize<List<ManufacturerRecord>>(
                        File.ReadAllText(Path.Combine(path, "manufacturers.json")) ?? "[]", _jsonSerializerOptions)?
                        .OrderBy(s => s.Name) ?? Enumerable.Empty<ManufacturerRecord>());

                Hardware.AddRange(
                    JsonSerializer.Deserialize<List<HardwareRecord>>(
                        File.ReadAllText(Path.Combine(path, "hardware.json")) ?? "[]", _jsonSerializerOptions)?
                        .OrderBy(s => s.Name) ?? Enumerable.Empty<HardwareRecord>());

                HardwareMapping.AddRange(
                    JsonSerializer.Deserialize<List<HardwareMappingRecord>>(
                        File.ReadAllText(Path.Combine(path, "hardwareMapping.tosec.json")) ?? "[]", _jsonSerializerOptions)
                        ?? Enumerable.Empty<HardwareMappingRecord>());
            }
        }
    }
}
