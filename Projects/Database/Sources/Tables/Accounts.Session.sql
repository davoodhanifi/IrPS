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

