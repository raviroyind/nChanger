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
    
    public partial class Province
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Province()
        {
            this.ProvinceCategories = new HashSet<ProvinceCategory>();
        }
    
        public System.Guid Id { get; set; }
        public string Province1 { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string EntryIP { get; set; }
        public string EntryId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProvinceCategory> ProvinceCategories { get; set; }
    }
}