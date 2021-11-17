
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Collector.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly IConfiguration Configuration;

        public static readonly string UserDataFolder;

        public static readonly ILogger Logger;

        static App()
        {
            UserDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Collector.TT");

            // TODO: move configuration to user profile
            var globalConfigurationPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory,
                "Configuration");

            Configuration = new ConfigurationBuilder()
                .SetBasePath(globalConfigurationPath)
                .AddJsonFile("Application.Settings.json")
                .Build();

            // TODO: execute refactoring with LiteDB logs database 
            Logger = NullLogger.Instance;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
