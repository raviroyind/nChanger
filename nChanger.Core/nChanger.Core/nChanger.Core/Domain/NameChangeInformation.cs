namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NameChangeInformation")]
    public partial class NameChangeInformation
    {
        public Guid Id { get; set; }

        public Guid PdfTemplateId { get; set; }

        [StringLength(30)]
        public string UserId { get; set; }

        public string ResonForNameChange { get; set; }

        [StringLength(100)]
        public string ChangedNamePriviously { get; set; }

        public int? PreviousNameChangeDay { get; set; }

        public int? PreviousNameChangeMonth { get; set; }

        public int? PreviousNameChangeYear { get; set; }

        [StringLength(100)]
        public string PreviousFirstName { get; set; }

        [StringLength(100)]
        public string PreviousMiddleName { get; set; }

        [StringLength(100)]
        public string PreviousLastName { get; set; }

        [StringLength(100)]
        public string FirstNameAfterChange { get; set; }

        [StringLength(100)]
        public string MiddleNameAfterChange { get; set; }

        [StringLength(100)]
        public string LastNameAfterChange { get; set; }

        [StringLength(100)]
        public string PreviousNameChangeProvince { get; set; }

        [StringLength(100)]
        public string PreviousNameChangeCountry { get; set; }

        public bool? AppliedForChangeAndRefused { get; set; }

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
