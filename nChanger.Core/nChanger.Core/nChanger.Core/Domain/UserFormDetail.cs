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
    
    public partial class UserFormDetail
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public System.Guid PdfTemplateId { get; set; }
        public System.Guid FrmGuid { get; set; }
        public string TableName { get; set; }
        public string AspxPath { get; set; }
        public string Completed { get; set; }
    
        public virtual PdfFormTemplate PdfFormTemplate { get; set; }
        public virtual User User { get; set; }
    }
}