using Collector.Scanner;

using System.Linq;
using System.Threading.Tasks;

namespace Collector.Identify
{
    public class IdentifyProcess
    {
        public async Task Execute(string path = "")
        {
            var options = new DirectoryScannerOptions
            {
                BasePath = path
            };

            var directoryScanner = new DirectoryScanner(options);
            var files = await directoryScanner.Scan(path);
            var sortedList = files.OrderBy(f => f.Size).ThenBy(f => f.Crc32).ToList();
        }
    }
}
