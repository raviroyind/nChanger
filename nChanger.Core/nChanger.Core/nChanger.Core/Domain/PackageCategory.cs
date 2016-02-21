namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackageCategory")]
    public partial class PackageCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PackageCategory()
        {
            Packages = new HashSet<Package>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Category { get; set; }

        [StringLength(500)]
        public string CategoryShortDescription { get; set; }

        public string CategoryLongDescription { get; set; }

        [StringLength(50)]
        public string ImageUrl { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Package> Packages { get; set; }
    }
}
