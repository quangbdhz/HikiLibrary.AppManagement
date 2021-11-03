using Library_Management.ViewModel.Book;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.ControlBarUc
{
    public class ControlBarAddBillViewModel : AddBookViewModel
    {
        #region commands
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ICommand MouseDoubleWindowCommand { get; set; }

        #endregion
        public ControlBarAddBillViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) => {
                if (p == null)
                    return false;

                if (CountOuputBillStatus == 1)
                    return false;

                return true;
            }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            });

            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                SizeWindow(p);
            });

            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else
                        w.WindowState = WindowState.Maximized;
                }
            });

            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });

            MouseDoubleWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                SizeWindow(p);
            });
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
        public void SizeWindow(UserControl p)
        {
            FrameworkElement window = GetWindowParent(p);
            var w = window as Window;
            w.ResizeMode = ResizeMode.NoResize;
            if (w != null)
            {

                if (w.WindowState != WindowState.Maximized)
                {
                    w.WindowStyle = WindowStyle.None;
                    w.WindowState = WindowState.Maximized;
                }
                else
                {
                    w.ResizeMode = ResizeMode.CanResize;
                    w.WindowState = WindowState.Normal;
                }
            }
        }
    }
}
