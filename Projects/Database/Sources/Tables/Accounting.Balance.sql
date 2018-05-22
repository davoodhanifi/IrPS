CREATE TABLE [Accounting].[Balance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](16) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Balance] [decimal](19,4) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Balance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]