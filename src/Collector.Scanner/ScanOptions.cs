using System.Collections.Generic;

namespace Collector.Scanner;

public class ScanOptions
{
    public bool ScanSubfolders { get; init; } = false;

    public List<string> Hashes { get; init; } = new List<string> { "crc32", "md5", "sha1" };

    // TODO: add support for known archives to act as a sets
    // TODO: think about folders as sets - is it really usefull?
    public bool ArchivesAsSet { get; init; } = false;

    public List<string> KnownArchiveExtensions { get; } = new List<string> { ".zip", ".7z" };
}