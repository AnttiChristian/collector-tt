using System.Collections.Generic;
using System.Linq;

namespace Collector.Wpf;

internal class BackgrounProcessManager
{
    private readonly List<BackgrounProcessInfo> _backgroundProcesses = new();

    public void AddProcess(BackgrounProcessInfo processInfo) => _backgroundProcesses.Add(processInfo);

    public IEnumerable<BackgrounProcessInfo> GetActiveTasks() => _backgroundProcesses.Where(bpi => !bpi.BackgroundTask.IsCompleted);

    public IEnumerable<BackgrounProcessInfo> GetCompletedTasks() => _backgroundProcesses.Where(bpi => bpi.BackgroundTask.IsCompleted);
}