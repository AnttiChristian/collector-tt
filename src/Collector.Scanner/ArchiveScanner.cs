using Collector.Compression;

using System.IO;

namespace Collector.Scanner;

public class ArchiveScanner
{
    public DumpScanResult ScanArchive(string filename, ScanOptions options)
    {
        var result = new DumpScanResult();
        var archiveManager = IArchiveManager.Create(Path.GetExtension(filename));
        archiveManager.Open(filename);
        result.Blobs.AddRange(archiveManager.Scan(options.Hashes));

        return result;
    }
}