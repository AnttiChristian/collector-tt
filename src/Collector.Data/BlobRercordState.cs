using System;

namespace Collector.Data
{
    [Flags]
    public enum BlobRercordState : byte
    {
        Ok = 0,
        Bad = 1
    }
}
