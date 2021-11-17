using Microsoft.Extensions.Configuration;

using System;
using System.Threading.Tasks;

namespace Collector.Update;

public interface IUpdater
{
    Task Update(string path);

    const string RepositoryUrlKey = "update:repositoruUrl";

    const string LocalRepositoryPath = "update:localRepositoryPath";

    static IUpdater Create(string type, IConfiguration configuration)
    {
        return type switch
        {
            "github" => new GithubUpdater(configuration),
            _ => throw new InvalidOperationException($"Unsupported updater of type '{type}'.")
        };
    }
}