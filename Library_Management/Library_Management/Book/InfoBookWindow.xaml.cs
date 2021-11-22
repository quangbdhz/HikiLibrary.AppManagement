using Library_Management.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_Management.Book
{
    /// <summary>
    /// Interaction logic for InfoBookWindow.xaml
    /// </summary>
    public partial class InfoBookWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Model.Author> _LvAuthor;
        public ObservableCollection<Model.Author> LvAuthor { get => _LvAuthor; set { _LvAuthor = value; OnPropertyChanged(); } }

        private string _StringSearchAuthor;
        public string StringSearchAuthor { get => _StringSearchAuthor; set { _StringSearchAuthor = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.BookSubject> _LvSubject;
        public ObservableCollection<Model.BookSubject> LvSubject { get => _LvSubject; set { _LvSubject = value; OnPropertyChanged(); } }

        private string _StringSearchSubject;
        public string StringSearchSubject { get => _StringSearchSubject; set { _StringSearchSubject = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Publisher> _LvPublisher;
        public ObservableCollection<Model.Publisher> LvPublisher { get => _LvPublisher; set { _LvPublisher = value; OnPropertyChanged(); } }

        private string _StringSearchPublisher;
        public string StringSearchPublisher { get => _StringSearchPublisher; set { _StringSearchPublisher = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Language> _LvLanguage;
        public ObservableCollection<Model.Language> LvLanguage { get => _LvLanguage; set { _LvLanguage = value; OnPropertyChanged(); } }

        private string _StringSearchLanguage;
        public string StringSearchLanguage { get => _StringSearchLanguage; set { _StringSearchLanguage = value; OnPropertyChanged(); } }

        public InfoBookWindow()
        {
            InitializeComponent();

            string nameSearchAuthor = tb_author.Text;
            var searchAuthor = DataProvider.Ins.DB.load_data_Author(nameSearchAuthor).ToList();

            StringSearchAuthor = "Name";

            LvAuthor = new ObservableCollection<Model.Author>(DataProvider.Ins.DB.Authors);
            Lv_Author.ItemsSource = LvAuthor;

            CollectionView viewAuthor = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Author.ItemsSource);
            viewAuthor.Filter = UserFilterAuthor;


            string nameSearchBookSubject = tb_author.Text;
            var searchBookSubject = DataProvider.Ins.DB.load_data_BookSubject(nameSearchBookSubject).ToList();

            StringSearchSubject = "Id";

            LvSubject = new ObservableCollection<Model.BookSubject>(DataProvider.Ins.DB.BookSubjects);
            Lv_Subject.ItemsSource = LvSubject;

            CollectionView viewSubject = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Subject.ItemsSource);
            viewSubject.Filter = UserFilterSubject;


            StringSearchPublisher = "Id";

            LvPublisher = new ObservableCollection<Model.Publisher>(DataProvider.Ins.DB.Publishers);
            Lv_Publisher.ItemsSource = LvPublisher;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Publisher.ItemsSource);
            view.Filter = UserFilterPublisher;


            StringSearchLanguage = "Id";

            LvLanguage = new ObservableCollection<Model.Language>(DataProvider.Ins.DB.Languages);
            Lv_Language.ItemsSource = LvLanguage;

            CollectionView viewLanguage = (CollectionView)CollectionViewSource.GetDefaultView(Lv_Language.ItemsSource);
            viewLanguage.Filter = UserFilterLanguage;
        }

        #region Author
        private bool UserFilterAuthor(object item)
        {

            if (String.IsNullOrEmpty(tb_author.Text))
                return true;
            else
            {
                if (StringSearchAuthor== "Name")
                {
                    return ((item as Model.Author).DisplayName.IndexOf(tb_author.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchAuthor == "Phone")
                {
                    return ((item as Model.Author).Phone.IndexOf(tb_author.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.Author).Email.IndexOf(tb_author.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }


        private void TextBoxAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Author.ItemsSource).Refresh();
        }

        private void OptionSearchAuthor_DropDownClosed(object sender, EventArgs e)
        {
            StringSearchAuthor = this.OptionSearchAuthor.Text;
        }
        #endregion

        #region Subject
        private bool UserFilterSubject(object item)
        {

            if (String.IsNullOrEmpty(tb_subject.Text))
                return true;
            else
            {
                if (StringSearchSubject == "Id")
                {
                    return ((item as Model.BookSubject).Id.ToString().IndexOf(tb_subject.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.BookSubject).DisplayName.IndexOf(tb_subject.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }


        private void TextBoxSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Subject.ItemsSource).Refresh();
        }

        private void OptionSearchSubject_DropDownClosed(object sender, EventArgs e)
        {
            StringSearchSubject = this.OptionSearchSubject.Text;
        }
        #endregion

        #region Language
        private bool UserFilterLanguage(object item)
        {

            if (String.IsNullOrEmpty(tb_language.Text))
                return true;
            else
            {
                if (StringSearchLanguage == "Id")
                {
                    return ((item as Model.Language).Id.ToString().IndexOf(tb_language.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.Language).DisplayName.IndexOf(tb_language.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }

        
        private void TextBoxLanguage_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Language.ItemsSource).Refresh();
        }

        private void OptionSearchLanguage_DropDownClosed(object sender, EventArgs e)
        {
            StringSearchLanguage = this.OptionSearchLanguage.Text;
        }
        #endregion

        #region Publisher
        private bool UserFilterPublisher(object item)
        {

            if (String.IsNullOrEmpty(tb_publisher.Text))
                return true;
            else
            {
                if (StringSearchPublisher == "Id")
                {
                    return ((item as Model.Publisher).Id.ToString().IndexOf(tb_publisher.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchPublisher == "Name")
                {
                    return ((item as Model.Publisher).Phone.IndexOf(tb_publisher.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (StringSearchPublisher == "Phone")
                {
                    return ((item as Model.Publisher).Phone.IndexOf(tb_publisher.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Model.Publisher).Email.IndexOf(tb_publisher.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }
        private void TextBoxPublisher_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lv_Publisher.ItemsSource).Refresh();
        }

        private void OptionSearchPublisher_DropDownClosed(object sender, EventArgs e)
        {
            StringSearchPublisher = this.OptionSearchPublisher.Text;
        }
        #endregion
    }
}
