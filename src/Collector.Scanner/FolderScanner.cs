using Collector.Collection;
using Collector.Cryptography;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;

namespace Collector.Scanner;

public class FolderScanner
{
    private ILogger? _logger;

    public FolderScanner(ILogger? logger)
    {
        _logger = logger;
    }

    private Dictionary<string, string> ComputeHashes(FileInfo fileInfo, IEnumerable<string> hashes)
    {
        var result = new Dictionary<string, string>();
        using var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(fileStream);

        var buffer = reader.ReadBytes((int)fileInfo.Length);
        foreach (var hash in hashes)
        {
            var algorithm = IHashAlgorithm.Create(hash);
            var hashBytes = algorithm.Compute(buffer);
            result.Add(hash, Convert.ToHexString(hashBytes));
        }

        return result;
    }

    public IEnumerable<DumpScanResult> ScanFolder(string path, ScanOptions options, string filter = "*.*")
    {
        var result = new List<DumpScanResult>();
        var searchOption = options.ScanSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        var archiveScanner = options.ArchivesAsSet ? new ArchiveScanner() : null;
        foreach (var filename in Directory.GetFiles(path, filter, searchOption))
        {
            try
            {
                var fileInfo = new FileInfo(filename);
                DumpScanResult blobSet;
                if (options.ArchivesAsSet && options.KnownArchiveExtensions.Contains(Path.GetExtension(filename)))
                {
                    blobSet = archiveScanner!.ScanArchive(filename, options);
                }
                else
                {
                    var blob = new Blob
                    {
                        Filename = filename,
                        Size = (uint)fileInfo.Length
                    };

                    foreach (var hash in ComputeHashes(fileInfo, options.Hashes))
                    {
                        blob.Hashes.Add(hash.Key, hash.Value);
                    }

                    blobSet = new DumpScanResult
                    {
                        Path = filename,
                    };

                    blobSet.Blobs.Add(blob);
                }

                result.Add(blobSet);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception.Message);
            }
        }

        return result;
    }
}
