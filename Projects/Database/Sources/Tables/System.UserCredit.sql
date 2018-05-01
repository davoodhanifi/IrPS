CREATE TABLE [System].[UserCredit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](255) NOT NULL,
	[DateTime] [char](19) NOT NULL,
	[DateTimeEn] [datetime] NOT NULL,
	[Credit] [varchar](255) NOT NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserCredit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]