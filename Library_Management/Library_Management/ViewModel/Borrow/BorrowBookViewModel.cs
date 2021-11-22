using Library_Management.Borrow;
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
    class BorrowBookViewModel : LoginViewModel
    {
        private ObservableCollection<Model.BorrowBook> _LvBorrowBookNotReturn;
        public ObservableCollection<Model.BorrowBook> LvBorrowBookNotReturn { get => _LvBorrowBookNotReturn; set { _LvBorrowBookNotReturn = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBookReturned;
        public ObservableCollection<Model.BorrowBook> LvBorrowBookReturned { get => _LvBorrowBookReturned; set { _LvBorrowBookReturned = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBookLost;
        public ObservableCollection<Model.BorrowBook> LvBorrowBookLost { get => _LvBorrowBookLost; set { _LvBorrowBookLost = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Fined> _LvFined;
        public ObservableCollection<Model.Fined> LvFined { get => _LvFined; set { _LvFined = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Status> _LvStatus;
        public ObservableCollection<Model.Status> LvStatus { get => _LvStatus; set { _LvStatus = value; OnPropertyChanged(); } }

        private ObservableCollection<CountBookCustomerBorrow> _LvBookCustomerBorrowLibrary;
        public ObservableCollection<CountBookCustomerBorrow> LvBookCustomerBorrowLibrary { get => _LvBookCustomerBorrowLibrary; set { _LvBookCustomerBorrowLibrary = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private Model.Human _SelectedItemHuman;
        public Model.Human SelectedItemHuman
        {
            get => _SelectedItemHuman;
            set
            {
                _SelectedItemHuman = value;
                OnPropertyChanged();
            }
        }

        private Model.Book _SelectedItemBook;
        public Model.Book SelectedItemBook
        {
            get => _SelectedItemBook;
            set
            {
                _SelectedItemBook = value;
                OnPropertyChanged();
            }
        }

        private int _IdBook;
        public int IdBook { get => _IdBook; set { _IdBook = value; OnPropertyChanged(); } }

        private DateTime? _DateBorrowed;
        public DateTime? DateBorrowed { get => _DateBorrowed; set { _DateBorrowed = value; OnPropertyChanged(); } }

        private DateTime? _DueDate;
        public DateTime? DueDate { get => _DueDate; set { _DueDate = value; OnPropertyChanged(); } }

        private Model.Status _SelectedItemStatus;
        public Model.Status SelectedItemStatus
        {
            get => _SelectedItemStatus;
            set
            {
                _SelectedItemStatus = value;
                OnPropertyChanged();
            }
        }

        private Model.Fined _SelectedItemFined;
        public Model.Fined SelectedItemFined
        {
            get => _SelectedItemFined;
            set
            {
                _SelectedItemFined = value;
                OnPropertyChanged();
            }
        }

        private double _ContractualFine;
        public double ContractualFine { get => _ContractualFine; set { _ContractualFine = value; OnPropertyChanged(); } }

        private Model.BorrowBook _SelectedItemBorrowBook;
        public Model.BorrowBook SelectedItemBorrowBook
        {
            get => _SelectedItemBorrowBook;
            set
            {
                _SelectedItemBorrowBook = value;
                OnPropertyChanged();
                if (SelectedItemBorrowBook != null)
                {
                    Id = SelectedItemBorrowBook.Id;
                    SelectedItemHuman = SelectedItemBorrowBook.Human;
                    SelectedItemBook = SelectedItemBorrowBook.Book;
                    DateBorrowed = SelectedItemBorrowBook.DateBorrowed;
                    DueDate = SelectedItemBorrowBook.DueDate;
                    SelectedItemStatus = SelectedItemBorrowBook.Status;
                    SelectedItemFined = SelectedItemBorrowBook.Fined;
                    ContractualFine = SelectedItemBorrowBook.ContractualFine;
                    Note = SelectedItemBorrowBook.Note;
                }
            }
        }


        private System.Windows.Media.Brush _ColourChangePass;
        public System.Windows.Media.Brush ColourChangePass { get => _ColourChangePass; set { _ColourChangePass = value; OnPropertyChanged(); } }

        private int _CountInitBill;
        public int CountInitBill { get => _CountInitBill; set { _CountInitBill = value; OnPropertyChanged(); } }

        private int _IdBillOfCustomer;
        public int IdBillOfCustomer { get => _IdBillOfCustomer; set { _IdBillOfCustomer = value; OnPropertyChanged(); } }

        private double _CashReceived;
        public double CashReceived { get => _CashReceived; set { _CashReceived = value; OnPropertyChanged(); } }

        private double _TotalBill;
        public double TotalBill { get => _TotalBill; set { _TotalBill = value; OnPropertyChanged(); } }

        private int _OldIdHuman;
        public int OldIdHuman { get => _OldIdHuman; set { _OldIdHuman = value; OnPropertyChanged(); } }

        private int _CountOuputBillStatus;
        public int CountOuputBillStatus { get => _CountOuputBillStatus; set { _CountOuputBillStatus = value; OnPropertyChanged(); } }

        private int _GetAddDateCustomerReturnBook;
        public int GetAddDateCustomerReturnBook { get => _GetAddDateCustomerReturnBook; set { _GetAddDateCustomerReturnBook = value; OnPropertyChanged(); } }

        public ICommand AddBorrowBookCommand { get; set; }
        public ICommand EditBorrowBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand PrintBillCustomerBorrowBookOfLibrary { get; set; }
        public ICommand SubmitCashReceivedCommand { get; set; }
        
        public BorrowBookViewModel()
        {
            LoadBorrowBook();
            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0));
            LvFined = new ObservableCollection<Model.Fined>(DataProvider.Ins.DB.Fineds);
            LvStatus = new ObservableCollection<Model.Status>(DataProvider.Ins.DB.Status);
            LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.IdStatus == 2 && x.CountDelete == 0));
            LvBookCustomerBorrowLibrary = new ObservableCollection<CountBookCustomerBorrow>();

            CountInitBill = 1;
            CountOuputBillStatus = 0;

            AddBorrowBookCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItemHuman == null || SelectedItemBook == null)
                    return false;

                if ((OldIdHuman != SelectedItemHuman.Id) && CountOuputBillStatus == 1)
                    return false;

                return true;
            }, (p) =>
            {
                IdBook = SelectedItemBook.Id;

                if(CountOuputBillStatus == 0)
                {
                    OldIdHuman = SelectedItemHuman.Id;
                    CountOuputBillStatus = 1;
                }

                try
                {
                    var checkBook = DataProvider.Ins.DB.Books.Where(x => x.Id == IdBook && x.CountDelete == 0 && x.IdStatusReturnBookToHuman == 4).SingleOrDefault();

                    if (checkBook.IdStatus == 2)
                    {
                        checkBook.IdStatus = 1;
                        checkBook.Color = "Red";
                        DataProvider.Ins.DB.SaveChanges();

                        ScoreOuputSubject(checkBook.BookSubject);

                        var addScoreStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == IdStaff && x.CountDelete == 0).SingleOrDefault();
                        addScoreStaff.ScoreOuputBook += 1;
                        DataProvider.Ins.DB.SaveChanges();

                        DateTime getDay = DateTime.Now;

                        LvBookCustomerBorrowLibrary.Add(new CountBookCustomerBorrow() { Book = SelectedItemBook, NumberOfBook = 1 });

                        var getDateCustomerBorrowBook = DataProvider.Ins.DB.LibraryRegulations.Where(x => x.Id == 1).SingleOrDefault();
                        int dateCustomerBorrowBook = Int16.Parse(getDateCustomerBorrowBook.TimeCustomersBorrowBooks);
                        GetAddDateCustomerReturnBook = dateCustomerBorrowBook;
                        //MessageBox.Show(DateTime.Now.AddDays(dateCustomerBorrowBook).ToString());

                        var BorrowBook = new Model.BorrowBook()
                        {
                            IdHuman = SelectedItemHuman.Id,
                            IdBook = IdBook,
                            DateBorrowed = DateTime.Now,
                            DueDate = DateTime.Now.AddDays(dateCustomerBorrowBook),
                            IdStatus = 1,
                            IdFined = 1,
                            Color = "Green",
                            ContractualFine = 0,
                            Note = Note,
                            CountDelete = 0
                        };

                        LvBook.Remove(checkBook);

                        if (getDay.AddDays(dateCustomerBorrowBook) > checkBook.LibraryDueDate)
                        {
                            BorrowBook.DueDate = checkBook.LibraryDueDate;
                        }

                        DataProvider.Ins.DB.BorrowBooks.Add(BorrowBook);
                        DataProvider.Ins.DB.SaveChanges();

                        LvBorrowBookNotReturn.Add(BorrowBook);

                        if (CountInitBill == 1)
                        {
                            var billBookOfCustomer= new Model.BillBookOfCustomer()
                            {
                                IdHuman = SelectedItemHuman.Id,
                                IdStaff = GetIdStaffHuman,
                                BorrowedDate = DateTime.Now,
                                DateOfRepayment = DateTime.Now.AddDays(dateCustomerBorrowBook),
                                Note = "",
                                IdStatusBill = 1,
                                CountDelete = 0
                            };
                            DataProvider.Ins.DB.BillBookOfCustomers.Add(billBookOfCustomer);
                            DataProvider.Ins.DB.SaveChanges();

                            CountInitBill = 0;
                        }

                        MessageBox.Show("Successful");
                    }
                    else
                    {
                        MessageBox.Show("Sách đã bị mượn");
                    }
                }
                catch
                {
                    MessageBox.Show("Sách đã bị mượn A");
                }
            });

            EditBorrowBookCommand = new RelayCommand<Object>((p) =>
            {
                if (Note == "" || Note == null)
                    return false;

                return true;
            }, (p) =>
            {
                var BorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == Id && x.CountDelete == 0).SingleOrDefault();
                BorrowBook.Note = Note;

                if (BorrowBook.IdHuman == SelectedItemHuman.Id && BorrowBook.IdBook == SelectedItemBook.Id)
                {
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Successful");
                }
                else
                {
                    MessageBox.Show("Khong duoc thay doi nguoi muon sach va sach bi muon");
                }
            });

            DeleteBookCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBox.Show(SelectedItemBook.Id.ToString());
            });

            PrintBillCustomerBorrowBookOfLibrary = new RelayCommand<Object>((p) =>
            {
                if (CountOuputBillStatus == 1)
                    return true;

                return false;
            }, (p) =>
            {
                var getIdBillOfCustomer = DataProvider.Ins.DB.BillBookOfCustomers.OrderByDescending(x => x.Id).First().Id;
                IdBillOfCustomer = getIdBillOfCustomer;

                foreach (CountBookCustomerBorrow item in LvBookCustomerBorrowLibrary)
                {
                    var listBookCustomerBorrowLibrary = new Model.ListBookCustomerBorrow()
                    {
                        IdBillBookOfCustomer = getIdBillOfCustomer,
                        IdBook = item.Book.Id,
                        NumberOfBooks = 1,
                        CountDelete = 0
                    };
                    DataProvider.Ins.DB.ListBookCustomerBorrows.Add(listBookCustomerBorrowLibrary);
                    DataProvider.Ins.DB.SaveChanges();
                }

                var billOfCustomer = DataProvider.Ins.DB.BillBookOfCustomers.Where(x => x.Id == getIdBillOfCustomer && x.CountDelete == 0).SingleOrDefault();
                billOfCustomer.IdStatusBill = 2;

                DataProvider.Ins.DB.SaveChanges();

                

                PrintBillForCustomerBorrow();

                

                CountInitBill = 1;
                CountOuputBillStatus = 0;
                LvBookCustomerBorrowLibrary.Clear();
            });

            SubmitCashReceivedCommand = new RelayCommand<Window>((p) => {
                if (CashReceived == 0)
                    return false;

                if (CashReceived < TotalBill)
                    return false;

                return true;
            }, (p) => {
                p.Close();
            });
        }
        public void PrintBillForCustomerBorrow()
        {
            FlowDocument fd = new FlowDocument();
            double totalAmount = 0;

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

            Paragraph paraBill = new Paragraph(new Run("Bills Customer Borrow Book"));
            paraBill.LineHeight = 1;
            paraBill.FontSize = 20;
            paraBill.TextAlignment = TextAlignment.Center;
            paraBill.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraBill);

            Paragraph paraIdBill = new Paragraph(new Run(IdBillOfCustomer.ToString()));
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

            Paragraph paraPartners = new Paragraph(new Run("       Customer: " + SelectedItemHuman.DisplayName));
            paraPartners.FontSize = 13;
            paraPartners.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraPartners);

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            string[] arrColumnHeader = { "ITEM NAME", "QTY", "PRICE", "AMOUNT" };

            Grid grid = new Grid();

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(26, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) });


            for (int i = 0; i < LvBookCustomerBorrowLibrary.Count + 2; i++)
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

            for (int i = 0; i < LvBookCustomerBorrowLibrary.Count; i++)
            {
                var item = new TextBlock() { Text = LvBookCustomerBorrowLibrary[i].Book.DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                item.SetValue(Grid.ColumnProperty, 0);
                item.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(item);

                var qty = new TextBlock() { Text = LvBookCustomerBorrowLibrary[i].NumberOfBook.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                qty.SetValue(Grid.ColumnProperty, 1);
                qty.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(qty);

                var rate = new TextBlock() { Text = LvBookCustomerBorrowLibrary[i].Book.BookPrice.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                rate.SetValue(Grid.ColumnProperty, 2);
                rate.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(rate);

                double valueBook = (double)(LvBookCustomerBorrowLibrary[i].Book.BookPrice * LvBookCustomerBorrowLibrary[i].NumberOfBook);
                totalAmount += valueBook;

                var value = new TextBlock() { Text = valueBook.ToString(), Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                value.SetValue(Grid.ColumnProperty, 3);
                value.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(value);
            }

            TotalBill = totalAmount;

            fd.Blocks.Add(new BlockUIContainer(grid));

            paraLine = new Paragraph(new Run("-------------------------------------------------------------------------------------------"));
            paraLine.FontSize = 10;
            paraLine.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraLine);

            Grid gridPayment = new Grid();

            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(220, GridUnitType.Star) });
            gridPayment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90, GridUnitType.Star) });

            for (int i = 0; i < 5; i++)
            {
                gridPayment.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            CashReceivedLibraryWindow CRW = new CashReceivedLibraryWindow(); CRW.ShowDialog();

            ObservableCollection<PaymentBill> paymenBill = new ObservableCollection<PaymentBill>();
            paymenBill.Add(new PaymentBill() { DisplayName = "Total:", Amount = totalAmount });
            paymenBill.Add(new PaymentBill() { DisplayName = "", Amount = 0 });
            paymenBill.Add(new PaymentBill() { DisplayName = "Total(Included 10% VAT):", Amount = totalAmount });
            paymenBill.Add(new PaymentBill() { DisplayName = "Cash received:", Amount = CashReceived }); 
            paymenBill.Add(new PaymentBill() { DisplayName = "Charge back (Cash):", Amount = CashReceived - totalAmount });

            for (int i = 0; i < 5; i++)
            {
                if (i != 1)
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

            Paragraph paraDateReturnBook = new Paragraph(new Run("       Return books to the library on:          " + DateTime.Now.AddDays(GetAddDateCustomerReturnBook).ToString("dd/MM/yyyy")));
            paraDateReturnBook.FontSize = 12;
            paraDateReturnBook.TextAlignment = TextAlignment.Left;
            paraDateReturnBook.FontWeight = FontWeights.Normal;
            fd.Blocks.Add(paraDateReturnBook);

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

        

        public void ScoreOuputSubject(string subject)
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
                        BookSubject.ScoreOuputSubject += 1;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        var BookSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == item).SingleOrDefault();
                        BookSubject.ScoreOuputSubject += 1;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
            }

            return;
        }

        public void LoadBorrowBook()
        {
            LvBorrowBookNotReturn = new ObservableCollection<Model.BorrowBook>();
            LvBorrowBookReturned = new ObservableCollection<Model.BorrowBook>();
            LvBorrowBookLost = new ObservableCollection<Model.BorrowBook>();

            LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.CountDelete == 0));

            foreach (BorrowBook item in LvBorrowBook)
            {
                if (item.IdStatus == 1)
                {
                    var editBorrowBook = DataProvider.Ins.DB.BorrowBooks.Where(x => x.Id == item.Id && x.IdFined == 1 && x.CountDelete == 0).SingleOrDefault();
                    if (editBorrowBook != null)
                    {
                        if (editBorrowBook.DueDate < DateTime.Now)
                        {
                            editBorrowBook.IdFined = 2;
                            editBorrowBook.Color = "Red";
                            DataProvider.Ins.DB.SaveChanges();
                        }
                    }
                    LvBorrowBookNotReturn.Add(item);
                }
                else if (item.IdStatus == 2)
                {
                    LvBorrowBookReturned.Add(item);
                }
                else
                {
                    LvBorrowBookLost.Add(item);
                }
            }
        }

        public class CountBookCustomerBorrow : BaseViewModel
        {
            private Model.Book _Book;
            public Model.Book Book { get => _Book; set { _Book = value; OnPropertyChanged(); } }

            private int _NumberOfBook;
            public int NumberOfBook { get => _NumberOfBook; set { _NumberOfBook = value; OnPropertyChanged(); } }
        }

        public class PaymentBill : BaseViewModel
        {
            private string _DisplayName;
            public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

            private double _Amount;
            public double Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
        }
    }
}
