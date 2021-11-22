using Library_Management.Borrow;
using Library_Management.Human;
using Library_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Library_Management.ViewModel.Borrow
{
    public class BorrowerViewModel : LoginViewModel
    {
        private ObservableCollection<Model.BorrowBook> _LvIdBorrowBook;
        public ObservableCollection<Model.BorrowBook> LvIdBorrowBook { get => _LvIdBorrowBook; set { _LvIdBorrowBook = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvIdBorrowBookSloved;
        public ObservableCollection<Model.BorrowBook> LvIdBorrowBookSloved { get => _LvIdBorrowBookSloved; set { _LvIdBorrowBookSloved = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Status> _LvStatus;
        public ObservableCollection<Model.Status> LvStatus { get => _LvStatus; set { _LvStatus = value; OnPropertyChanged(); } }

        private static ObservableCollection<Model.Book> _LvBookToHuman;
        public static ObservableCollection<Model.Book> LvBookToHuman { get => _LvBookToHuman; set { _LvBookToHuman = value; } }

        private static ObservableCollection<Model.ListBookCustomerBorrow> _LvCustomerReturnBookBorrowLibrary;
        public static ObservableCollection<Model.ListBookCustomerBorrow> LvCustomerReturnBookBorrowLibrary { get => _LvCustomerReturnBookBorrowLibrary; set { _LvCustomerReturnBookBorrowLibrary = value; } }

        private ObservableCollection<InfoBookCustomerReturn> _LvInfoBookCustomerReturn;
        public ObservableCollection<InfoBookCustomerReturn> LvInfoBookCustomerReturn { get => _LvInfoBookCustomerReturn; set { _LvInfoBookCustomerReturn = value; OnPropertyChanged(); } }

        private ObservableCollection<InfoBookLibraryReturnPartner> _LvLibraryReturnBookPartner;
        public ObservableCollection<InfoBookLibraryReturnPartner> LvLibraryReturnBookPartner { get => _LvLibraryReturnBookPartner; set { _LvLibraryReturnBookPartner = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private static Model.Book _BookToHuman;
        public static Model.Book BookToHuman { get => _BookToHuman; set { _BookToHuman = value; } }

        private bool _IsActive;
        public bool IsActive { get => _IsActive; set { _IsActive = value; OnPropertyChanged(); } }

        private int _IdHumanReturnBook;
        public int IdHumanReturnBook { get => _IdHumanReturnBook; set { _IdHumanReturnBook = value; OnPropertyChanged(); } }

        private int _IdBillCustomerBorrowBookLibrary;
        public int IdBillCustomerReturnBookLibrary { get => _IdBillCustomerBorrowBookLibrary; set { _IdBillCustomerBorrowBookLibrary = value; OnPropertyChanged(); } }

        private double _AllTheDepositAmount;
        public double AllTheDepositAmount { get => _AllTheDepositAmount; set { _AllTheDepositAmount = value; OnPropertyChanged(); } }

        private double _AllFines;
        public double AllFines { get => _AllFines; set { _AllFines = value; OnPropertyChanged(); } }

        private static double _ContractualFine;
        public static double ContractualFine { get => _ContractualFine; set { _ContractualFine = value;  } }

        private static double _BookPrice;
        public static double BookPrice { get => _BookPrice; set { _BookPrice = value; } }

        private static double _PayFine;
        public static double PayFine { get => _PayFine; set { _PayFine = value; } }

        private static double _GetMoney;
        public static double GetMoney { get => _GetMoney; set { _GetMoney = value; } }

        private int _IdBillLibraryReturnBookPartner;
        public int IdBillLibraryReturnBookPartner{ get => _IdBillLibraryReturnBookPartner; set { _IdBillLibraryReturnBookPartner = value; OnPropertyChanged(); } }

        private double _AllTheDepositAmountBook;
        public double AllTheDepositAmountBook { get => _AllTheDepositAmountBook; set { _AllTheDepositAmountBook = value; OnPropertyChanged(); } }

        private double _AllLibraryFines;
        public double AllLibraryFines { get => _AllLibraryFines; set { _AllLibraryFines = value; OnPropertyChanged(); } }

        private int _IdHumanLoadDelete;
        public int IdHumanLoadDelete { get => _IdHumanLoadDelete; set { _IdHumanLoadDelete = value; OnPropertyChanged(); } }

        public ICommand ChangePasswordUserHuman { get; set; }
        public ICommand CheckUserHumanCommand { get; set; }
        public ICommand CustomerReturnBookLibraryCommand { get; set; }
        public ICommand ReturnBookToHumanCommand { get; set; }
        public ICommand ActionSnackBarCommand { get; set; }
        public ICommand DoubleClickGridCommand { get; set; }
        public ICommand MouseUpBookGridCommand { get; set; }
        public ICommand PrintBillCustomerReturnBookLibraryCommand { get; set; }
        public ICommand PrintBillLibraryReturnBookPartnerCommand { get; set; }

        public ICommand Test { get; set; }

        public BorrowerViewModel()
        {
            IsActive = true;
            AllTheDepositAmount = 0;
            AllFines = 0;


            LvInfoBookCustomerReturn = new ObservableCollection<InfoBookCustomerReturn>();
            LvLibraryReturnBookPartner = new ObservableCollection<InfoBookLibraryReturnPartner>();

            ChangePasswordUserHuman = new RelayCommand<Button>((p) => { return true; }, (p) => { ChangePassUserHumandWindow CPHW = new ChangePassUserHumandWindow(); CPHW.ShowDialog(); });

            Test = new RelayCommand<Object>((p) => { return true; }, (p) => { MessageBox.Show(LvBorrowBook.Count.ToString()); });

            CheckUserHumanCommand = new RelayCommand<Button>((p) =>
            {
                if (UserNameSearchUserHuman == "" || UserNameSearchUserHuman == null)
                    return false;

                if (LvInfoBookCustomerReturn.Count != 0)
                    return false;

                if (LvLibraryReturnBookPartner.Count != 0)
                    return false;

                return true;
            }, (p) =>
            {
                try
                {
                    var UserHuman = DataProvider.Ins.DB.UserHumen.Where(x => (x.UserName == UserNameSearchUserHuman) && x.CountDelete == 0).SingleOrDefault();

                    if (UserHuman != null)
                    {
                        int IdHuman = UserHuman.IdHuman;
                        IdHumanReturnBook = IdHuman;

                        var Human = DataProvider.Ins.DB.Humen.Where(x => (x.Id == IdHuman) && x.CountDelete == 0).SingleOrDefault();

                        bool checkWhile = true;
                        while (checkWhile == true)
                        {
                            try
                            {
                                var editBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHuman && x.DueDate < DateTime.Now && x.IdFined == 1).First();
                                editBorrowBook.IdFined = 2;
                                editBorrowBook.Color = "Red";

                                DataProvider.Ins.DB.SaveChanges();
                            }
                            catch
                            {
                                checkWhile = false;
                            }
                        }

                        DataProvider.Ins.DB.SaveChanges();

                        DisplayName = Human.DisplayName;
                        DateOfBirth = Human.DateOfBirth;
                        Address = Human.Address;

                        if (Human.IdGender == 1)
                            Gender = "Male";
                        else if (Human.IdGender == 2)
                            Gender = "Female";
                        else
                            Gender = "Custom";

                        Email = Human.Email;
                        Note = Human.Note;
                        UrlAvatarHuman = Human.UrlAvatarHuman;

                        LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHuman && x.IdStatus == 1 && x.CountDelete == 0));
                        LvBorrowBookSloved = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHuman && x.IdStatus > 1 && x.CountDelete == 0));
                        LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == IdHuman && x.IdStatusReturnBookToHuman == 4 && x.CountDelete == 0));
                        LvBookReturned = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == IdHuman && x.IdStatusReturnBookToHuman == 5 && x.CountDelete == 0));
                    }
                    else
                    {
                        MessageBox.Show("No User Human");
                    }
                }
                catch
                {
                    //MessageBox.Show("Khong tim ra tai khoan nguoi dung");
                }

            });

            PrintBillCustomerReturnBookLibraryCommand = new RelayCommand<Object>((p) => {
                if (LvInfoBookCustomerReturn.Count == 0)
                    return false;

                return true; 
            }, (p) => 
            {
                var billCustomerReturnBook = new Model.BillCustomerReturnBookLibrary()
                {
                    IdHuman = IdHumanReturnBook,
                    IdStaff = GetIdStaffHuman,
                    DateOfRepayment = DateTime.Now,
                    AllTheDepositAmount = AllTheDepositAmount,
                    AllFines = AllFines,
                    Note = "",
                    IdStatusBill = 1,
                    CountDelete = 0
                };
                DataProvider.Ins.DB.BillCustomerReturnBookLibraries.Add(billCustomerReturnBook);
                DataProvider.Ins.DB.SaveChanges();

                var getIdBillOfCustomerReturn = DataProvider.Ins.DB.BillCustomerReturnBookLibraries.OrderByDescending(x => x.Id).First().Id;
                IdBillCustomerReturnBookLibrary = getIdBillOfCustomerReturn;


                foreach (InfoBookCustomerReturn item in LvInfoBookCustomerReturn)
                {
                    var listBookCustomerReturnBookLibrary = new Model.ListBookCustomerReturnBookLibrary()
                    {
                        IdBillCustomerReturnBookLibrary = IdBillCustomerReturnBookLibrary,
                        IdBook = item.Book.Id,
                        NumberOfBooks = 1,
                        CountDelete = 0
                    };
                    DataProvider.Ins.DB.ListBookCustomerReturnBookLibraries.Add(listBookCustomerReturnBookLibrary);
                    DataProvider.Ins.DB.SaveChanges();

                    if (item.IdFined == 1)
                    {
                        var ReturnBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == item.IdBorrowBook).SingleOrDefault();
                        ReturnBorrowBook.IdStatus = 2;
                        DataProvider.Ins.DB.SaveChanges();

                        var Book = DataProvider.Ins.DB.Books.Where(x => x.Id == item.Book.Id).SingleOrDefault();
                        Book.IdStatus = 2;
                        Book.Color = "Green";
                        DataProvider.Ins.DB.SaveChanges();

                        var addScoreStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == IdStaff && x.CountDelete == 0).SingleOrDefault();
                        addScoreStaff.ScoreInputBook += 1;
                        DataProvider.Ins.DB.SaveChanges();

                        ScoreInputSubject(Book.BookSubject);
                    }
                    else
                    {

                    }
                }

                PrintBillForCustomerReturnBook();

                LvInfoBookCustomerReturn.Clear();
                AllTheDepositAmount = 0;
                AllFines = 0;
            });

            CustomerReturnBookLibraryCommand = new RelayCommand<Model.BorrowBook>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (LvInfoBookCustomerReturn.Count == 0)
                {
                    if (DateTime.Now <= p.DueDate)
                    {
                        LvInfoBookCustomerReturn.Add(new InfoBookCustomerReturn() { Book = p.Book, IdFined = 1, MoneyFines = 0, IdBorrowBook = p.Id });
                        if (p.Book.BookPrice == null)
                        {
                            p.Book.BookPrice = 0;
                        }

                        AllTheDepositAmount += (double)p.Book.BookPrice;
                    }
                    else
                    {
                        double moneyFine = (double)(p.Book.BookPrice * 10 / 100);
                        Id = p.Id;

                        if (p.Book.BookPrice == null)
                        {
                            p.Book.BookPrice = 0;
                        }

                        AllTheDepositAmount += (double)p.Book.BookPrice;
                        BookPrice = (double)p.Book.BookPrice;



                        var ReturnBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == p.Id).SingleOrDefault();
                        ReturnBorrowBook.IdFined = 2;
                        ReturnBorrowBook.Color = "Red";
                        DataProvider.Ins.DB.SaveChanges();

                        ContractualFine = moneyFine;
                        PayFine = moneyFine;

                        LvIdBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == Id && x.CountDelete == 0));

                        LvStatus = new ObservableCollection<Model.Status>(DataProvider.Ins.DB.Status.Where(x => x.Id == 2 || x.Id == 3));

                        ReceiveBookWindow RBW = new ReceiveBookWindow(); RBW.ShowDialog();

                        LvInfoBookCustomerReturn.Add(new InfoBookCustomerReturn() { Book = p.Book, IdFined = 2, MoneyFines = GetMoney, IdBorrowBook = p.Id });
                        AllFines += GetMoney;
                    }
                    LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHumanReturnBook && x.IdStatus == 1 && x.CountDelete == 0));
                    //LvBorrowBook.Remove(p);
                    LvBorrowBookSloved.Add(p);
                }
                else
                {
                    int countCheckLoopBook = 0;

                    foreach (var item in LvInfoBookCustomerReturn)
                    {
                        if (item.Book.Id == p.IdBook)
                            countCheckLoopBook++;
                    }

                    if (countCheckLoopBook != 0)
                    {
                        MessageBox.Show("Returned The Book");
                    }
                    else
                    {
                        if (DateTime.Now <= p.DueDate)
                        {
                            LvInfoBookCustomerReturn.Add(new InfoBookCustomerReturn() { Book = p.Book, IdFined = 1, MoneyFines = 0, IdBorrowBook = p.Id });
                            if (p.Book.BookPrice == null)
                            {
                                p.Book.BookPrice = 0;
                            }

                            AllTheDepositAmount += (double)p.Book.BookPrice;
                        }
                        else
                        {
                            double moneyFine = (double)(p.Book.BookPrice * 10 / 100);
                            Id = p.Id;

                            if (p.Book.BookPrice == null)
                            {
                                p.Book.BookPrice = 0;
                            }

                            AllTheDepositAmount += (double)p.Book.BookPrice;
                            BookPrice = (double)p.Book.BookPrice;



                            var ReturnBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == p.Id).SingleOrDefault();
                            ReturnBorrowBook.IdFined = 2;
                            ReturnBorrowBook.Color = "Red";
                            DataProvider.Ins.DB.SaveChanges();

                            ContractualFine = moneyFine;
                            PayFine = moneyFine;

                            LvIdBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == Id && x.CountDelete == 0));

                            LvStatus = new ObservableCollection<Model.Status>(DataProvider.Ins.DB.Status.Where(x => x.Id == 2 || x.Id == 3));

                            ReceiveBookWindow RBW = new ReceiveBookWindow(); RBW.ShowDialog();

                            LvInfoBookCustomerReturn.Add(new InfoBookCustomerReturn() { Book = p.Book, IdFined = 2, MoneyFines = GetMoney, IdBorrowBook = p.Id });
                            AllFines += GetMoney;
                        }
                        LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHumanReturnBook && x.IdStatus == 1 && x.CountDelete == 0));
                        //LvBorrowBook.Remove(p);
                        LvBorrowBookSloved.Add(p);
                    }

                }
            });

            PrintBillLibraryReturnBookPartnerCommand = new RelayCommand<Object>((p) => {
                if (LvLibraryReturnBookPartner.Count == 0)
                    return false;

                return true; 
            }, (p) => 
            {
                var billLibraryReturnBook = new Model.BillReturnBookHuman()
                {
                    IdHuman = IdHumanReturnBook,
                    IdStaff = GetIdStaffHuman,
                    DateOfRepayment = DateTime.Now,
                    Note = "",
                    IdStatusBill = 1,
                    CountDelete = 0
                };
                DataProvider.Ins.DB.BillReturnBookHumen.Add(billLibraryReturnBook);
                DataProvider.Ins.DB.SaveChanges();

                var getIdBillOfLibraryReturn = DataProvider.Ins.DB.BillReturnBookHumen.OrderByDescending(x => x.Id).First().Id;
                IdBillLibraryReturnBookPartner = getIdBillOfLibraryReturn;

 
                foreach (InfoBookLibraryReturnPartner item in LvLibraryReturnBookPartner)
                {
                    var listBookLibraryReturnBookCustomer = new Model.ListReturnBookHuman()
                    {
                        IdBillReturnBookHuman = IdBillLibraryReturnBookPartner,
                        IdBook = item.Book.Id,
                        NumberOfBooks = 1,
                        CountDelete = 0
                    };
                    DataProvider.Ins.DB.ListReturnBookHumen.Add(listBookLibraryReturnBookCustomer);
                    DataProvider.Ins.DB.SaveChanges();

                    var book = DataProvider.Ins.DB.Books.Where(x => x.Id == item.Book.Id).SingleOrDefault();
                    book.IdStatusReturnBookToHuman = 2;
                    book.DateReturnBookToHuman = DateTime.Now;
                    DataProvider.Ins.DB.SaveChanges();
                }

                PrintBillForLibraryReturnBookPartner();
                AllTheDepositAmountBook = 0;
                AllLibraryFines = 0;

                LvLibraryReturnBookPartner.Clear();
            });

            ReturnBookToHumanCommand = new RelayCommand<Model.Book>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Book = DataProvider.Ins.DB.Books.Where(x => x.Id == p.Id).SingleOrDefault();
                

                if(LvLibraryReturnBookPartner.Count == 0)
                {

                    if (DateTime.Now <= p.LibraryDueDate)
                    {
                        MessageBox.Show("Not Return Book To Partner");
                    }
                    else if (p.IdStatus == 1)
                    {
                        MessageBox.Show("Books borrowed by customers that have not yet been returned");
                    }
                    else
                    {
                        double moneyFines = 0;
                        if (p.IdStatus == 3)
                            moneyFines = (double)p.BookPrice;

                        LvLibraryReturnBookPartner.Add(new InfoBookLibraryReturnPartner() { Book = p, MoneyFines = moneyFines });
                        AllTheDepositAmountBook += (double)p.BookPrice;
                        AllLibraryFines += moneyFines;

                        LvBook.Remove(p);
                        MessageBox.Show("Successful");
                    }
                }
                else
                {
                    int countCheckLoopBook = 0;

                    foreach(var item in LvLibraryReturnBookPartner)
                    {
                        if (item.Book.Id == p.Id)
                            countCheckLoopBook++; 
                    }

                    if(countCheckLoopBook != 0)
                    {
                        MessageBox.Show("Returned The Book");
                    }
                    else
                    {
                        if (DateTime.Now <= p.LibraryDueDate)
                        {
                            MessageBox.Show("Not Return Book To Partner");
                        }
                        else if (p.IdStatus == 1)
                        {
                            MessageBox.Show("Books borrowed by customers that have not yet been returned");
                        }
                        else
                        {
                            double moneyFines = 0;
                            if (p.IdStatus == 3)
                                moneyFines = (double)p.BookPrice;

                            LvLibraryReturnBookPartner.Add(new InfoBookLibraryReturnPartner() { Book = p, MoneyFines = moneyFines });
                            AllTheDepositAmountBook += (double)p.BookPrice;
                            AllLibraryFines += moneyFines;

                            LvBook.Remove(p);
                            MessageBox.Show("Successful");
                        }
                    }
                }
                
            });

            MouseUpBookGridCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                IsActive = false;
            });

            DoubleClickGridCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                IsActive = false;
            });
        }

        public void PrintBillForCustomerReturnBook()
        {
            FlowDocument fd = new FlowDocument();

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaWidth;
            fd.PageWidth = 325;
            fd.ColumnWidth = 325;
            fd.FontFamily = new FontFamily("Times New Roman");
            fd.LineHeight = 0.7;

            Paragraph para = new Paragraph(new Run("HIKI LIBRARY"));
            para.FontSize = 17;
            para.TextAlignment = TextAlignment.Center;
            para.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(para);

            Paragraph paraAddressLibrary = new Paragraph(new Run("Address: 330 West 42nd Street"));
            paraAddressLibrary.FontSize = 12;
            paraAddressLibrary.TextAlignment = TextAlignment.Center;
            paraAddressLibrary.Foreground = Brushes.Green;
            fd.Blocks.Add(paraAddressLibrary);

            Paragraph paraPhoneLibrary = new Paragraph(new Run("Phone: 0708046010"));
            paraPhoneLibrary.LineHeight = 1;
            paraPhoneLibrary.FontSize = 12;
            paraPhoneLibrary.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraPhoneLibrary);

            Paragraph paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Paragraph paraEnter = new Paragraph(new Run(""));
            paraEnter.FontSize = 6;
            paraEnter.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraEnter);

            Paragraph paraBill = new Paragraph(new Run("Bills Customer Return Book"));
            paraBill.LineHeight = 1;
            paraBill.FontSize = 20;
            paraBill.TextAlignment = TextAlignment.Center;
            paraBill.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraBill);

            Paragraph paraIdBill = new Paragraph(new Run(IdBillCustomerReturnBookLibrary.ToString()));
            paraIdBill.LineHeight = 1;
            paraIdBill.FontSize = 17;
            paraIdBill.FontWeight = FontWeights.Bold;
            paraIdBill.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraStaff = new Paragraph(new Run("       Staff: " + IdStaff.ToString()));
            paraStaff.LineHeight = 1;
            paraStaff.FontSize = 13;
            paraStaff.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraStaff);

            paraIdBill = new Paragraph(new Run("       Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "             Time: " + DateTime.Now.ToString("HH:mm")));
            paraIdBill.LineHeight = 0.5;
            paraIdBill.FontSize = 13;
            paraIdBill.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraPartners = new Paragraph(new Run("       Customer: " + IdHumanReturnBook.ToString()));
            paraPartners.FontSize = 13;
            paraPartners.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraPartners);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            string[] arrColumnHeader = { "ITEM NAME", "QTY", "FINE", "AMOUNT" };

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(26, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) });


            for (int i = 0; i < LvInfoBookCustomerReturn.Count + 2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            var t1 = new TextBlock() { Text = arrColumnHeader[0], Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1.SetValue(Grid.ColumnProperty, 0);
            t1.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t1);

            var t2 = new TextBlock() { Text = arrColumnHeader[1], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2.SetValue(Grid.ColumnProperty, 1);
            t2.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t2);

            var t3 = new TextBlock() { Text = arrColumnHeader[2], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3.SetValue(Grid.ColumnProperty, 2);
            t3.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t3);

            var t4 = new TextBlock() { Text = arrColumnHeader[3], Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4.SetValue(Grid.ColumnProperty, 3);
            t4.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t4);

            var t1Line = new TextBlock() { Text = "------------------------------------------------", Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1Line.SetValue(Grid.ColumnProperty, 0);
            t1Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t1Line);

            var t2Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2Line.SetValue(Grid.ColumnProperty, 1);
            t2Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t2Line);

            var t3Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3Line.SetValue(Grid.ColumnProperty, 2);
            t3Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t3Line);

            var t4Line = new TextBlock() { Text = "----------------------------------", Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4Line.SetValue(Grid.ColumnProperty, 3);
            t4Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t4Line);

            for (int i = 0; i < LvInfoBookCustomerReturn.Count; i++)
            {
                var item = new TextBlock() { Text = LvInfoBookCustomerReturn[i].Book.DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                item.SetValue(Grid.ColumnProperty, 0);
                item.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(item);

                var qty = new TextBlock() { Text = "1", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                qty.SetValue(Grid.ColumnProperty, 1);
                qty.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(qty);

                var rate = new TextBlock() { Text = LvInfoBookCustomerReturn[i].MoneyFines.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                rate.SetValue(Grid.ColumnProperty, 2);
                rate.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(rate);

                var value = new TextBlock() { Text = LvInfoBookCustomerReturn[i].Book.BookPrice.ToString(), Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                value.SetValue(Grid.ColumnProperty, 3);
                value.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(value);
            }


            fd.Blocks.Add(new BlockUIContainer(grid));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Grid gridPayment = new Grid();

            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(220, GridUnitType.Star) });
            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90, GridUnitType.Star) });

            for (int i = 0; i < 6; i++)
            {
                gridPayment.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            ObservableCollection<PaymentBill> paymenBill = new ObservableCollection<PaymentBill>();
            paymenBill.Add(new PaymentBill() { DisplayName = "Total Amount:", Amount = AllTheDepositAmount });
            paymenBill.Add(new PaymentBill() { DisplayName = "Total Fine:", Amount = AllFines });
            paymenBill.Add(new PaymentBill() { DisplayName = "", Amount = 0 });
            paymenBill.Add(new PaymentBill() { DisplayName = "Cash received:", Amount = AllTheDepositAmount });
            paymenBill.Add(new PaymentBill() { DisplayName = "Fined:", Amount = AllFines });
            paymenBill.Add(new PaymentBill() { DisplayName = "Charge back (Cash):", Amount = AllTheDepositAmount - AllFines });

            for (int i = 0; i < 6; i++)
            {
                if (i != 2)
                {
                    var displayName = new TextBlock() { Text = paymenBill[i].DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(15, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                    displayName.SetValue(Grid.ColumnProperty, 0);
                    displayName.FontSize = 14;
                    displayName.FontWeight = FontWeights.Bold;
                    displayName.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(displayName);

                    var amount = new TextBlock() { Text = paymenBill[i].Amount.ToString(), Margin = new Thickness(2, 0, 25, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    amount.SetValue(Grid.ColumnProperty, 1);
                    amount.FontSize = 14;
                    amount.TextAlignment = TextAlignment.Right;
                    amount.FontWeight = FontWeights.Bold;
                    amount.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(amount);
                }
                else
                {
                    var lineHorizontal1 = new TextBlock() { Text = "-----------------------------------------------------------------", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12 };
                    lineHorizontal1.SetValue(Grid.ColumnProperty, 0);
                    lineHorizontal1.FontSize = 10;
                    lineHorizontal1.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal1);

                    var lineHorizontal2 = new TextBlock() { Text = "-----------------------------", Margin = new Thickness(-3, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
                    lineHorizontal2.SetValue(Grid.ColumnProperty, 1);
                    lineHorizontal2.FontSize = 10;
                    lineHorizontal2.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal2);
                }
            }
            fd.Blocks.Add(new BlockUIContainer(gridPayment));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            paraLine.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraLine);

            Paragraph paraContact = new Paragraph(new Run("In a city filled with so many choices, we thank for choosing us. Please feel free to contact us if " +
                "you encounter any problems related to our services."));
            paraContact.Margin = new Thickness(15, 0, 15, 0);
            paraContact.FontSize = 12;
            paraContact.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraContact);

            Paragraph paraHappy = new Paragraph(new Run("We are happy to serve you!"));
            paraHappy.FontSize = 12;
            paraHappy.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraHappy);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            paraLine.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraLine);

            IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }

        public void PrintBillForLibraryReturnBookPartner()
        {
            FlowDocument fd = new FlowDocument();

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaWidth;
            fd.PageWidth = 325;
            fd.ColumnWidth = 325;
            fd.FontFamily = new FontFamily("Times New Roman");
            fd.LineHeight = 0.7;

            Paragraph para = new Paragraph(new Run("HIKI LIBRARY"));
            para.FontSize = 17;
            para.TextAlignment = TextAlignment.Center;
            para.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(para);

            Paragraph paraAddressLibrary = new Paragraph(new Run("Address: 330 West 42nd Street"));
            paraAddressLibrary.FontSize = 12;
            paraAddressLibrary.TextAlignment = TextAlignment.Center;
            paraAddressLibrary.Foreground = Brushes.Green;
            fd.Blocks.Add(paraAddressLibrary);

            Paragraph paraPhoneLibrary = new Paragraph(new Run("Phone: 0708046010"));
            paraPhoneLibrary.LineHeight = 1;
            paraPhoneLibrary.FontSize = 12;
            paraPhoneLibrary.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraPhoneLibrary);

            Paragraph paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Paragraph paraEnter = new Paragraph(new Run(""));
            paraEnter.FontSize = 6;
            paraEnter.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraEnter);

            Paragraph paraBill = new Paragraph(new Run("Library Bills to Partner"));
            paraBill.LineHeight = 1;
            paraBill.FontSize = 20;
            paraBill.TextAlignment = TextAlignment.Center;
            paraBill.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraBill);

            Paragraph paraIdBill = new Paragraph(new Run(IdBillLibraryReturnBookPartner.ToString()));
            paraIdBill.LineHeight = 1;
            paraIdBill.FontSize = 17;
            paraIdBill.FontWeight = FontWeights.Bold;
            paraIdBill.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraStaff = new Paragraph(new Run("       Staff: " + IdStaff.ToString()));
            paraStaff.LineHeight = 1;
            paraStaff.FontSize = 13;
            paraStaff.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraStaff);

            paraIdBill = new Paragraph(new Run("       Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "             Time: " + DateTime.Now.ToString("HH:mm")));
            paraIdBill.LineHeight = 0.5;
            paraIdBill.FontSize = 13;
            paraIdBill.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraPartners = new Paragraph(new Run("       Customer: " + IdHumanReturnBook.ToString()));
            paraPartners.FontSize = 13;
            paraPartners.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraPartners);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            string[] arrColumnHeader = { "ITEM NAME", "QTY", "FINE", "AMOUNT" };

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(26, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) });


            for (int i = 0; i < LvLibraryReturnBookPartner.Count + 2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            var t1 = new TextBlock() { Text = arrColumnHeader[0], Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1.SetValue(Grid.ColumnProperty, 0);
            t1.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t1);

            var t2 = new TextBlock() { Text = arrColumnHeader[1], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2.SetValue(Grid.ColumnProperty, 1);
            t2.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t2);

            var t3 = new TextBlock() { Text = arrColumnHeader[2], Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3.SetValue(Grid.ColumnProperty, 2);
            t3.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t3);

            var t4 = new TextBlock() { Text = arrColumnHeader[3], Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4.SetValue(Grid.ColumnProperty, 3);
            t4.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(t4);

            var t1Line = new TextBlock() { Text = "------------------------------------------------", Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
            t1Line.SetValue(Grid.ColumnProperty, 0);
            t1Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t1Line);

            var t2Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t2Line.SetValue(Grid.ColumnProperty, 1);
            t2Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t2Line);

            var t3Line = new TextBlock() { Text = "--------------------------", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t3Line.SetValue(Grid.ColumnProperty, 2);
            t3Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t3Line);

            var t4Line = new TextBlock() { Text = "----------------------------------", Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
            t4Line.SetValue(Grid.ColumnProperty, 3);
            t4Line.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(t4Line);

            for (int i = 0; i < LvLibraryReturnBookPartner.Count; i++)
            {
                var item = new TextBlock() { Text = LvLibraryReturnBookPartner[i].Book.DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                item.SetValue(Grid.ColumnProperty, 0);
                item.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(item);

                var qty = new TextBlock() { Text = "1", Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                qty.SetValue(Grid.ColumnProperty, 1);
                qty.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(qty);

                var rate = new TextBlock() { Text = LvLibraryReturnBookPartner[i].MoneyFines.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                rate.SetValue(Grid.ColumnProperty, 2);
                rate.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(rate);

                var value = new TextBlock() { Text = LvLibraryReturnBookPartner[i].Book.BookPrice.ToString(), Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                value.SetValue(Grid.ColumnProperty, 3);
                value.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(value);
            }


            fd.Blocks.Add(new BlockUIContainer(grid));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Grid gridPayment = new Grid();

            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(210, GridUnitType.Star) });
            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90, GridUnitType.Star) });

            for (int i = 0; i < 6; i++)
            {
                gridPayment.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            ObservableCollection<PaymentBill> paymenBill = new ObservableCollection<PaymentBill>();
            paymenBill.Add(new PaymentBill() { DisplayName = "Total Amount:", Amount = AllTheDepositAmountBook });
            paymenBill.Add(new PaymentBill() { DisplayName = "Total Fine:", Amount = AllLibraryFines });
            paymenBill.Add(new PaymentBill() { DisplayName = "", Amount = 0 });
            paymenBill.Add(new PaymentBill() { DisplayName = "Cash received Book:", Amount = LvLibraryReturnBookPartner.Count() });
            paymenBill.Add(new PaymentBill() { DisplayName = "Fined:", Amount = AllLibraryFines });
            paymenBill.Add(new PaymentBill() { DisplayName = "Charge back (Cash):", Amount = AllTheDepositAmountBook - AllLibraryFines });

            for (int i = 0; i < 6; i++)
            {
                if (i != 2)
                {
                    var displayName = new TextBlock() { Text = paymenBill[i].DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(15, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                    displayName.SetValue(Grid.ColumnProperty, 0);
                    displayName.FontSize = 14;
                    displayName.FontWeight = FontWeights.Bold;
                    displayName.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(displayName);

                    var amount = new TextBlock() { Text = paymenBill[i].Amount.ToString(), Margin = new Thickness(2, 0, 25, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                    amount.SetValue(Grid.ColumnProperty, 1);
                    amount.FontSize = 14;
                    amount.TextAlignment = TextAlignment.Right;
                    amount.FontWeight = FontWeights.Bold;
                    amount.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(amount);
                }
                else
                {
                    var lineHorizontal1 = new TextBlock() { Text = "-----------------------------------------------------------------", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12 };
                    lineHorizontal1.SetValue(Grid.ColumnProperty, 0);
                    lineHorizontal1.FontSize = 10;
                    lineHorizontal1.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal1);

                    var lineHorizontal2 = new TextBlock() { Text = "-----------------------------", Margin = new Thickness(-3, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center };
                    lineHorizontal2.SetValue(Grid.ColumnProperty, 1);
                    lineHorizontal2.FontSize = 10;
                    lineHorizontal2.SetValue(Grid.RowProperty, i);
                    gridPayment.Children.Add(lineHorizontal2);
                }
            }
            fd.Blocks.Add(new BlockUIContainer(gridPayment));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            paraLine.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraLine);

            Paragraph paraContact = new Paragraph(new Run("In a city filled with so many choices, we thank for choosing us. Please feel free to contact us if " +
                "you encounter any problems related to our services."));
            paraContact.Margin = new Thickness(15, 0, 15, 0);
            paraContact.FontSize = 12;
            paraContact.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraContact);

            Paragraph paraHappy = new Paragraph(new Run("We are happy to serve you!"));
            paraHappy.FontSize = 12;
            paraHappy.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraHappy);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            paraLine.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraLine);

            IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }
       
        public void ScoreInputSubject(string subject)
        {
            string[] arrListStr = subject.Split(',');

            foreach (string item in arrListStr)
            {
                if (item != "" && item != " ")
                {
                    if (item[0] == ' ')
                    {
                        string newItem = item.Substring(1);

                        var BookSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == newItem).SingleOrDefault();
                        BookSubject.ScoreInputSubject += 1;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        var BookSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == item).SingleOrDefault();
                        BookSubject.ScoreInputSubject += 1;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
            }

            return;
        }
    }
    public class PaymentBill : BaseViewModel
    {
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private double _Amount;
        public double Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
    }

    public class InfoBookCustomerReturn : BaseViewModel
    {
        private Model.Book _Book;
        public Model.Book Book { get => _Book; set { _Book = value; OnPropertyChanged(); } }

        private int _IdFined;
        public int IdFined { get => _IdFined; set { _IdFined = value; OnPropertyChanged(); } }

        private double _MoneyFines;
        public double MoneyFines { get => _MoneyFines; set { _MoneyFines = value; OnPropertyChanged(); } }

        private int _IdBorrowBook;
        public int IdBorrowBook { get => _IdBorrowBook; set { _IdBorrowBook = value; OnPropertyChanged(); } }
    }

    public class InfoBookLibraryReturnPartner : BaseViewModel
    {
        private Model.Book _Book;
        public Model.Book Book { get => _Book; set { _Book = value; OnPropertyChanged(); } }

        private double _MoneyFines;
        public double MoneyFines { get => _MoneyFines; set { _MoneyFines = value; OnPropertyChanged(); } }


    }
}
