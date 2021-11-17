using Collector.Collection;
using Collector.Cryptography;

using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;

namespace Collector.Compression;

internal class SevenZipArchiveManager : IArchiveManager
{
    private SevenZipArchive? _archive;

    public void Open(string filename)
    {
        _archive = SevenZipArchive.Open(filename);
    }

    public List<Blob> Scan(IEnumerable<string> hashTypes)
    {
        var result = new List<Blob>();
        if (_archive == null) return result;

        foreach (var entry in _archive.Entries)
        {
            if (entry.IsDirectory) continue;

            var blob = new Blob
            {
                Size = (uint)entry.Size,
                Filename = entry.Key
            };

            using var memoryStream = new MemoryStream();
            entry.WriteTo(memoryStream);

            foreach (string hashType in hashTypes)
            {
                var hashAlgo = IHashAlgorithm.Create(hashType);
                blob.Hashes.Add(hashType, Convert.ToHexString(hashAlgo.Compute(memoryStream.ToArray())));
            }

            result.Add(blob);
        }

        return result;
    }
}