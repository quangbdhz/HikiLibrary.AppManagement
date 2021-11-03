using Library_Management.Borrow;
using Library_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Init List Data
        private ObservableCollection<Model.BorrowBook> _LvBorrowBook;
        public ObservableCollection<Model.BorrowBook> LvBorrowBook { get => _LvBorrowBook; set { _LvBorrowBook = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BorrowBook> _LvBorrowBookSloved;
        public ObservableCollection<Model.BorrowBook> LvBorrowBookSloved { get => _LvBorrowBookSloved; set { _LvBorrowBookSloved = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBook;
        public ObservableCollection<Model.Book> LvBook { get => _LvBook; set { _LvBook = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBookReturned;
        public ObservableCollection<Model.Book> LvBookReturned { get => _LvBookReturned; set { _LvBookReturned = value; OnPropertyChanged(); } }
        #endregion

        private static int _IdStaff;
        public static int IdStaff { get => _IdStaff; set { _IdStaff = value; } }

        private static int _GetIdHuman;
        public static int GetIdHuman { get => _GetIdHuman; set { _GetIdHuman = value; } }

        private static int _GetIdStaffHuman;
        public static int GetIdStaffHuman { get => _GetIdStaffHuman; set { _GetIdStaffHuman = value; } }

        #region Init Variable Login Window
        public bool IsLogin { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Information Human Borrow Book
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private DateTime? _DateOfBirth;
        public DateTime? DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _UrlAvatarHuman;
        public string UrlAvatarHuman { get => _UrlAvatarHuman; set { _UrlAvatarHuman = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }
        #endregion

        private string _UserNameSearchUserHuman;
        public string UserNameSearchUserHuman { get => _UserNameSearchUserHuman; set { _UserNameSearchUserHuman = value; OnPropertyChanged(); } }

        private bool _CheckIsEnabled;
        public bool CheckIsEnabled { get => _CheckIsEnabled; set { _CheckIsEnabled = value; OnPropertyChanged(); } }

        private bool _IsEnabledCheckUserHuman;
        public bool IsEnabledCheckUserHuman { get => _IsEnabledCheckUserHuman; set { _IsEnabledCheckUserHuman = value; OnPropertyChanged(); } }

        private bool _InEnabledReturnBook;
        public bool InEnabledReturnBook { get => _InEnabledReturnBook; set { _InEnabledReturnBook = value; OnPropertyChanged(); } }

        private Visibility _OptionVisibilityHuman;
        public Visibility OptionVisibilityHuman { get => _OptionVisibilityHuman; set { _OptionVisibilityHuman = value; OnPropertyChanged(); } }

        private Visibility _OptionVisibilityChangePass;
        public Visibility OptionVisibilityChangePass { get => _OptionVisibilityChangePass; set { _OptionVisibilityChangePass = value; OnPropertyChanged(); } }

        private string _ReturnBook;
        public string ReturnBook { get => _ReturnBook; set { _ReturnBook = value; OnPropertyChanged(); } }

        private static string _WellComeHuman;
        public static string WellComeHuman { get => _WellComeHuman; set { _WellComeHuman = value; } }

        #region Init Variable Option Login Window
        private bool _CheckCustomer;
        public bool CheckCustomer { get => _CheckCustomer; set { _CheckCustomer = value; OnPropertyChanged(); } }

        private bool _CheckStaff;
        public bool CheckStaff { get => _CheckStaff; set { _CheckStaff = value; OnPropertyChanged(); } }

        private bool _CheckAdmin;
        public bool CheckAdmin { get => _CheckAdmin; set { _CheckAdmin = value; OnPropertyChanged(); } }

        private static Visibility _OptionVisibilityStatistic;
        public static Visibility OptionVisibilityStatistic { get => _OptionVisibilityStatistic; set { _OptionVisibilityStatistic = value; } }
        #endregion

        private static int _OptionChangeWidthMenu;
        public static int OptionChangeWidthMenu { get => _OptionChangeWidthMenu; set { _OptionChangeWidthMenu = value; } }

        private static int _RuleLogin;
        public static int RuleLogin { get => _RuleLogin; set { _RuleLogin = value; } }

        private static int _GetRuleLoginBorrowBook;
        public static int GetRuleLoginBorrowBook { get => _GetRuleLoginBorrowBook; set { _GetRuleLoginBorrowBook = value; } }
        
        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CheckCustomerCommand{ get; set; }
        public ICommand CheckStaffCommand { get; set; }
        public ICommand CheckAdminCommand { get; set; }
        public ICommand LoadedSplashScreenCommand { get; set; }

        public LoginViewModel()
        {
            UserName = "oreki";
            Password = "12345";
            CheckCustomer = true;
            GetRuleLoginBorrowBook = 0;

            IsLogin = false;

            LoadedSplashScreenCommand = new RelayCommand<Window>((p) => { 
                return true; 
            }, (p) => 
            {
                //p.Hide();

                //SplashScreen SS = new SplashScreen(); SS.ShowDialog();

                //p.Show();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            #region Command Option Login Window
            CheckCustomerCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckCustomer = (bool)p.IsChecked;
                if (CheckCustomer == true)
                {
                    CheckStaff = !CheckCustomer;
                    CheckAdmin = !CheckCustomer;
                }
            });

            CheckStaffCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckStaff = (bool)p.IsChecked;
                if (CheckStaff == true)
                {
                    CheckCustomer = !CheckStaff;
                    CheckAdmin = !CheckStaff;
                }
            });

            CheckAdminCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckAdmin = (bool)p.IsChecked;
                if (CheckAdmin == true)
                {
                    CheckCustomer = !CheckAdmin;
                    CheckStaff = !CheckAdmin;
                }
            });
            #endregion

            

        }

        void Login(Window p)
        {
            IdStaff = 0;
            if (p == null)
            {
                return;
            }

            //if (Password == "" || Password.Length < 5)
            //{
            //    MessageBox.Show("Wrong password format");
            //    return;
            //}

            if (CheckCustomer == true)
            {
                IsEnabledCheckUserHuman = true;
                InEnabledReturnBook = false;
                OptionVisibilityHuman = Visibility.Collapsed;
                OptionVisibilityChangePass = Visibility.Visible;
                ReturnBook = "";
                GetRuleLoginBorrowBook = 0;

                string passEncode = ComputeSha256Hash(MD5Hash(Password));

                var accCount = DataProvider.Ins.DB.UserHumen.Where(x => x.UserName == UserName && x.Password == passEncode).Count();

                if (accCount > 0)
                {
                    IsLogin = true;
                    p.Hide();


                    var UserHuman = DataProvider.Ins.DB.UserHumen.Where(x => (x.UserName == UserName && x.Password == passEncode) && x.CountDelete == 0).SingleOrDefault();
                    int IdHuman = UserHuman.IdHuman;

                    var Human = DataProvider.Ins.DB.Humen.Where(x => (x.Id == IdHuman) && x.CountDelete == 0).SingleOrDefault();

                    WellComeHuman = "Wellcome to: " + Human.DisplayName;

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

                    CheckIsEnabled = false;
                    UserNameSearchUserHuman = UserName;

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

                    LvBorrowBook = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHuman && x.IdStatus == 1 && x.CountDelete == 0));
                    LvBorrowBookSloved = new ObservableCollection<Model.BorrowBook>(DataProvider.Ins.DB.BorrowBooks.Where(x => x.IdHuman == IdHuman && x.IdStatus > 1 && x.CountDelete == 0));
                    LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == IdHuman && x.IdStatusReturnBookToHuman == 4 && x.CountDelete == 0));
                    LvBookReturned = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.BorrowingIdHuman == IdHuman && x.IdStatusReturnBookToHuman == 2 && x.CountDelete == 0));
                    BorrowerWindow BW = new BorrowerWindow(); BW.ShowDialog();
                    p.ShowDialog();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("SAI TAI KHOAN HOAC MAT KHAU");
                }
            }
            else if (CheckStaff == true)
            {
                IsEnabledCheckUserHuman = false;
                InEnabledReturnBook = true;
                OptionVisibilityHuman = Visibility.Visible;
                OptionVisibilityChangePass = Visibility.Collapsed;
                OptionVisibilityStatistic = Visibility.Collapsed;
                ReturnBook = "Return Book";
                RuleLogin = 1;
                GetRuleLoginBorrowBook = 1;

                string passEncode = ComputeSha256Hash(MD5Hash(Password));

                var accCount = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.Password == passEncode && x.CountDelete == 0).Count();

                if (accCount > 0)
                {
                    var getIdStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.Password == passEncode && x.CountDelete == 0).SingleOrDefault();
                    IdStaff = getIdStaff.Id;
                    GetIdHuman = getIdStaff.IdHuman;

                    var Human = DataProvider.Ins.DB.Humen.Where(x => (x.Id == getIdStaff.IdHuman) && x.CountDelete == 0).SingleOrDefault();
                    GetIdStaffHuman = Human.Id;
                    WellComeHuman = "Wellcome to: " + Human.DisplayName;

                    IsLogin = true;
                    CheckIsEnabled = true;

                    p.Hide();
                    MainWindow MW = new MainWindow(); MW.ShowDialog();
                    p.ShowDialog();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("SAI TAI KHOAN HOAC MAT KHAU");
                }
            }
            else if(CheckAdmin == true)
            {
                RuleLogin = 2;
                OptionVisibilityStatistic = Visibility.Visible;

                string passEncode = ComputeSha256Hash(MD5Hash(Password));

                var accCount = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.Password == passEncode && x.CountDelete == 2 && x.IdAuthorityStaff == 4).Count();

                if (accCount > 0)
                {
                    var getIdStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.Password == passEncode && x.CountDelete == 2 && x.IdAuthorityStaff == 4).SingleOrDefault();
                    IdStaff = getIdStaff.Id;
                    GetIdHuman = getIdStaff.IdHuman;

                    var Human = DataProvider.Ins.DB.Humen.Where(x => (x.Id == getIdStaff.IdHuman) && x.CountDelete == 0).SingleOrDefault();
                    GetIdStaffHuman = Human.Id;
                    WellComeHuman = "Wellcome to: " + Human.DisplayName;

                    IsLogin = true;
                    CheckIsEnabled = true;

                    p.Hide();
                    MainWindow MW = new MainWindow(); MW.ShowDialog();
                    p.ShowDialog();

                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("SAI TAI KHOAN HOAC MAT KHAU");
                }


                //MessageBox.Show("Welcome Admin Of Library");
            }
            else
            {
                MessageBox.Show("Seclect Rule Login");
            }

            //pass test = xJqVYnJM9i
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
