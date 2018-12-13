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
