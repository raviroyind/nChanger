namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CriminalOffenceInformation")]
    public partial class CriminalOffenceInformation
    {
        public Guid Id { get; set; }

        public Guid PdfTemplateId { get; set; }

        [StringLength(30)]
        public string UserId { get; set; }

        public bool OutstandingCourtProceedings { get; set; }

        [StringLength(100)]
        public string CourtFileNumber { get; set; }

        [StringLength(100)]
        public string CourtName { get; set; }

        [StringLength(200)]
        public string CourtAddress { get; set; }

        [StringLength(500)]
        public string DescribeProceedings { get; set; }

        public bool OutstandingEnforcementOrders { get; set; }

        [StringLength(500)]
        public string DetailsOfOutstandingEnforcementOrders { get; set; }

        public bool EverConvictedOfCriminalOffence { get; set; }

        [StringLength(500)]
        public string DetailsOfCriminalOffence { get; set; }

        public bool FoundGuiltyDischarged { get; set; }

        [StringLength(500)]
        public string FoundGuiltyDetailsOfOffence { get; set; }

        public bool AdultSentenceImposed { get; set; }

        [StringLength(500)]
        public string DescribeAdultSentence { get; set; }

        public bool PendingCharges { get; set; }

        [StringLength(100)]
        public string PendingChargesCourtNumber { get; set; }

        [StringLength(100)]
        public string PendingChargesCourtName { get; set; }

        [StringLength(200)]
        public string PendingChargesCourtAddress { get; set; }

        [StringLength(500)]
        public string PendingChargesDescribe { get; set; }

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
