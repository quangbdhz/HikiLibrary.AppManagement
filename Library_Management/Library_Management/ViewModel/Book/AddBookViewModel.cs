using Library_Management.Book;
using Library_Management.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library_Management.ViewModel.Book
{
    public class AddBookViewModel : BookViewModel
    {
        private ObservableCollection<CountBookLibraryBorrow> _LvBookLibraryBorrowCustomer;
        public ObservableCollection<CountBookLibraryBorrow> LvBookLibraryBorrowCustomer { get => _LvBookLibraryBorrowCustomer; set { _LvBookLibraryBorrowCustomer = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BillBookOfHuman> _LvBillOfHuman;
        public ObservableCollection<Model.BillBookOfHuman> LvBillOfHuman { get => _LvBillOfHuman; set { _LvBillOfHuman = value; OnPropertyChanged(); } }

        private int _CountInitBillStatus;
        public int CountInitBillStatus { get => _CountInitBillStatus; set { _CountInitBillStatus = value; OnPropertyChanged(); } }

        private static int _CountOuputBillStatus;
        public static int CountOuputBillStatus { get => _CountOuputBillStatus; set { _CountOuputBillStatus = value; } }

        private int _IdBillOfHuman;
        public int IdBillOfHuman { get => _IdBillOfHuman; set { _IdBillOfHuman = value; OnPropertyChanged(); } }

        private int _OldIdHuman;
        public int OldIdHuman { get => _OldIdHuman; set { _OldIdHuman = value; OnPropertyChanged(); } }

        private BitmapImage _PhotoBook;
        public BitmapImage PhotoBook { get => _PhotoBook; set { _PhotoBook = value; OnPropertyChanged(); } }
        public ICommand ClickSubjectBookCommand { get; set; }
        public ICommand AddBookCommand { get; set; }
        public ICommand UpLoadImageCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand PrintBillLibraryBorrowBookOfCustomer { get; set; }
        public ICommand TestCommand { get; set; }
        public ICommand SubmitCashReceivedCommand { get; set; }
        
        public AddBookViewModel()
        {

            LvAuthor = new ObservableCollection<Model.Author>(DataProvider.Ins.DB.Authors);
            LvBookLibraryBorrowCustomer = new ObservableCollection<CountBookLibraryBorrow>();
            int countSlectedAuthor = 0;
            CountOuputBillStatus = 0;
            CountInitBillStatus = 1;

            SelectedAuthorCombobox = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemAuthor != null)
                {
                    countSlectedAuthor++;
                    if (countSlectedAuthor == 1)
                    {
                        Author += SelectedItemAuthor.DisplayName;
                    }
                    else
                    {
                        Author += ", " + SelectedItemAuthor.DisplayName;
                    }
                }
            });

            UpLoadImageCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    UrlImageBook = openFileDialog.FileName;
                }
            });

            int checkPrintBill = 0;

            PrintBillLibraryBorrowBookOfCustomer = new RelayCommand<Object>((p) => {
                if (checkPrintBill == 0)
                    return false;
                return true; 
            }, (p) =>
            {
                CountOuputBillStatus = 2;
                var getIdBillOfHuman = DataProvider.Ins.DB.BillBookOfHumen.OrderByDescending(x => x.Id).First().Id;
                IdBillOfHuman = getIdBillOfHuman;
                foreach (CountBookLibraryBorrow item in LvBookLibraryBorrowCustomer)
                {
                    var listBookLibraryBorrowHuman = new Model.ListBookLibraryBorrowHuman()
                    {
                        IdBillBookOfHuman = getIdBillOfHuman,
                        IdBook = item.Book.Id,
                        NumberOfBooks = item.NumberOfBook,
                        CountDelete = 0
                    };
                    DataProvider.Ins.DB.ListBookLibraryBorrowHumen.Add(listBookLibraryBorrowHuman);
                    DataProvider.Ins.DB.SaveChanges();
                }

                var billOfHuman = DataProvider.Ins.DB.BillBookOfHumen.Where(x => x.Id == getIdBillOfHuman && x.CountDelete == 0).SingleOrDefault();
                billOfHuman.IdStatusBill = 2;

                DataProvider.Ins.DB.SaveChanges();

                PrintBillForHumanPartner();
                LvBookLibraryBorrowCustomer.Clear();
                checkPrintBill = 0;
                CountInitBillStatus = 1;

                MessageBox.Show("Successful");
            });

            AddBookCommand = new RelayCommand<Window>((p) => 
            {
                if (DisplayName == null || SelectedItemHuman == null || StringSubject == null || Author == null || SelectedItemLanguage == null ||
                    SelectedItemPublisher == null || SelectedItemStatus == null || UrlImageBook == null || SelectedItemAuthor == null)
                    return false;

                if (Number < 1)
                    return false;

                if ((OldIdHuman != SelectedItemHuman.Id) && checkPrintBill == 1)
                    return false;

                return true; 
            }, (p) => 
            {
                if (CountOuputBillStatus == 0)
                {
                    OldIdHuman = SelectedItemHuman.Id;
                }

                FilePathProject = System.IO.Directory.GetCurrentDirectory();
                string newUrlAvatarBook = "";
                try
                {

                    for (int i = UrlImageBook.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarBook += UrlImageBook[i];
                        if ((int)UrlImageBook[i - 1] == 92)
                            break;
                    }

                    string urlReverse = newUrlAvatarBook;
                    newUrlAvatarBook = "";

                    for (int i = urlReverse.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarBook += urlReverse[i];
                    }

                    newUrlAvatarBook = FilePathProject + @"\DataImageBook\" + newUrlAvatarBook;

                    if (System.IO.File.Exists(newUrlAvatarBook))
                        System.IO.File.Delete(newUrlAvatarBook);

                    CopyFiles(UrlImageBook, newUrlAvatarBook);
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR");
                }

                string BookSubjectConvert = ConvertSubject(StringSubject, Number);

                DateTime getDate = DateTime.Now;

                int dayBorrow = 0;
                var getDateLibraryBorrowBook = DataProvider.Ins.DB.LibraryRegulations.Where(x => x.Id == 1).SingleOrDefault();

                

                if (SelectedItemHuman.Id == 1)
                {
                    dayBorrow = Int16.Parse(getDateLibraryBorrowBook.LibraryTimeToBorrowBooks) * 10;
                }
                else
                    dayBorrow = Int16.Parse(getDateLibraryBorrowBook.LibraryTimeToBorrowBooks);


                //DateReturnBookToHuman = null,
                var Book = new Model.Book()
                {
                    DisplayName = DisplayName,
                    BorrowingIdHuman = SelectedItemHuman.Id,
                    BookSubject = BookSubjectConvert,
                    Author = Author,
                    IdLanguage = SelectedItemLanguage.Id,
                    IdPublisher = SelectedItemPublisher.Id,
                    IdStatus = SelectedItemStatus.Id,
                    IdStatusReturnBookToHuman = 4,
                    LibraryDateBorrowed = getDate,
                    LibraryDueDate = getDate.AddDays(dayBorrow),
                    BookPrice = BookPrice,
                    Color = "Green",
                    DateReturnBookToHuman = getDate.AddDays(dayBorrow),
                    Note = Note,
                    UrlImageBook = newUrlAvatarBook,
                    CountDelete = 0
                };

                LvBookLibraryBorrowCustomer.Add(new CountBookLibraryBorrow { Book = Book, NumberOfBook = Number }) ;

                for (int i = 0; i < Number; i++)
                {
                    DataProvider.Ins.DB.Books.Add(Book);
                    DataProvider.Ins.DB.SaveChanges();
                }

                var Human = DataProvider.Ins.DB.Humen.Where(x => x.Id == SelectedItemHuman.Id).SingleOrDefault();
                Human.Score += Number * 1;
                DataProvider.Ins.DB.SaveChanges();

                var Publisher = DataProvider.Ins.DB.Publishers.Where(x => x.Id == SelectedItemPublisher.Id).SingleOrDefault();
                Publisher.Score += Number * 1;
                DataProvider.Ins.DB.SaveChanges();

                if(CountInitBillStatus == 1)
                {
                    var billBookOfHuman = new Model.BillBookOfHuman()
                    {
                        IdHuman = SelectedItemHuman.Id,
                        IdStaff = GetIdStaffHuman,
                        BorrowedDate = getDate,
                        DateOfRepayment = getDate.AddDays(dayBorrow),
                        Note = "",
                        IdStatusBill = 1,
                        CountDelete = 0
                    };
                    DataProvider.Ins.DB.BillBookOfHumen.Add(billBookOfHuman);
                    DataProvider.Ins.DB.SaveChanges();

                    CountInitBillStatus = 0;
                }
                CountOuputBillStatus = 1;
                checkPrintBill = 1;
                MessageBox.Show("Successful");
            });

            

            CloseWindowCommand = new RelayCommand<Window>((p) => {
                if (CountOuputBillStatus == 1)
                    return false;

                return true; 
            }, (p) => {  p.Close();  });

            
        }

        public void PrintBillForHumanPartner()
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

            Paragraph paraBill = new Paragraph(new Run("Library Bills to Partners"));
            paraBill.LineHeight = 1;
            paraBill.FontSize = 20;
            paraBill.TextAlignment = TextAlignment.Center;
            paraBill.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraBill);

            Paragraph paraIdBill = new Paragraph(new Run(IdBillOfHuman.ToString()));
            paraIdBill.LineHeight = 1;
            paraIdBill.FontSize = 17;
            paraIdBill.FontWeight = FontWeights.Bold;
            paraIdBill.TextAlignment = TextAlignment.Center;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraStaff = new Paragraph(new Run("       Staff: " + IdStaff.ToString())); // + IdBillOfHuman.ToString()
            paraStaff.LineHeight = 1;
            paraStaff.FontSize = 13;
            paraStaff.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraStaff);

            paraIdBill = new Paragraph(new Run("       Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "             Time: " + DateTime.Now.ToString("HH:mm")));
            paraIdBill.LineHeight = 0.5;
            paraIdBill.FontSize = 13;
            paraIdBill.TextAlignment = TextAlignment.Left;
            fd.Blocks.Add(paraIdBill);

            Paragraph paraPartners = new Paragraph(new Run("       Partners: " + SelectedItemHuman.DisplayName));
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


            for(int i = 0; i < LvBookLibraryBorrowCustomer.Count + 2; i++)
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

            for (int i = 0; i < LvBookLibraryBorrowCustomer.Count; i++)
            {
                var item = new TextBlock() { Text = LvBookLibraryBorrowCustomer[i].Book.DisplayName, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Left };
                item.SetValue(Grid.ColumnProperty, 0);
                item.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(item);

                var qty = new TextBlock() { Text = LvBookLibraryBorrowCustomer[i].NumberOfBook.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                qty.SetValue(Grid.ColumnProperty, 1);
                qty.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(qty);

                var rate = new TextBlock() { Text = LvBookLibraryBorrowCustomer[i].Book.BookPrice.ToString(), Margin = new Thickness(2, 0, 0, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
                rate.SetValue(Grid.ColumnProperty, 2);
                rate.SetValue(Grid.RowProperty, i + 2);
                grid.Children.Add(rate);

                double valueBook = (double)(LvBookLibraryBorrowCustomer[i].Book.BookPrice * LvBookLibraryBorrowCustomer[i].NumberOfBook);
                totalAmount += valueBook;

                var value = new TextBlock() { Text = valueBook.ToString(), Margin = new Thickness(2, 0, 10, 0), FontSize = 12, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, };
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

            for(int i = 0; i < 5; i++)
            {
                gridPayment.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            ObservableCollection<PaymentBill> paymenBill = new ObservableCollection<PaymentBill>();
            paymenBill.Add(new PaymentBill() { DisplayName = "Total:", Amount = totalAmount });
            paymenBill.Add(new PaymentBill() { DisplayName = "", Amount = 0 });

            for(int i = 0; i < 2; i++)
            {
                if(i != 1)
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
                    var lineHorizontal1 = new TextBlock() { Text = "-----------------------------------------------------------------", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10, 0, 0, 0), FontSize = 12};
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

            Paragraph paraDateReturnBook = new Paragraph(new Run("       Date Return Book:          " + DateTime.Now.AddDays(365).ToString("dd/MM/yyyy")));
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

    }

    public class PaymentBill : BaseViewModel
    {
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private double _Amount;
        public double Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }
    }

    public class CountBookLibraryBorrow : BaseViewModel
    {
        private Model.Book _Book;
        public Model.Book Book { get => _Book; set { _Book = value; OnPropertyChanged(); } }

        private int _NumberOfBook;
        public int NumberOfBook { get => _NumberOfBook; set { _NumberOfBook = value; OnPropertyChanged(); } }
    }
}
