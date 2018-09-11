CREATE TABLE [Accounts].[Contact]
    (
      [Id] [INT] IDENTITY(1, 1) NOT NULL ,
      [AccountId] [INT] NOT NULL ,
      [Tel] [NVARCHAR](20) NOT NULL ,
      [Fax] [NVARCHAR](20) NULL ,
      [CountryId] [INT] NOT NULL ,
      [ProvinceId] [INT] NOT NULL ,
      [City] [NVARCHAR](255) NOT NULL ,
      [Address] [NVARCHAR](MAX) NOT NULL ,
      [AddressEn] [VARCHAR](MAX) NULL ,
      [PostalCode] [NVARCHAR](20) NOT NULL ,
      [TypeId] [INT] NOT NULL ,
      [RecordVersion] TIMESTAMP ,
      [RecordState] INT NOT NULL CONSTRAINT [DF_Contact_RecordState] DEFAULT 0,
      [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_Contact_RecordInsertDateTime] DEFAULT GETDATE(),
      [RecordUpdateDateTime] DATETIME NULL ,
      [RecordDeleteDateTime] DATETIME NULL ,
      CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ( [Id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]