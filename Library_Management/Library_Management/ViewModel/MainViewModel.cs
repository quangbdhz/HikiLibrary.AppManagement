using Library_Management.Admin;
using Library_Management.Book;
using Library_Management.Borrow;
using Library_Management.Human;
using Library_Management.Model;
using Library_Management.Staff;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Library_Management.ViewModel
{
    public class MainViewModel : LoginViewModel
    {
        private ObservableCollection<Model.ListDetleImage> _LvDeteleImage;
        public ObservableCollection<Model.ListDetleImage> LvDeteleImage{ get => _LvDeteleImage; set { _LvDeteleImage = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private double _ChangeWidthMenu;
        public double ChangeWidthMenu { get => _ChangeWidthMenu; set { _ChangeWidthMenu = value; OnPropertyChanged(); } }

        private int _CountChangeWidth;
        public int CountChangeWidth { get => _CountChangeWidth; set { _CountChangeWidth = value; OnPropertyChanged(); } }

        private static int _CountChangeThem;
        public static int CountChangeThem { get => _CountChangeThem; set { _CountChangeThem = value; } }

        private string _ColorTheme;
        public string ColorTheme { get => _ColorTheme; set { _ColorTheme = value; OnPropertyChanged(); } }

        private string _ColorText;
        public string ColorText { get => _ColorText; set { _ColorText = value; OnPropertyChanged(); } }

        private string _ColorBackgroundCharts;
        public string ColorBackgroundCharts { get => _ColorBackgroundCharts; set { _ColorBackgroundCharts = value; OnPropertyChanged(); } }

        private string _ColorFillCartesianChart;
        public string ColorFillCartesianChart { get => _ColorFillCartesianChart; set { _ColorFillCartesianChart = value; OnPropertyChanged(); } }

        private string _ColorZoneAssistCombobox;
        public string ColorZoneAssistCombobox { get => _ColorZoneAssistCombobox; set { _ColorZoneAssistCombobox = value; OnPropertyChanged(); } }

        private int _OptionMenu;
        public int OptionMenu { get => _OptionMenu; set { _OptionMenu = value; OnPropertyChanged(); } }

        private int _OptionTheme;
        public int OptionTheme { get => _OptionTheme; set { _OptionTheme = value; OnPropertyChanged(); } }

        private string _LinkFolder;
        public string LinkFolder { get => _LinkFolder; set { _LinkFolder = value; OnPropertyChanged(); } }

        private Page _PageMain;
        public Page PageMain { get => _PageMain; set { _PageMain = value; OnPropertyChanged(); } }

        private string _GetUrlAvatarStaff;
        public string GetUrlAvatarStaff { get => _GetUrlAvatarStaff; set { _GetUrlAvatarStaff = value; OnPropertyChanged(); } }

        private string _GetDisplayNameStaff;
        public string GetDisplayNameStaff { get => _GetDisplayNameStaff; set { _GetDisplayNameStaff = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionAvailableBooks;
        public SeriesCollection SeriesCollectionAvailableBooks { get => _SeriesCollectionAvailableBooks; set { _SeriesCollectionAvailableBooks = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionBookOfLibrary;
        public SeriesCollection SeriesCollectionBookOfLibrary { get => _SeriesCollectionBookOfLibrary; set { _SeriesCollectionBookOfLibrary = value; OnPropertyChanged(); } }

        private string[] _LabelsMonth;
        public string[] LabelsMonth { get => _LabelsMonth; set { _LabelsMonth = value; OnPropertyChanged(); } }

        private Func<double, string> _FormatterHuman;
        public Func<double, string> FormatterHuman { get => _FormatterHuman; set { _FormatterHuman = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionIncome;
        public SeriesCollection SeriesCollectionIncome { get => _SeriesCollectionIncome; set { _SeriesCollectionIncome = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollectionTotalBorrow;
        public SeriesCollection SeriesCollectionTotalBorrow { get => _SeriesCollectionTotalBorrow; set { _SeriesCollectionTotalBorrow = value; OnPropertyChanged(); } }

        private string _TotalHuman;
        public string TotalHuman { get => _TotalHuman; set { _TotalHuman = value; OnPropertyChanged(); } }

        private string _TotalBook;
        public string TotalBook { get => _TotalBook; set { _TotalBook = value; OnPropertyChanged(); } }

        private string _TotalNumberBorrow;
        public string TotalNumberBorrow { get => _TotalNumberBorrow; set { _TotalNumberBorrow = value; OnPropertyChanged(); } }

        private string _TotalRevenue;
        public string TotalRevenue { get => _TotalRevenue; set { _TotalRevenue = value; OnPropertyChanged(); } }

        private string _TotalStaff;
        public string TotalStaff { get => _TotalStaff; set { _TotalStaff = value; OnPropertyChanged(); } }

        private string _ValueSearch;
        public string ValueSearch { get => _ValueSearch; set { _ValueSearch = value; OnPropertyChanged(); } }

        private int _BookAvailable;
        public int BookAvailable { get => _BookAvailable; set { _BookAvailable = value; OnPropertyChanged(); } }

        private int _CountBook;
        public int CountBook { get => _CountBook; set { _CountBook = value; OnPropertyChanged(); } }

        private int _BookBorrowed;
        public int BookBorrowed { get => _BookBorrowed; set { _BookBorrowed = value; OnPropertyChanged(); } }

        private int _BookOfLibrary;
        public int BookOfLibrary { get => _BookOfLibrary; set { _BookOfLibrary = value; OnPropertyChanged(); } }

        private string[] _LabelsHuman;
        public string[] LabelsHuman { get => _LabelsHuman; set { _LabelsHuman = value; OnPropertyChanged(); } }


        private string _SizeFolderCustomer;
        public string SizeFolderCustomer { get => _SizeFolderCustomer; set { _SizeFolderCustomer = value; OnPropertyChanged(); } }

        private string _SizeFolderBook;
        public string SizeFolderBook { get => _SizeFolderBook; set { _SizeFolderBook = value; OnPropertyChanged(); } }

        private ChartValues<double> _ValueScoreHuman;
        public ChartValues<double> ValueScoreHuman { get => _ValueScoreHuman; set { _ValueScoreHuman = value; OnPropertyChanged(); } }

        private string _UserNameStaff;
        public string UserNameStaff { get => _UserNameStaff; set { _UserNameStaff = value; OnPropertyChanged(); } }

        public ICommand FunctionCommand { get; set; }
        public ICommand ChangeWidthMenuCommand { get; set; }
        public ICommand ChangeThemeDarkCommand { get; set; }
        public ICommand ChangeThemeLightCommand { get; set; }
        public ICommand GetFolder { get; set; }
        public ICommand GetBackground { get; set; }
        public ICommand ResetCommand { get; set; }

        public ICommand TestPage { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        

        public MainViewModel()
        {
            ChangeWidthMenu = 30;
            CountChangeWidth = 0;
            CountChangeThem = 0;
            ColorTheme = "#2d3436";
            ColorText = "White";
            ColorBackgroundCharts = "#282c31";
            ColorFillCartesianChart = "#1dd1a1";
            ColorZoneAssistCombobox = "Inverted";
            OptionTheme = 0;

            CheckCreateFolderData();
            ListDeteleImageNotUse();
            LoadContentCard();
            TotalBorrow();
            LoadChart();

            var getUrlAvatarStaff = DataProvider.Ins.DB.Humen.Where(x => x.Id == GetIdStaffHuman && x.CountDelete == 0).SingleOrDefault();
            if(getUrlAvatarStaff != null)
            {
                GetUrlAvatarStaff = getUrlAvatarStaff.UrlAvatarHuman;
                GetDisplayNameStaff = getUrlAvatarStaff.DisplayName;

                var getUserNameStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == GetIdStaffHuman).SingleOrDefault();
                UserNameStaff = getUserNameStaff.UserName;
            }

            FunctionCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                OptionMenu = p.SelectedIndex;
                if (OptionMenu == 0)
                {
                    TimeTableWindow TTW = new TimeTableWindow(); TTW.ShowDialog();
                }
                else if (OptionMenu == 1)
                {
                    HumanWindow HW = new HumanWindow(); HW.ShowDialog();
                }
                else if (OptionMenu == 2)
                {
                    BookWindow BW = new BookWindow(); BW.ShowDialog();
                }
                else if (OptionMenu == 3)
                {
                    InfoBookWindow IBW = new InfoBookWindow(); IBW.ShowDialog();
                }
                else if (OptionMenu == 4)
                {
                    BorrowBookWindow BBW = new BorrowBookWindow(); BBW.ShowDialog();
                }
                else if (OptionMenu == 5)
                {
                    BorrowerWindow BW = new BorrowerWindow(); BW.ShowDialog();
                }
                else if (OptionMenu == 6)
                {
                    StatisticWindow SW = new StatisticWindow(); SW.ShowDialog();
                }
                else if (OptionMenu == 7)
                {
                    BillWindow BW = new BillWindow(); BW.ShowDialog();
                }
                else if (OptionMenu == 8)
                {
                    UserStaffWindow USW = new UserStaffWindow(); USW.ShowDialog();
                }
                else if (OptionMenu == 9)
                {
                    SupportWindow SW = new SupportWindow(); SW.ShowDialog();
                }
                else
                {

                }
            });

            RefreshCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                CountBook = 0;
                BookOfLibrary = 0;
                BookAvailable = 0;

                LoadContentCard();
                TotalBorrow();
                LoadChart();
            });

            GetBackground = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.Text == "Light")
                {
                    ColorTheme = "#dfe4ea";
                    ColorText = "Black";
                    ColorZoneAssistCombobox = "Light";
                    ColorBackgroundCharts = "#c8d6e5";
                    ColorFillCartesianChart = "#ff6b6b";
                }
                else
                {
                    ColorTheme = "#2d3436";
                    ColorText = "White";
                    ColorZoneAssistCombobox = "Inverted";
                    ColorBackgroundCharts = "#282c31";
                    ColorFillCartesianChart = "#1dd1a1";
                }
            });

            HomeCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
            });

            ChangeWidthMenuCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (CountChangeWidth == 0)
                {
                    CountChangeWidth = 1;
                    ChangeWidthMenu = 180;
                }
                else
                {
                    CountChangeWidth = 0;
                    ChangeWidthMenu = 30;
                }
            });

            ChangeThemeLightCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ColorTheme = "#dfe4ea";
                ColorText = "Black";
                ColorZoneAssistCombobox = "Light";
                ColorBackgroundCharts = "#c8d6e5";
                ColorFillCartesianChart = "#ff6b6b";
                OptionTheme = 1;
            });

            ChangeThemeDarkCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ColorTheme = "#2d3436";
                ColorText = "White";
                ColorZoneAssistCombobox = "Inverted";
                ColorBackgroundCharts = "#282c31";
                ColorFillCartesianChart = "#1dd1a1";
                OptionTheme = 0;
            });

            LogOutCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });

            

        }

        public void LoadChart()
        {
            ObservableCollection<Model.Book> books = new ObservableCollection<Model.Book>();
            books = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0));


            int percentAvailable = (int)((BookAvailable / (1.0 * CountBook)) * 100);
            int percentBorrowed = 100 - percentAvailable;

            SeriesCollectionAvailableBooks = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Available",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(percentAvailable) },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Crimson
                },
                new PieSeries
                {
                    Title = "Borrowed",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(percentBorrowed) },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.BlueViolet
                }
            };

            int percentBookOfLibrary = (int)((BookOfLibrary / (1.0 * CountBook)) * 100);
            int percentPartner = 100 - percentBookOfLibrary;

            SeriesCollectionBookOfLibrary = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Of The Library",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(percentBookOfLibrary) },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.Tomato
                },
                new PieSeries
                {
                    Title = "Of Partner",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(percentPartner) },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.LightSeaGreen
                }
            };

        }

        public void LoadContentCard()
        {
            var countHuman = DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).Count();
            TotalHuman = countHuman.ToString("N0");

            ObservableCollection<Model.Book> books = new ObservableCollection<Model.Book>();
            books = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0));


            foreach (Model.Book item in books)
            {
                CountBook++;

                if (item.BorrowingIdHuman == 1) BookOfLibrary++;

                if (item.IdStatus == 2) BookAvailable++;
                else if (item.IdStatus == 3) BookBorrowed++;
                else
                {

                }
            }

            TotalBook = CountBook.ToString("N0");

            var countBorrow = DataProvider.Ins.DB.BorrowBooks.Where(x => x.CountDelete == 0).Count();
            TotalNumberBorrow = countBorrow.ToString("N0");

            var countRevenue = DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).Count();

            LabelsHuman = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" };
            ValueScoreHuman = new ChartValues<double>() { 1, 1, 1, 1, 1, 1, 1 };

            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).OrderByDescending(x => x.Score));
            int numberHuman = LvHuman.Count;
            for(int i = 0; i < 7; i++)
            {
                if (i+1 >= numberHuman) break;
                else
                {
                    LabelsHuman[i] = LvHuman[i].DisplayName;
                    ValueScoreHuman[i] = (double)LvHuman[i].Score;
                }
            }


            double? sumRevenue = 0;

            foreach (var item in LvHuman)
            {
                sumRevenue += item.Forfeit;
            }

            TotalRevenue = sumRevenue.HasValue ? sumRevenue.Value.ToString("N0") : string.Empty;


            var countStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0).Count();
            TotalStaff = countStaff.ToString("N0");
        }

        public void TotalBorrow()
        {
            ChartValues<int> countBorrowInMonth = new ChartValues<int>();
            ChartValues<int> incomeInMonth = new ChartValues<int>();

            for (int i = 0; i < 12; i++)
            {
                countBorrowInMonth.Add(0);
                incomeInMonth.Add(0);
            }

            LabelsMonth = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.CountDelete == 0));
            int getMonth = 0;

            foreach (BorrowBook item in LvBorrowBook)
            {
                getMonth = item.DateBorrowed.Month;
                countBorrowInMonth[getMonth - 1]++;
                incomeInMonth[getMonth - 1] += (int)item.ContractualFine;
            }

            SeriesCollectionTotalBorrow = new SeriesCollection
            {
                new ColumnSeries
                {
                    MaxColumnWidth=10,
                    Fill = System.Windows.Media.Brushes.Red,
                    Title = "Number",
                    Values = new ChartValues<int>(countBorrowInMonth)
                }
            };
            // #1a234a #5467c3
            SeriesCollectionIncome = new SeriesCollection
            {

                new LineSeries
                {
                    Title = "Revenue",
                    Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5467c3")),
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1a234a")),
                    Values = new ChartValues<int>(incomeInMonth)
                }
            };
        }

        public void CheckCreateFolderData()
        {
            LinkFolder = System.IO.Directory.GetCurrentDirectory();

            string addressCreateFolderImageCustomer = LinkFolder + @"\DataImageCustomer";
            string addressCreateFolderImageBook = LinkFolder + @"\DataImageBook";

            if (!Directory.Exists(addressCreateFolderImageCustomer))
            {
                Directory.CreateDirectory(addressCreateFolderImageCustomer);
            }
            if (!Directory.Exists(addressCreateFolderImageBook))
            {
                Directory.CreateDirectory(addressCreateFolderImageBook);
            }
        }

        public void ListDeteleImageNotUse()
        {
            LvDeteleImage = new ObservableCollection<Model.ListDetleImage>(DataProvider.Ins.DB.ListDetleImages);

            if(LvDeteleImage.Count != 0)
            {
                foreach (ListDetleImage item in LvDeteleImage)
                {
                    try
                    {
                        System.IO.File.Delete(item.UrlImageDelete);
                        DataProvider.Ins.DB.ListDetleImages.Remove(item);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            }
            
        }

        
    }
}
