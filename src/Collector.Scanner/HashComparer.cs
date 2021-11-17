using System.Collections.Generic;

namespace Collector.Scanner;

public static class HashComparer
{
    public static decimal Compare(Dictionary<string, string> first, Dictionary<string, string> second)
    {
        var increment = (decimal)1 / first.Count;
        decimal result = 0;
        foreach (var key in first.Keys)
        {
            if (second.TryGetValue(key, out var hash) && (hash == first[key])) result += increment;
        }

        return result;
    }
}