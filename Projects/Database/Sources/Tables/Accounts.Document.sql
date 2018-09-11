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