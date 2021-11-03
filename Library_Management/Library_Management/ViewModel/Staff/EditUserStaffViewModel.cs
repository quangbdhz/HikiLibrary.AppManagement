using Library_Management.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Library_Management.ViewModel.Staff
{
    class EditUserStaffViewModel : UserStaffViewModel
    {
        private int _CurrentId;
        public int CurrentId { get => _CurrentId; set { _CurrentId = value; OnPropertyChanged(); } }

        private string _CurrentUserName;
        public string CurrentUserName { get => _CurrentUserName; set { _CurrentUserName = value; OnPropertyChanged(); } }

        public ICommand EditCommand { get; set; }

        public EditUserStaffViewModel()
        {
            int countUserNameChange = 0;
            EditCommand = new RelayCommand<Object>((p) =>
            {
                countUserNameChange++;
                if(countUserNameChange == 1)
                {
                    CurrentUserName = UserName;
                }

                if (UserName == "" || UserName == null || SelectedItemAuthorityStaff == null)
                    return false;

                var checkAddUser = DataProvider.Ins.DB.UserStaffs.Where(x => x.UserName == UserName && x.IdAuthorityStaff == SelectedItemAuthorityStaff.Id && x.CountDelete == 0).Count();
                if (checkAddUser > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var User = DataProvider.Ins.DB.UserStaffs.Where(x => x.Id == Id && x.CountDelete == 0).SingleOrDefault();
                User.IdHuman = SelectedItemHuman.Id;
                User.UserName = UserName;
                User.Password = Password;
                User.IdAuthorityStaff = SelectedItemAuthorityStaff.Id;
                User.Note = Note;
                User.CountDelete = 0;

                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");
            });
        }
    }
}
