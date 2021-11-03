using Library_Management.Model;
using Library_Management.ViewModel.Borrow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_Management.Borrow
{
    /// <summary>
    /// Interaction logic for BorrowerWindow.xaml
    /// </summary>
    public partial class BorrowerWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBook;
        public ObservableCollection<Model.BorrowBook> LvBorrowBook { get => _LvBorrowBook; set { _LvBorrowBook = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBook;
        public ObservableCollection<Model.Book> LvBook { get => _LvBook; set { _LvBook = value; OnPropertyChanged(); } }

        private string _StringSearchIdBook;
        public string StringSearchIdBook { get => _StringSearchIdBook; set { _StringSearchIdBook = value; OnPropertyChanged(); } }

        private int _OptionSearchBorrowBook;
        public int OptionSearchBorrowBook { get => _OptionSearchBorrowBook; set { _OptionSearchBorrowBook = value; OnPropertyChanged(); } }

        private int _OptionSearchBook;
        public int OptionSearchBook { get => _OptionSearchBook; set { _OptionSearchBook = value; OnPropertyChanged(); } }

        private string _GetOptonTabControl;
        public string GetOptonTabControl { get => _GetOptonTabControl; set { _GetOptonTabControl = value; OnPropertyChanged(); } }

        private int _GetIdHuman;
        public int GetIdHuman { get => _GetIdHuman; set { _GetIdHuman = value; OnPropertyChanged(); } }

        private int _GetIdStaff;
        public int GetIdStaff { get => _GetIdStaff; set { _GetIdStaff = value; OnPropertyChanged(); } }

        public BorrowerWindow()
        {
            InitializeComponent();

            OptionSearchBorrowBook = 0;
            OptionSearchBook = 0;

            if (getRuleLoginBorrowBook.Text == "1")
            {
                Search();
            }

        }
        
        public void Search()
        {
            GetIdStaff = Int16.Parse(getIdStaff.Text);

            LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == GetIdHuman && x.IdStatus == 1 && x.CountDelete == 0));
            Lv_BorrowBook.ItemsSource = LvBorrowBook;

            CollectionView viewBookBorrow = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BorrowBook.ItemsSource);
            viewBookBorrow.Filter = UserFilterBookBorrow;

            LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == GetIdHuman && x.IdStatusReturnBookToHuman == 4 && x.CountDelete == 0));
            Lv_Book.ItemsSource = LvBook;

            CollectionView viewBook = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Book.ItemsSource);
            viewBook.Filter = UserFilterBook;
        }

        private bool UserFilterBook(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBook.Text))
                return true;
            else
            {
                if(OptionSearchBook == 0)
                    return ((item as Model.Book).Id.ToString().IndexOf(tb_IdBook.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                else
                    return ((item as Model.Book).DisplayName.IndexOf(tb_IdBook.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void IdBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Book.ItemsSource).Refresh();
        }

        private void cbx_OptionSearchBook_DropDownClosed(object sender, EventArgs e)
        {
            OptionSearchBook = cbx_OptionSearchBook.SelectedIndex;
        }

        private bool UserFilterBookBorrow(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBookBorrow.Text))
                return true;
            else
            {
                if (OptionSearchBorrowBook == 0)
                {
                    return ((item as Model.BorrowBook).IdBook.ToString().IndexOf(tb_IdBookBorrow.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.BorrowBook).Book.DisplayName.IndexOf(tb_IdBookBorrow.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
                
        }

        private void IdBookBorrow_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_BorrowBook.ItemsSource).Refresh();
        }


        private void cbx_OptionSearchBorrowBook_DropDownClosed(object sender, EventArgs e)
        {
            OptionSearchBorrowBook = cbx_OptionSearchBorrowBook.SelectedIndex;
        }


        #region ListViewScrollViewer
        private void ListViewScrollViewerBorrow_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerSolved_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerLoan_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewScrollViewerReturned_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        #endregion

        private void CheckUserHuman(object sender, RoutedEventArgs e)
        {
            string userHuman = getIdHuman.Text;
            var getIdOfHumanLoginBorrower = DataProvider.Ins.DB.Humen.Where(x => x.MS == userHuman && x.CountDelete == 0).SingleOrDefault();
            if(getIdOfHumanLoginBorrower != null)
            {
                GetIdHuman = getIdOfHumanLoginBorrower.Id;

                Search();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if(GetOptonTabControl == "1")
            //{
            //    LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == GetIdHuman && x.IdStatus == 1 && x.CountDelete == 0));
            //    Lv_BorrowBook.ItemsSource = LvBorrowBook;
                
            //    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BorrowBook.ItemsSource);
            //    view.Filter = UserFilter;
            //}
            //else
            //{
            //    LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == GetIdHuman && x.IdStatusReturnBookToHuman == 4 && x.CountDelete == 0));
            //    Lv_Book.ItemsSource = LvBook;

            //    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Book.ItemsSource);
            //    view.Filter = UserFilter;
            //}
        }


        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(tb_book.Text))
        //        return true;

        //    else
        //    {
        //        if (OptionTabControlBookBorrow == 0)
        //            return ((item as Model.BorrowBook).IdBook.ToString().IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //        else
        //            return ((item as Model.Book).Id.ToString().IndexOf(tb_book.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }
        //}

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //if (OptionTabControlBookBorrow == 0)
        //{
        //    CollectionViewSource.GetDefaultView(Lv_BorrowBook.ItemsSource).Refresh();
        //}
        //else if (OptionTabControlBookLoan == 0)
        //{
        //    CollectionViewSource.GetDefaultView(Lv_Book.ItemsSource).Refresh();
        //}
        //else
        //{

        //}
        //}

        //private void nameTabControlBookBorrow_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //if(GetIdStaff != 0)
        //{
        //    OptionTabControlBookLoan = 1;
        //    OptionTabControlBookBorrow = nameTabControlBookBorrow.SelectedIndex;
        //    if (nameTabControlBookBorrow.SelectedIndex == 0)
        //    {
        //        LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == GetIdHuman && x.IdStatus == 1 && x.CountDelete == 0));
        //        Lv_BorrowBook.ItemsSource = LvBorrowBook;

        //        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BorrowBook.ItemsSource);
        //        view.Filter = UserFilter;
        //    }
        //    else
        //    {

        //    }
        //}
        //}

        //private void nameTabControlBookLoan_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //if(GetIdStaff != 0)
        //{
        //    OptionTabControlBookBorrow = 1;
        //    OptionTabControlBookLoan = nameTabControlBookLoan.SelectedIndex;
        //    if (nameTabControlBookLoan.SelectedIndex == 0)
        //    {
        //        LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == GetIdHuman && x.IdStatusReturnBookToHuman == 4 && x.CountDelete == 0));
        //        Lv_Book.ItemsSource = LvBook;

        //        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Book.ItemsSource);
        //        view.Filter = UserFilter;
        //    }
        //    else
        //    {

        //    }
        //}
        //}
    }
}
