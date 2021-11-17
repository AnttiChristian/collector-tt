using Collector.Data;
using Collector.Update;

using Microsoft.Extensions.Configuration;

using System.Linq;

namespace Collector.Collection;

public static class CollectionManager
{
    public static readonly Collection Collection = new Collection();

    public static async Task BuildCollection(IConfiguration configuration)
    {
        Collection.Manufacturers.Clear();

        var dataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            configuration["application:applicationName"],
            configuration["application:databaseRelativePath"]);

        IUpdater updater = IUpdater.Create("github", configuration);
        await updater.Update(dataFolder);

        Repository.Load(dataFolder);

        foreach (var manufacturerRecord in Repository.Manufacturers)
        {
            var manufacturer = new Manufacturer
            {
                Code = manufacturerRecord.Code,
                Id = manufacturerRecord.Id,
                Name = manufacturerRecord.Name
            };

            foreach (var hardwareRecord in Repository.Hardware.Where(h => h.ManufacturerId == manufacturer.Id))
            {
                var hardware = new Hardware
                {
                    Code = hardwareRecord.Code,
                    Id = hardwareRecord.Id,
                    ManufacturerId = hardwareRecord.ManufacturerId,
                    Name = hardwareRecord.Name
                };

                manufacturer.Hardware.Add(hardware);
            }

            Collection.Manufacturers.Add(manufacturer);
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