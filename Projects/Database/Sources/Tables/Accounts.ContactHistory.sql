CREATE TABLE [Accounts].[ContactHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL ,
      [AccountId] [INT] NULL ,
      [Tel] [NVARCHAR](20) NULL ,
      [Fax] [NVARCHAR](20) NULL ,
      [CountryId] [INT] NULL ,
      [ProvinceId] INT NULL ,
      [City] [NVARCHAR](255) NULL ,
      [Address] [NVARCHAR](MAX) NULL ,
      [AddressEn] [VARCHAR](MAX) NULL ,
      [PostalCode] [NVARCHAR](20) NULL ,
      [TypeId] [INT] NULL ,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NOT NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]