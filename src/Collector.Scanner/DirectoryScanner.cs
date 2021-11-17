using Collector.Data;

using Force.Crc32;

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Collector.Scanner;

public class DirectoryScanner : IScanner<FileScanResult>
{
    private readonly DirectoryScannerOptions _options;

    public DirectoryScanner()
    {
        _options = DirectoryScannerOptions.DefaultOptions;
    }

    public DirectoryScanner(DirectoryScannerOptions options)
    {
        _options = options;
    }

    private async Task<FileScanResult> ComputeFile(string path)
    {
        var fileinfo = new FileInfo(path);
        var result = new FileScanResult
        {
            Path = path,
            Size = fileinfo.Length
        };

        var buffer = await Task.Run(() => File.ReadAllBytes(path));
        result.Sha1 = BitConverter.ToString(SHA1.Create().ComputeHash(buffer)).Replace("-", "").ToLower();
        result.Md5 = BitConverter.ToString(MD5.Create().ComputeHash(buffer)).Replace("-", "").ToLower();
        result.Crc32 = Crc32Algorithm.Compute(buffer).ToString("x8");

        return result;
    }

    public async Task<IEnumerable<FileScanResult>> Scan(string path)
    {
        var result = new List<FileScanResult>();
        var files = await Task.Run(() =>
            Directory.GetFiles(
                Path.Combine(_options.BasePath, path),
                _options.Filter, _options.Recurisive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));

        foreach (var filename in files)
        {
            result.Add(await ComputeFile(filename));
        }

        return result;
    }
}