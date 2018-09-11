CREATE TABLE [Bank].[BankAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[BranchName] [NVARCHAR](MAX) NULL,
	[BranchNameEn] [VARCHAR](MAX) NULL,
	[BranchCode] [NVARCHAR](MAX) NULL,
	[TypeId] [INT] NULL,
	[Iban] [NVARCHAR](MAX) NULL, 
	[OnlineLinkStatusId] [int] NOT NULL,
	[Notes] NVARCHAR(MAX) NULL,
	[RecordVersion] TIMESTAMP ,
	[RecordState] INT NOT NULL CONSTRAINT [DF_BankAccount_RecordState] DEFAULT 0,
	[RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_BankAccount_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] DATETIME NULL ,
	[RecordDeleteDateTime] DATETIME NULL ,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

