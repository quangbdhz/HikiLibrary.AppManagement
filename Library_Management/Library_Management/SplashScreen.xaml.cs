using System;
using System.Windows;
using System.Windows.Threading;

namespace Library_Management
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();
            Loading();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }

        void Loading()
        {
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
    }
}
