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
