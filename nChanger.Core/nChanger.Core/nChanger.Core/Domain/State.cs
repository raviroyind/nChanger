namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [StringLength(50)]
        public string StateId { get; set; }

        [StringLength(250)]
        public string StateName { get; set; }

        [Required]
        [StringLength(50)]
        public string CountryId { get; set; }

        public bool IsActive { get; set; }

        public virtual Country Country { get; set; }
    }
}
