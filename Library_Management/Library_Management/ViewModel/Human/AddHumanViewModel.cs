using Library_Management.Model;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Library_Management.ViewModel.Human
{
    public class AddHumanViewModel: HumanViewModel
    {
        

        public ICommand UpLoadImageCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SelectGenderCommand { get; set; }
        public ICommand AddHumanCommand { get; set; }
        public AddHumanViewModel()
        {

            UpLoadImageCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    UrlAvatarHuman = openFileDialog.FileName;
                }
            });

            AddHumanCommand = new RelayCommand<Window>((p) => 
            {
                if (DisplayName == null || MS == null || SelectedItemAuthorityHuman == null || DateOfBirth == null || Address == null || SelectedItemGender == null 
                    || Phone == null || Email == null || UrlAvatarHuman == null)
                    return false;

                var checkMsHuman = DataProvider.Ins.DB.Humen.Where(x => x.MS == MS && x.CountDelete == 0).Count();
                if (checkMsHuman > 0)
                    return false;

                return true;
            }, (p) => 
            {
                FilePathProject = System.IO.Directory.GetCurrentDirectory();

                string newUrlAvatarHuman = "";

                try
                {

                    for (int i = UrlAvatarHuman.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarHuman += UrlAvatarHuman[i];
                        if ((int)UrlAvatarHuman[i - 1] == 92)
                            break;
                    }

                    string urlReverse = newUrlAvatarHuman;
                    newUrlAvatarHuman = "";

                    for (int i = urlReverse.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarHuman += urlReverse[i];
                    }

                    newUrlAvatarHuman = FilePathProject + @"\DataImageCustomer\" + newUrlAvatarHuman;

                    if (System.IO.File.Exists(newUrlAvatarHuman))
                        System.IO.File.Delete(newUrlAvatarHuman);

                    CopyFiles(UrlAvatarHuman, newUrlAvatarHuman);
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR");
                }

                var Human = new Model.Human()
                {
                    MS = MS,
                    DisplayName = DisplayName,
                    IdAuthorityHuman = SelectedItemAuthorityHuman.Id,
                    DateOfBirth = (DateTime)DateOfBirth,
                    Address = Address,
                    IdGender = SelectedItemGender.Id,
                    Phone = Phone,
                    Email = Email,
                    UrlAvatarHuman = newUrlAvatarHuman,
                    Score = 0,
                    Forfeit = 0,
                    PayFine = 0,
                    Compensation = 0,
                    Note = Note,
                    CountDelete = 0
                };
                DataProvider.Ins.DB.Humen.Add(Human);
                DataProvider.Ins.DB.SaveChanges();

                PasswordOfHuman = CreatePassword(10);
                var UserHuman = new Model.UserHuman()
                {
                    IdHuman = Human.Id,
                    UserName = Human.MS,
                    Password = ComputeSha256Hash(MD5Hash(PasswordOfHuman)),
                    DateInitPass = DateTime.Now,
                    DatePasswordChange = null,
                    IdStatusChangePass = 1,
                    Note = null,
                    CountDelete = 0
                };
                DataProvider.Ins.DB.UserHumen.Add(UserHuman);
                DataProvider.Ins.DB.SaveChanges();


                PrintInformationHuman(MS, DisplayName, DateOfBirth, Address, Email, Phone, Note,
                    newUrlAvatarHuman, SelectedItemAuthorityHuman.Id, SelectedItemGender.Id, PasswordOfHuman);

                MessageBox.Show("Successful");
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            
        }

        public void CopyFiles(string sourcePath, string destinationPath)
        {

            System.IO.File.Copy(sourcePath, destinationPath);
        }

        

        
    }
}
