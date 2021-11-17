using System;

namespace Collector.Data
{
    public class FileScanResult : IEquatable<FileScanResult>
    {
        public string Path { get; set; } = string.Empty;

        public long Size { get; set; }

        public string Crc32 { get; set; } = string.Empty;

        public string Md5 { get; set; } = string.Empty;

        public string Sha1 { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            return
                obj is FileScanResult other &&
                Equals(other);
        }

        public bool Equals(FileScanResult? other)
        {
            return
                other is not null &&
                Size == other.Size &&
                Crc32 == other.Crc32 &&
                Md5 == other.Md5 &&
                Sha1 == other.Sha1;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
