//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPayment
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public System.Guid PackageId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string EntryIP { get; set; }
        public string EntryId { get; set; }
    
        public virtual User User { get; set; }
    }
}
