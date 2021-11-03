using Library_Management.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library_Management.ViewModel.Book
{
    class EditBookViewModel : BookViewModel
    {
        private ObservableCollection<Model.Book> _GetIdBookEdit;
        public ObservableCollection<Model.Book> GetIdBookEdit { get => _GetIdBookEdit; set { _GetIdBookEdit = value; OnPropertyChanged(); } }

        private string _GetUrlImageBook;
        public string GetUrlImageBook { get => _GetUrlImageBook; set { _GetUrlImageBook = value; OnPropertyChanged(); } }

        private string _UrlImageBookDelete;
        public string UrlImageBookDelete { get => _UrlImageBookDelete; set { _UrlImageBookDelete = value; OnPropertyChanged(); } }

        public ICommand UpLoadImageCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand TestCommand { get; set; }
        

        public EditBookViewModel()
        {
            FilePathProject = System.IO.Directory.GetCurrentDirectory();

            string newUrlAvatarBook = "";
            int countCheckUpload = 0;

            LvAuthor = new ObservableCollection<Model.Author>(DataProvider.Ins.DB.Authors);
            int countSlectedAuthor = 0;

            SelectedAuthorCombobox = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
               if(SelectedItemAuthor != null)
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
                    GetUrlImageBook = openFileDialog.FileName;

                    newUrlAvatarBook = "";

                    for (int i = GetUrlImageBook.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarBook += GetUrlImageBook[i];
                        if ((int)GetUrlImageBook[i - 1] == 92)
                            break;
                    }

                    string urlReverse = newUrlAvatarBook;
                    newUrlAvatarBook = "";

                    for (int i = urlReverse.Length - 1; i >= 0; i--)
                    {
                        newUrlAvatarBook += urlReverse[i];
                    }

                    newUrlAvatarBook = FilePathProject + @"\DataImageBook\" + newUrlAvatarBook;

                    UrlImageBook = GetUrlImageBook;

                    UrlImageBookDelete = newUrlAvatarBook;

                    countCheckUpload = 1;
                }
            });

            int countImageInSystem = 0;
            EditCommand = new RelayCommand<Window>((p) => {
                if (DisplayName == null)
                    return false;
                return true;
            }, (p) => {
                string oldUrlAvatarBook = "";

                string BookSubjectConvert = ConvertSubject(StringSubject, 1);

                try
                {
                    if (countCheckUpload == 1)
                    {
                        try
                        {
                            CopyFiles(GetUrlImageBook, newUrlAvatarBook);
                        }
                        catch
                        {
                            countImageInSystem = 1;
                        }
                        UrlImageBook = newUrlAvatarBook;
                    }

                    GetIdBookEdit = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.DisplayName == GetBookEdit.DisplayName && x.Author == GetBookEdit.Author && x.BookPrice == GetBookEdit.BookPrice &&
                                    x.BorrowingIdHuman == GetBookEdit.BorrowingIdHuman && x.LibraryDateBorrowed == x.LibraryDateBorrowed && x.UrlImageBook == GetBookEdit.UrlImageBook));

                    foreach(Model.Book item in GetIdBookEdit)
                    {
                        var Book = DataProvider.Ins.DB.Books.Where(x => x.Id == item.Id && x.CountDelete == 0).SingleOrDefault();
                        oldUrlAvatarBook = Book.UrlImageBook;
                        Book.DisplayName = DisplayName;
                        Book.BookSubject = BookSubjectConvert;
                        Book.Author = Author;
                        Book.IdLanguage = SelectedItemLanguage.Id;
                        Book.IdPublisher = SelectedItemPublisher.Id;
                        Book.IdStatus = SelectedItemStatus.Id;
                        Book.BookPrice = BookPrice;
                        Book.UrlImageBook = UrlImageBook;
                        Book.Note = Note;
                        Book.CountDelete = 0;

                        DataProvider.Ins.DB.SaveChanges();
                    }

                    if (countCheckUpload == 1 && countImageInSystem == 0)
                    {
                        if(oldUrlAvatarBook != UrlImageBook)
                        {
                            var deleteImage = new Model.ListDetleImage() { UrlImageDelete = oldUrlAvatarBook };
                            DataProvider.Ins.DB.ListDetleImages.Add(deleteImage);
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        
                    }
                    MessageBox.Show("Successful");
                }
                catch
                {
                    MessageBox.Show("ERROR");
                }
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }

    }
}
