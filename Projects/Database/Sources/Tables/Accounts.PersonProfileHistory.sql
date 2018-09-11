CREATE TABLE [Accounts].[PersonProfileHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [int] NULL,
      [FirstName] [nvarchar](max) NULL,
      [LastName] [nvarchar](max) NULL,
      [GenderTypeId] [int] NULL,
      [FatherName] [nvarchar](max) NULL,
      [NationalCode] [nvarchar](max) NULL,
      [IdentificationNumber] [nvarchar](max) NULL,
      [IdentificationSerial] [nvarchar](max) NULL,
      [Birthdate] [date] NULL,
      [IdentificationPlaceOfIssue] [NVARCHAR](MAX) NULL,
      [RecordVersion] VARBINARY(8) ,
      [RecordState] INT NULL,
      [RecordInsertDateTime] DATETIME NULL,
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]