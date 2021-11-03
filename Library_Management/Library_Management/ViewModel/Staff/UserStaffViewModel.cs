using Library_Management.Model;
using Library_Management.Staff;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Library_Management.ViewModel.Staff
{
    public class UserStaffViewModel : BaseViewModel
    {
        private ObservableCollection<Model.UserStaff> _LvUserStaff;
        public ObservableCollection<Model.UserStaff> LvUserStaff { get => _LvUserStaff; set { _LvUserStaff = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.AuthorityStaff> _LvAuthorityStaff;
        public ObservableCollection<Model.AuthorityStaff> LvAuthorityStaff { get => _LvAuthorityStaff; set { _LvAuthorityStaff = value; OnPropertyChanged(); } }

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

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _RePassword;
        public string RePassword { get => _RePassword; set { _RePassword = value; OnPropertyChanged(); } }

        private Model.AuthorityStaff _SelectedItemAuthorityStaff;
        public Model.AuthorityStaff SelectedItemAuthorityStaff
        {
            get => _SelectedItemAuthorityStaff;
            set
            {
                _SelectedItemAuthorityStaff = value;
                OnPropertyChanged();
            }
        }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private Model.UserStaff _SelectedItem;
        public Model.UserStaff SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    SelectedItemHuman = SelectedItem.Human;
                    UserName = SelectedItem.UserName;
                    Password = SelectedItem.Password;
                    SelectedItemAuthorityStaff = SelectedItem.AuthorityStaff;
                    Note = SelectedItem.Note;
                }
            }
        }

        private int _CheckClickLvUser;
        public int CheckClickLvUser { get => _CheckClickLvUser; set { _CheckClickLvUser = value; OnPropertyChanged(); } }

        public ICommand AddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand ChangePasswordUserCommand { get; set; }
        public ICommand DoubleClickUserListViewCommand { get; set; }
        public ICommand MouseUpUserListViewCommand { get; set; }

        public UserStaffViewModel()
        {
            CheckClickLvUser = 0;

            LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0 && x.IdAuthorityHuman != 6));
            LvAuthorityStaff = new ObservableCollection<Model.AuthorityStaff>(DataProvider.Ins.DB.AuthorityStaffs.Where(x => x.CountDelete == 0));

            AddUserCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddUserStaffWindow AUSW = new AddUserStaffWindow(); AUSW.ShowDialog();
                LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            });

            EditUserCommand = new RelayCommand<Object>((p) =>
            {
                if (CheckClickLvUser == 0)
                    return false;
                return true;
            }, (p) =>
            {
                CheckClickLvUser = 0;

                EditUserStaffWindow EUSW = new EditUserStaffWindow(); EUSW.ShowDialog();
                LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            });

            DeleteUserCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var deleteUserStaff = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == SelectedItem.Id && x.CountDelete == 0).SingleOrDefault();
                deleteUserStaff.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();
                LvUserStaff.Remove(deleteUserStaff);

                MessageBox.Show("Successful");
            });

            ChangePasswordUserCommand = new RelayCommand<Object>((p) => {
                return true;
            }, (p) =>
            {
                ChangePassUserStaffWindow CPUSW = new ChangePassUserStaffWindow(); CPUSW.ShowDialog();
                LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            });

            DoubleClickUserListViewCommand = new RelayCommand<Model.UserStaff>((p) =>
            {
                if (p == null)
                    return false;
                return true;
            }, (p) =>
            {
                Id = p.Id;
                SelectedItemHuman = p.Human;
                UserName = p.UserName;
                Password = p.Password;
                SelectedItemAuthorityStaff = p.AuthorityStaff;
                Note = p.Note;
                EditUserStaffWindow EUSW = new EditUserStaffWindow(); EUSW.ShowDialog();
                LvUserStaff = new ObservableCollection<Model.UserStaff>(DataProvider.Ins.DB.UserStaffs.Where(x => x.CountDelete == 0));
            });

            MouseUpUserListViewCommand = new RelayCommand<Model.UserStaff>((p) =>
            {
                if (p == null)
                    return false;
                return true;
            }, (p) =>
            {
                CheckClickLvUser = 1;

                Id = p.Id;
                SelectedItemHuman = p.Human;
                UserName = p.UserName;
                Password = p.Password;
                SelectedItemAuthorityStaff = p.AuthorityStaff;
                Note = p.Note;
            });
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
