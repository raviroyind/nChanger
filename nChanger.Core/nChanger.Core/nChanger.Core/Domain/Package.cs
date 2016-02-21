namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Package")]
    public partial class Package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Package()
        {
            UserPackages = new HashSet<UserPackage>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string PackageShortName { get; set; }

        [StringLength(500)]
        public string PackageLongName { get; set; }

        public string PackageDescription { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int DurationMonths { get; set; }

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

        public virtual PackageCategory PackageCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPackage> UserPackages { get; set; }
    }
}
