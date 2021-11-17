using Collector.Collection;

namespace Collector.Compression;

public interface IArchiveManager
{
    void Open(string filename);

    List<Blob> Scan(IEnumerable<string> hashTypes);

    static IArchiveManager Create(string archiveType)
    {

        return archiveType switch
        {
            ".zip" => new ZipArchiveManager(),
            ".7z" => new SevenZipArchiveManager(),
            _ => throw new NotSupportedException($"Archive of type '{archiveType}' is not supported.")
        };
    }
}