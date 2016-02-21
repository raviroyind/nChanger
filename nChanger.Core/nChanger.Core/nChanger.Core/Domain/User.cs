namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserPackages = new HashSet<UserPackage>();
        }

        public Guid Id { get; set; }

        [Key]
        [StringLength(20)]
        public string UserId { get; set; }

        [Required]
        [StringLength(2)]
        public string UserTypeId { get; set; }

        [Required]
        [StringLength(500)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(90)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [StringLength(12)]
        public string Zip { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        public bool? EmailVerified { get; set; }

        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        public string VerificationCode { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPackage> UserPackages { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
