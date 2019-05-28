CREATE TABLE [System].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Source] [nvarchar](max) NOT NULL,
    [LevelId] [int] NOT NULL,
	[AccountId] [int] NULL,
	[DateTime] [datetime] NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
	[Parameters] [nvarchar](max) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_Log_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Log_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Account_Log] ON [System].[Log]
(
	[AccountId] ASC,
	[RecordState] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

