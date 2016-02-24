namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FieldMapping")]
    public partial class FieldMapping
    {
        public Guid Id { get; set; }

        public Guid PdfFormTemplateId { get; set; }

        [StringLength(50)]
        public string PdfFieldName { get; set; }

        public int PdfPageNumber { get; set; }

        [StringLength(50)]
        public string FieldType { get; set; }

        public double? Left { get; set; }

        public double? Right { get; set; }

        public double? Top { get; set; }

        public double? Bottom { get; set; }

        [StringLength(50)]
        public string DbFieldName { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }

        public virtual PdfFormTemplate PdfFormTemplate { get; set; }
    }
}
