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
    
    public partial class QuestionOption
    {
        public System.Guid Id { get; set; }
        public System.Guid DefineQuestionsId { get; set; }
        public string OptionLabel { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string EntryIP { get; set; }
        public string EntryId { get; set; }
    
        public virtual DefineQuestion DefineQuestion { get; set; }
    }
}
