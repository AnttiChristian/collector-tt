using Collector.Collection;
using Collector.Identify;
using Collector.Wpf.Dialogs;

using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace Collector.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    private bool _shown = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void LaunchGitHubSite(object sender, System.Windows.RoutedEventArgs e)
    {
        var url = "https://github.com/AnttiChristian";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            _ = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _ = Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            _ = Process.Start("open", url);
        }
    }

    private async void UpdateDatfiles(object sender, System.Windows.RoutedEventArgs e)
    {
        UpdateDatfilesButton.IsEnabled = false;
        await CollectionManager.BuildCollection(App.Configuration);
        LoadTree();
        UpdateDatfilesButton.IsEnabled = true;
    }

    private void LoadTree()
    {
        DatabaseTree.Items.Clear();
        foreach (Manufacturer manufacturer in CollectionManager.Collection.Manufacturers)
        {
            var manufacturerTreeViewItem = new TreeViewItem
            {
                Header = manufacturer.ToString(),
                Tag = manufacturer
            };

            foreach (var hardware in manufacturer.Hardware)
            {
                var hardwareTreeViewItem = new TreeViewItem
                {
                    Header = hardware.ToString(),
                    Tag = hardware,
                    Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xff, 0xe0))
                };

                _ = manufacturerTreeViewItem.Items.Add(hardwareTreeViewItem);
            }

            _ = DatabaseTree.Items.Add(manufacturerTreeViewItem);
        }
    }

    private async void IdentifyButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        IdentifyButton.IsEnabled = false;
        var dialogResult = await this.ShowChildWindowAsync<DirectoryInfo>(new OpenItemDialog
        {
            IsModal = true,
            AllowMove = true
        });

        if (dialogResult is not null)
        {
            var process = new IdentifyProcess();
            await process.Execute(dialogResult.FullName);
        }

        IdentifyButton.IsEnabled = true;
    }

    private async void ImportButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ImportButton.IsEnabled = false;
        var dialogResult = await this.ShowChildWindowAsync<DirectoryInfo>(new OpenItemDialog
        {
            IsModal = true,
            AllowMove = true
        });

        if (dialogResult is not null)
        {
            var process = new IdentifyProcess();
            await process.Execute(dialogResult.FullName);
        }

        ImportButton.IsEnabled = true;
    }

    protected override async void OnContentRendered(System.EventArgs e)
    {
        if (_shown) return;

        _shown = true;
        base.OnContentRendered(e);
        await CollectionManager.BuildCollection(App.Configuration);
        LoadTree();
    }

    private async void ScanButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ScanButton.IsEnabled = false;
        var dialogResult = await this.ShowChildWindowAsync<DirectoryInfo>(new OpenItemDialog
        {
            IsModal = true,
            AllowMove = true
        });

    }
}