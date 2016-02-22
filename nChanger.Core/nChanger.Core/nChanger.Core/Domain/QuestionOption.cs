namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuestionOption
    {
        public Guid Id { get; set; }

        public Guid DefineQuestionsId { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionLabel { get; set; }

        public bool IsActive { get; set; }

        public DateTime EntryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryIP { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryId { get; set; }

        public virtual DefineQuestion DefineQuestion { get; set; }
    }
}
