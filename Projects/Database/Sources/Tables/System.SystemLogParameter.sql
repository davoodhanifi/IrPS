CREATE TABLE [System].[SystemLogParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemLogId] [int] NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[BooleanValue] [bit] NULL,
	[IntegerValue] [bigint] NULL,
	[DecimalValue] [decimal](19, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[DateTime] [datetime] NULL,
	[BinaryValue] [varbinary](max) NULL
 CONSTRAINT [PK_SystemLogParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]