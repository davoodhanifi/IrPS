CREATE TABLE [System].[Otp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](16) NOT NULL,
	[DeviceId] [nvarchar](128) NULL,
	[Password] [varchar](16) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ExpiryDate] [datetime] NOT NULL
 CONSTRAINT [PK_Otp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]