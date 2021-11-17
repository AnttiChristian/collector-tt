namespace Collector.Cryptography;

internal class Crc32HashAlgorithm : IHashAlgorithm
{
    public byte[] Compute(byte[] data)
    {
        var crc = Force.Crc32.Crc32Algorithm.Compute(data);
        return BitConverter.GetBytes(crc);
    }
}