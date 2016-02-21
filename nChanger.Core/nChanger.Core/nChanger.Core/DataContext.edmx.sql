
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/19/2016 18:40:28
-- Generated from EDMX file: D:\Projects\nChanger\nChanger.Core\nChanger.Core\nChanger.Core\DataContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NameChangerDB18-FEB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_CustomField_dbo_PdfTemplate_PdfTemplateId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomFields] DROP CONSTRAINT [FK_dbo_CustomField_dbo_PdfTemplate_PdfTemplateId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_FieldMapping_dbo_PdfTemplate_PdfTemplateId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FieldMappings] DROP CONSTRAINT [FK_dbo_FieldMapping_dbo_PdfTemplate_PdfTemplateId];
GO
IF OBJECT_ID(N'[dbo].[FK_Package_PackageCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Package] DROP CONSTRAINT [FK_Package_PackageCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_State_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[State] DROP CONSTRAINT [FK_State_Country];
GO
IF OBJECT_ID(N'[dbo].[FK_TemplateTable_PdfTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemplateTable] DROP CONSTRAINT [FK_TemplateTable_PdfTemplates];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPackages_Package]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPackages] DROP CONSTRAINT [FK_UserPackages_Package];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPackages_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPackages] DROP CONSTRAINT [FK_UserPackages_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_UserType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Country];
GO
IF OBJECT_ID(N'[dbo].[CriminalOffenceInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CriminalOffenceInformation];
GO
IF OBJECT_ID(N'[dbo].[CustomFields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomFields];
GO
IF OBJECT_ID(N'[dbo].[FieldMappings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FieldMappings];
GO
IF OBJECT_ID(N'[dbo].[FinancialInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialInformation];
GO
IF OBJECT_ID(N'[dbo].[InputFormTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InputFormTables];
GO
IF OBJECT_ID(N'[dbo].[NameChangeInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NameChangeInformation];
GO
IF OBJECT_ID(N'[dbo].[Package]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Package];
GO
IF OBJECT_ID(N'[dbo].[PackageCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackageCategory];
GO
IF OBJECT_ID(N'[dbo].[ParentInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParentInformation];
GO
IF OBJECT_ID(N'[dbo].[PdfTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PdfTemplates];
GO
IF OBJECT_ID(N'[dbo].[PersonalInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalInformation];
GO
IF OBJECT_ID(N'[dbo].[State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[State];
GO
IF OBJECT_ID(N'[dbo].[TemplateTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemplateTable];
GO
IF OBJECT_ID(N'[dbo].[UserPackages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPackages];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserType];
GO
IF OBJECT_ID(N'[nChangerDbStoreContainer].[InputFormSchemaView]', 'U') IS NOT NULL
    DROP TABLE [nChangerDbStoreContainer].[InputFormSchemaView];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [CountryId] nvarchar(50)  NOT NULL,
    [CountryName] nvarchar(250)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'CriminalOffenceInformations'
CREATE TABLE [dbo].[CriminalOffenceInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(30)  NULL,
    [OutstandingCourtProceedings] bit  NOT NULL,
    [CourtFileNumber] nvarchar(100)  NULL,
    [CourtName] nvarchar(100)  NULL,
    [CourtAddress] nvarchar(200)  NULL,
    [DescribeProceedings] nvarchar(500)  NULL,
    [OutstandingEnforcementOrders] bit  NOT NULL,
    [DetailsOfOutstandingEnforcementOrders] nvarchar(500)  NULL,
    [EverConvictedOfCriminalOffence] bit  NOT NULL,
    [DetailsOfCriminalOffence] nvarchar(500)  NULL,
    [FoundGuiltyDischarged] bit  NOT NULL,
    [FoundGuiltyDetailsOfOffence] nvarchar(500)  NULL,
    [AdultSentenceImposed] bit  NOT NULL,
    [DescribeAdultSentence] nvarchar(500)  NULL,
    [PendingCharges] bit  NOT NULL,
    [PendingChargesCourtNumber] nvarchar(100)  NULL,
    [PendingChargesCourtName] nvarchar(100)  NULL,
    [PendingChargesCourtAddress] nvarchar(200)  NULL,
    [PendingChargesDescribe] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CustomFields'
CREATE TABLE [dbo].[CustomFields] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [FieldValue] nvarchar(500)  NULL,
    [FieldType] nvarchar(50)  NULL,
    [PdfPageNumber] int  NOT NULL,
    [Left] float  NULL,
    [Right] float  NULL,
    [Top] float  NULL,
    [Bottom] float  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'FieldMappings'
CREATE TABLE [dbo].[FieldMappings] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [PdfFieldName] nvarchar(50)  NULL,
    [PdfPageNumber] int  NOT NULL,
    [FieldType] nvarchar(50)  NULL,
    [Left] float  NULL,
    [Right] float  NULL,
    [Top] float  NULL,
    [Bottom] float  NULL,
    [DbFieldName] nvarchar(50)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'FinancialInformations'
CREATE TABLE [dbo].[FinancialInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(30)  NULL,
    [CourtOrTribunalOrder] bit  NOT NULL,
    [CourtFileNumber] nvarchar(100)  NULL,
    [NameOfCourt] nvarchar(100)  NULL,
    [DateOfCourtOrderDay] int  NULL,
    [DateOfCourtOrderMonth] int  NULL,
    [DateOfCourtOrderYear] int  NULL,
    [NameOfPersonWhoSuedYou] nvarchar(100)  NULL,
    [AddressCourtTribunal] nvarchar(200)  NULL,
    [SheriffDirected] bit  NOT NULL,
    [WritNumber] nvarchar(100)  NULL,
    [NameOfSherrif] nvarchar(100)  NULL,
    [AddressOfSheriff] nvarchar(100)  NULL,
    [LiensOrSecurityInterests] bit  NOT NULL,
    [LiensOrSecurityInterestsNameOfPerson] nvarchar(100)  NULL,
    [AmountOfMoneyOwed] nvarchar(100)  NULL,
    [RegitrationNumber] nvarchar(100)  NULL,
    [FinancialStatementsRegistered] bit  NOT NULL,
    [FinancialStatementsRegitrationNumber] nvarchar(100)  NULL,
    [UndischargedBankrupt] bit  NOT NULL,
    [DetailsOfBankruptcy] nvarchar(1000)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'InputFormTables'
CREATE TABLE [dbo].[InputFormTables] (
    [Id] uniqueidentifier  NOT NULL,
    [TableName] nvarchar(500)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'NameChangeInformations'
CREATE TABLE [dbo].[NameChangeInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(30)  NULL,
    [ResonForNameChange] nvarchar(max)  NULL,
    [ChangedNamePriviously] nvarchar(100)  NULL,
    [PreviousNameChangeDay] int  NULL,
    [PreviousNameChangeMonth] int  NULL,
    [PreviousNameChangeYear] int  NULL,
    [PreviousFirstName] nvarchar(100)  NULL,
    [PreviousMiddleName] nvarchar(100)  NULL,
    [PreviousLastName] nvarchar(100)  NULL,
    [FirstNameAfterChange] nvarchar(100)  NULL,
    [MiddleNameAfterChange] nvarchar(100)  NULL,
    [LastNameAfterChange] nvarchar(100)  NULL,
    [PreviousNameChangeProvince] nvarchar(100)  NULL,
    [PreviousNameChangeCountry] nvarchar(100)  NULL,
    [AppliedForChangeAndRefused] bit  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Packages'
CREATE TABLE [dbo].[Packages] (
    [Id] uniqueidentifier  NOT NULL,
    [CategoryId] uniqueidentifier  NOT NULL,
    [PackageShortName] nvarchar(200)  NOT NULL,
    [PackageLongName] nvarchar(500)  NULL,
    [PackageDescription] nvarchar(max)  NULL,
    [ImageUrl] nvarchar(500)  NULL,
    [IsDefault] bit  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [DurationMonths] int  NOT NULL,
    [Comments] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PackageCategories'
CREATE TABLE [dbo].[PackageCategories] (
    [Id] uniqueidentifier  NOT NULL,
    [Category] nvarchar(200)  NOT NULL,
    [CategoryShortDescription] nvarchar(500)  NULL,
    [CategoryLongDescription] nvarchar(max)  NULL,
    [ImageUrl] nvarchar(50)  NULL,
    [Comments] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ParentInformations'
CREATE TABLE [dbo].[ParentInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(30)  NULL,
    [FatherFirstName] nvarchar(100)  NULL,
    [FatherMiddleName] nvarchar(100)  NULL,
    [FatherLastName] nvarchar(100)  NULL,
    [FatherOtherLastName] nvarchar(100)  NULL,
    [MotherFirstName] nvarchar(100)  NULL,
    [MotherMiddleName] nvarchar(100)  NULL,
    [MotherLastNameWhenBorn] nvarchar(100)  NULL,
    [MotherLastNamePresent] nvarchar(100)  NULL,
    [MotherLastNameOther] nvarchar(100)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PdfTemplates'
CREATE TABLE [dbo].[PdfTemplates] (
    [Id] uniqueidentifier  NOT NULL,
    [TemplateName] nvarchar(max)  NOT NULL,
    [PdfFileName] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PersonalInformations'
CREATE TABLE [dbo].[PersonalInformations] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(30)  NULL,
    [PresentFirstName] nvarchar(500)  NULL,
    [PresentMiddleName] nvarchar(500)  NULL,
    [PresentLastName] nvarchar(500)  NULL,
    [Sex] nvarchar(500)  NULL,
    [MailAddStreetNo] nvarchar(500)  NULL,
    [MailAddPOBox] nvarchar(500)  NULL,
    [MailAddAptSuitNo] nvarchar(500)  NULL,
    [MailAddBuzzerNo] nvarchar(500)  NULL,
    [MailAddCityTownVillage] nvarchar(500)  NULL,
    [MailAddProvience] nvarchar(500)  NULL,
    [MailAddPostalCode] nvarchar(20)  NULL,
    [MailAddHomePhoneCode] nvarchar(10)  NULL,
    [MailAddHomePhoneNo] nvarchar(50)  NULL,
    [MailAddWorkPhoneCode] nvarchar(10)  NULL,
    [MailAddWorkPhoneNo] nvarchar(50)  NULL,
    [LivedInOntarioYears] int  NULL,
    [LivedInOntarioMonths] int  NULL,
    [LivedInOntarioPast12Months] char(1)  NULL,
    [DOBYear] int  NULL,
    [DOBMonth] int  NULL,
    [DOBDay] int  NULL,
    [BirthCityTownVillage] nvarchar(500)  NULL,
    [BirthProvinceOrState] nvarchar(500)  NULL,
    [BirthCountry] nvarchar(500)  NULL,
    [NewFirstName] nvarchar(500)  NULL,
    [NewMiddleName] nvarchar(500)  NULL,
    [NewLastName] nvarchar(500)  NULL,
    [Married] char(1)  NULL,
    [PartnerFisrtName] nvarchar(500)  NULL,
    [PartnerMiddleName] nvarchar(500)  NULL,
    [PartnerLastName] nvarchar(500)  NULL,
    [DateMarriedMonth] int  NULL,
    [DateMarriedDay] int  NULL,
    [DateMarriedYear] int  NULL,
    [CityTownMarried] nvarchar(500)  NULL,
    [StateOrProvinceMarried] nvarchar(500)  NULL,
    [CountryMarried] nvarchar(500)  NULL,
    [JDeclarationSigned] char(1)  NULL,
    [JDeclarationPersonFirstName] nvarchar(500)  NULL,
    [JDeclarationPersonMiddleName] nvarchar(500)  NULL,
    [JDeclarationPersonLastName] nvarchar(500)  NULL,
    [SentRegistrarMonth] int  NULL,
    [SentRegistrarDay] int  NULL,
    [SentRegistrarYear] int  NULL,
    [SubmittedForm4] char(1)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [StateId] nvarchar(50)  NOT NULL,
    [StateName] nvarchar(250)  NULL,
    [CountryId] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'TemplateTables'
CREATE TABLE [dbo].[TemplateTables] (
    [Id] uniqueidentifier  NOT NULL,
    [PdfTemplateId] uniqueidentifier  NOT NULL,
    [TableName] nvarchar(250)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserPackages'
CREATE TABLE [dbo].[UserPackages] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(20)  NOT NULL,
    [PackageId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(20)  NOT NULL,
    [UserTypeId] char(2)  NOT NULL,
    [Email] nvarchar(500)  NOT NULL,
    [Password] nvarchar(500)  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Address] nvarchar(100)  NULL,
    [Address2] nvarchar(50)  NULL,
    [City] nvarchar(90)  NULL,
    [State] nvarchar(20)  NULL,
    [Zip] nvarchar(12)  NULL,
    [Country] nvarchar(20)  NULL,
    [Phone] nvarchar(20)  NULL,
    [Fax] nvarchar(20)  NULL,
    [EmailVerified] bit  NULL,
    [RegistrationDate] datetime  NOT NULL,
    [VerificationCode] nvarchar(50)  NULL,
    [IP] nvarchar(50)  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'UserTypes'
CREATE TABLE [dbo].[UserTypes] (
    [Id] char(2)  NOT NULL,
    [UserType1] nvarchar(150)  NOT NULL,
    [Description] nvarchar(1000)  NULL,
    [IsActive] bit  NOT NULL,
    [EntryDate] datetime  NOT NULL,
    [EntryIP] nvarchar(50)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'InputFormSchemaViews'
CREATE TABLE [dbo].[InputFormSchemaViews] (
    [TABLE_CATALOG] nvarchar(128)  NULL,
    [TABLE_SCHEMA] nvarchar(128)  NULL,
    [TABLE_NAME] nvarchar(128)  NOT NULL,
    [COLUMN_NAME] nvarchar(128)  NULL,
    [ORDINAL_POSITION] int  NULL,
    [COLUMN_DEFAULT] nvarchar(4000)  NULL,
    [IS_NULLABLE] varchar(3)  NULL,
    [DATA_TYPE] nvarchar(128)  NULL,
    [CHARACTER_MAXIMUM_LENGTH] int  NULL,
    [CHARACTER_OCTET_LENGTH] int  NULL,
    [NUMERIC_PRECISION] tinyint  NULL,
    [NUMERIC_PRECISION_RADIX] smallint  NULL,
    [NUMERIC_SCALE] int  NULL,
    [DATETIME_PRECISION] smallint  NULL,
    [CHARACTER_SET_CATALOG] nvarchar(128)  NULL,
    [CHARACTER_SET_SCHEMA] nvarchar(128)  NULL,
    [CHARACTER_SET_NAME] nvarchar(128)  NULL,
    [COLLATION_CATALOG] nvarchar(128)  NULL,
    [COLLATION_SCHEMA] nvarchar(128)  NULL,
    [COLLATION_NAME] nvarchar(128)  NULL,
    [DOMAIN_CATALOG] nvarchar(128)  NULL,
    [DOMAIN_SCHEMA] nvarchar(128)  NULL,
    [DOMAIN_NAME] nvarchar(128)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CountryId] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([CountryId] ASC);
GO

-- Creating primary key on [Id] in table 'CriminalOffenceInformations'
ALTER TABLE [dbo].[CriminalOffenceInformations]
ADD CONSTRAINT [PK_CriminalOffenceInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomFields'
ALTER TABLE [dbo].[CustomFields]
ADD CONSTRAINT [PK_CustomFields]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FieldMappings'
ALTER TABLE [dbo].[FieldMappings]
ADD CONSTRAINT [PK_FieldMappings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FinancialInformations'
ALTER TABLE [dbo].[FinancialInformations]
ADD CONSTRAINT [PK_FinancialInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InputFormTables'
ALTER TABLE [dbo].[InputFormTables]
ADD CONSTRAINT [PK_InputFormTables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NameChangeInformations'
ALTER TABLE [dbo].[NameChangeInformations]
ADD CONSTRAINT [PK_NameChangeInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [PK_Packages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PackageCategories'
ALTER TABLE [dbo].[PackageCategories]
ADD CONSTRAINT [PK_PackageCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParentInformations'
ALTER TABLE [dbo].[ParentInformations]
ADD CONSTRAINT [PK_ParentInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PdfTemplates'
ALTER TABLE [dbo].[PdfTemplates]
ADD CONSTRAINT [PK_PdfTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalInformations'
ALTER TABLE [dbo].[PersonalInformations]
ADD CONSTRAINT [PK_PersonalInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [StateId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([StateId] ASC);
GO

-- Creating primary key on [Id] in table 'TemplateTables'
ALTER TABLE [dbo].[TemplateTables]
ADD CONSTRAINT [PK_TemplateTables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [PK_UserPackages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Id] in table 'UserTypes'
ALTER TABLE [dbo].[UserTypes]
ADD CONSTRAINT [PK_UserTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TABLE_NAME] in table 'InputFormSchemaViews'
ALTER TABLE [dbo].[InputFormSchemaViews]
ADD CONSTRAINT [PK_InputFormSchemaViews]
    PRIMARY KEY CLUSTERED ([TABLE_NAME] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_State_Country]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([CountryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_State_Country'
CREATE INDEX [IX_FK_State_Country]
ON [dbo].[States]
    ([CountryId]);
GO

-- Creating foreign key on [PdfTemplateId] in table 'CustomFields'
ALTER TABLE [dbo].[CustomFields]
ADD CONSTRAINT [FK_dbo_CustomField_dbo_PdfTemplate_PdfTemplateId]
    FOREIGN KEY ([PdfTemplateId])
    REFERENCES [dbo].[PdfTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_CustomField_dbo_PdfTemplate_PdfTemplateId'
CREATE INDEX [IX_FK_dbo_CustomField_dbo_PdfTemplate_PdfTemplateId]
ON [dbo].[CustomFields]
    ([PdfTemplateId]);
GO

-- Creating foreign key on [PdfTemplateId] in table 'FieldMappings'
ALTER TABLE [dbo].[FieldMappings]
ADD CONSTRAINT [FK_dbo_FieldMapping_dbo_PdfTemplate_PdfTemplateId]
    FOREIGN KEY ([PdfTemplateId])
    REFERENCES [dbo].[PdfTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_FieldMapping_dbo_PdfTemplate_PdfTemplateId'
CREATE INDEX [IX_FK_dbo_FieldMapping_dbo_PdfTemplate_PdfTemplateId]
ON [dbo].[FieldMappings]
    ([PdfTemplateId]);
GO

-- Creating foreign key on [CategoryId] in table 'Packages'
ALTER TABLE [dbo].[Packages]
ADD CONSTRAINT [FK_Package_PackageCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[PackageCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Package_PackageCategory'
CREATE INDEX [IX_FK_Package_PackageCategory]
ON [dbo].[Packages]
    ([CategoryId]);
GO

-- Creating foreign key on [PackageId] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [FK_UserPackages_Package]
    FOREIGN KEY ([PackageId])
    REFERENCES [dbo].[Packages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPackages_Package'
CREATE INDEX [IX_FK_UserPackages_Package]
ON [dbo].[UserPackages]
    ([PackageId]);
GO

-- Creating foreign key on [PdfTemplateId] in table 'TemplateTables'
ALTER TABLE [dbo].[TemplateTables]
ADD CONSTRAINT [FK_TemplateTable_PdfTemplates]
    FOREIGN KEY ([PdfTemplateId])
    REFERENCES [dbo].[PdfTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TemplateTable_PdfTemplates'
CREATE INDEX [IX_FK_TemplateTable_PdfTemplates]
ON [dbo].[TemplateTables]
    ([PdfTemplateId]);
GO

-- Creating foreign key on [UserId] in table 'UserPackages'
ALTER TABLE [dbo].[UserPackages]
ADD CONSTRAINT [FK_UserPackages_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPackages_Users'
CREATE INDEX [IX_FK_UserPackages_Users]
ON [dbo].[UserPackages]
    ([UserId]);
GO

-- Creating foreign key on [UserTypeId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_UserType]
    FOREIGN KEY ([UserTypeId])
    REFERENCES [dbo].[UserTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_UserType'
CREATE INDEX [IX_FK_Users_UserType]
ON [dbo].[Users]
    ([UserTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------