namespace Collector.Collection;

public enum BlobQuality
{
    /// <summary>
    /// Quality needs to be verfied.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Quality is verified from multiple sources and 100% good.
    /// </summary>
    Verified = 1,
    /// <summary>
    /// Quality is trusted.
    /// </summary>
    Good = 2,

    /// <summary>
    ///Content of the blob is verified as bad (errors during dumping etc.)
    /// </summary>
    Bad = 3,
}