using Collector.Update;

using Microsoft.Extensions.Configuration;

using System.Linq;
using System.Text.Json;

namespace Collector.Collection;

public static class CollectionManager
{
    public static Collection Collection = new();

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public static async Task BuildCollection(IConfiguration configuration)
    {
        Collection?.Manufacturers.Clear();

        var dataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            configuration["application:applicationName"],
            configuration["application:databaseRelativePath"]);

        IUpdater updater = IUpdater.Create("github", configuration);
        await updater.Update(dataFolder);

        if (File.Exists(Path.Combine(dataFolder, "manufacturers.json")))
        {
            Collection = JsonSerializer.Deserialize<Collection>(File.ReadAllText(Path.Combine(dataFolder, "collection.json")), _jsonSerializerOptions)
                ?? new Collection();

            Collection!.Manufacturers.AddRange(
                JsonSerializer.Deserialize<List<Manufacturer>>(
                    File.ReadAllText(Path.Combine(dataFolder, "manufacturers.json")) ?? "[]", _jsonSerializerOptions)?
                    .OrderBy(s => s.Name) ?? Enumerable.Empty<Manufacturer>());

            var hardware = JsonSerializer.Deserialize<List<Hardware>>(
                   File.ReadAllText(Path.Combine(dataFolder, "hardware.json")) ?? "[]", _jsonSerializerOptions)?
                   .OrderBy(s => s.Name) ?? Enumerable.Empty<Hardware>();

            foreach (var manufacturer in Collection.Manufacturers)
            {
                foreach (var hardwareRecord in hardware.Where(h => h.ManufacturerId == manufacturer.Id))
                {
                    var hardwareItem = new Hardware
                    {
                        Code = hardwareRecord.Code,
                        Id = hardwareRecord.Id,
                        ManufacturerId = hardwareRecord.ManufacturerId,
                        Name = hardwareRecord.Name
                    };

                    manufacturer.Hardware.Add(hardwareItem);
                }
            }
        }
    }

    public static string GetRelativePath(Hardware hardware)
    {
        var manufacturer = Collection.Manufacturers.FirstOrDefault(m => m.Id == hardware.ManufacturerId);
        return manufacturer is null
            ? throw new IndexOutOfRangeException()
            : Path.Combine(manufacturer.Code, hardware.Code);
    }
}