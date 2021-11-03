using Library_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Library_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();

            string FilePathProjectCustomer = System.IO.Directory.GetCurrentDirectory();
            FilePathProjectCustomer += @"\DataImageCustomer";

            getSizeFolderCustomer.Text = BytesToString(GetFolderSize(FilePathProjectCustomer));

            string FilePathProjectBook = System.IO.Directory.GetCurrentDirectory();
            FilePathProjectBook += @"\DataImageBook";

            getSizeFolderBook.Text = BytesToString(GetFolderSize(FilePathProjectBook));

        }

        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void ListViewScrollViewerNotifications_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerMessager_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        static long GetFolderSize(string s)
        {
            string[] fileNames = Directory.GetFiles(s, "*.*");
            long size = 0;

            // Calculate total size by looping through files in the folder and totalling their sizes
            foreach (string name in fileNames)
            {
                // length of each file.
                FileInfo details = new FileInfo(name);
                size += details.Length;
            }
            return size;
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void FoldersCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (getRule.Text == "2")
            {
                string FilePathProject = System.IO.Directory.GetCurrentDirectory();
                FilePathProject += @"\DataImageCustomer";
                Process.Start(FilePathProject);

                getSizeFolderCustomer.Text = BytesToString(GetFolderSize(FilePathProject));



            }    
        }

        private void FoldersBook_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (getRule.Text == "2")
            {
                string FilePathProject = System.IO.Directory.GetCurrentDirectory();
                FilePathProject += @"\DataImageBook";
                Process.Start(FilePathProject);

                getSizeFolderBook.Text = BytesToString(GetFolderSize(FilePathProject));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            timelbl.Content = DateTime.Now.ToLongTimeString();
            datelbl.Content = DateTime.Now.ToLongDateString();
        }

        private void menu_Logout_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.Close();
        }
    }
}
