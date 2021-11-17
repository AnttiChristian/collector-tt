namespace Collector.Cryptography;

public interface IHashAlgorithm
{
    byte[] Compute(byte[] data);

    static IHashAlgorithm Create(string name)
    {
        return name switch
        {
            "crc32" => new Crc32HashAlgorithm(),
            "md5" => new MD5HashAlgorithm(),
            "sha1" => new SHA1HashAlgorithm(),
            _ => throw new NotSupportedException($"Hash algorithm '{name} is not supported.")
        };
    }
}