using Library_Management.Book;
using Library_Management.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.Book
{
    public class BookViewModel : LoginViewModel
    {
        #region

        private ObservableCollection<Model.Book> _LvBookAvailable;
        public ObservableCollection<Model.Book> LvBookAvailable { get => _LvBookAvailable; set { _LvBookAvailable = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBookBorrowed;
        public ObservableCollection<Model.Book> LvBookBorrowed { get => _LvBookBorrowed; set { _LvBookBorrowed = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Book> _LvBookLiquidation;
        public ObservableCollection<Model.Book> LvBookLiquidation { get => _LvBookLiquidation; set { _LvBookLiquidation = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Human> _LvHuman;
        public ObservableCollection<Model.Human> LvHuman { get => _LvHuman; set { _LvHuman = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BookSubject> _LvSubject;
        public ObservableCollection<Model.BookSubject> LvSubject { get => _LvSubject; set { _LvSubject = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Language> _LvLanguage;
        public ObservableCollection<Model.Language> LvLanguage { get => _LvLanguage; set { _LvLanguage = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Publisher> _LvPublisher;
        public ObservableCollection<Model.Publisher> LvPublisher { get => _LvPublisher; set { _LvPublisher = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Status> _LvStatus;
        public ObservableCollection<Model.Status> LvStatus { get => _LvStatus; set { _LvStatus = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Author> _LvAuthor;
        public ObservableCollection<Model.Author> LvAuthor { get => _LvAuthor; set { _LvAuthor = value; OnPropertyChanged(); } }

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

        private Model.BookSubject _SelectedItemSubject;
        public Model.BookSubject SelectedItemSubject
        {
            get => _SelectedItemSubject;
            set
            {
                _SelectedItemSubject = value;
                OnPropertyChanged();
            }
        }

        private string _StringSubject;
        public string StringSubject { get => _StringSubject; set { _StringSubject = value; OnPropertyChanged(); } }
        
        private string _Author;
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }
        
        private Model.Language _SelectedItemLanguage;
        public Model.Language SelectedItemLanguage
        {
            get => _SelectedItemLanguage;
            set
            {
                _SelectedItemLanguage = value;
                OnPropertyChanged();
            }
        }

        private Model.Publisher _SelectedItemPublisher;
        public Model.Publisher SelectedItemPublisher
        {
            get => _SelectedItemPublisher;
            set
            {
                _SelectedItemPublisher = value;
                OnPropertyChanged();
            }
        }

        private Model.Status _SelectedItemStatus;
        public Model.Status SelectedItemStatus
        {
            get => _SelectedItemStatus;
            set
            {
                _SelectedItemStatus = value;
                OnPropertyChanged();
            }
        }

        private string _UrlImageBook;
        public string UrlImageBook { get => _UrlImageBook; set { _UrlImageBook = value; OnPropertyChanged(); } }

        private int _Number;
        public int Number { get => _Number; set { _Number = value; OnPropertyChanged(); } }

        private Model.Book _SelectedItemBook;
        public Model.Book SelectedItemBook
        {
            get => _SelectedItemBook;
            set
            {
                _SelectedItemBook = value;
                OnPropertyChanged();
                if (SelectedItemBook != null)
                {
                    DisplayName = SelectedItemBook.DisplayName;
                    SelectedItemHuman = SelectedItemBook.Human;
                    StringSubject = SelectedItemBook.BookSubject;
                    BookPrice = (double)SelectedItemBook.BookPrice;
                    Author = SelectedItemBook.Author;
                    SelectedItemLanguage = SelectedItemBook.Language;
                    SelectedItemPublisher = SelectedItemBook.Publisher;
                    SelectedItemStatus = SelectedItemBook.Status;
                    UrlImageBook = SelectedItemBook.UrlImageBook;
                    Note = SelectedItemBook.Note;
                }
            }
        }
        
        private string _FilePathProject;
        public string FilePathProject { get => _FilePathProject; set { _FilePathProject = value; OnPropertyChanged(); } }
        
        private string _CurrentSubject;
        public string CurrentSubject { get => _CurrentSubject; set { _CurrentSubject = value; OnPropertyChanged(); } }
        #endregion

        private int _OptionTabControl;
        public int OptionTabControl { get => _OptionTabControl; set { _OptionTabControl = value; OnPropertyChanged(); } }

        private bool _CheckBookTitle;
        public bool CheckBookTitle { get => _CheckBookTitle; set { _CheckBookTitle = value; OnPropertyChanged(); } }

        private bool _CheckStatus;
        public bool CheckStatus { get => _CheckStatus; set { _CheckStatus = value; OnPropertyChanged(); } }

        private static string _CurrentBookSubject;
        public static string CurrentBookSubject { get => _CurrentBookSubject; set { _CurrentBookSubject = value; } }

        private Model.Author _SelectedItemAuthor;
        public Model.Author SelectedItemAuthor
        {
            get => _SelectedItemAuthor;
            set
            {
                _SelectedItemAuthor = value;
                OnPropertyChanged();
            }
        }

        private double _BookPrice;
        public double BookPrice { get => _BookPrice; set { _BookPrice = value; OnPropertyChanged(); } }

        public static Model.Book _GetBookEdit;
        public static Model.Book GetBookEdit { get => _GetBookEdit; set { _GetBookEdit = value; } }

        public ICommand AddBookWindowCommand { get; set; }
        public ICommand EditBookWindowCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand DoubleClickBookListViewCommand { get; set; }
        public ICommand MouseUpBookListViewCommand { get; set; }
        public ICommand SelectedAuthorCombobox { get; set; }
        public ICommand TextTabControlCommand { get; set; }
        public ICommand SortBookTitleCommand { get; set; }
        public ICommand SortStatusCommand { get; set; }
        public BookViewModel()
        {
            LvBookAvailable = new ObservableCollection<Model.Book>();
            LvBookBorrowed = new ObservableCollection<Model.Book>();
            LvBookLiquidation = new ObservableCollection<Model.Book>();

            LoadBook();
            OptionTabControl = 0;

            LvHuman = new ObservableCollection<Model.Human>(DataProvider.Ins.DB.Humen.Where(x => x.CountDelete == 0));
            LvSubject = new ObservableCollection<Model.BookSubject>(DataProvider.Ins.DB.BookSubjects);
            LvLanguage = new ObservableCollection<Model.Language>(DataProvider.Ins.DB.Languages);
            LvPublisher = new ObservableCollection<Model.Publisher>(DataProvider.Ins.DB.Publishers.Where(x => x.CountDelete == 0));
            LvStatus = new ObservableCollection<Model.Status>(DataProvider.Ins.DB.Status.Where(x => x.Id == 2));



            AddBookWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {
                AddBookWindow ABW = new AddBookWindow(); ABW.ShowDialog();
                LoadBook();
            });

            EditBookWindowCommand = new RelayCommand<Object>((p) => { return true; }, (p) => { 
                CurrentSubject = SelectedItemBook.BookSubject; 
                EditBookWindow EBW = new EditBookWindow(); EBW.ShowDialog(); 
            });

            DeleteBookCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemBook == null)
                    return false;

                return true;
            }, (p) =>
            { 
                MessageBox.Show("Return Book to Partner");
            });
            
            DoubleClickBookListViewCommand = new RelayCommand<Model.Book>((p) => {
                if (p == null)
                    return false;
                return true;
            }, (p) => {
                Id = p.Id;
                DisplayName = p.DisplayName;
                StringSubject = p.BookSubject;
                BookPrice = (double)p.BookPrice;
                Author = p.Author;
                SelectedItemLanguage = p.Language;
                SelectedItemPublisher = p.Publisher;
                SelectedItemStatus = p.Status;
                UrlImageBook = p.UrlImageBook;
                Note = p.Note;
                CurrentSubject = StringSubject;
                GetBookEdit = p;
                EditBookWindow EHW = new EditBookWindow(); EHW.ShowDialog();

            });

            TextTabControlCommand = new RelayCommand<TabControl>((p) => { return true; }, (p) => {
                CheckBookTitle = false;
                CheckStatus = false;
                OptionTabControl = p.SelectedIndex;
            });

            SortBookTitleCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckBookTitle = (bool)p.IsChecked;

                if (CheckBookTitle == true)
                {
                    CheckStatus = !CheckBookTitle;

                    if (OptionTabControl == 0)
                    {
                        LvBookAvailable = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 2 && x.IdStatusReturnBookToHuman == 4).OrderByDescending(x => x.DisplayName));
                    }
                    else if (OptionTabControl == 1)
                    {
                        LvBookBorrowed = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 1 && x.IdStatusReturnBookToHuman == 4).OrderByDescending(x => x.DisplayName));
                    }
                    else
                    {

                    }
                }
            });

            SortStatusCommand = new RelayCommand<CheckBox>((p) => { return true; }, (p) => {
                CheckStatus = (bool)p.IsChecked;
                if (CheckStatus == true)
                {
                    CheckBookTitle = !CheckStatus;

                    if (OptionTabControl == 0)
                    {
                        LvBookAvailable = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 2 && x.IdStatusReturnBookToHuman == 4).OrderByDescending(x => x.IdStatus));
                    }
                    else if (OptionTabControl == 1)
                    {
                        LvBookBorrowed = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0 && x.IdStatus == 1 && x.IdStatusReturnBookToHuman == 4).OrderByDescending(x => x.IdStatus));
                    }
                    else
                    {

                    }
                }
            });
            
        }

        public string ConvertSubject(string subject, int number)
        {
            string newSubject = "";

            string[] arrListStr = subject.Split(',');

            List<string> newListSubject = new List<string>();

            foreach (string item in arrListStr)
            {
                if (item != "" && item != " ")
                {
                    newListSubject.Add(item);

                    if (item[0] == ' ')
                    {
                        string newItem = item.Substring(1);

                        var BookSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == newItem).SingleOrDefault();
                        BookSubject.ScoreInputSubject += 1 * number;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        var BookSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == item).SingleOrDefault();
                        BookSubject.ScoreInputSubject += 1 * number;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
            }

            int sizeCountSubject = newListSubject.Count;

            for (int i = 0; i < sizeCountSubject - 1; i++)
            {
                newSubject += newListSubject[i] + ",";
            }

            if (sizeCountSubject == 0)
            {
                newSubject = "";
            }
            else
            {
                newSubject += newListSubject[sizeCountSubject - 1];
            }

            return newSubject;

        }

        public void LoadBook()
        {
            LvBookAvailable.Clear();
            LvBookBorrowed.Clear();
            LvBookLiquidation.Clear();

            LvBook = new ObservableCollection<Model.Book>(DataProvider.Ins.DB.Books.Where(x => x.CountDelete == 0));
            foreach (Model.Book item in LvBook)
            {
                if (item.IdStatus == 2 && item.IdStatusReturnBookToHuman == 4)
                {
                    LvBookAvailable.Add(item);
                }
                else if (item.IdStatus == 1 && item.IdStatusReturnBookToHuman == 4)
                {
                    LvBookBorrowed.Add(item);
                }
                else
                {
                    LvBookLiquidation.Add(item);
                }
            }
        }

        public void CopyFiles(string sourcePath, string destinationPath)
        {
            System.IO.File.Copy(sourcePath, destinationPath);
        }
    }
}
