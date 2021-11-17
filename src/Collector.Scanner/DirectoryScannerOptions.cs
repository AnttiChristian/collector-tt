namespace Collector.Scanner;

public class DirectoryScannerOptions
{
    public readonly static DirectoryScannerOptions DefaultOptions = new();

    public string BasePath { get; set; } = string.Empty;

    public bool Recurisive { get; set; } = true;

    public string Filter { get; set; } = "*.*";
}
