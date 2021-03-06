﻿CREATE TABLE [Accounts].[LegalProfileHistory]
    (
      [History_ID] [INT] IDENTITY(1, 1) NOT NULL ,
      [History_DateTime] [DATETIME] NOT NULL ,
      [History_Action] INT NOT NULL ,
      [Id] [INT] NULL,
      [AccountId] [INT] NULL,
      [Name] [NVARCHAR](MAX) NULL,
      [NameEn] [VARCHAR](MAX) NULL,
      [RegistrationCode] [NVARCHAR](MAX) NULL,
      [NationalCode] [NVARCHAR](MAX) NULL,
      [EconomicCode] [NVARCHAR](MAX) NULL,
      [RegistrationDate] [DATE] NULL,
	  [AttorneyId] [INT] NULL,
      [PowerOfAttorneyEndDate] [DATETIME] NULL,
      [PowerOfAttorneyNumber] [NVARCHAR](MAX) NULL,
      [RegistrationPlace] [NVARCHAR](MAX) NULL,
      [BusinessDescription] [NVARCHAR](MAX) NULL,
      [BusinessEntityTypeId] [NVARCHAR](MAX) NULL,
      [OfficialPaperNumber] [NVARCHAR](MAX) NULL,
      [OfficialPaperDate] [NVARCHAR](MAX) NULL,
      [WebSite] [NVARCHAR](MAX) NULL,
      [TradingAgent] [NVARCHAR](MAX) NULL,
      [FidaCode] [NVARCHAR](MAX) NULL,
	  [CountryId] [INT] NULL,
      [RecordVersion] [VARBINARY](8) NULL,
      [RecordState] [INT] NULL,
      [RecordInsertDateTime] [DATETIME] NULL,
      [RecordUpdateDateTime] [DATETIME] NULL,
      [RecordDeleteDateTime] [DATETIME] NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]