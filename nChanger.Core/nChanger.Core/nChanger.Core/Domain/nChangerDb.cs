using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace nChanger.Core
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class nChangerDb : DbContext
    {
        public nChangerDb()
            : base("name=nChangerDb")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CriminalOffenceInformation> CriminalOffenceInformations { get; set; }
        public virtual DbSet<DefineQuestion> DefineQuestions { get; set; }
        public virtual DbSet<FinancialInformation> FinancialInformations { get; set; }
        public virtual DbSet<NameChangeInformation> NameChangeInformations { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageCategory> PackageCategories { get; set; }
        public virtual DbSet<ParentInformation> ParentInformations { get; set; }
        public virtual DbSet<PdfFormTemplate> PdfFormTemplates { get; set; }
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<ProvinceCategory> ProvinceCategories { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TemplateTable> TemplateTables { get; set; }
        public virtual DbSet<UserPackage> UserPackages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<InputFormSchemaView> InputFormSchemaViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.States)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DefineQuestion>()
                .HasMany(e => e.QuestionOptions)
                .WithRequired(e => e.DefineQuestion)
                .HasForeignKey(e => e.DefineQuestionsId);

            modelBuilder.Entity<Package>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Package>()
                .HasMany(e => e.UserPackages)
                .WithRequired(e => e.Package)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PackageCategory>()
                .HasMany(e => e.Packages)
                .WithRequired(e => e.PackageCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PersonalInformation>()
                .Property(e => e.LivedInOntarioPast12Months)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PersonalInformation>()
                .Property(e => e.Married)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PersonalInformation>()
                .Property(e => e.JDeclarationSigned)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PersonalInformation>()
                .Property(e => e.SubmittedForm4)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ProvinceCategory>()
                .HasMany(e => e.PdfFormTemplates)
                .WithRequired(e => e.ProvinceCategory)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .Property(e => e.UserTypeId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserPackages)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.Id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InputFormSchemaView>()
                .Property(e => e.IS_NULLABLE)
                .IsUnicode(false);
        }


        public virtual ObjectResult<string> UspSelectValueByColumnName(string value, string table, string userId)
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
