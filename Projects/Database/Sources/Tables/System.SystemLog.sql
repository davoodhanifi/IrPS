CREATE TABLE [System].[SystemLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [bigint] NOT NULL,
	[Action] [nvarchar](max) NOT NULL
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]