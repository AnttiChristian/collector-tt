using MahApps.Metro.SimpleChildWindow;

using System.Windows;

namespace Collector.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for CoolChildWindow.xaml
    /// </summary>
    public partial class ScanDialog : ChildWindow
    {
        public ScanDialog()
        {
            InitializeComponent();
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            _ = Close(CloseReason.Ok);
        }

        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            _ = Close(CloseReason.Cancel, false);
        }
    }
}