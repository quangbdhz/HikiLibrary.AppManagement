//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library_Management.Model
{
    using Library_Management.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class Book : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.BorrowBooks = new HashSet<BorrowBook>();
            this.ListBookCustomerBorrows = new HashSet<ListBookCustomerBorrow>();
            this.ListBookCustomerReturnBookLibraries = new HashSet<ListBookCustomerReturnBookLibrary>();
            this.ListBookLibraryBorrowHumen = new HashSet<ListBookLibraryBorrowHuman>();
            this.ListReturnBookHumen = new HashSet<ListReturnBookHuman>();
        }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private Nullable<double> _BookPrice;
        public Nullable<double> BookPrice { get => _BookPrice; set { _BookPrice = value; OnPropertyChanged(); } }

        private int _BorrowingIdHuman;
        public int BorrowingIdHuman { get => _BorrowingIdHuman; set { _BorrowingIdHuman = value; OnPropertyChanged(); } }

        private string _BookSubject;
        public string BookSubject { get => _BookSubject; set { _BookSubject = value; OnPropertyChanged(); } }

        private string _Author;
        public string Author { get => _Author; set { _Author = value; OnPropertyChanged(); } }

        private int _IdLanguage;
        public int IdLanguage { get => _IdLanguage; set { _IdLanguage = value; OnPropertyChanged(); } }

        private Nullable<int> _IdPublisher;
        public Nullable<int> IdPublisher { get => _IdPublisher; set { _IdPublisher = value; OnPropertyChanged(); } }

        private int _IdStatus;
        public int IdStatus { get => _IdStatus; set { _IdStatus = value; OnPropertyChanged(); } }

        private int _IdStatusReturnBookToHuman;
        public int IdStatusReturnBookToHuman { get => _IdStatusReturnBookToHuman; set { _IdStatusReturnBookToHuman = value; OnPropertyChanged(); } }

        private System.DateTime _LibraryDateBorrowed;
        public System.DateTime LibraryDateBorrowed { get => _LibraryDateBorrowed; set { _LibraryDateBorrowed = value; OnPropertyChanged(); } }

        private System.DateTime _LibraryDueDate;
        public System.DateTime LibraryDueDate { get => _LibraryDueDate; set { _LibraryDueDate = value; OnPropertyChanged(); } }

        private string _Color;
        public string Color { get => _Color; set { _Color = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _DateReturnBookToHuman;
        public Nullable<System.DateTime> DateReturnBookToHuman { get => _DateReturnBookToHuman; set { _DateReturnBookToHuman = value; OnPropertyChanged(); } }

        private string _UrlImageBook;
        public string UrlImageBook { get => _UrlImageBook; set { _UrlImageBook = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        public Nullable<int> CountDelete { get; set; }
    
        public virtual Human Human { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BorrowBook> BorrowBooks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListBookCustomerBorrow> ListBookCustomerBorrows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListBookCustomerReturnBookLibrary> ListBookCustomerReturnBookLibraries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListBookLibraryBorrowHuman> ListBookLibraryBorrowHumen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListReturnBookHuman> ListReturnBookHumen { get; set; }
        public virtual Language Language { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Status Status { get; set; }
    }
}
