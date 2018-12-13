CREATE SCHEMA [Accounting] AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Accounts] AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Bank] AUTHORIZATION [dbo]
GO
CREATE SCHEMA [System] AUTHORIZATION [dbo]
GO
CREATE TABLE [Accounting].[Balance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[CurrentBalance] [decimal](19,4) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Transaction_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Transaction_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL 
 CONSTRAINT [PK_Balance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Accounting].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAccountId] [int] NOT NULL,
	[ToAccountId] [int] NOT NULL,
	[Amount] [decimal](19,4) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Description] [nvarchar](MAX) NULL,
	[TypeId] [int] NOT NULL,
	[OnlinePaymentId] [int] NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Transaction_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Transaction_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL 
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Accounting].[TransactionType]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [Title] [NVARCHAR](MAX) NOT NULL ,
      [TitleEn] [VARCHAR](MAX) NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_TransactionType_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_TransactionType_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NOT NULL,
    [EntityTypeId] [int] NULL,
	[UserCode] [nvarchar](16) NOT NULL,
	[Username] [nvarchar](255) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[Email] [nvarchar](255) NULL,
	[Mobile] [nvarchar](255) NULL,
	[CreationDateTime] [datetime] NOT NULL,
    [EmailVerificationStatusId] [INT] NOT NULL,
    [MobileVerificationStatusId] [INT] NOT NULL,
	[EmailVerificationToken] [nvarchar](max) NULL,
	[EmailVerificationTokenExpirationDate] [datetime] NULL,
	[MobileVerificationToken] [nvarchar](max) NULL,
	[MobileVerificationTokenExpirationDate] [datetime] NULL,
	[StateId] [int] NOT NULL,
	[StateNotes] [nvarchar](max) NULL,
	[Roles] NVARCHAR(max) NULL, 
	[Permissions] NVARCHAR(max) NULL,
	[VerificationStatusId] INT NOT NULL,
	[ReferrerAccountId] INT NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Account_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Account_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL
CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [Accounts].[AccountEntityType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_AccountEntityType_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_AccountEntityType_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_AccountEntityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[AccountHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
	  [TypeId] [int] NOT NULL,
	  [EntityTypeId] [int] NULL,
	  [UserCode] [nvarchar](16) NOT NULL,
	  [Username] [nvarchar](255) NULL,
	  [PasswordHash] [nvarchar](max) NULL,
	  [Email] [nvarchar](255) NULL,
	  [Mobile] [nvarchar](255) NULL,
	  [CreationDateTime] [datetime] NOT NULL,
	  [EmailVerificationStatusId] [INT] NOT NULL,
	  [MobileVerificationStatusId] [INT] NOT NULL,
	  [EmailVerificationToken] [nvarchar](max) NULL,
	  [EmailVerificationTokenExpirationDate] [datetime] NULL,
	  [MobileVerificationToken] [nvarchar](max) NULL,
	  [MobileVerificationTokenExpirationDate] [datetime] NULL,
	  [StateId] [int] NOT NULL,
	  [StateNotes] [nvarchar](max) NULL,
	  [Roles] NVARCHAR(max) NULL, 
	  [Permissions] NVARCHAR(max) NULL,
	  [VerificationStatusId] INT NOT NULL,
	  [ReferrerAccountId] INT NULL,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[AccountState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_AccountState_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_AccountState_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_AccountState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[AccountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_AccountType_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_AccountType_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[Contact]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [AccountId] [INT] NOT NULL ,
      [Tel] [NVARCHAR](20) NOT NULL ,
      [Fax] [NVARCHAR](20) NULL ,
      [CountryId] [INT] NOT NULL ,
      [ProvinceId] [INT] NOT NULL ,
      [City] [NVARCHAR](255) NOT NULL ,
      [Address] [NVARCHAR](MAX) NOT NULL ,
      [AddressEn] [VARCHAR](MAX) NULL ,
      [PostalCode] [NVARCHAR](20) NOT NULL ,
      [TypeId] [INT] NOT NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_Contact_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Contact_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
      CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[ContactHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL ,
      [AccountId] [INT] NULL ,
      [Tel] [NVARCHAR](20) NULL ,
      [Fax] [NVARCHAR](20) NULL ,
      [CountryId] [INT] NULL ,
      [ProvinceId] INT NULL ,
      [City] [NVARCHAR](255) NULL ,
      [Address] [NVARCHAR](MAX) NULL ,
      [AddressEn] [VARCHAR](MAX) NULL ,
      [PostalCode] [NVARCHAR](20) NULL ,
      [TypeId] [INT] NULL ,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NOT NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[ContactType]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [Title] [NVARCHAR](MAX) NOT NULL ,
      [TitleEn] [VARCHAR](MAX) NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_ContactType_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_ContactType_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
      CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[Country] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Country_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Country_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[Document]
    (
      [Id] [INT] IDENTITY(1, 1)  NOT NULL ,
      [AccountId] INT NOT NULL ,
      [DateTime] DATETIME NOT NULL ,
      [TypeId] INT NOT NULL ,
      [Title] NVARCHAR(MAX) NOT NULL ,
      [TitleEn] NVARCHAR(MAX) NULL ,
      [MimeType] NVARCHAR(MAX) NOT NULL ,
      [Data] VARBINARY(MAX) NOT NULL ,
      [Note] NVARCHAR(MAX) NULL,
      [FileName] NVARCHAR(MAX) NOT NULL,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_Document_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Document_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
        CONSTRAINT [PK_Document]
        PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
CREATE TABLE [Accounts].[DocumentType]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [Title] [NVARCHAR](MAX) NOT NULL ,
      [TitleEn] [NVARCHAR](MAX) NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_DocumentType_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_DocumentType_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
        CONSTRAINT [PK_DocumentType]
        PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
CREATE TABLE [Accounts].[GenderType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP,
	[RecordState] INT NOT NULL CONSTRAINT [DF_GenderType_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_GenderType_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL,
	[RecordDeleteDateTime] DATETIME NULL,
 CONSTRAINT [PK_GenderType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[LegalProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[NameEn] [varchar](max) NULL,
	[RegistrationCode] [nvarchar](max) NOT NULL,
	[NationalCode] [nvarchar](max) NOT NULL,
	[EconomicCode] [nvarchar](max) NULL,
	[RegistrationDate] [date] NOT NULL,
	[AttorneyId] [INT] NULL,
	[PowerOfAttorneyEndDate] [DATETIME] NULL,
	[PowerOfAttorneyNumber] [NVARCHAR](MAX) NULL,
	[RegistrationPlace] [NVARCHAR](MAX) NOT NULL,
	[BusinessDescription] [NVARCHAR](MAX) NOT NULL,
	[BusinessEntityTypeId] [NVARCHAR](MAX) NOT NULL,
	[OfficialPaperNumber] [NVARCHAR](MAX) NOT NULL,
	[OfficialPaperDate] [NVARCHAR](MAX) NOT NULL,
	[WebSite] [NVARCHAR](MAX) NULL,
	[TradingAgent] [NVARCHAR](MAX) NOT NULL,
	[FidaCode] [NVARCHAR](MAX) NULL,
	[CountryId] [INT] NOT NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_CompanyProfile_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_CompanyProfile_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_CompanyProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [Accounts].[LegalProfileHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [INT] NULL,
      [Name] [NVARCHAR](MAX) NULL,
      [NameEn] [VARCHAR](MAX) NULL,
      [RegistrationCode] [NVARCHAR](MAX) NULL,
      [NationalCode] [NVARCHAR](MAX) NULL,
      [EconomicCode] [NVARCHAR](MAX) NULL,
      [RegistrationDate] [DATE] NULL,
	  [AttorneyId] [INT] NULL,
      [PowerOfAttorneyEndDate] [DATETIME] NULL,
      [PowerOfAttorneyNumber] [NVARCHAR](MAX) NULL,
      [RegistrationPlace] [NVARCHAR](MAX) NULL,
      [BusinessDescription] [NVARCHAR](MAX) NULL,
      [BusinessEntityTypeId] [NVARCHAR](MAX) NULL,
      [OfficialPaperNumber] [NVARCHAR](MAX) NULL,
      [OfficialPaperDate] [NVARCHAR](MAX) NULL,
      [WebSite] [NVARCHAR](MAX) NULL,
      [TradingAgent] [NVARCHAR](MAX) NULL,
      [FidaCode] [NVARCHAR](MAX) NULL,
	  [CountryId] [INT] NULL,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[PersonProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[GenderTypeId] [int] NULL,
	[FatherName] [nvarchar](max) NULL,
	[NationalCode] [nvarchar](max) NULL,
	[IdentificationNumber] [nvarchar](max) NULL,
	[IdentificationSerial] [nvarchar](max) NULL,
	[Birthdate] [date] NULL,
	[IdentificationPlaceOfIssue] [NVARCHAR](MAX) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_PersonProfile_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_PersonProfile_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_PersonProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [Accounts].[PersonProfileHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [int] NULL,
      [FirstName] [nvarchar](max) NULL,
      [LastName] [nvarchar](max) NULL,
      [GenderTypeId] [int] NULL,
      [FatherName] [nvarchar](max) NULL,
      [NationalCode] [nvarchar](max) NULL,
      [IdentificationNumber] [nvarchar](max) NULL,
      [IdentificationSerial] [nvarchar](max) NULL,
      [Birthdate] [date] NULL,
      [IdentificationPlaceOfIssue] [NVARCHAR](MAX) NULL,
      [RecordVersion] VARBINARY(8) ,
      [RecordState] INT NULL,
      [RecordInsertDateTime] DATETIME NULL,
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[Province] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Province_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Province_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[PushTarget](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[TypeId] [int] NOT NULL,
	[PlatformUniqueId] [nvarchar](255) NOT NULL,
	[PlatformName] [nvarchar](255) NOT NULL,
	[PlatformVersion] [nvarchar](255) NOT NULL,
	[RegistrationDateTime] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL ,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_PushTarget_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_PushTarget_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL,
CONSTRAINT [PK_PushTarget] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [Accounts].[PushTargetType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_PushTargetType_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_PushTargetType_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_PushTargetType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Accounts].[PushTargetStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TitleEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_PushTargetStatus_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_PushTargetStatus_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
CONSTRAINT [PK_PushTargetStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [Accounts].[Session](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessToken] [nvarchar](255) NOT NULL,
	[AccountId] [int] NULL,
	[CreationDateTime] [datetime] NOT NULL,
	[InvalidateDateTime] [datetime] NULL,
	[Permissions] NVARCHAR(max) NULL,
	[StateId] INT NOT NULL,
	[LastAccessDateTime] DATETIME NOT NULL,
	[Ip] VARCHAR(MAX) NOT NULL,
	[UserAgent] NVARCHAR(MAX) NOT NULL,
	[TypeId] INT NOT NULL,
	[Mobile] NVARCHAR(20) NULL,
	[MobileToken] NVARCHAR(20) NULL,
	[MobileTokenGenerationDateTime] DATETIME NULL,
	[MobileTokenExpirationDateTime] DATETIME NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Session_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Session_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
CREATE TABLE [Accounts].[SessionState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(MAX) NOT NULL,
	[TitleEn] NVARCHAR(MAX) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_SessionState_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_SessionState_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_SessionState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO
CREATE TABLE [Accounts].[SessionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(MAX) NOT NULL,
	[TitleEn] VARCHAR(MAX) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_SessionType_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_SessionType_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_SessionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [Accounts].[VerificationStatus]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL,
      [Title] [NVARCHAR](MAX) NOT NULL,
      [TitleEn] [VARCHAR](MAX) NULL,
      [RecordVersion] TIMESTAMP,
      [RecordState] INT NOT NULL CONSTRAINT [DF_VerificationStatus_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_VerificationStatus_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL,
      [RecordDeleteDateTime] DATETIME NULL,
      CONSTRAINT [PK_VerificationStatus] PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Bank].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[NameEn] [varchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Bank_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Bank_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Bank].[BankAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[BranchName] [NVARCHAR](MAX) NULL,
	[BranchNameEn] [VARCHAR](MAX) NULL,
	[BranchCode] [NVARCHAR](MAX) NULL,
	[TypeId] [INT] NULL,
	[Iban] [NVARCHAR](MAX) NULL, 
	[OnlineLinkStatusId] [int] NOT NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_BankAccount_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_BankAccount_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO
CREATE TABLE [Bank].[BankAccountHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [INT] NULL,
      [BankId] [INT] NULL,
      [Number] [NVARCHAR](MAX) NULL,
      [BranchName] [NVARCHAR](MAX) NULL,
      [BranchNameEn] [VARCHAR](MAX) NULL,
      [BranchCode] [NVARCHAR](MAX) NULL,
      [TypeId] [INT] NULL,
      [Iban] [NVARCHAR](MAX) NULL,
	  [OnlineLinkStatusId] [int] NULL DEFAULT 1,
      [Notes] NVARCHAR(MAX) NULL,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Bank].[BankAccountOnlineLinkStatus]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL,
      [Title] [NVARCHAR](MAX) NOT NULL,
      [TitleEn] [VARCHAR](MAX) NULL, 
      [RecordVersion] TIMESTAMP,
      [RecordState] INT NOT NULL CONSTRAINT [DF_BankAccountOnlineLinkStatus_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_BankAccountOnlineLinkStatus_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL,
      [RecordDeleteDateTime] DATETIME NULL,
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [Bank].[BankAccountType]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL,
      [Title] [NVARCHAR](MAX) NOT NULL,
      [TitleEn] [VARCHAR](MAX) NULL,
      [RecordVersion] TIMESTAMP,
      [RecordState] INT NOT NULL CONSTRAINT [DF_BankAccountType_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_BankAccountType_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL,
      [RecordDeleteDateTime] DATETIME NULL,
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [System].[Configuration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[BooleanValue] [bit] NULL,
	[IntegerValue] [bigint] NULL,
	[DecimalValue] [decimal](19, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[DateValue] [datetime] NULL,
	[BinaryValue] [varbinary](4000) NULL,
	[Tags] [nvarchar](4000) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [System].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AccountId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL
CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [System].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AccountId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [System].[SystemLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Action] [nvarchar](max) NOT NULL
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [System].[SystemLogParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemLogId] [int] NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[BooleanValue] [bit] NULL,
	[IntegerValue] [bigint] NULL,
	[DecimalValue] [decimal](19, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[DateTime] [datetime] NULL,
	[BinaryValue] [varbinary](max) NULL
 CONSTRAINT [PK_SystemLogParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [System].[UserLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [System].[UserLogParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserLogId] [int] NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[BooleanValue] [bit] NULL,
	[IntegerValue] [bigint] NULL,
	[DecimalValue] [decimal](19, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[DateValue] [datetime] NULL,
	[BinaryValue] [varbinary](max) NULL,
 CONSTRAINT [PK_UserLogParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Accounts].[SessionState] ON
INSERT INTO [Accounts].[SessionState] (Id, Title)
VALUES (1, N'باز'), (2, N'بسته'), (3, N'منقضی شده')
SET IDENTITY_INSERT [Accounts].[SessionState] OFF
GO
SET IDENTITY_INSERT [Accounting].[TransactionType] ON
INSERT INTO [Accounting].[TransactionType] (Id, Title, TitleEn) VALUES(1, N'پرداخت کرایه تاکسی', 'Taxi Payment')
INSERT INTO [Accounting].[TransactionType] (Id, Title, TitleEn) VALUES(2, N'افزایش اعتبار', 'Increase Credit')
INSERT INTO [Accounting].[TransactionType] (Id, Title, TitleEn) VALUES(3, N'شارژ هدیه', 'Reward')
INSERT INTO [Accounting].[TransactionType] (Id, Title, TitleEn) VALUES(4, N'هدیه دعوت دوستان', 'User Invitation Reward')
INSERT INTO [Accounting].[TransactionType] (Id, Title, TitleEn) VALUES(5, N'انتقال موجودی', 'Transfer Credit')
SET IDENTITY_INSERT [Accounting].[TransactionType] OFF
GO
SET IDENTITY_INSERT [Accounts].[Account] ON
INSERT INTO [Accounts].[Account] ([Id],
                                 [TypeId],
                                 [EntityTypeId],
								 [UserCode],
								 [Username],
								 [PasswordHash],
								 [CreationDateTime],
								 [EmailVerificationStatusId],
								 [MobileVerificationStatusId],
								 [StateId],
								 [Roles],
								 [VerificationStatusId]) 
						  VALUES(1,
						         1,
                                 1,
								 '123456',
								 'admin',
								 '1000:2b7/4PNTOGtb2fuUxLADzccSl3bfXxwg:KTS5kYwZzsePZswA0dDj9tg1QBS6SxOR', --123
								 GETDATE(),
								 '1',
								 '1',
								 1,
								 N'admin',
								 1)
SET IDENTITY_INSERT [Accounts].[Account] OFF


GO
SET IDENTITY_INSERT [Accounts].[AccountEntityType] ON 
INSERT [Accounts].[AccountEntityType] ([Id], [Title], [TitleEn]) VALUES (1, N'حقیقی', 'Person')
INSERT [Accounts].[AccountEntityType] ([Id], [Title], [TitleEn]) VALUES (2, N'حقوقی', 'Legal')
SET IDENTITY_INSERT [Accounts].[AccountEntityType] OFF

GO
SET IDENTITY_INSERT [Accounts].[AccountState] ON 
INSERT [Accounts].[AccountState] ([Id], [Title], [TitleEn]) VALUES (1, N'فعال', 'Active')
INSERT [Accounts].[AccountState] ([Id], [Title], [TitleEn]) VALUES (2, N'غیر فعال', 'Deactive')
SET IDENTITY_INSERT [Accounts].[AccountState] OFF

GO
SET IDENTITY_INSERT [Accounts].[AccountType] ON 
INSERT [Accounts].[AccountType] ([Id], [Title], [TitleEn]) VALUES (1, N'کاربر', 'User')
INSERT [Accounts].[AccountType] ([Id], [Title], [TitleEn]) VALUES (2, N'مشتری', 'Customer')
SET IDENTITY_INSERT [Accounts].[AccountType] OFF

GO
INSERT  INTO [Accounts].[ContactType]
        ( [Title] )
VALUES
        ( N'آدرس منزل'),
        ( N'آدرس محل کار'),
        ( N'دفتر مرکزی'),
        ( N'دفتر مرکزی خارج از کشور')
GO
SET IDENTITY_INSERT [Accounts].[Country] ON 

INSERT INTO [Accounts].[Country] ([Id], [Title], [TitleEn]) VALUES
(1, N'ایران', N'Iran'),
(2, N'جزایر آسنسیون', N'Ascension Island'),
(3, N'آندورا', N'Andorra'),
(4, N'امارات متحدهٔ عربی', N'United Arab Emirates'),
(5, N'افغانستان', N'Afghanistan'),
(6, N'آنتیگوا و باربودا', N'Antigua and Barbuda'),
(7, N'آنگویلا', N'Anguilla'),
(8, N'آلبانی', N'Albania'),
(9, N'ارمنستان', N'Armenia'),
(10, N'آنگولا', N'Angola'),
(11, N'جنوبگان', N'Antarctica'),
(12, N'آرژانتین', N'Argentina'),
(13, N'ساموآی آمریکا', N'American Samoa'),
(14, N'اتریش', N'Austria'),
(15, N'استرالیا', N'Australia'),
(16, N'آروبا', N'Aruba'),
(17, N'جزایر اُلند', N'?land Islands'),
(18, N'جمهوری آذربایجان', N'Azerbaijan'),
(19, N'بوسنی و هرزگوین', N'Bosnia and Herzegovina'),
(20, N'باربادوس', N'Barbados'),
(21, N'بنگلادش', N'Bangladesh'),
(22, N'بلژیک', N'Belgium'),
(23, N'بورکینافاسو', N'Burkina Faso'),
(24, N'بلغارستان', N'Bulgaria'),
(25, N'بحرین', N'Bahrai'),
(26, N'بوروندی', N'Burundi'),
(27, N'بنین', N'Benin'),
(28, N'سن بارتلمی', N'Saint Barthélemy'),
(29, N'برمودا', N'Bermuda'),
(30, N'برونئی', N'Brunei'),
(31, N'بولیوی', N'Bolivia'),
(32, N'جزایر کارائیب هلند', N'Bonaire, Sint Eustatius, and Saba'),
(33, N'برزیل', N'Brazil'),
(34, N'باهاما', N'Bahamas'),
(35, N'بوتان', N'Bhutan'),
(36, N'بوتسوانا', N'Botswana'),
(37, N'بلاروس', N'Belarus'),
(38, N'بلیز', N'Belize'),
(39, N'کانادا', N'Canada'),
(40, N'جزایر کوکوس (کیلینگ)', N'Cocos [Keeling] Islands'),
(41, N'کنگو - کینشاسا', N'Congo - Kinshasa'),
(42, N'جمهوری افریقای مرکزی', N'Central African Republic'),
(43, N'کنگو - برازویل', N'Congo - Brazzaville'),
(44, N'سوئیس', N'Switzerland'),
(45, N'ساحل عاج', N'Côte d’Ivoire'),
(46, N'جزایر کوک', N'Cook Islands'),
(47, N'شیلی', N'Chile'),
(48, N'کامرون', N'Cameroo'),
(49, N'چین', N'China'),
(50, N'کلمبیا', N'Colombia'),
(51, N'کاستاریکا', N'Costa Rica'),
(52, N'کوبا', N'Cuba'),
(53, N'کیپ‌ورد', N'Cape Verde'),
(54, N'کوراسائو', N'Curaçao'),
(55, N'جزیرهٔ کریسمس', N'Christmas Island'),
(56, N'قبرس', N'Cyprus'),
(57, N'جمهوری چک', N'Czech Republic'),
(58, N'آلمان', N'Germany'),
(59, N'دیه‌گو گارسیا', N'Diego Garcia'),
(60, N'جیبوتی', N'Djibouti'),
(61, N'دانمارک', N'Denmark'),
(62, N'دومینیکا', N'Dominica'),
(63, N'جمهوری دومینیکن', N'Dominican Republic'),
(64, N'الجزایر', N'Algeria'),
(65, N'سبته و ملیله', N'Ceuta and Melilla'),
(66, N'اکوادور', N'Ecuador'),
(67, N'استونی', N'Estonia'),
(68, N'مصر', N'Egypt'),
(69, N'صحرای غربی', N'Western Sahara'),
(70, N'اریتره', N'Eritrea'),
(71, N'اسپانیا', N'Spain'),
(72, N'اتیوپی', N'Ethiopia'),
(73, N'فنلاند', N'Finland'),
(74, N'فیجی', N'Fiji'),
(75, N'جزایر فالکلند', N'Falkland Islands'),
(76, N'میکرونزی', N'Micronesia'),
(77, N'جزایر فارو', N'Faroe Islands'),
(78, N'فرانسه', N'France'),
(79, N'گابن', N'Gabon'),
(80, N'بریتانیا', N'United Kingdom'),
(81, N'گرنادا', N'Grenada'),
(82, N'گرجستان', N'Georgia'),
(83, N'گویان فرانسه', N'French Guiana'),
(84, N'گرنزی', N'Guernsey'),
(85, N'غنا', N'Ghana'),
(86, N'جبل‌الطارق', N'Gibraltar'),
(87, N'گرینلند', N'Greenland'),
(88, N'گامبیا', N'Gambia'),
(89, N'گینه', N'Guinea'),
(90, N'گوادلوپ', N'Guadeloupe'),
(91, N'گینهٔ استوایی', N'Equatorial Guinea'),
(92, N'یونان', N'Greece'),
(93, N'جزایر جورجیای جنوبی و ساندویچ جنوبی', N'South Georgia and the South Sandwich Islands'),
(94, N'گواتمالا', N'Guatemala'),
(95, N'گوام', N'Guam'),
(96, N'گینهٔ بیسائو', N'Guinea-Bissau'),
(97, N'گویان', N'Guyana'),
(98, N'هنگ‌کنگ، ناحیهٔ ویژهٔ حکومتی چین', N'Hong Kong SAR China'),
(99, N'هندوراس', N'Honduras'),
(100, N'کرواسی', N'Croatia'),
(101, N'هائیتی', N'Haiti'),
(102, N'مجارستان', N'Hungary'),
(103, N'جزایر قناری', N'Canary Islands'),
(104, N'اندونزی', N'Indonesia'),
(105, N'ایرلند', N'Ireland'),
(106, N'اسرائیل', N'Israel'),
(107, N'جزیرهٔ من', N'Isle of Man'),
(108, N'هند', N'India'),
(109, N'قلمرو بریتانیا در اقیانوس هند', N'British Indian Ocean Territory'),
(110, N'عراق', N'Iraq'),
(111, N'ایسلند', N'Iceland'),
(112, N'ایتالیا', N'Italy'),
(113, N'جرزی', N'Jersey'),
(114, N'جامائیکا', N'Jamaica'),
(115, N'اردن', N'Jordan'),
(116, N'ژاپن', N'Japan'),
(117, N'کنیا', N'Kenya'),
(118, N'قرقیزستان', N'Kyrgyzstan'),
(119, N'کامبوج', N'Cambodia'),
(120, N'کیریباتی', N'Kiribati'),
(121, N'کومور', N'Comoros'),
(122, N'سنت کیتس و نویس', N'Saint Kitts and Nevis'),
(123, N'کرهٔ شمالی', N'North Korea'),
(124, N'کرهٔ جنوبی', N'South Korea'),
(125, N'کویت', N'Kuwait'),
(126, N'جزایر کِیمن', N'Cayman Islands'),
(127, N'قزاقستان', N'Kazakhstan'),
(128, N'لائوس', N'Laos'),
(129, N'لبنان', N'Lebanon'),
(130, N'سنت لوسیا', N'Saint Lucia'),
(131, N'لیختن‌اشتاین', N'Liechtenstein'),
(132, N'سری‌لانکا', N'Sri Lanka'),
(133, N'لیبریا', N'Liberia'),
(134, N'لسوتو', N'Lesotho'),
(135, N'لیتوانی', N'Lithuania'),
(136, N'لوکزامبورگ', N'Luxembourg'),
(137, N'لتونی', N'Latvia'),
(138, N'لیبی', N'Libya'),
(139, N'مراکش', N'Morocco'),
(140, N'موناکو', N'Monaco'),
(141, N'مولداوی', N'Moldova'),
(142, N'مونته‌نگرو', N'Montenegro'),
(143, N'سنت مارتین', N'Saint Martin'),
(144, N'ماداگاسکار', N'Madagascar'),
(145, N'جزایر مارشال', N'Marshall Islands'),
(146, N'مقدونیه', N'Macedonia'),
(147, N'مالی', N'Mali'),
(148, N'میانمار (برمه)', N'Myanmar [Burma]'),
(149, N'مغولستان', N'Mongolia'),
(150, N'ماکائو، ناحیهٔ ویژهٔ حکومتی چین', N'Macau SAR China'),
(151, N'جزایر ماریانای شمالی', N'Northern Mariana Islands'),
(152, N'مارتینیک', N'Martinique'),
(153, N'موریتانی', N'Mauritania'),
(154, N'مونت‌سرات', N'Montserrat'),
(155, N'مالت', N'Malta'),
(156, N'موریس', N'Mauritius'),
(157, N'مالدیو', N'Maldives'),
(158, N'مالاوی', N'Malawi'),
(159, N'مکزیک', N'Mexico'),
(160, N'مالزی', N'Malaysia'),
(161, N'موزامبیک', N'Mozambique'),
(162, N'نامیبیا', N'Namibia'),
(163, N'کالدونیای جدید', N'New Caledonia'),
(164, N'نیجر', N'Niger'),
(165, N'جزیره نورفک', N'Norfolk Island'),
(166, N'نیجریه', N'Nigeria'),
(167, N'نیکاراگوئه', N'Nicaragua'),
(168, N'هلند', N'Netherlands'),
(169, N'نروژ', N'Norway'),
(170, N'نپال', N'Nepal'),
(171, N'نائورو', N'Nauru'),
(172, N'نیوئه', N'Niue'),
(173, N'زلاند نو', N'New Zealand'),
(174, N'عمان', N'Oman'),
(175, N'پاناما', N'Panama'),
(176, N'پرو', N'Peru'),
(177, N'پلی‌نزی فرانسه', N'French Polynesia'),
(178, N'پاپوا گینهٔ نو', N'Papua New Guinea'),
(179, N'فیلیپین', N'Philippines'),
(180, N'پاکستان', N'Pakistan'),
(181, N'لهستان', N'Poland'),
(182, N'سن پیر و میکلن', N'Saint Pierre and Miquelon'),
(183, N'جزایر پیت‌کرن', N'Pitcairn Islands'),
(184, N'پورتوریکو', N'Puerto Rico'),
(185, N'سرزمین‌های فلسطینی', N'Palestinian Territories'),
(186, N'پرتغال', N'Portugal'),
(187, N'پالائو', N'Palau'),
(188, N'پاراگوئه', N'Paraguay'),
(189, N'قطر', N'Qatar'),
(190, N'رئونیون', N'Réunion'),
(191, N'رومانی', N'Romania'),
(192, N'صربستان', N'Serbia'),
(193, N'روسیه', N'Russia'),
(194, N'رواندا', N'Rwanda'),
(195, N'عربستان سعودی', N'Saudi Arabia'),
(196, N'جزایر سلیمان', N'Solomon Islands'),
(197, N'سیشل', N'Seychelles'),
(198, N'سودان', N'Sudan'),
(199, N'سوئد', N'Sweden'),
(200, N'سنگاپور', N'Singapore'),
(201, N'سنت هلن', N'Saint Helena'),
(202, N'اسلوونی', N'Slovenia'),
(203, N'اسوالبارد و جان‌ماین', N'Svalbard and Jan Mayen'),
(204, N'اسلواکی', N'Slovakia'),
(205, N'سیرالئون', N'Sierra Leone'),
(206, N'سن مارینو', N'San Marino'),
(207, N'سنگال', N'Senegal'),
(208, N'سومالی', N'Somalia'),
(209, N'سورینام', N'SuriTitle'),
(210, N'سودان جنوبی', N'South Sudan'),
(211, N'سائوتومه و پرینسیپ', N'S?o Tomé and Pr?ncipe'),
(212, N'السالوادور', N'El Salvador'),
(213, N'سنت مارتن', N'Sint Maarten'),
(214, N'سوریه', N'Syria'),
(215, N'سوازیلند', N'Swaziland'),
(216, N'تریستان دا کونا', N'Tristan da Cunha'),
(217, N'جزایر تورکس و کایکوس', N'Turks and Caicos Islands'),
(218, N'چاد', N'Chad'),
(219, N'قلمروهای جنوبی فرانسه', N'French Southern Territories'),
(220, N'توگو', N'Togo'),
(221, N'تایلند', N'Thailand'),
(222, N'تاجیکستان', N'Tajikistan'),
(223, N'توکلائو', N'Tokelau'),
(224, N'تیمور شرقی', N'Timor-Leste'),
(225, N'ترکمنستان', N'Turkmenistan'),
(226, N'تونس', N'Tunisia'),
(227, N'تونگا', N'Tonga'),
(228, N'ترکیه', N'Turkey'),
(229, N'ترینیداد و توباگو', N'TrinIdad and Tobago'),
(230, N'تووالو', N'Tuvalu'),
(231, N'تایوان', N'Taiwan'),
(232, N'تانزانیا', N'Tanzania'),
(233, N'اوکراین', N'Ukraine'),
(234, N'اوگاندا', N'Uganda'),
(235, N'جزایر دورافتادهٔ ایالات متحده', N'U.S. Minor Outlying Islands'),
(236, N'ایالات متحدهٔ امریکا', N'United States'),
(237, N'اروگوئه', N'Uruguay'),
(238, N'ازبکستان', N'Uzbekistan'),
(239, N'واتیکان', N'Vatican City'),
(240, N'سنت وینسنت و گرنادین‌ها', N'Saint Vincent and the Grenadines'),
(241, N'ونزوئلا', N'Venezuela'),
(242, N'جزایر ویرجین بریتانیا', N'British Virgin Islands'),
(243, N'جزایر ویرجین ایالات متحده', N'U.S. Virgin Islands'),
(244, N'ویتنام', N'Vietnam'),
(245, N'وانواتو', N'Vanuatu'),
(246, N'والیس و فوتونا', N'Wallis and Futuna'),
(247, N'ساموآ', N'Samoa'),
(248, N'یمن', N'Yemen'),
(249, N'مایوت', N'Mayotte'),
(250, N'افریقای جنوبی', N'South Africa'),
(251, N'زامبیا', N'Zambia'),
(252, N'زیمبابوه', N'Zimbabwe')

SET IDENTITY_INSERT [Accounts].[Country] OFF
GO
SET IDENTITY_INSERT	 [Accounts].[DocumentType] ON

INSERT INTO [Accounts].[DocumentType]
(
    [Id],
    [Title]
)
VALUES
(1, N'شناسنامه'),
(2, N'کارت ملی'),
(3, N'مدرک تحصیلی'),
(4, N'پاسپورت'),
(5, N'آگهی ثبت'),
(6, N'آگهی تغییرات'),
(7, N'وکالتنامه'),
(8, N'نمونه امضا'),
(9, N'عکس'),
(10, N'قرارداد اعتباری'),
(11, N'قرارداد آنلاین'),
(12, N'فرم افتتاح حساب'),
(13, N'فرم‌های پیشخوان')

SET IDENTITY_INSERT	 [Accounts].[DocumentType] OFF
GO
SET IDENTITY_INSERT [Accounts].[GenderType] ON 
INSERT [Accounts].[GenderType] ([Id], [Title], [TitleEn]) VALUES (1, N'مرد', 'Male')
INSERT [Accounts].[GenderType] ([Id], [Title], [TitleEn]) VALUES (2, N'زن', 'Female')
SET IDENTITY_INSERT [Accounts].[GenderType] OFF

GO
SET IDENTITY_INSERT [Accounts].[Province] ON
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(1, 1, N'آذربایجان شرقی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(2, 1, N'آذربایجان غربی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(3, 1, N'اردبیل', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(4, 1, N'اصفهان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(5, 1, N'ایلام', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(6, 1, N'بوشهر', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(7, 1, N'تهران', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(8, 1, N'خراسان جنوبی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(9, 1, N'خراسان رضوی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(10, 1, N'خراسان شمالی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(11, 1, N'خوزستان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(12, 1, N'زنجان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(13, 1, N'سمنان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(14, 1, N'سیستان و بلوچستان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(15, 1, N'فارس', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(16, 1, N'قزوین', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(17, 1, N'قم', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(18, 1, N'لرستان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(19, 1, N'مازندران', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(20, 1, N'مرکزی', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(21, 1, N'هرمزگان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(22, 1, N'همدان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(23, 1, N'چهارمحال و بختیاری', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(24, 1, N'کردستان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(25, 1, N'کرمان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(26, 1, N'کرمانشاه', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(27, 1, N'کهگیلویه و بویراحمد', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(28, 1, N'گلستان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(29, 1, N'گیلان', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(30, 1, N'یزد', NULL)
INSERT INTO [Accounts].[Province] ([Id], [CountryId], [Title], [TitleEn]) VALUES(31, 1, N'البرز', NULL)
SET IDENTITY_INSERT [Accounts].[Province] OFF

GO
SET IDENTITY_INSERT [Accounts].[PushTargetType] ON 
INSERT [Accounts].[PushTargetType] ([Id], [Title], [TitleEn]) VALUES (1, N'اندروید', 'Android')
INSERT [Accounts].[PushTargetType] ([Id], [Title], [TitleEn]) VALUES (2, N'آی او اس', 'ios')
SET IDENTITY_INSERT [Accounts].[PushTargetType] OFF

GO
SET IDENTITY_INSERT [Accounts].[PushTargetStatus] ON 
INSERT [Accounts].[PushTargetStatus] ([Id], [Title], [TitleEn]) VALUES (1, N'فعال', 'Active')
INSERT [Accounts].[PushTargetStatus] ([Id], [Title], [TitleEn]) VALUES (2, N'غیر فعال', 'Deactive')
SET IDENTITY_INSERT [Accounts].[PushTargetStatus] OFF

GO
SET IDENTITY_INSERT [Accounts].[SessionType] ON

INSERT INTO [Accounts].[SessionType] (Id, Title, TitleEn)
VALUES
(1, N'عادی', 'Normal'),
(2, N'دائمی', 'Permanent')

SET IDENTITY_INSERT [Accounts].[SessionType] OFF
GO
INSERT  INTO [Accounts].[VerificationStatus]
        ( [Title] )
VALUES
        ( N'تایید شده'),
        ( N'تایید نشده'),
        ( N'در حال تایید')
GO
SET IDENTITY_INSERT [Bank].[Bank] ON 
INSERT [Bank].[Bank] ([Id], [Key], [Name],[NameEn]) 
VALUES 
(1,'ir.bank.saman', N'بانک سامان' , 'Saman Bank'),
(2,'ir.bank.mellat', N'بانک ملت', null),
(3,'ir.bank.melli', N'بانک ملی', null),
(4,'ir.bank.saderat', N'بانک صادرات', null),
(5,'ir.bank.sepah', N'بانک سپه', null),
(6,'ir.bank.tejarat', N'بانک تجارت', null),
(7,'ir.bank.refah', N'بانک رفاه کارگران', null),
(8,'ir.bank.tosetaavon', N'بانک توسعه تعاون', null),
(9,'ir.bank.tosesaderat', N'بانک توسعه صادرات', null),
(10,'ir.bank.sanatvamadan', N'بانک صنعت و معدن', null),
(11,'ir.bank.keshavarzi', N'بانک کشاورزی', null),
(12,'ir.bank.maskan', N'بانک مسکن', null),
(13,'ir.bank.postbank', N'بانک پست بانک', null),
(14,'ir.bank.eghtesadnovin', N'بانک اقتصاد نوین', null),
(15,'ir.bank.parsian', N'بانک پارسیان', null),
(16,'ir.bank.pasargad', N'بانک پاسارگاد', null),
(17,'ir.bank.dey', N'بانک دی', null),
(18,'ir.bank.ayande', N'بانک آینده', null),
(19,'ir.bank.sarmaye', N'بانک سرمایه', null),
(20,'ir.bank.sina', N'بانک سینا', null),
(21,'ir.bank.shahr', N'بانک شهر', null),
(22,'ir.bank.karafarin', N'بانک کارآفرین', null),
(23,'ir.bank.gardeshgari', N'بانک گردشگری', null),
(24,'ir.bank.iranzamin', N'بانک ایران زمین', null),
(25,'ir.bank.hekmatiranian', N'بانک حکمت ایرانیان', null),
(26,'ir.bank.ansar', N'بانک انصار', null),
(27,'ir.bank.khavarmiane', N'بانک خاورمیانه', null)

SET IDENTITY_INSERT [Bank].[Bank] OFF
GO
SET IDENTITY_INSERT [Bank].[BankAccountOnlineLinkStatus] ON

INSERT INTO [Bank].[BankAccountOnlineLinkStatus]
(Id, Title, TitleEn) Values
(1, N'وصل نشده', 'Not Linked'),
(2, N'وصل شده', 'Linked'),
(3, N'قطع شده', 'Unlinked')

SET IDENTITY_INSERT [Bank].[BankAccountOnlineLinkStatus] OFF
GO
SET IDENTITY_INSERT [Bank].[BankAccountType] ON

INSERT  INTO [Bank].[BankAccountType]
        ( [Id], [Title] )
VALUES
        ( 1, N'کوتاه مدت' ),
        ( 2, N'جاری' )
GO
INSERT [System].[Configuration] ([Key], [TextValue], [Tags]) VALUES (N'System.Version', N'0.1', N'system internal')
INSERT [System].[Configuration] ([Key], [TextValue], [Tags]) VALUES (N'System.UniqueId', NEWID(), N'system internal')

GO
CREATE TRIGGER [Accounting].[Transaction_Delete] ON [Accounting].[Transaction]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounting].[Transaction]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounting].[Transaction_Update] ON [Accounting].[Transaction]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounting].[Transaction] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounting].[TransactionType_Delete] ON [Accounting].[TransactionType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounting].[TransactionType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounting].[TransactionType_Update] ON [Accounting].[TransactionType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounting].[TransactionType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[Account_Delete] ON [Accounts].[Account]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Account]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[AccountHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END
GO
CREATE TRIGGER [Accounts].[Account_Insert] ON [Accounts].[Account]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[AccountHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END
GO
CREATE TRIGGER [Accounts].[Account_Update] ON [Accounts].[Account]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Account] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
              
        INSERT INTO [Accounts].[AccountHistory]
        SELECT GETDATE(), 1, * FROM INSERTED
    END
END
GO
CREATE TRIGGER [Accounts].[AccountEntityType_Delete] ON [Accounts].[AccountEntityType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[AccountEntityType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[AccountEntityType_Update] ON [Accounts].[AccountEntityType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[AccountEntityType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[AccountState_Delete] ON [Accounts].[AccountState]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[AccountState]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[AccountState_Update] ON [Accounts].[AccountState]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[AccountState] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[Contact_Delete] ON [Accounts].[Contact]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Contact]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[ContactHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END
GO
CREATE TRIGGER [Accounts].[Contact_Insert] ON [Accounts].[Contact]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[ContactHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END
GO
CREATE TRIGGER [Accounts].[Contact_Update] ON [Accounts].[Contact]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Contact] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
              
        INSERT INTO [Accounts].[ContactHistory]
        SELECT GETDATE(), 1, * FROM INSERTED
    END
END
GO
CREATE TRIGGER [Accounts].[ContactType_Delete] ON [Accounts].[ContactType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[ContactType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[ContactType_Update] ON [Accounts].[ContactType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[ContactType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[Country_Delete] ON [Accounts].[Country]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Country]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[Country_Update] ON [Accounts].[Country]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Country] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[Document_Delete] ON [Accounts].[Document]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Document]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[Document_Update] ON [Accounts].[Document]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Document] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[DocumentType_Delete] ON [Accounts].[DocumentType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[DocumentType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[DocumentType_Update] ON [Accounts].[DocumentType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[DocumentType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[GenderType_Delete] ON [Accounts].[GenderType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[GenderType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[GenderType_Update] ON [Accounts].[GenderType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[GenderType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[LegalProfile_Delete] ON [Accounts].[LegalProfile]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[LegalProfile]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[LegalProfileHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END
GO
CREATE TRIGGER [Accounts].[LegalProfile_Insert] ON [Accounts].[LegalProfile]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[LegalProfileHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END
GO
CREATE TRIGGER [Accounts].[LegalProfile_Update] ON [Accounts].[LegalProfile]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[LegalProfile] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
              
        INSERT INTO [Accounts].[LegalProfileHistory]
        SELECT GETDATE(), 1, * FROM INSERTED
    END
END
GO
CREATE TRIGGER [Accounts].[PersonProfile_Delete] ON [Accounts].[PersonProfile]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[PersonProfile]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[PersonProfileHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END
GO
CREATE TRIGGER [Accounts].[PersonProfile_Insert] ON [Accounts].[PersonProfile]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[PersonProfileHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END
GO
CREATE TRIGGER [Accounts].[PersonProfile_Update] ON [Accounts].[PersonProfile]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[PersonProfile] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
              
        INSERT INTO [Accounts].[PersonProfileHistory]
        SELECT GETDATE(), 1, * FROM INSERTED
    END
END
GO
CREATE TRIGGER [Accounts].[Province_Delete] ON [Accounts].[Province]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Province]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[Province_Update] ON [Accounts].[Province]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Province] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[PushTarget_Delete] ON [Accounts].[PushTarget]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[PushTarget]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[PushTarget_Update] ON [Accounts].[PushTarget]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[PushTarget] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[PushTargetType_Delete] ON [Accounts].[PushTargetType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[PushTargetType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[PushTargetType_Update] ON [Accounts].[PushTargetType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[PushTargetType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[PushTargetStatus_Delete] ON [Accounts].[PushTargetStatus]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[PushTargetStatus]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[PushTargetStatus_Update] ON [Accounts].[PushTargetStatus]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[PushTargetStatus] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[Session_Delete] ON [Accounts].[Session]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[Session]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[Session_Update] ON [Accounts].[Session]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[Session] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[SessionState_Delete] ON [Accounts].[SessionState]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[SessionState]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[SessionState_Update] ON [Accounts].[SessionState]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[SessionState] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[SessionType_Delete] ON [Accounts].[SessionType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[SessionType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[SessionType_Update] ON [Accounts].[SessionType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[SessionType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Accounts].[VerificationStatus_Delete] ON [Accounts].[VerificationStatus]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Accounts].[VerificationStatus]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Accounts].[VerificationStatus_Update] ON [Accounts].[VerificationStatus]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Accounts].[VerificationStatus] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Bank].[Bank_Delete] ON [Bank].[Bank]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Bank].[Bank]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Bank].[Bank_Update] ON [Bank].[Bank]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Bank].[Bank] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Bank].[BankAccount_Delete] ON [Bank].[BankAccount]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Bank].[BankAccount]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Bank].[BankAccountHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END
GO
CREATE TRIGGER [Bank].[BankAccount_Insert] ON [Bank].[BankAccount]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Bank].[BankAccountHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END
GO
CREATE TRIGGER [Bank].[BankAccount_Update] ON [Bank].[BankAccount]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Bank].[BankAccount] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
              
        INSERT INTO [Bank].[BankAccountHistory]
        SELECT GETDATE(), 1, * FROM INSERTED
    END
END
GO
CREATE TRIGGER [Bank].[BankAccountOnlineLinkStatus_Delete] ON [Bank].[BankAccountOnlineLinkStatus]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Bank].[BankAccountOnlineLinkStatus]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Bank].[BankAccountOnlineLinkStatus_Update] ON [Bank].[BankAccountOnlineLinkStatus]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Bank].[BankAccountOnlineLinkStatus] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
CREATE TRIGGER [Bank].[BankAccountType_Delete] ON [Bank].[BankAccountType]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Bank].[BankAccountType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END
GO
CREATE TRIGGER [Bank].[BankAccountType_Update] ON [Bank].[BankAccountType]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Bank].[BankAccountType] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END
GO
