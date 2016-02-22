namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalInformation")]
    public partial class PersonalInformation
    {
        public Guid Id { get; set; }

        public Guid PdfTemplateId { get; set; }

        [StringLength(30)]
        public string UserId { get; set; }

        [StringLength(500)]
        public string PresentFirstName { get; set; }

        [StringLength(500)]
        public string PresentMiddleName { get; set; }

        [StringLength(500)]
        public string PresentLastName { get; set; }

        [StringLength(500)]
        public string Sex { get; set; }

        [StringLength(500)]
        public string MailAddStreetNo { get; set; }

        [StringLength(500)]
        public string MailAddPOBox { get; set; }

        [StringLength(500)]
        public string MailAddAptSuitNo { get; set; }

        [StringLength(500)]
        public string MailAddBuzzerNo { get; set; }

        [StringLength(500)]
        public string MailAddCityTownVillage { get; set; }

        [StringLength(500)]
        public string MailAddProvience { get; set; }

        [StringLength(20)]
        public string MailAddPostalCode { get; set; }

        [StringLength(10)]
        public string MailAddHomePhoneCode { get; set; }

        [StringLength(50)]
        public string MailAddHomePhoneNo { get; set; }

        [StringLength(10)]
        public string MailAddWorkPhoneCode { get; set; }

        [StringLength(50)]
        public string MailAddWorkPhoneNo { get; set; }

        public int? LivedInOntarioYears { get; set; }

        public int? LivedInOntarioMonths { get; set; }

        [StringLength(1)]
        public string LivedInOntarioPast12Months { get; set; }

        public int? DOBYear { get; set; }

        public int? DOBMonth { get; set; }

        public int? DOBDay { get; set; }

        [StringLength(500)]
        public string BirthCityTownVillage { get; set; }

        [StringLength(500)]
        public string BirthProvinceOrState { get; set; }

        [StringLength(500)]
        public string BirthCountry { get; set; }

        [StringLength(500)]
        public string NewFirstName { get; set; }

        [StringLength(500)]
        public string NewMiddleName { get; set; }

        [StringLength(500)]
        public string NewLastName { get; set; }

        [StringLength(1)]
        public string Married { get; set; }

        [StringLength(500)]
        public string PartnerFisrtName { get; set; }

        [StringLength(500)]
        public string PartnerMiddleName { get; set; }

        [StringLength(500)]
        public string PartnerLastName { get; set; }

        public int? DateMarriedMonth { get; set; }

        public int? DateMarriedDay { get; set; }

        public int? DateMarriedYear { get; set; }

        [StringLength(500)]
        public string CityTownMarried { get; set; }

        [StringLength(500)]
        public string StateOrProvinceMarried { get; set; }

        [StringLength(500)]
        public string CountryMarried { get; set; }

        [StringLength(1)]
        public string JDeclarationSigned { get; set; }

        [StringLength(500)]
        public string JDeclarationPersonFirstName { get; set; }

        [StringLength(500)]
        public string JDeclarationPersonMiddleName { get; set; }

        [StringLength(500)]
        public string JDeclarationPersonLastName { get; set; }

        public int? SentRegistrarMonth { get; set; }

        public int? SentRegistrarDay { get; set; }

        public int? SentRegistrarYear { get; set; }

        [StringLength(1)]
        public string SubmittedForm4 { get; set; }

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
