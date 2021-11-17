using Collector.Collection;
using Collector.Cryptography;

using SharpCompress.Archives;
using SharpCompress.Archives.Zip;

namespace Collector.Compression;

internal class ZipArchiveManager : IArchiveManager
{
    private ZipArchive _archive = ZipArchive.Create();

    public void Open(string filename)
    {
        _archive = ZipArchive.Open(filename);
    }

    public List<Blob> Scan(IEnumerable<string> hashTypes)
    {
        var result = new List<Blob>();

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