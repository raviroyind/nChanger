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
    
    public partial class ParentInformation
    {
        public System.Guid Id { get; set; }
        public System.Guid PdfTemplateId { get; set; }
        public string UserId { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherMiddleName { get; set; }
        public string FatherLastName { get; set; }
        public string FatherOtherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherMiddleName { get; set; }
        public string MotherLastNameWhenBorn { get; set; }
        public string MotherLastNamePresent { get; set; }
        public string MotherLastNameOther { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string EntryIP { get; set; }
        public string EntryId { get; set; }
    }
}
