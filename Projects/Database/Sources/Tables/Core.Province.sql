CREATE TABLE [Core].[Province](
	[Id] [int] IdENTITY(1,1) NOT NULL,
	[Code] [varchar](4000) NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[NameEn] [varchar](4000) NULL,
	CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]