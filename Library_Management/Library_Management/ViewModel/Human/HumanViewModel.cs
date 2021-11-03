using Library_Management.Human;
using Library_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library_Management.ViewModel.Human
{
    public class HumanViewModel : BaseViewModel
    {
        #region Init List Data
        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.AuthorityHuman> _LvAuthorityHuman;
        public ObservableCollection<Model.AuthorityHuman> LvAuthorityHuman { get => _LvAuthorityHuman; set { _LvAuthorityHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Gender> _LvGender;
        public ObservableCollection<Model.Gender> LvGender { get => _LvGender; set { _LvGender = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.UserHuman> _LvUserHuman;
        public ObservableCollection<Model.UserHuman> LvUserHuman { get => _LvUserHuman; set { _LvUserHuman = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Authority Human
        private Model.AuthorityHuman _SelectedItemAuthorityHuman;
        public Model.AuthorityHuman SelectedItemAuthorityHuman
        {
            get => _SelectedItemAuthorityHuman;
            set
            {
                _SelectedItemAuthorityHuman = value;
                OnPropertyChanged();
            }
        }

        private bool _CheckAuthority;
        public bool CheckAuthority { get => _CheckAuthority; set { _CheckAuthority = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Gender
        private Model.Gender _SelectedItemGender;
        public Model.Gender SelectedItemGender
        {
            get => _SelectedItemGender;
            set
            {
                _SelectedItemGender = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Init Variable UserHuman
        private static string _PasswordOfHuman;
        public static string PasswordOfHuman { get => _PasswordOfHuman; set { _PasswordOfHuman = value; } }

        private Model.UserHuman _SelectedItemUserHuman;
        public Model.UserHuman SelectedItemUserHuman
        {
            get => _SelectedItemUserHuman;
            set
            {
                _SelectedItemUserHuman = value;
                OnPropertyChanged();
            }
        }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _StringStatusChangePassword;
        public string StringStatusChangePassword { get => _StringStatusChangePassword; set { _StringStatusChangePassword = value; OnPropertyChanged(); } }

        private static string _MSUserHumanChangePassword;
        public static string MSUserHumanChangePassword { get => _MSUserHumanChangePassword; set { _MSUserHumanChangePassword = value; } }
        #endregion

        #region Init Variable Status Change Password
        private Model.StatusChangePass _SelectedItemStatusChangePass;
        public Model.StatusChangePass SelectedItemStatusChangePass
        {
            get => _SelectedItemStatusChangePass;
            set
            {
                _SelectedItemStatusChangePass = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _DatePasswordInit;
        public DateTime? DatePasswordInit { get => _DatePasswordInit; set { _DatePasswordInit = value; OnPropertyChanged(); } }

        private DateTime? _DatePasswordChange;
        public DateTime? DatePasswordChange { get => _DatePasswordChange; set { _DatePasswordChange = value; OnPropertyChanged(); } }

        private string _Changed;
        public string Changed { get => _Changed; set { _Changed = value; OnPropertyChanged(); } }

        private System.Windows.Media.Brush _ColorChangePass;
        public System.Windows.Media.Brush ColorChangePass { get => _ColorChangePass; set { _ColorChangePass = value; OnPropertyChanged(); } }
        #endregion

        #region Init Variable Human
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _MS;
        public string MS { get => _MS; set { _MS = value; OnPropertyChanged(); } }

        private DateTime? _DateOfBirth;
        public DateTime? DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _UrlAvatarHuman;
        public string UrlAvatarHuman { get => _UrlAvatarHuman; set { _UrlAvatarHuman = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private Model.Human _SelectedItemHuman;
        public Model.Human SelectedItemHuman
        {
            get => _SelectedItemHuman;
            set
            {
                _SelectedItemHuman = value;
                OnPropertyChanged();
                if (SelectedItemHuman != null)
                {
                    Id = SelectedItemHuman.Id;
                    MS = SelectedItemHuman.MS;
                    DisplayName = SelectedItemHuman.DisplayName;
                    SelectedItemAuthorityHuman = SelectedItemHuman.AuthorityHuman;
                    DateOfBirth = SelectedItemHuman.DateOfBirth;
                    SelectedItemGender = SelectedItemHuman.Gender;
                    Address = SelectedItemHuman.Address;
                    Phone = SelectedItemHuman.Phone;
                    Email = SelectedItemHuman.Email;
                    UrlAvatarHuman = SelectedItemHuman.UrlAvatarHuman;
                    Note = SelectedItemHuman.Note;
                }
            }
        }

        private bool _CheckName;
        public bool CheckName { get => _CheckName; set { _CheckName = value; OnPropertyChanged(); } }

        private bool _CheckGender;
        public bool CheckGender { get => _CheckGender; set { _CheckGender = value; OnPropertyChanged(); } }
        #endregion

        private string _FilePathProject;
        public string FilePathProject { get => _FilePathProject; set { _FilePathProject = value; OnPropertyChanged(); } }

        

        public ICommand AddHumanWindowCommand { get; set; }
        public ICommand EditHumanWindowCommand { get; set; }
        public ICommand DeleteHumanCommand { get; set; }
        public ICommand DoubleClickHumanListViewCommand { get; set; }
        public ICommand SortNameCommand { get; set; }
        public ICommand SortGenderCommand { get; set; }
        public ICommand SortAuthorityCommand { get; set; }

        #region Init Command UserHuman
        public ICommand CheckUserHumanCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand DeteleUserHumanCommand { get; set; }
        #endregion

        public HumanViewModel()
        {
            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0));
            LvAuthorityHuman = new ObservableCollection<Model.AuthorityHuman>(DataProvider.Ins.DB.AuthorityHumen.Where(x => x.Id < 5));
            //LvAuthorityHuman = new ObservableCollection<Model.AuthorityHuman>(DataProvider.Ins.DB.AuthorityHumen.Where(x => x.CountDelete == 0));
            LvGender = new ObservableCollection<Model.Gender>(DataProvider.Ins.DB.Genders);

            #region Comamnd Human
            AddHumanWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                AddHumanWindow AHW = new AddHumanWindow(); AHW.ShowDialog();
                LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen);

            });

            EditHumanWindowCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemHuman == null)
                    return false;

                return true; 
            }, (p) => { EditHumanWindow EHW = new EditHumanWindow(); EHW.ShowDialog(); });

            DeleteHumanCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemHuman == null)
                    return false;
                return true;
            }, (p) => {
                var deleteHuman = DataProvider.Ins.DB.Humen.Where(x => x.Id == SelectedItemHuman.Id && x.CountDelete == 0).SingleOrDefault();
                deleteHuman.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();

                var deleteUserHuman = DataProvider.Ins.DB.UserHumen.Where(x => x.IdHuman == deleteHuman.Id && x.CountDelete == 0).SingleOrDefault();
                deleteUserHuman.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();

                var deleteUserStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.IdHuman == deleteHuman.Id && x.CountDelete == 0).SingleOrDefault();
                if (deleteUserStaff != null)
                {
                    deleteUserHuman.CountDelete = 1;
                    //DataProvider.Ins.DB.SaveChanges();
                    //MessageBox.Show("Oke");
                }

                LvHuman.Remove(deleteHuman);
                // xóa human, là xóa userhuman, xóa nhân viên nếu có làm việc, xóa tài khoảng userstaff
                MessageBox.Show("Successful");
            });

            DoubleClickHumanListViewCommand = new RelayCommand<Model.Human>((p) =>
            {
                if (p == null)
                    return false;
                return true;
            }, (p) =>
            {
                Id = p.Id;
                MS = p.MS;
                DisplayName = p.DisplayName;
                DateOfBirth = p.DateOfBirth;
                SelectedItemGender = p.Gender;
                Address = p.Address;
                Phone = p.Phone;
                Email = p.Email;
                UrlAvatarHuman = p.UrlAvatarHuman;
                Note = p.Note;
                EditHumanWindow EHW = new EditHumanWindow(); EHW.ShowDialog();
            });

            #endregion

            #region Command Sort Human            
            SortNameCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                CheckName = (bool)p.IsChecked;
                if (CheckName == true)
                {
                    CheckGender = !CheckName;
                    CheckAuthority = !CheckName;
                    LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).OrderByDescending(x => x.DisplayName));
                }
            });

            SortGenderCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                CheckGender = (bool)p.IsChecked;
                if (CheckGender == true)
                {
                    CheckName = !CheckGender;
                    CheckAuthority = !CheckGender;
                    LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).OrderByDescending(x => x.IdGender));
                }
            });

            SortAuthorityCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                CheckAuthority = (bool)p.IsChecked;
                if (CheckAuthority == true)
                {
                    CheckGender = !CheckAuthority;
                    CheckName = !CheckAuthority;
                    LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0).OrderByDescending(x => x.IdAuthorityHuman));
                }
            });
            #endregion

            #region Command UserHuman
            CheckUserHumanCommand = new RelayCommand<Model.Human>((p) => { return true; }, (p) =>
            {
                var UserHuman = DataProvider.Ins.DB.UserHumen.Where(x => x.IdHuman == p.Id).SingleOrDefault();

                if (UserHuman.IdStatusChangePass == 1)
                {
                    ColorChangePass = System.Windows.Media.Brushes.Red;
                }
                else
                    ColorChangePass = System.Windows.Media.Brushes.Green;

                LvUserHuman = new ObservableCollection<Model.UserHuman>(DataProvider.Ins.DB.UserHumen.Where(x => x.IdHuman == p.Id));

                if (LvUserHuman[0].IdStatusChangePass == 1)
                {
                    StringStatusChangePassword = "Chưa đổi mật khẩu";
                }
                else
                {
                    StringStatusChangePassword = "Đã đổi mật khẩu";
                }


                CheckUserHumanWindow CUHW = new CheckUserHumanWindow(); CUHW.ShowDialog();
            });

            ResetPasswordCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                int getIdHuman = LvUserHuman[0].IdHuman;
                var getHuman = DataProvider.Ins.DB.Humen.Where(x => x.Id == getIdHuman).SingleOrDefault();
                PasswordOfHuman = CreatePassword(10);

                PrintInformationHuman(getHuman.MS, getHuman.DisplayName, getHuman.DateOfBirth, getHuman.Address, getHuman.Email, getHuman.Phone, getHuman.Note, 
                    getHuman.UrlAvatarHuman, getHuman.IdAuthorityHuman, getHuman.IdGender, PasswordOfHuman);

                var userHuman = DataProvider.Ins.DB.UserHumen.Where(x => x.IdHuman == getIdHuman && x.CountDelete == 0).SingleOrDefault();
                userHuman.Password = ComputeSha256Hash(MD5Hash(PasswordOfHuman));
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");
            });

            ChangePasswordCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                MSUserHumanChangePassword = LvUserHuman[0].UserName;
                ChangePassUserHumandWindow CPUHW = new ChangePassUserHumandWindow(); CPUHW.ShowDialog();
            });

            DeteleUserHumanCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("Hello Bro. See you again!");
            });
            #endregion
        }

        public static void PrintInformationHuman(string MS, string DisplayName, DateTime? DateOfBirth, string Address, string Email, string Phone, string Note, 
            string newUrlAvatarHuman, int authorityHuman, int genderHuman, string passwordHuman)
        {
            string stringGenderHuman = GetGenderHuman(genderHuman);
            string stringAuthorityHuman = GetAuthorityHuman(authorityHuman);

            FlowDocument fd = new FlowDocument();

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            fd.PageHeight = pd.PrintableAreaHeight;
            fd.PageWidth = 816;
            fd.ColumnWidth = 816;

            Paragraph para = new Paragraph(new Run("INFORMATION HUMAN"));
            para.FontSize = 18;
            para.TextAlignment = TextAlignment.Center;
            para.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(para);

            Image image = new Image();

            Uri uri = new Uri(newUrlAvatarHuman, UriKind.Relative);
            BitmapImage bimg = new BitmapImage(uri);

            image.Source = bimg;
            image.MaxHeight = 200;
            image.MaxWidth = 300;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            fd.Blocks.Add(new BlockUIContainer(image));

            string[] arrColumnHeader = { "Id", "Full Name", "Date Of Birth", "Authority", "Gender", "Address", "Phone", "Email", "Note" };

            Table table = new Table();
            TableRowGroup tableRowGroup = new TableRowGroup();
            TableRow r = new TableRow();

            for (int j = 0; j < arrColumnHeader.Length; j++)
            {

                r.Cells.Add(new TableCell(new Paragraph(new Run(arrColumnHeader[j]))));
                r.Cells[j].ColumnSpan = 8;
                r.Cells[j].Padding = new Thickness(4);
                r.Cells[j].BorderBrush = Brushes.Black;
                r.Cells[j].FontWeight = FontWeights.Bold;
                r.Cells[j].Background = Brushes.DarkGray;
                r.Cells[j].Foreground = Brushes.White;
                r.Cells[j].BorderThickness = new Thickness(1, 1, 1, 1);
                r.Cells[j].TextAlignment = TextAlignment.Center;
            }

            tableRowGroup.Rows.Add(r);
            table.RowGroups.Add(tableRowGroup);

            table.BorderBrush = Brushes.Gray;
            table.BorderThickness = new Thickness(1, 1, 0, 0);
            table.FontSize = 12;
            tableRowGroup = new TableRowGroup();
            r = new TableRow();

            r.Cells.Add(new TableCell(new Paragraph(new Run(MS.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(DisplayName.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(DateOfBirth.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(stringAuthorityHuman.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(stringGenderHuman.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(Address.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(Phone.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(Email.ToString()))));
            r.Cells.Add(new TableCell(new Paragraph(new Run(Note))));


            for (int i = 0; i < 9; i++)
            {
                r.Cells[i].ColumnSpan = 8;
                r.Cells[i].Padding = new Thickness(4);
                r.Cells[i].BorderBrush = Brushes.DarkGray;
                r.Cells[i].BorderThickness = new Thickness(0, 0, 1, 1);
                if (i != 1)
                {
                    r.Cells[i].TextAlignment = TextAlignment.Center;
                }
            }


            tableRowGroup.Rows.Add(r);
            table.RowGroups.Add(tableRowGroup);

            fd.Blocks.Add(table);

            fd.Blocks.Add(new Paragraph(new Run("")));

            Paragraph paraUser = new Paragraph(new Run("USER HUMAN"));
            paraUser.FontSize = 16;
            paraUser.TextAlignment = TextAlignment.Center;
            paraUser.FontWeight = FontWeights.Bold;
            fd.Blocks.Add(paraUser);

            Paragraph paraUserName = new Paragraph(new Run("Username Library: " + MS));
            paraUserName.FontSize = 14;
            paraUserName.Foreground = Brushes.DarkGreen;
            fd.Blocks.Add(paraUserName);


            Paragraph paraPassword = new Paragraph(new Run("Password Library: " + passwordHuman));
            paraPassword.FontSize = 14;
            paraPassword.Foreground = Brushes.Red;
            fd.Blocks.Add(paraPassword);

            IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");
        }

        public static string GetAuthorityHuman(int option)
        {
            if(option == 1)
            {
                return "Student";
            }
            else if(option == 2)
            {
                return "Teacher";
            }
            else if (option == 3)
            {
                return "Staff";
            }
            else if (option == 4)
            {
                return "Manager";
            }
            else if (option == 5)
            {
                return "Director";
            }
            else if (option == 6)
            {
                return "Chairman";
            }
            else
            {
                return "";
            }

        }

        public static string GetGenderHuman(int option)
        {
            if (option == 1)
            {
                return "Male";
            }
            else if (option == 2)
            {
                return "Female";
            }
            else
            {
                return "Custom";
            }
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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
