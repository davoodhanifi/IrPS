CREATE TABLE [Accounting].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromUserCode] [nvarchar](16) NULL,
	[ToUserCode] [nvarchar](16) NOT NULL,
	[Amount] [decimal](19,4) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[TransactionType] [tinyint] NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]