﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nChanger.Core
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class nChangerDb : DbContext
    {
        public nChangerDb()
            : base("name=nChangerDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CriminalOffenceInformation> CriminalOffenceInformations { get; set; }
        public virtual DbSet<DefineQuestion> DefineQuestions { get; set; }
        public virtual DbSet<FeatureMaster> FeatureMasters { get; set; }
        public virtual DbSet<FieldMapping> FieldMappings { get; set; }
        public virtual DbSet<FinancialInformation> FinancialInformations { get; set; }
        public virtual DbSet<FormInfo> FormInfoes { get; set; }
        public virtual DbSet<FormSection> FormSections { get; set; }
        public virtual DbSet<NameChangeInformation> NameChangeInformations { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageCategory> PackageCategories { get; set; }
        public virtual DbSet<PackageFeature> PackageFeatures { get; set; }
        public virtual DbSet<ParentInformation> ParentInformations { get; set; }
        public virtual DbSet<PdfFormTemplate> PdfFormTemplates { get; set; }
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<ProvinceCategory> ProvinceCategories { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TemplateTable> TemplateTables { get; set; }
        public virtual DbSet<UserFormDetail> UserFormDetails { get; set; }
        public virtual DbSet<UserPackage> UserPackages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<InputFormTable> InputFormTables { get; set; }
        public virtual DbSet<InputFormSchemaView> InputFormSchemaViews { get; set; }
    
        public virtual ObjectResult<string> uspSelectValueByColumnName(string value, string table, string userId)
        {
            var valueParameter = value != null ?
                new ObjectParameter("value", value) :
                new ObjectParameter("value", typeof(string));
    
            var tableParameter = table != null ?
                new ObjectParameter("table", table) :
                new ObjectParameter("table", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspSelectValueByColumnName", valueParameter, tableParameter, userIdParameter);
        }
    }
}
