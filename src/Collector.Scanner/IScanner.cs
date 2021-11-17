using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collector.Scanner;

public interface IScanner<T>
{
    Task<IEnumerable<T>> Scan(string path);
}