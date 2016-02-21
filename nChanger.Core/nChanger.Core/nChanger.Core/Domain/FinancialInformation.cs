namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FinancialInformation")]
    public partial class FinancialInformation
    {
        public Guid Id { get; set; }

        public Guid PdfTemplateId { get; set; }

        [StringLength(30)]
        public string UserId { get; set; }

        public bool CourtOrTribunalOrder { get; set; }

        [StringLength(100)]
        public string CourtFileNumber { get; set; }

        [StringLength(100)]
        public string NameOfCourt { get; set; }

        public int? DateOfCourtOrderDay { get; set; }

        public int? DateOfCourtOrderMonth { get; set; }

        public int? DateOfCourtOrderYear { get; set; }

        [StringLength(100)]
        public string NameOfPersonWhoSuedYou { get; set; }

        [StringLength(200)]
        public string AddressCourtTribunal { get; set; }

        public bool SheriffDirected { get; set; }

        [StringLength(100)]
        public string WritNumber { get; set; }

        [StringLength(100)]
        public string NameOfSherrif { get; set; }

        [StringLength(100)]
        public string AddressOfSheriff { get; set; }

        public bool LiensOrSecurityInterests { get; set; }

        [StringLength(100)]
        public string LiensOrSecurityInterestsNameOfPerson { get; set; }

        [StringLength(100)]
        public string AmountOfMoneyOwed { get; set; }

        [StringLength(100)]
        public string RegitrationNumber { get; set; }

        public bool FinancialStatementsRegistered { get; set; }

        [StringLength(100)]
        public string FinancialStatementsRegitrationNumber { get; set; }

        public bool UndischargedBankrupt { get; set; }

        [StringLength(1000)]
        public string DetailsOfBankruptcy { get; set; }

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
