CREATE TABLE [Accounts].[Document]
    (
      [Id] [INT] IDENTITY(1, 1)  NOT NULL ,
      [AccountId] [int] NOT NULL ,
      [DateTime] [datetime] NOT NULL,
      [TypeId] [int] NOT NULL ,
      [Title] [nvarchar](MAX) NULL ,
      [TitleEn] [nvarchar](MAX) NULL ,
      [MimeType] [nvarchar](MAX) NOT NULL ,
      [Data] [varbinary](MAX) NOT NULL ,
      [Note][nvarchar](MAX) NULL,
      [FileName] [nvarchar](MAX) NULL,
      [DocumentUrl] [nvarchar](MAX) NULL,
      [RecordVersion] TIMESTAMP ,
      [RecordState] [int] NOT NULL CONSTRAINT [DF_Document_RecordState] DEFAULT 0,
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