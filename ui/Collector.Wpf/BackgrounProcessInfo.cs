using System.Threading.Tasks;

namespace Collector.Wpf;

internal class BackgrounProcessInfo
{
    public string Name { get; }

    public Task BackgroundTask { get; }

    public BackgrounProcessInfo(string name, Task task)
    {
        Name = name;
        BackgroundTask = task;
    }
}