using MahApps.Metro.SimpleChildWindow;

using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Collector.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for CoolChildWindow.xaml
    /// </summary>
    public partial class OpenItemDialog : ChildWindow
    {
        public OpenItemDialog()
        {
            InitializeComponent();
            LoadRootItems();
        }

        private void LoadRootItems()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                var treeViewItem = new TreeViewItem
                {
                    Header = drive.Name,
                    Tag = drive.RootDirectory
                };

                _ = ItemTreeView.Items.Add(treeViewItem);
            }
        }

        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            var directory = (ItemTreeView.SelectedItem as TreeViewItem)?.Tag as DirectoryInfo;
            _ = Close(CloseReason.Ok, directory);
        }

        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            _ = Close(CloseReason.Cancel, false);
        }

        private void ItemTreeView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = ItemTreeView.SelectedItem as TreeViewItem;
            if (selectedItem is null) return;

            var directory = selectedItem.Tag as DirectoryInfo;
            if (directory is null) return;

            foreach (var childDirectory in directory.GetDirectories())
            {
                var treeViewItem = new TreeViewItem
                {
                    Header = childDirectory.Name,
                    Tag = childDirectory
                };

                _ = selectedItem.Items.Add(treeViewItem);
            }
        }
    }
}