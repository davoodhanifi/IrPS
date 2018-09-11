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
