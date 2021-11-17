using System.Security.Cryptography;

namespace Collector.Cryptography;

internal class MD5HashAlgorithm : IHashAlgorithm
{
    public byte[] Compute(byte[] data)
    {
        var md5 = MD5.Create();
        return md5.ComputeHash(data);
    }
}