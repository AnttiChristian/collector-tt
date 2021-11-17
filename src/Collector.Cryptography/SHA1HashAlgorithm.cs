using System.Security.Cryptography;

namespace Collector.Cryptography;

internal class SHA1HashAlgorithm : IHashAlgorithm
{
    public byte[] Compute(byte[] data)
    {
        var sha1 = SHA1.Create();
        return sha1.ComputeHash(data);
    }
}