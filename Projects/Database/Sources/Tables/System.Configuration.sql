CREATE TABLE [System].[Configuration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](255) NOT NULL,
	[BooleanValue] [bit] NULL,
	[IntegerValue] [bigint] NULL,
	[DecimalValue] [decimal](19, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[DateValue] [bigint] NULL,
	[BinaryValue] [varbinary](4000) NULL,
	[Tags] [nvarchar](4000) NULL,
	[Notes] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]