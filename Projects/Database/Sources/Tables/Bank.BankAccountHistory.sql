CREATE TABLE [Bank].[BankAccountHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [INT] NULL,
      [BankId] [INT] NULL,
      [Number] [NVARCHAR](MAX) NULL,
      [BranchName] [NVARCHAR](MAX) NULL,
      [BranchNameEn] [VARCHAR](MAX) NULL,
      [BranchCode] [NVARCHAR](MAX) NULL,
      [TypeId] [INT] NULL,
      [Iban] [NVARCHAR](MAX) NULL,
	  [OnlineLinkStatusId] [int] NULL DEFAULT 1,
      [Notes] NVARCHAR(MAX) NULL,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]