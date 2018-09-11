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
