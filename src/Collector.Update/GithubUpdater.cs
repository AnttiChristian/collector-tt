using LibGit2Sharp;

using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Collector.Update;

internal class GithubUpdater : IUpdater
{
    private readonly IConfiguration _configuration;

    public GithubUpdater(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Update(string path)
    {
        if (!Directory.Exists(Path.Combine(path, ".git")))
        {
            _ = await Task.Run(() => Repository.Clone(_configuration["update:repositoruUrl"], path));
        }
        else
        {
            var signature = new Signature("application", "application@app.com", DateTime.Now);
            var pullOptions = new PullOptions();
            var repository = new Repository(path);
            _ = await Task.Run(() => Commands.Pull(repository, signature, pullOptions));
        }
    }
}
