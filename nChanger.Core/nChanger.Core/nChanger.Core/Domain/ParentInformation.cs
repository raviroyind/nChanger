namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParentInformation")]
    public partial class ParentInformation
    {
        public Guid Id { get; set; }

        public Guid PdfTemplateId { get; set; }

        [StringLength(30)]
        public string UserId { get; set; }

        [StringLength(100)]
        public string FatherFirstName { get; set; }

        [StringLength(100)]
        public string FatherMiddleName { get; set; }

        [StringLength(100)]
        public string FatherLastName { get; set; }

        [StringLength(100)]
        public string FatherOtherLastName { get; set; }

        [StringLength(100)]
        public string MotherFirstName { get; set; }

        [StringLength(100)]
        public string MotherMiddleName { get; set; }

        [StringLength(100)]
        public string MotherLastNameWhenBorn { get; set; }

        [StringLength(100)]
        public string MotherLastNamePresent { get; set; }

        [StringLength(100)]
        public string MotherLastNameOther { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }
    }
}
