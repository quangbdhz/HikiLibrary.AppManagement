using Library_Management.Model;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Library_Management.ViewModel.Human
{
    public class EditHumanViewModel : HumanViewModel
    {
        private string _GetUrlImageHuman;
        public string GetUrlImageHuman { get => _GetUrlImageHuman; set { _GetUrlImageHuman = value; OnPropertyChanged(); } }

        private string _UrlImageHumanDelete;
        public string UrlImageHumanDelete { get => _UrlImageHumanDelete; set { _UrlImageHumanDelete = value; OnPropertyChanged(); } }

        public ICommand UpLoadImageCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public EditHumanViewModel()
        {
            FilePathProject = System.IO.Directory.GetCurrentDirectory();
            string newUrlAvatarHuman = "";
            int countUpload = 0;

            UpLoadImageCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    GetUrlImageHuman = openFileDialog.FileName;

                    newUrlAvatarHuman = "";
                    try
                    {
                        for (int i = GetUrlImageHuman.Length - 1; i >= 0; i--)
                        {
                            newUrlAvatarHuman += GetUrlImageHuman[i];
                            if ((int)GetUrlImageHuman[i - 1] == 92)
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
                    

                        UrlImageHumanDelete = UrlAvatarHuman;


                        UrlAvatarHuman = GetUrlImageHuman;

                        countUpload = 1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ERROR");
                    }
                }
            });

            EditCommand = new RelayCommand<Window>((p) => {
                if (DisplayName == null || MS == null || SelectedItemAuthorityHuman == null || DateOfBirth == null || Address == null || SelectedItemGender == null 
                    || Phone == null || Email == null || UrlAvatarHuman == null)
                    return false;

                return true;
            }, (p) => {
                string oldUrlAvatarHuman = "";

                var Human = DataProvider.Ins.DB.Humen.Where(x => x.Id == Id && x.CountDelete == 0).SingleOrDefault();
                oldUrlAvatarHuman = Human.UrlAvatarHuman;
                Human.MS = MS;
                Human.DisplayName = DisplayName;
                Human.IdAuthorityHuman = SelectedItemAuthorityHuman.Id;
                Human.DateOfBirth = (DateTime)DateOfBirth;
                Human.Address = Address;
                Human.IdGender = SelectedItemGender.Id;
                Human.Phone = Phone;
                Human.Email = Email;
                Human.UrlAvatarHuman = UrlAvatarHuman;
                Human.Note = Note;
                Human.CountDelete = 0;
                DataProvider.Ins.DB.SaveChanges();
                
                if(countUpload == 1)
                {
                    CopyFiles(GetUrlImageHuman, newUrlAvatarHuman);
                    UrlAvatarHuman = newUrlAvatarHuman;
                    countUpload = 0;
                    var deleteImage = new Model.ListDetleImage() { UrlImageDelete = oldUrlAvatarHuman };
                    DataProvider.Ins.DB.ListDetleImages.Add(deleteImage);
                    DataProvider.Ins.DB.SaveChanges();
                }

                MessageBox.Show("Successful");
            });


            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            });
        }

        public void CopyFiles(string sourcePath, string destinationPath)
        {
            System.IO.File.Copy(sourcePath, destinationPath);
        }
    }
}
