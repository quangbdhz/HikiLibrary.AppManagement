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
    
    public partial class ListBookCustomerBorrow : BaseViewModel
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private int _IdBillBookOfCustomer;
        public int IdBillBookOfCustomer { get => _IdBillBookOfCustomer; set { _IdBillBookOfCustomer = value; OnPropertyChanged(); } }

        private int _IdBook;
        public int IdBook { get => _IdBook; set { _IdBook = value; OnPropertyChanged(); } }

        private int _NumberOfBooks;
        public int NumberOfBooks { get => _NumberOfBooks; set { _NumberOfBooks = value; OnPropertyChanged(); } }

        public Nullable<int> CountDelete { get; set; }
    
        public virtual BillBookOfCustomer BillBookOfCustomer { get; set; }
        public virtual Book Book { get; set; }
    }
}