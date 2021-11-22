using Library_Management.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Library_Management.ViewModel.Book
{
    public class InfoBookViewModel : BaseViewModel
    {
        #region Init List Data
        private ObservableCollection<Model.Author> _LvAuthor;
        public ObservableCollection<Model.Author> LvAuthor { get => _LvAuthor; set { _LvAuthor = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BookSubject> _LvSubject;
        public ObservableCollection<Model.BookSubject> LvSubject { get => _LvSubject; set { _LvSubject = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Publisher> _LvPublisher;
        public ObservableCollection<Model.Publisher> LvPublisher { get => _LvPublisher; set { _LvPublisher = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Language> _LvLanguage;
        public ObservableCollection<Model.Language> LvLanguage { get => _LvLanguage; set { _LvLanguage = value; OnPropertyChanged(); } }
        #endregion

        /*--------------------------------------------*/

        #region Init Variable Author
        private int _IdAuthor;
        public int IdAuthor { get => _IdAuthor; set { _IdAuthor = value; OnPropertyChanged(); } }

        private string _DisplayNameAuthor;
        public string DisplayNameAuthor { get => _DisplayNameAuthor; set { _DisplayNameAuthor = value; OnPropertyChanged(); } }

        private string _AddressAuthor;
        public string AddressAuthor { get => _AddressAuthor; set { _AddressAuthor = value; OnPropertyChanged(); } }

        private string _PhoneAuthor;
        public string PhoneAuthor { get => _PhoneAuthor; set { _PhoneAuthor = value; OnPropertyChanged(); } }

        private string _EmailAuthor;
        public string EmailAuthor { get => _EmailAuthor; set { _EmailAuthor = value; OnPropertyChanged(); } }

        private string _NoteAuthor;
        public string NoteAuthor { get => _NoteAuthor; set { _NoteAuthor = value; OnPropertyChanged(); } }

        private Model.Author _SelectedItemAuthor;
        public Model.Author SelectedItemAuthor
        {
            get => _SelectedItemAuthor;
            set
            {
                _SelectedItemAuthor = value;
                OnPropertyChanged();
                if (SelectedItemAuthor != null)
                {
                    IdAuthor = SelectedItemAuthor.Id;
                    DisplayNameAuthor = SelectedItemAuthor.DisplayName;
                    AddressAuthor = SelectedItemAuthor.Address;
                    PhoneAuthor = SelectedItemAuthor.Phone;
                    EmailAuthor = SelectedItemAuthor.Email;
                    NoteAuthor = SelectedItemAuthor.Note;
                }
            }
        }
        #endregion

        #region Init Variable Subject
        private int _IdSubject;
        public int IdSubject { get => _IdSubject; set { _IdSubject = value; OnPropertyChanged(); } }

        private string _DisplayNameSubject;
        public string DisplayNameSubject { get => _DisplayNameSubject; set { _DisplayNameSubject = value; OnPropertyChanged(); } }

        private string _NoteSubject;
        public string NoteSubject { get => _NoteSubject; set { _NoteSubject = value; OnPropertyChanged(); } }

        private Model.BookSubject _SelectedItemSubject;
        public Model.BookSubject SelectedItemSubject
        {
            get => _SelectedItemSubject;
            set
            {
                _SelectedItemSubject = value;
                OnPropertyChanged();
                if (SelectedItemSubject != null)
                {
                    IdSubject = SelectedItemSubject.Id;
                    DisplayNameSubject = SelectedItemSubject.DisplayName;
                    NoteSubject = SelectedItemSubject.Note;
                }
            }
        }
        #endregion

        #region Init Variable Publisher
        private int _IdPublisher;
        public int IdPublisher { get => _IdPublisher; set { _IdPublisher = value; OnPropertyChanged(); } }

        private string _DisplayNamePublisher;
        public string DisplayNamePublisher { get => _DisplayNamePublisher; set { _DisplayNamePublisher = value; OnPropertyChanged(); } }

        private string _AddressPublisher;
        public string AddressPublisher { get => _AddressPublisher; set { _AddressPublisher = value; OnPropertyChanged(); } }

        private string _PhonePublisher;
        public string PhonePublisher { get => _PhonePublisher; set { _PhonePublisher = value; OnPropertyChanged(); } }

        private string _EmailPublisher;
        public string EmailPublisher { get => _EmailPublisher; set { _EmailPublisher = value; OnPropertyChanged(); } }

        private string _NotePublisher;
        public string NotePublisher { get => _NotePublisher; set { _NotePublisher = value; OnPropertyChanged(); } }

        private Model.Publisher _SelectedItemPublisher;
        public Model.Publisher SelectedItemPublisher
        {
            get => _SelectedItemPublisher;
            set
            {
                _SelectedItemPublisher = value;
                OnPropertyChanged();
                if (SelectedItemPublisher != null)
                {
                    IdPublisher = SelectedItemPublisher.Id;
                    DisplayNamePublisher = SelectedItemPublisher.DisplayName;
                    AddressPublisher = SelectedItemPublisher.Address;
                    PhonePublisher = SelectedItemPublisher.Phone;
                    EmailPublisher = SelectedItemPublisher.Email;
                    NotePublisher = SelectedItemPublisher.Note;
                }
            }
        }
        #endregion

        #region Init Variable Language
        private int _IdLanguage;
        public int IdLanguage { get => _IdLanguage; set { _IdLanguage = value; OnPropertyChanged(); } }

        private string _DisplayNameLanguage;
        public string DisplayNameLanguage { get => _DisplayNameLanguage; set { _DisplayNameLanguage = value; OnPropertyChanged(); } }

        private string _NoteLanguage;
        public string NoteLanguage { get => _NoteLanguage; set { _NoteLanguage = value; OnPropertyChanged(); } }

        private Model.Language _SelectedItemLanguage;
        public Model.Language SelectedItemLanguage
        {
            get => _SelectedItemLanguage;
            set
            {
                _SelectedItemLanguage = value;
                OnPropertyChanged();
                if (SelectedItemLanguage != null)
                {
                    IdLanguage = SelectedItemLanguage.Id;
                    DisplayNameLanguage = SelectedItemLanguage.DisplayName;
                    NoteLanguage = SelectedItemLanguage.Note;
                }
            }
        }
        #endregion

        /*--------------------------------------------*/

        #region Command Author
        public ICommand AddAuthorCommand { get; set; }
        public ICommand EditAuthorCommand { get; set; }
        public ICommand DeleteAuthorCommand { get; set; }
        #endregion

        #region Command Subject
        public ICommand AddSubjectCommand { get; set; }
        public ICommand EditSubjectCommand { get; set; }
        public ICommand DeleteSubjectCommand { get; set; }
        #endregion

        #region Command Publisher
        public ICommand AddPublisherCommand { get; set; }
        public ICommand EditPublisherCommand { get; set; }
        public ICommand DeletePublisherCommand { get; set; }
        #endregion

        #region Command Language
        public ICommand AddLanguageCommand { get; set; }
        public ICommand EditLanguageCommand { get; set; }
        public ICommand DeleteLanguageCommand { get; set; }
        #endregion

        /*--------------------------------------------*/

        public InfoBookViewModel()
        {
            LvAuthor = new ObservableCollection<Model.Author>(DataProvider.Ins.DB.Authors.Where(x => x.CountDelete == 0));

            

            LvSubject = new ObservableCollection<Model.BookSubject>(DataProvider.Ins.DB.BookSubjects.Where(x => x.CountDelete == 0));
            LvPublisher = new ObservableCollection<Model.Publisher>(DataProvider.Ins.DB.Publishers.Where(x => x.CountDelete == 0));
            LvLanguage = new ObservableCollection<Model.Language>(DataProvider.Ins.DB.Languages.Where(x => x.CountDelete == 0));

            /*--------------------------------------------*/

            #region Command Author
            AddAuthorCommand = new RelayCommand<Object>((p) => {
                if (DisplayNameAuthor == null)
                    return false;

                if (DisplayNameAuthor == "")
                    return false;

                var checkAuthor = DataProvider.Ins.DB.Authors.Where(x => x.DisplayName == DisplayNameAuthor && x.Address == AddressAuthor && x.Phone == PhoneAuthor &&
                    x.Email == EmailAuthor && x.Note == NoteAuthor && x.CountDelete == 0).Count();
                if (checkAuthor > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var Author = new Model.Author() { DisplayName = DisplayNameAuthor, Address = AddressAuthor, Phone = PhoneAuthor, Email = EmailAuthor, Note = NoteAuthor, CountDelete = 0 };

                DataProvider.Ins.DB.Authors.Add(Author);
                DataProvider.Ins.DB.SaveChanges();

                LvAuthor.Add(Author);

            });

            EditAuthorCommand = new RelayCommand<Button>((p) => {
                if (DisplayNameAuthor == null)
                    return false;

                if (DisplayNameAuthor == "")
                    return false;

                var checkAuthor = DataProvider.Ins.DB.Authors.Where(x => x.Id != IdAuthor && x.DisplayName == DisplayNameAuthor && x.Address == AddressAuthor && x.Phone == PhoneAuthor
                    && x.Email == EmailAuthor && x.Note == NoteAuthor && x.CountDelete == 0).Count();
                if (checkAuthor > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var Author = DataProvider.Ins.DB.Authors.Where(x => x.Id == IdAuthor && x.CountDelete == 0).SingleOrDefault();
                Author.DisplayName = DisplayNameAuthor;
                Author.Address = AddressAuthor;
                Author.Phone = PhoneAuthor;
                Author.Email = EmailAuthor;
                Author.Note = NoteAuthor;
                Author.CountDelete = 0;


                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");

            });

            DeleteAuthorCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemAuthor == null)
                    return false;

                return true;
            }, (p) =>
            {
                var deleteAuthor = DataProvider.Ins.DB.Authors.Where(x => x.Id == SelectedItemAuthor.Id && x.CountDelete == 0).SingleOrDefault();
                deleteAuthor.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();
                LvAuthor.Remove(deleteAuthor);

                MessageBox.Show("Successful");
            });

            #endregion

            /*--------------------------------------------*/

            #region Command Subject
            AddSubjectCommand = new RelayCommand<Button>((p) => {
                if (DisplayNameSubject == null || DisplayNameSubject == "")
                {
                    return false;
                }

                int checkSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == DisplayNameSubject && x.Note == NoteSubject && x.CountDelete == 0).Count();

                if (checkSubject > 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var subject = new Model.BookSubject() { DisplayName = DisplayNameSubject, Note = NoteSubject, ScoreInputSubject = 0, ScoreOuputSubject = 0, CountDelete = 0 };

                DataProvider.Ins.DB.BookSubjects.Add(subject);
                DataProvider.Ins.DB.SaveChanges();

                LvSubject.Add(subject);

                MessageBox.Show("Successful");

            });

            EditSubjectCommand = new RelayCommand<Button>((p) => {
                if (DisplayNameSubject == null || DisplayNameSubject == "")
                    return false;

                var checkSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.DisplayName == DisplayNameSubject && x.Note == NoteSubject && x.CountDelete == 0).Count();
                if (checkSubject > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var subject = DataProvider.Ins.DB.BookSubjects.Where(x => x.Id == IdSubject && x.CountDelete == 0).SingleOrDefault();
                subject.DisplayName = DisplayNameSubject;
                subject.Note = NoteSubject;

                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");
            });

            DeleteSubjectCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemSubject == null)
                    return false;

                return true;
            }, (p) =>
            {
                var deleteSubject = DataProvider.Ins.DB.BookSubjects.Where(x => x.Id == SelectedItemSubject.Id && x.CountDelete == 0).SingleOrDefault();
                deleteSubject.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();
                LvSubject.Remove(deleteSubject);

                MessageBox.Show("Successful");
            });

            #endregion

            /*--------------------------------------------*/

            #region Command Pulisher
            AddPublisherCommand = new RelayCommand<Button>((p) => {
                if (DisplayNamePublisher == null || AddressPublisher == null || PhonePublisher == null || EmailPublisher == null || NotePublisher == null)
                    return false;

                if (DisplayNamePublisher == "" || AddressPublisher == "" || PhonePublisher == "" || EmailPublisher == "" || NotePublisher == "")
                    return false;

                var checkPublisher = DataProvider.Ins.DB.Publishers.Where(x => x.DisplayName == DisplayNamePublisher && x.Address == AddressPublisher && x.Phone == PhonePublisher && 
                    x.Email == EmailPublisher && x.Note == NotePublisher && x.CountDelete == 0).Count();
                if (checkPublisher > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var Publisher = new Model.Publisher() { DisplayName = DisplayNamePublisher, Address = AddressPublisher, Phone = PhonePublisher, Email = EmailPublisher, Note = NotePublisher, CountDelete = 0 };

                DataProvider.Ins.DB.Publishers.Add(Publisher);
                DataProvider.Ins.DB.SaveChanges();

                LvPublisher.Add(Publisher);

                MessageBox.Show("Successful");

            });

            EditPublisherCommand = new RelayCommand<Button>((p) => {
                if (DisplayNamePublisher == null || AddressPublisher == null || PhonePublisher == null || EmailPublisher == null || NotePublisher == null)
                    return false;

                if (DisplayNamePublisher == "" || AddressPublisher == "" || PhonePublisher == "" || EmailPublisher == "" || NotePublisher == "")
                    return false;

                var checkPublisher = DataProvider.Ins.DB.Publishers.Where(x => x.Id != IdPublisher && x.DisplayName == DisplayNamePublisher && x.Address == AddressPublisher && x.Phone == PhonePublisher 
                    && x.Email == EmailPublisher && x.Note == NotePublisher && x.CountDelete == 0).Count();
                if (checkPublisher > 0)
                    return false;

                return true;
            }, (p) =>
            {

                var Publisher = DataProvider.Ins.DB.Publishers.Where(x => x.Id == IdPublisher && x.CountDelete == 0).SingleOrDefault();
                Publisher.DisplayName = DisplayNamePublisher;
                Publisher.Address = AddressPublisher;
                Publisher.Phone = PhonePublisher;
                Publisher.Email = EmailPublisher;
                Publisher.Note = NotePublisher;
                Publisher.CountDelete = 0;


                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");

            });

            DeletePublisherCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemPublisher == null)
                    return false;

                return true;
            }, (p) =>
            {
                var deletePublisher = DataProvider.Ins.DB.Publishers.Where(x => x.Id == SelectedItemPublisher.Id && x.CountDelete == 0).SingleOrDefault();
                deletePublisher.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();
                LvPublisher.Remove(deletePublisher);

                MessageBox.Show("Successful");
            });

            #endregion

            /*--------------------------------------------*/

            #region Command Language
            AddLanguageCommand = new RelayCommand<Button>((p) => {
                if (DisplayNameLanguage == null || DisplayNameLanguage == "")
                    return false;

                var checkLanguage = DataProvider.Ins.DB.Languages.Where(x => x.DisplayName == DisplayNameLanguage && x.Note == NoteLanguage && x.CountDelete == 0).Count();
                if (checkLanguage > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var language = new Model.Language() { DisplayName = DisplayNameLanguage, Note = NoteLanguage, CountDelete = 0 };

                DataProvider.Ins.DB.Languages.Add(language);
                DataProvider.Ins.DB.SaveChanges();

                LvLanguage.Add(language);

                MessageBox.Show("Successful");

            });

            EditLanguageCommand = new RelayCommand<Button>((p) => {
                if (DisplayNameLanguage == null || DisplayNameLanguage == "")
                    return false;

                var checkLanguage = DataProvider.Ins.DB.Languages.Where(x => x.DisplayName == DisplayNameLanguage && x.Note == NoteLanguage && x.CountDelete == 0).Count();
                if (checkLanguage > 0)
                    return false;

                return true;
            }, (p) =>
            {
                var language = DataProvider.Ins.DB.Languages.Where(x => x.Id == IdLanguage && x.CountDelete == 0).SingleOrDefault();
                language.DisplayName = DisplayNameLanguage;
                language.Note = NoteLanguage;

                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show("Successful");
            });

            DeleteLanguageCommand = new RelayCommand<Object>((p) => {
                if (SelectedItemLanguage == null)
                    return false;

                return true;
            }, (p) =>
            {
                var deleteLanguage = DataProvider.Ins.DB.Languages.Where(x => x.Id == SelectedItemLanguage.Id && x.CountDelete == 0).SingleOrDefault();
                deleteLanguage.CountDelete = 1;
                //DataProvider.Ins.DB.SaveChanges();
                LvLanguage.Remove(deleteLanguage);

                MessageBox.Show("Successful");
            });
            #endregion

            /*--------------------------------------------*/
        }
    }
}
