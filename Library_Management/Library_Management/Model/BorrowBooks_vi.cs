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
    using System;
    using System.Collections.Generic;
    
    public partial class BorrowBooks_vi
    {
        public int Id { get; set; }
        public string Human { get; set; }
        public string Book { get; set; }
        public System.DateTime DateBorrowed { get; set; }
        public System.DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Fine { get; set; }
        public string Color { get; set; }
        public double ContractualFine { get; set; }
        public string Note { get; set; }
    }
}
