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