using Library_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Library_Management.Admin
{
    /// <summary>
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Model.ListBookLibraryBorrowHuman> _LvListBookLibraryBorrowHuman;
        public ObservableCollection<Model.ListBookLibraryBorrowHuman> LvListBookLibraryBorrowHuman { get => _LvListBookLibraryBorrowHuman; set { _LvListBookLibraryBorrowHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListBookCustomerBorrow> _LvListBookCustomerBorrow;
        public ObservableCollection<Model.ListBookCustomerBorrow> LvListBookCustomerBorrow { get => _LvListBookCustomerBorrow; set { _LvListBookCustomerBorrow = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListBookCustomerReturnBookLibrary> _LvListBookCustomerReturnBookLibrary;
        public ObservableCollection<Model.ListBookCustomerReturnBookLibrary> LvListBookCustomerReturnBookLibrary { get => _LvListBookCustomerReturnBookLibrary; set { _LvListBookCustomerReturnBookLibrary = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListReturnBookHuman> _LvListReturnBookHuman;
        public ObservableCollection<Model.ListReturnBookHuman> LvListReturnBookHuman { get => _LvListReturnBookHuman; set { _LvListReturnBookHuman = value; OnPropertyChanged(); } }



        public BillWindow()
        {
            InitializeComponent();

            #region ListBookLibraryBorrowHuman
            LvListBookLibraryBorrowHuman = new ObservableCollection<Model.ListBookLibraryBorrowHuman>(DataProvider.Ins.DB.ListBookLibraryBorrowHumen.Where(x => x.CountDelete == 0));
            Lv_BookLibraryBorrowHuman.ItemsSource = LvListBookLibraryBorrowHuman;

            CollectionView viewBookLibraryBorrowHuman = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookLibraryBorrowHuman.ItemsSource);
            viewBookLibraryBorrowHuman.Filter = UserFilterBookLibraryBorrowHuman;
            #endregion

            #region ListBookCustomerBorrow
            LvListBookCustomerBorrow = new ObservableCollection<Model.ListBookCustomerBorrow>(DataProvider.Ins.DB.ListBookCustomerBorrows.Where(x => x.CountDelete == 0));
            Lv_BookCustomerBorrow.ItemsSource = LvListBookCustomerBorrow;

            CollectionView viewBookCustomerBorrow = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookCustomerBorrow.ItemsSource);
            viewBookCustomerBorrow.Filter = UserFilterBookCustomerBorrow;
            #endregion

            #region ListBookCustomerReturnBookLibrary
            LvListBookCustomerReturnBookLibrary = new ObservableCollection<Model.ListBookCustomerReturnBookLibrary>(DataProvider.Ins.DB.ListBookCustomerReturnBookLibraries.Where(x => x.CountDelete == 0));
            Lv_BookCustomerReturnBookLibrary.ItemsSource = LvListBookCustomerReturnBookLibrary;

            CollectionView viewBookCustomerReturnBookLibrary = (CollectionView)CollectionViewSource.GetDefaultView(Lv_BookCustomerReturnBookLibrary.ItemsSource);
            viewBookCustomerReturnBookLibrary.Filter = UserFilterBookCustomerReturnBookLibrary;
            #endregion

            #region ListReturnBookHuman
            LvListReturnBookHuman = new ObservableCollection<Model.ListReturnBookHuman>(DataProvider.Ins.DB.ListReturnBookHumen.Where(x => x.CountDelete == 0));
            Lv_ListReturnBookHuman.ItemsSource = LvListReturnBookHuman;

            CollectionView viewListReturnBookHuman = (CollectionView)CollectionViewSource.GetDefaultView(Lv_ListReturnBookHuman.ItemsSource);
            viewListReturnBookHuman.Filter = UserFilterListReturnBookHuman;
            #endregion
        }

        private bool UserFilterListReturnBookHuman(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBillLibraryReturn.Text))
                return true;
            else
                return ((item as Model.ListReturnBookHuman).IdBillReturnBookHuman.ToString().IndexOf(tb_IdBillLibraryReturn.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void IdBillLibraryReturn_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_ListReturnBookHuman.ItemsSource).Refresh();
        }


        private bool UserFilterBookCustomerReturnBookLibrary(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBillCustomerReturn.Text))
                return true;
            else
                return ((item as Model.ListBookCustomerReturnBookLibrary).IdBillCustomerReturnBookLibrary.ToString().IndexOf(tb_IdBillCustomerReturn.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void IdBillCustomerReturn_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_BookCustomerReturnBookLibrary.ItemsSource).Refresh();
        }


        private bool UserFilterBookCustomerBorrow(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBillCustomer.Text))
                return true;
            else
                return ((item as Model.ListBookCustomerBorrow).IdBillBookOfCustomer.ToString().IndexOf(tb_IdBillCustomer.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void IdBillCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_BookCustomerBorrow.ItemsSource).Refresh();
        }



        private bool UserFilterBookLibraryBorrowHuman(object item)
        {
            if (String.IsNullOrEmpty(tb_IdBillPartner.Text))
                return true;
            else
                return ((item as Model.ListBookLibraryBorrowHuman).IdBillBookOfHuman.ToString().IndexOf(tb_IdBillPartner.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void IdBillPartner_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_BookLibraryBorrowHuman.ItemsSource).Refresh();
        }



        private void LvListBookLibraryBorrowHuman_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void LvListBookCustomerBorrow_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void LvListBookCustomerReturnBookLibrary_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        private void LvListReturnBookHuman_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
