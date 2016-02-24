namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InputFormTable
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string TableName { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsActive { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime EntryDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string EntryId { get; set; }
    }
}
