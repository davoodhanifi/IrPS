CREATE TABLE [Accounting].[TransactionType]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [Title] [NVARCHAR](MAX) NOT NULL ,
      [TitleEn] [VARCHAR](MAX) NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_TransactionType_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_TransactionType_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]