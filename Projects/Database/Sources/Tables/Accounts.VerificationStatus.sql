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