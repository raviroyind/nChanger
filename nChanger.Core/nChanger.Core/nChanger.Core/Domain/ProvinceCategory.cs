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
    
    public partial class ProvinceCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProvinceCategory()
        {
            this.PdfFormTemplates = new HashSet<PdfFormTemplate>();
            this.DefineQuestions = new HashSet<DefineQuestion>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ProvinceId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string EntryIP { get; set; }
        public string EntryId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PdfFormTemplate> PdfFormTemplates { get; set; }
        public virtual Province Province { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefineQuestion> DefineQuestions { get; set; }
    }
}
