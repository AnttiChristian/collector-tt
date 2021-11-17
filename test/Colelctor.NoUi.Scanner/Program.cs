// See https://aka.ms/new-console-template for more information
using Collector.Data;
using Collector.Import;

using System.Text.Json;

var previousHardwareKey = string.Empty;

var files = Directory.GetFiles("c:\\Temp\\cmpro\\datfiles\\tosec\\", "*.dat");
var tosecImporter = new TosecImporter();
var hardware = new List<HardwareMappingRecord>();
var manufacturerPath = string.Empty;
var hardwarePath = string.Empty;

Repository.Load("c:\\Users\\Developer\\AppData\\Local\\Collector.TT\\Database\\");

foreach (var file in files)
{
    var filename = Path.GetFileName(file);
    var fileNameParts = filename.Split(" - ");

    var hwMapping = Repository.HardwareMapping.FirstOrDefault(hm => hm.Code == fileNameParts[0]);
    if (hwMapping is null)
    {
        hwMapping = new HardwareMappingRecord
        {
            Code = fileNameParts[0],
            HardwareId = Guid.Empty
        };

        hardware.Add(hwMapping);
        manufacturerPath = fileNameParts[0];
        hardwarePath = string.Empty;
    }
    else
    {
        var machine = Repository.Hardware.FirstOrDefault(hw => hw.Id == hwMapping.HardwareId);
        if (machine is null) Console.WriteLine($"Mahchine : {hwMapping.Code}");
        var manufacturer = Repository.Manufacturers.FirstOrDefault(m => m.Id == machine!.ManufacturerId);
        manufacturerPath = manufacturer!.Code;
        hardwarePath = machine!.Code;
    }

    var path = Path.Combine("dats", manufacturerPath, hardwarePath);
    Directory.CreateDirectory(path);
    Console.WriteLine($"Importing '{file}'.");
    var dumps = tosecImporter.Import(file);
    File.WriteAllText(Path.Combine(path, filename), JsonSerializer.Serialize(dumps, new JsonSerializerOptions { WriteIndented = true }));
    previousHardwareKey = fileNameParts[0];
}

//File.WriteAllText("hardwareMapping.json", JsonSerializer.Serialize(hardware, new JsonSerializerOptions { WriteIndented = true }));
