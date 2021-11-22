using Library_Management.Model;
using Library_Management.ViewModel.Book;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Library_Management.Book
{
    /// <summary>
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private ObservableCollection<Model.Book> _LvBookAvailable;
        public ObservableCollection<Model.Book> LvBookAvailable { get => _LvBookAvailable; set { _LvBookAvailable = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBookBorrowed;
        public ObservableCollection<Model.Book> LvBookBorrowed { get => _LvBookBorrowed; set { _LvBookBorrowed = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBookLiquidation;
        public ObservableCollection<Model.Book> LvBookLiquidation { get => _LvBookLiquidation; set { _LvBookLiquidation = value; OnPropertyChanged(); } }


        private string _StringSearchBook;
        public string StringSearchBook { get => _StringSearchBook; set { _StringSearchBook = value; OnPropertyChanged(); } }

        private int _OptionTabControl;
        public int OptionTabControl { get => _OptionTabControl; set { _OptionTabControl = value; OnPropertyChanged(); } }

        public BookWindow()
        {
            InitializeComponent();
            //this.DataContext = new BookViewModel();

            StringSearchBook = "Book title";
            OptionTabControl = 0;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(tb_book.Text))
                return true;
            else
            {
                if (StringSearchBook == "Book title")
                {
                    return ((item as Model.Book).DisplayName.IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchBook == "Subject")
                {
                    return ((item as Model.Book).BookSubject.IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchBook == "Author")
                {
                    return ((item as Model.Book).Author.IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.Book).IdStatus.ToString().IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameTabControl.SelectedIndex == 0)
            {
                CollectionViewSource.GetDefaultView(Lv_BookAvailable.ItemsSource).Refresh();
            }
            else if (nameTabControl.SelectedIndex == 1)
            {
                CollectionViewSource.GetDefaultView(Lv_BookBorrowed.ItemsSource).Refresh();
            }
            else
            {
                CollectionViewSource.GetDefaultView(Lv_BookLiquidation.ItemsSource).Refresh();
            }
        }

        private void OptionSearch_DropDownClosed(object sender, EventArgs e)
        {

            StringSearchBook = this.OptionSearch.Text;
        }

        private void nameTabControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OptionTabControl = nameTabControl.SelectedIndex;
            if (nameTabControl.SelectedIndex == 0)
            {
                LvBookAvailable = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 2 && x.IdStatusReturnBookToHuman == 4));
                Lv_BookAvailable.ItemsSource = LvBookAvailable;

                //MessageBox.Show("!");
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookAvailable.ItemsSource);
                view.Filter = UserFilter;
            }
            else if (nameTabControl.SelectedIndex == 1)
            {
                LvBookBorrowed = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 1 && x.IdStatusReturnBookToHuman == 4));
                Lv_BookBorrowed.ItemsSource = LvBookBorrowed;

                //MessageBox.Show("!11111");
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookBorrowed.ItemsSource);
                view.Filter = UserFilter;
            }
            else
            {
                //LvBookLiquidation = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 1 && x.IdStatusReturnBookToHuman == 4));
                //Lv_BookLiquidation.ItemsSource = LvBookLiquidation;

                ////MessageBox.Show("!11111");
                //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookLiquidation.ItemsSource);
                //view.Filter = UserFilter;
            }
        }



        private void ListViewScrollViewerLiquidation_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerBorrowed_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerBook_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
