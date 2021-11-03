using Library_Management.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.Borrow
{
    public class ReceiveBookViewModel : BorrowerViewModel
    {
        private int _IdStatus;
        public int IdStatus { get => _IdStatus; set { _IdStatus = value; OnPropertyChanged(); } }

        private int _SelectedIdStatus;
        public int SelectedIdStatus { get => _SelectedIdStatus; set { _SelectedIdStatus = value; OnPropertyChanged(); } }

        private double _PayMoneyBook;
        public double PayMoneyBook { get => _PayMoneyBook; set { _PayMoneyBook = value; OnPropertyChanged(); } }

        private Visibility _OptionVisibilityPayFines;
        public Visibility OptionVisibilityPayFines { get => _OptionVisibilityPayFines; set { _OptionVisibilityPayFines = value; OnPropertyChanged(); } }

        private Visibility _OptionVisibilityPayMoney;
        public Visibility OptionVisibilityPayMoney { get => _OptionVisibilityPayMoney; set { _OptionVisibilityPayMoney = value; OnPropertyChanged(); } }
        public ICommand GiveBackBookCommand { get; set; }
        public ICommand SelectStatusCommand { get; set; }

        public ReceiveBookViewModel()
        {
            IdStatus = 0;
            SelectedIdStatus = 0;
            OptionVisibilityPayFines = Visibility.Visible;
            OptionVisibilityPayMoney = Visibility.Collapsed;

            SelectStatusCommand = new RelayCommand<ComboBox>((p) => {
                return true;
            }, (p) => {
                IdStatus = p.SelectedIndex;
                if(IdStatus == 1)
                {
                    ContractualFine = BookPrice;
                    PayMoneyBook = BookPrice;
                    OptionVisibilityPayFines = Visibility.Collapsed;
                    OptionVisibilityPayMoney = Visibility.Visible;
                }
                else
                {
                    ContractualFine = PayFine;
                    OptionVisibilityPayFines = Visibility.Visible;
                    OptionVisibilityPayMoney = Visibility.Collapsed;
                }
            });



            GiveBackBookCommand = new RelayCommand<Window>((p) => {
                var ReturnBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == Id).SingleOrDefault();
                if (ReturnBorrowBook.IdStatus > 1)
                    return false;
                return true;
            }, (p) => {
                IdStatus += 2;
                SelectedIdStatus = 0;
                GetMoney = ContractualFine;

                var ReturnBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == Id).SingleOrDefault();
                ReturnBorrowBook.IdStatus = IdStatus;
                ReturnBorrowBook.ContractualFine = ContractualFine;
                DataProvider.Ins.DB.SaveChanges();

                string color = "Green";
                if (IdStatus == 3) color = "Red";
                var Book = DataProvider.Ins.DB.Books.Where(x => x.Id == ReturnBorrowBook.IdBook).SingleOrDefault();
                Book.IdStatus = IdStatus;
                Book.Color = color;
                DataProvider.Ins.DB.SaveChanges();

                var Human = DataProvider.Ins.DB.Humen.Where(x => x.Id == ReturnBorrowBook.IdHuman && x.CountDelete == 0).SingleOrDefault();
                Human.Forfeit += ContractualFine;
                Human.PayFine += PayFine;
                DataProvider.Ins.DB.SaveChanges();

                var addScoreStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == IdStaff && x.CountDelete == 0).SingleOrDefault();
                addScoreStaff.ScoreInputBook += 1;
                DataProvider.Ins.DB.SaveChanges();

                ScoreInputSubject(Book.BookSubject);

                p.Close();
            });
        }
    }
}
