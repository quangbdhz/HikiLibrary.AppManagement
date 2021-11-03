using Library_Management.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.Staff
{
    class AddUserStaffViewModel : UserStaffViewModel
    {
        public ICommand PasswordCommand { get; set; }
        public ICommand CheckPasswordCommand { get; set; }
        public ICommand AddUserStaffCommand { get; set; }
        public ICommand CloseAddUserStaffWindowCommand { get; set; }
        public AddUserStaffViewModel()
        {
            PasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

            CheckPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });

            AddUserStaffCommand = new RelayCommand<Object>((p) =>
            {
                if (UserName == "" || UserName == null || SelectedItemHuman == null || SelectedItemAuthorityStaff == null)
                    return false;

                if(RePassword != null && Password != null)
                {
                    if (RePassword != Password || Password.Length < 5)
                        return false;
                }

                var checkAddUser = DataProvider.Ins.DB.UserStaffs.Where(x => (x.IdHuman == SelectedItemHuman.Id || x.UserName == UserName) && x.CountDelete == 0).Count();
                if (checkAddUser > 0)
                    return false;

                return true;
            }, (p) =>
            {

                string passEncode = ComputeSha256Hash(MD5Hash(Password));

                var UserStaff = new Model.UserStaff() { IdHuman = SelectedItemHuman.Id, UserName = UserName, Password = passEncode, IdAuthorityStaff = SelectedItemAuthorityStaff.Id, 
                    Note = Note, ScoreInputBook = 0, ScoreOuputBook = 0, CountDelete = 0 };

                DataProvider.Ins.DB.UserStaffs.Add(UserStaff);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Successful");
            });


            CloseAddUserStaffWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

        }
    
    }
}
