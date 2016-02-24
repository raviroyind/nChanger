namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserPackage
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string UserId { get; set; }

        public Guid PackageId { get; set; }

        public virtual User User { get; set; }
    }
}
