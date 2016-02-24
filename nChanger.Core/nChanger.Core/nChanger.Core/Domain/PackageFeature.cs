namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackageFeature")]
    public partial class PackageFeature
    {
        public Guid Id { get; set; }

        public Guid PackageId { get; set; }

        public Guid FeatureId { get; set; }

        [Required]
        [StringLength(150)]
        public string FeatureName { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }

        public virtual FeatureMaster FeatureMaster { get; set; }

        public virtual Package Package { get; set; }
    }
}
