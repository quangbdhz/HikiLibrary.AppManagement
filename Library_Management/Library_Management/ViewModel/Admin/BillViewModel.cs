using Library_Management.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library_Management.ViewModel.Admin
{
    public class BillViewModel : BaseViewModel
    {
        private ObservableCollection<Model.ListBookLibraryBorrowHuman> _LvListBookLibraryBorrowHuman;
        public ObservableCollection<Model.ListBookLibraryBorrowHuman> LvListBookLibraryBorrowHuman { get => _LvListBookLibraryBorrowHuman; set { _LvListBookLibraryBorrowHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListBookCustomerBorrow> _LvListBookCustomerBorrow;
        public ObservableCollection<Model.ListBookCustomerBorrow> LvListBookCustomerBorrow { get => _LvListBookCustomerBorrow; set { _LvListBookCustomerBorrow = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListBookCustomerReturnBookLibrary> _LvListBookCustomerReturnBookLibrary;
        public ObservableCollection<Model.ListBookCustomerReturnBookLibrary> LvListBookCustomerReturnBookLibrary { get => _LvListBookCustomerReturnBookLibrary; set { _LvListBookCustomerReturnBookLibrary = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.ListReturnBookHuman> _LvListReturnBookHuman;
        public ObservableCollection<Model.ListReturnBookHuman> LvListReturnBookHuman { get => _LvListReturnBookHuman; set { _LvListReturnBookHuman = value; OnPropertyChanged(); } }
        public BillViewModel()
        {
            LvListBookLibraryBorrowHuman = new ObservableCollection<Model.ListBookLibraryBorrowHuman>(DataProvider.Ins.DB.ListBookLibraryBorrowHumen.Where(x => x.CountDelete == 0));
            LvListBookCustomerBorrow = new ObservableCollection<Model.ListBookCustomerBorrow>(DataProvider.Ins.DB.ListBookCustomerBorrows.Where(x => x.CountDelete == 0));
            LvListBookCustomerReturnBookLibrary = new ObservableCollection<Model.ListBookCustomerReturnBookLibrary>(DataProvider.Ins.DB.ListBookCustomerReturnBookLibraries.Where(x => x.CountDelete == 0));
            LvListReturnBookHuman = new ObservableCollection<Model.ListReturnBookHuman>(DataProvider.Ins.DB.ListReturnBookHumen.Where(x => x.CountDelete == 0));
        }

    }
}
