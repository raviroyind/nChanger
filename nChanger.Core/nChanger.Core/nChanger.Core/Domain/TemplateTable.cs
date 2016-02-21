namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TemplateTable")]
    public partial class TemplateTable
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PdfFormTemplateId { get; set; }

        [Required]
        [StringLength(250)]
        public string TableName { get; set; }

        [Required]
        [StringLength(500)]
        public string SectionName { get; set; }

        [Required]
        [StringLength(1000)]
        public string SectionPath { get; set; }

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
