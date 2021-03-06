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
    
    public partial class BillBookOfHuman : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BillBookOfHuman()
        {
            this.ListBookLibraryBorrowHumen = new HashSet<ListBookLibraryBorrowHuman>();
        }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private int _IdHuman;
        public int IdHuman { get => _IdHuman; set { _IdHuman = value; OnPropertyChanged(); } }

        private int _IdStaff;
        public int IdStaff { get => _IdStaff; set { _IdStaff = value; OnPropertyChanged(); } }

        private System.DateTime _BorrowedDate;
        public System.DateTime BorrowedDate { get => _BorrowedDate; set { _BorrowedDate = value; OnPropertyChanged(); } }

        private System.DateTime _DateOfRepayment;
        public System.DateTime DateOfRepayment { get => _DateOfRepayment; set { _DateOfRepayment = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private int _IdStatusBill;
        public int IdStatusBill { get => _IdStatusBill; set { _IdStatusBill = value; OnPropertyChanged(); } }

        public Nullable<int> CountDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListBookLibraryBorrowHuman> ListBookLibraryBorrowHumen { get; set; }
        public virtual Human Human { get; set; }
        public virtual UserStaff UserStaff { get; set; }
        public virtual StatusBill StatusBill { get; set; }
    }
}
