using Library_Management.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.Staff
{
    public class ChangePassUserStaffViewModel : UserStaffViewModel
    {
        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }

        private string _ReNewPassword;
        public string ReNewPassword { get => _ReNewPassword; set { _ReNewPassword = value; OnPropertyChanged(); } }

        public ICommand CurrentPasswordChangedCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand ReNewPasswordChangedCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ChangePassUserStaffViewModel()
        {
            Password = "";
            NewPassword = "";
            ReNewPassword = "";

            CurrentPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPassword = p.Password; });
            ReNewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { ReNewPassword = p.Password; });

            PasswordChangedCommand = new RelayCommand<Window>((p) =>
            {
                string passwordCurrentEncode = ComputeSha256Hash(MD5Hash(Password));

                var accCount = DataProvider.Ins.DB.UserStaffs.Where(x => (x.UserName == UserName && x.Password == passwordCurrentEncode) && x.CountDelete == 0).Count();

                if (accCount > 0)
                    return true;

                return false;
            }, (p) =>
            {
                if (NewPassword == ReNewPassword)
                {
                    if (NewPassword.Length > 4)
                    {
                        string newPasswordEncode = ComputeSha256Hash(MD5Hash(NewPassword));

                        var User = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.CountDelete == 0).SingleOrDefault();
                        User.IdHuman = User.IdHuman;
                        User.UserName = UserName;
                        User.Password = newPasswordEncode;
                        User.IdAuthorityStaff = User.IdAuthorityStaff;
                        User.Note = User.Note;
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Successful");
                        p.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu tối thiểu 5 kí tự.");
                    }
                }
                else
                {
                    MessageBox.Show("Nhập lại mật khẩu bị sai");
                }
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }
    }
}
