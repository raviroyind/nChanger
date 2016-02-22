namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PdfFormTemplate")]
    public partial class PdfFormTemplate
    {
        public Guid Id { get; set; }

        public Guid ProvinceCategoryId { get; set; }

        [Required]
        public string TemplateName { get; set; }

        [Required]
        [StringLength(250)]
        public string PdfFileName { get; set; }

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

        public virtual ProvinceCategory ProvinceCategory { get; set; }
    }
}
