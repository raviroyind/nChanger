namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserType")]
    public partial class UserType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [StringLength(2)]
        public string Id { get; set; }

        [Column("UserType")]
        [Required]
        [StringLength(150)]
        public string UserType1 { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
