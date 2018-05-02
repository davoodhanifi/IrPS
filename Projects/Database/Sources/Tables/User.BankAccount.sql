CREATE TABLE [User].[BankAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [bigint] NOT NULL,
	[BankName] [nvarchar](128) NULL,
	[AccountOwner] [nvarchar](256) NULL,
	[ShebaCode] [nvarchar](32) NULL,
	[CardNo] [nvarchar](32) NULL,
	[CreationDateTime] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]