using Library_Management.ViewModel.Staff;
using System.Windows;

namespace Library_Management.Staff
{
    /// <summary>
    /// Interaction logic for TimeTableWindow.xaml
    /// </summary>
    public partial class TimeTableWindow : Window
    {
        public TimeTableWindow()
        {
            InitializeComponent();
            this.DataContext = new TimeTableViewModel();
        }
    }
}
